---
title: ASP.NET Core MVC app running on Raspberry Pi
permalink: /2017/04/14/ASPNET-Core-MVC-app-running-on-raspberry-pi/
date: 2017-04-14 17:26:06
tags: [".NET Core", "Raspberry Pi", "Linux"]
disqusIdentifier: 20170414172606
coverSize: partial
coverCaption: 'Promenade Grand Anse Le Diamant'
coverImage: https://c1.staticflickr.com/4/3952/33263293801_059bf0b71b_h.jpg
thumbnailImage: https://c1.staticflickr.com/4/3952/33263293801_287339c443_q.jpg
---

After running a first console app on my Raspberry Pi 3, I had to try ASP.NET Core and API. Two weeks ago when I posted [".NET Core console app running on Raspberry Pi"](http://laurentkempe.com/2017/04/03/Dotnet-Core-app-running-on-raspberry-pi/), I could make API working, but I had no chance with MVC. Today it worked!

<!-- more -->

This post will describe all steps I had to go through to have an ASP.NET Core MVC application running on my Raspberry Pi 3. I will not repeat the steps needed to install the Ubuntu MATE on the Pi, neither how to install the different tools, e.g., SSH Server, Putty, WinSCP... to have an efficient development environment, you can check the previous post for that.

Again you will need to install the .NET Core 2.0 SDK on your Windows machine. This time I used [Windows x64 2.0.0-preview1-005791](https://github.com/dotnet/cli/tree/master) which I downloaded as a zip. I unzipped it, and then I added it to the System Path.

So now when I run dotnet with the help flag, I see the following, and I am sure to run the correct version

```shell {data-file=dotnetHelp data-gist=38e29bb3942d167a252d13e56d8a45a9}
C:\Projects\pi
> dotnet -h
.NET Command Line Tools (2.0.0-preview1-005791)
Usage: dotnet [host-options] [command] [arguments] [common-options]
```

Next, we will create the [ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/) project using the following

```shell {data-file=createProject data-gist=38e29bb3942d167a252d13e56d8a45a9}
> mkdir mvc
> cd mvc
> dotnet new mvc
The template "ASP.NET Core Web App" was created successfully.
This template contains technologies from parties other than Microsoft, see https://github.com/dotnet/templating/blob/rel/vs2017/post-rtw/template_feed/THIRD-PARTY-NOTICES for details.

Processing post-creation actions...
Running 'dotnet restore' on C:\Projects\pi\mvc\mvc.csproj...
Restore succeeded.
```

Now we have to adapt the **mvc.csproj** like this

```xml {data-file=mvc.csproj data-gist=38e29bb3942d167a252d13e56d8a45a9}
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
    <RuntimeFrameworkVersion>2.0.0-preview1-001978-00</RuntimeFrameworkVersion>
    <RuntimeIdentifiers>ubuntu.16.04-arm</RuntimeIdentifiers>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore" Version="2.0.0-preview1-*" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.0.0-preview1-*" />
    <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="2.0.0-preview1-*" />
    <PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="2.0.0-preview1-*" />
    <PackageReference Include="Microsoft.VisualStudio.Web.BrowserLink" Version="2.0.0-preview1-*" />
  </ItemGroup>

  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.0-preview1-*" />
  </ItemGroup>

</Project>
```

We removed the *PackageTargetFallback* and added *RuntimeFrameworkVersion*, *RuntimeIdentifiers*.

To get access on the network to our ASP.NET Core MVC application we must first adapt the generated **Program.cs** file, to add the line **.UseUrls("http://*:8000")**

```csharp {data-file=Program.cs data-gist=38e29bb3942d167a252d13e56d8a45a9}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://*:8000")
                .UseIISIntegration()
                .ConfigureConfiguration((context, configBuilder) => {
                    configBuilder
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
                })
                .ConfigureLogging(loggerFactory => loggerFactory
                    .AddConsole()
                    .AddDebug())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
```

This code change will instruct the framework to bind to all network cards available on the PI, and thus make the web application accessible from your network.

Then we need to run the restore command

```shell {data-file=dotnetRestore data-gist=38e29bb3942d167a252d13e56d8a45a9}
> dotnet restore

  Restoring packages for C:\Projects\pi\mvc\mvc.csproj...
  Restoring packages for C:\Projects\pi\mvc\mvc.csproj...
  Restore completed in 2.01 sec for C:\Projects\pi\mvc\mvc.csproj.
  Lock file has not changed. Skipping lock file write. Path: C:\Projects\pi\mvc\obj\project.assets.json
  Restore completed in 2.82 sec for C:\Projects\pi\mvc\mvc.csproj.

  NuGet Config files used:
      C:\Projects\pi\mvc\NuGet.Config
      C:\Users\laure\AppData\Roaming\NuGet\NuGet.Config
      C:\Program Files (x86)\NuGet\Config\Microsoft.VisualStudio.Offline.config

  Feeds used:
      https://dotnet.myget.org/F/dotnet-core/api/v3/index.json
      https://dotnet.myget.org/F/aspnetcore-ci-dev/api/v3/index.json
      https://dotnet.myget.org/F/msbuild/api/v3/index.json
      https://api.nuget.org/v3/index.json
      C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\
```

Then we publish 

```shell {data-file=dotnetPublish data-gist=38e29bb3942d167a252d13e56d8a45a9}
> dotnet publish -r ubuntu.16.04-arm
Microsoft (R) Build Engine version 15.2.93.5465
Copyright (C) Microsoft Corporation. All rights reserved.

  mvc -> C:\Projects\pi\mvc\bin\Debug\netcoreapp2.0\ubuntu.16.04-arm\mvc.dll
  mvc -> C:\Projects\pi\mvc\bin\Debug\netcoreapp2.0\ubuntu.16.04-arm\publish\
```

We use WinSCP to copy all the files create in the folder C:\Projects\pi\mvc\bin\Debug\netcoreapp2.0\ubuntu.16.04-arm32\publish\ to the Raspberry Pi. Then we run the application from Putty

```shell {data-file=dotnetMVC data-gist=38e29bb3942d167a252d13e56d8a45a9}
laurent@laurent-desktop:~/core/mvc$ dotnet mvc.dll
Hosting environment: Production
Content root path: /home/laurent/core/mvc
Now listening on: http://[::]:8000
Application started. Press Ctrl+C to shut down.
```

Now we are ready to display our first web page using ASP.NET Core MVC running on the Raspberry Pi 3. The first time your browse the site, it will be slow because the Raspberry Pi needs to compile the Razor Page, but you will finally end in front of

![ASP.NET Core MVC on Raspberry PI](https://c1.staticflickr.com/3/2818/33902220761_b539cfd3fa_o.png)

Enjoy!
