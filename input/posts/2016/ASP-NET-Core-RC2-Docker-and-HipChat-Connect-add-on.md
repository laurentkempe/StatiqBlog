---
title: 'ASP.NET Core RC2, Docker and HipChat Connect add-on'
date: 2016-05-16 20:37:03
tags: ["ASP.NET Core RC2", "Docker", "HipChat Connect"]
permalink: /2016/05/16/ASP-NET-Core-RC2-Docker-and-HipChat-Connect-add-on/
disqusIdentifier: 20160516203703
coverSize: partial
coverCaption: 'Anse caffard, Martinique'
coverImage: 'https://farm2.staticflickr.com/1580/25532266625_c2d799e8ba_h.jpg'
thumbnailImage: 'https://farm2.staticflickr.com/1580/25532266625_a9c8d2b5b5_q.jpg'
---
This weekend ASP.NET Core RC2 was starting to show up! And it finally was [released today](https://blogs.msdn.microsoft.com/webdev/2016/05/16/announcing-asp-net-core-rc2/). Get it fresh from [here](https://www.microsoft.com/net/core). We had here a long three days weekend with quite awful gray clouds and cold weather for the season, so a perfect excuse to get started!
<!-- more -->
The first project I wanted to port to ASP.NET Core RC2 is something I began to work on some time ago when [Atlassian HipChat](https://www.hipchat.com/) announced their new [Connect](https://developer.atlassian.com/hipchat/about-hipchat-connect) framework!

I had it working with [NancyFx](https://github.com/NancyFx/Nancy); it is quite small and hacky at the moment, but at least an interesting little project to port on a weekend. The second part I wanted to have is to be able to make it run in a [Docker](https://www.docker.com/) container so that I will be able to deploy it on our Linux server at work.

So I installed the [ASP.NET Core Tooling Preview](https://blogs.msdn.microsoft.com/visualstudio/2016/05/16/announcing-updated-web-development-tools-for-asp-net-core-rc2/) for Visual Studio 2015 and created a new ASP.NET Core Web Application (.NET Core) in C#, for sure!

![New Project](https://farm8.staticflickr.com/7477/26962278712_2de5a67090_o.png)

picked up Web API

![New ASP.NET Core Web Application (.NET Core)](https://farm8.staticflickr.com/7649/26988369191_7b0369cf04_o.png)

Finally, I started the port which took me something like two to three hours!

I ended up with the following code for the *Program.cs* file. The interesting part is the **UseUrls()** which I didn't have while trying to make it run with Docker, then it wasn't bound to the right network, and the application wasn't accessible outside of the Docker container.

```csharp {data-file=Program.cs  docker_build.log data-gist=38b53ab6c53b15a9630580b6115d2067}
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace HipChatConnect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Get environment variables
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables("")
                .Build();
            var url = config["ASPNETCORE_URLS"] ?? "http://*:5000";
            var env = config["ASPNETCORE_ENVIRONMENT"] ?? "Development";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(url)
                .UseEnvironment(env)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
```

Then I had some difficulties to have [CORS](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing) working the way I wanted, but in fact, it ended up being an issue of returning JSON from my HipChat Connect GetGlance method. So it is quite easy to configure it in the *Configure()* method.

```csharp {data-file=Startup.cs  docker_build.log data-gist=38b53ab6c53b15a9630580b6115d2067}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HipChatConnect
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(settings => settings.NGrokUrl = Configuration["NGROK_URL"]);

            // Add framework services.
            services.AddCors();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();
            app.UseCors(builder =>
            {
                builder.WithOrigins("*")
                    .WithMethods("GET")
                    .WithHeaders("accept", "content-type", "origin");
            });
            app.UseMvc();
        }
    }
}
```

Next step was to port from NancyFx module to ASP.NET Core RC2 controller, which was quite natural with the *Route*, *HttpGet*, *HttpPost*, *FromBody* and *FromQuery* attributes. The main point of interest is the **ValidateToken()** method which validates a JWT token using a **SymmetricSecurityKey**, and that wasn't straight!

```csharp {data-file=HipChatConnectController.cs  docker_build.log data-gist=38b53ab6c53b15a9630580b6115d2067}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nubot.Plugins.Samples.HipChatConnect.Models;

namespace HipChatConnect.Controllers
{
    [Route("/hipchat")]
    public class HipChatConnectController : Controller
    {
        private readonly IOptions<AppSettings> _settings;
        private static readonly ConcurrentDictionary<string, object> Cache = new ConcurrentDictionary<string, object>();

        public HipChatConnectController(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

        [HttpGet("atlassian-connect.json")]
        public async Task<string> Get()
        {
            var baseUri = _settings.Value?.NGrokUrl ?? "http://localhost:52060/";

            return await Task.FromResult(GetCapabilitiesDescriptor(baseUri));
        }

        [HttpPost("installable")]
        public async Task<HttpStatusCode> Installable([FromBody]InstallationData installationData)
        {
            Cache.TryAdd(installationData.oauthId, installationData);

            var capabilitiesRoot = await GetCapabilitiesRoot(installationData);

            var accessToken = await GetAccessToken(installationData, capabilitiesRoot);

            return Cache.TryAdd("accessToken", accessToken) ? HttpStatusCode.OK : HttpStatusCode.NotFound;
        }

        [HttpGet("uninstalled")]
        public async Task<IActionResult> UnInstalled([FromQuery(Name = "redirect_url")]string redirectUrl,
                                                     [FromQuery(Name = "installable_url")]string installableUrl)
        {
            var client = new HttpClient();

            var httpResponse = await client.GetAsync(installableUrl);
            httpResponse.EnsureSuccessStatusCode();

            var jObject = await httpResponse.Content.ReadAsAsync<JObject>();

            object anobject ;
            Cache.TryRemove((string)jObject["oauthId"], out anobject);

            return await Task.FromResult(Redirect(redirectUrl));
        }

        [HttpGet("glance")]
        public string GetGlance([FromQuery(Name = "signed_request")]string signedRequest)
        {
            if (ValidateToken(signedRequest))
            {
                return BuildInitialGlance();
            }

            return "";
        }

        private static string GetCapabilitiesDescriptor(string baseUri)
        {
            var capabilitiesDescriptor = new
            {
                name = "Nubot",
                description = "An add-on to talk to Nubot.",
                key = "nubot-addon",
                links = new
                {
                    self = $"{baseUri}/hipchat/atlassian-connect.json",
                    homepage = $"{baseUri}/hipchat/atlassian-connect.json"
                },
                vendor = new
                {
                    name = "Laurent Kempe",
                    url = "http://laurentkempe.com"
                },
                capabilities = new
                {
                    hipchatApiConsumer = new
                    {
                        scopes = new[]
                        {
                            "send_notification",
                            "view_room"
                        }
                    },
                    installable = new
                    {
                        callbackUrl = $"{baseUri}/hipchat/installable",
                        uninstalledUrl = $"{baseUri}/hipchat/uninstalled"
                    },
                    glance = new[]
                    {
                        new
                        {
                            name = new
                            {
                                value = "Hello TC"
                            },
                            queryUrl = $"{baseUri}/hipchat/glance",
                            key = "nubot.glance",
                            target = "nubot.sidebar",
                            icon = new Icon
                            {
                                url = $"{baseUri}/nubot/TC.png",
                                url2 = $"{baseUri}/nubot/TC2.png"
                            }
                        }
                    }
                }
            };

            return JsonConvert.SerializeObject(capabilitiesDescriptor);
        }

        private async Task<CapabilitiesRoot> GetCapabilitiesRoot(InstallationData installationData)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(installationData.capabilitiesUrl));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<CapabilitiesRoot>();
        }

        private async Task<AccessToken> GetAccessToken(InstallationData installationData, CapabilitiesRoot capabilitiesRoot)
        {
            var client = new HttpClient();

            var dataContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("scope", "send_notification")
            });

            var credentials = Encoding.ASCII.GetBytes($"{installationData.oauthId}:{installationData.oauthSecret}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(credentials));

            var tokenResponse =
                await client.PostAsync(new Uri(capabilitiesRoot.capabilities.oauth2Provider.tokenUrl), dataContent);
            return await tokenResponse.Content.ReadAsAsync<AccessToken>();
        }

        private bool ValidateToken(string jwt)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var readToken = jwtSecurityTokenHandler.ReadToken(jwt);

            object installationDataObject;
            if (!Cache.TryGetValue(readToken.Issuer, out installationDataObject))
            {
                return false;
            }

            var installationData = (InstallationData)installationDataObject;

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = installationData.oauthId,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(installationData.oauthSecret)),
                ValidateAudience = false,
                ValidateLifetime = true
            };

            try
            {
                SecurityToken token;
                var validatedToken = jwtSecurityTokenHandler.ValidateToken(jwt, validationParameters, out token);
                return validatedToken != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string BuildInitialGlance()
        {
            var response = new
            {
                label = new
                {
                    type = "html",
                    value = "<b>4</b> Builds"
                },
                status = new
                {
                    type = "lozenge",
                    value = new
                    {
                        label = "GOOD",
                        type = "success"
                    }
                },
                metadata = new
                {
                    customData = new { customAttr = "customValue" }
                }
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}
```

To be able to test the HipChat Connect add-on, I needed to be able to expose my application from my local development machine to the internet so that I can add the add-on to one HipChat room and for that I used [ngrok](https://ngrok.com/)!

Using the same ngrok command I used for NancyFx with ASP.NET Core RC2 gave me as a result "*Http Bad Request error while calling your end point!*"

> ngrok http -bind-tls=true 8080

To be able to make it work with ASP.NET Core RC2 I had to fine tune the command so that the host header is adapted, then it worked!

> ngrok http -bind-tls=true -host-header="localhost:52060" 52060

And to finish, I wanted to have the project running in a Docker container using [Docker for Windows](http://laurentkempe.com/2016/04/30/Docker-for-Windows-Beta-review/). To achieve that goal I used the following *Dockerfile*

```yaml {data-file=Dockerfile  docker_build.log data-gist=38b53ab6c53b15a9630580b6115d2067}
FROM microsoft/dotnet

# Set environment variables
ENV ASPNETCORE_URLS="http://*:5000"
ENV ASPNETCORE_ENVIRONMENT="Development"

# Copy files to app directory
COPY . /app

# Set working directory
WORKDIR /app

# Restore NuGet packages
RUN ["dotnet", "restore"]

# Open up port
EXPOSE 5000

# Run the app
ENTRYPOINT ["dotnet", "run"]
```

Built the Docker image with

> docker build -t hipchatconnect .

Then started the Docker container with

> docker run -d -p 5000:5000 --name hipchatconnect hipchatconnect

Checked that I could access my first ASP.NET Core RC2 project running in Docker with the following url:

> http://docker:5000/hipchat/atlassian-connect.json

You might be also interested to read the following post ["Docker and .NET Core CLR Release Candidate 2"](https://blog.docker.com/2016/05/docker-net-core-clr-rc2/) by [Mano Marks](https://blog.docker.com/author/mano/).

To expose the container using ngrok I had to use:

> ngrok http -bind-tls=true -host-header="docker:5000" docker:5000

After adding the add-on to one of our room, the final result is a [HipChat Connect Glance](https://developer.atlassian.com/hipchat/getting-started#GettingStarted-AddstatustoHipChatroomsviaGlances) showing the number of our TeamCity builds and their states.

![HipChat Connect add-on based on ASP.NET Core RC2](https://farm8.staticflickr.com/7598/26989288911_6a7439863d_o.png)

As a conclusion, to that especially long post, I am so happy that I could finally play with the ASP.NET Core RC2 bits, run a little Web application on my Windows 10 machine but also in a Linux Docker container using Docker for Windows! I love those two technologies and see a bright future for both of them. I am also delighted that Microsoft made .NET Core an open source project.
