---
title: 'Service to service invocation with Dapr .NET SDK'
permalink: /2021/03/16/service-to-service-invocation-with-dapr-dotnet-sdk/
date: 03/16/2021 17:00:39
disqusIdentifier: 20210316050039
coverSize: partial
tags: [ASP.NET Core, .NET, Dapr]
coverCaption: 'Matamata, Hobbiton, New Zealand'
coverImage: 'https://live.staticflickr.com/4399/36332910612_10d4a7d2da_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/4399/36332910612_149b9735ec_q.jpg'
---
In the previous two posts, we tackled the way to start with Dapr and how to call services. In this one, we will see how we can leverage the Dapr .NET SDK to handle service to service calls.

* [Getting started with Dapr for .NET Developers](https://laurentkempe.com/2021/03/09/getting-started-with-dapr-for-dotnet-developers/)
* [Using Service Invocation from Dapr .NET SDK](https://laurentkempe.com/2021/03/11/using-service-invocation-from-dapr-dotnet-sdk/)

<!-- more -->

# Introduction

The example in this blog post is quite simple. It focuses on the main topic which is to see how you can do service to service invocation with Dapr .NET SDK. We have two services implemented in C# ASP.NET exposing REST API, one named **proxy** and the second **backend**. **proxy** is calling **backend** using Dapr .NET SDK.

Note that both services could be implemented in different languages on different platforms, nevertheless, the principles would be the same.

# Backend service

Nothing special here, it is the normal WeatherService from the default webapi .NET template, in the project WeatherForecastService. We just start it using Dapr and expose it under the name **backend**. That's one of the beauties of Dapr, you don't need to change your service to expose through a Dapr sidecar.

{% codeblock start.ps1 lang:powershell %}
dapr.exe run --app-id backend --app-port 5000 --dapr-http-port 3500 --app-ssl dotnet run -- --urls=https://localhost:5000/ -p WeatherForecastService/WeatherForecastService.csproj
{% endcodeblock %}

Again, we can access our web API using its URI, https://localhost:5000/swagger/index.html and https://localhost:5000/WeatherForecast/ but what we want is that it can be called through Dapr at http://localhost:3500/v1.0/invoke/backend/method/weatherforecast.

# Proxy service

Here is the interesting part of this post. The **backend** service call is encapsulated in a client interface in the project WeatherForecastProxyService.

{% codeblock IWeatherForecastClient.cs lang:csharp %}
    public interface IWeatherForecastClient
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherForecast(int count);
    }
{% endcodeblock %}

The implementation of the interface is straightforward and the interesting part is that it gets an *HttpClient* injected through its constructor which can call our **backend** service by using only the '\<method-name\>' of our service, in this case, *weatherforecast*.

The clear benefit is that the client only focuses on the API it is calling and not where it is located, this is delegated to *backendHttpClient*.

{% codeblock WeatherForecastClient.cs lang:csharp %}
    public class WeatherForecastClient : IWeatherForecastClient
    {
        private readonly HttpClient _backendHttpClient;

        public WeatherForecastClient(HttpClient backendHttpClient)
        {
            _backendHttpClient = backendHttpClient;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(int count)
        {
            var weatherForecasts =
                await _backendHttpClient.GetFromJsonAsync<List<WeatherForecast>>("weatherforecast");

            return weatherForecasts?.Take(count);
        }
    }
{% endcodeblock %}

To be able to inject the **backendHttpClient** into our client class we configure the ASP.NET IOC container, taking advantage of Dapr .NET SDK to create the HttpClient using the Dapr **appid** *backend* which was used to start our backend service through Dapr. This is the way that the *HttpClient* knows how to call our **backend** service and decoupling it from its real address, letting Dapr handle the resolution of its location.

{% codeblock Startup.cs lang:csharp %}
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            services.AddSingleton<IWeatherForecastClient, WeatherForecastClient>(
                _ => new WeatherForecastClient(DaprClient.CreateInvokeHttpClient("backend")));
        }
{% endcodeblock %}

Finally, we are injecting the client interface into the proxy controller so that we can use it in the controller method and call our **backend** service

{% codeblock WeatherForecastProxyController.cs lang:csharp %}
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastProxyController : ControllerBase
    {
        private readonly IWeatherForecastClient _weatherForecastClient;

        public WeatherForecastProxyController(IWeatherForecastClient weatherForecastClient)
        {
            _weatherForecastClient = weatherForecastClient;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get(int count)
        {
            return await _weatherForecastClient.GetWeatherForecast(count);
        }
    }
{% endcodeblock %}

We start it using Dapr and expose it under the name **proxy**.

{% codeblock start.ps1 lang:powershell %}
dapr.exe run --app-id proxy --app-port 5001 --dapr-http-port 3501 --app-ssl dotnet run -- --urls=https://localhost:5001/ -p WeatherForecastProxyService/WeatherForecastProxyService.csproj
{% endcodeblock %}

We can also access the proxy web API using its URI, https://localhost:5001/swagger/index.html, and https://localhost:5001/WeatherForecastProxy?count=2 but what we want is that it can be called through Dapr too at http://localhost:3501/v1.0/invoke/proxy/method/weatherforecastproxy?count=2.

# Starting proxy and backend Dapr sidecar

You can use the `start.ps1` PowerShell script if you have Windows Terminal installed, and it will display side by side both outputs in a new full-screen window. On the left is the **proxy** sidecar output and on the right the **backend**.

# Calling the proxy

As I wrote previously, you can access the **proxy** API through its Dapr sidecar at http://localhost:3501/v1.0/invoke/proxy/method/weatherforecastproxy?count=2.
It is interesting to note that it is also accessible through the **backend** sidecar at http://localhost:3500/v1.0/invoke/proxy/method/weatherforecastproxy?count=2, notice the port number difference from *3501* to *3500*.

{% alert info %}
This means that the Dapr runtime handles correctly the routing of our call to the correct service.
{% endalert %}

Here is the result

![Calling the proxy](https://live.staticflickr.com/65535/51034623513_011b0ca0c1_c.jpg)

# Conclusion

In this post, we saw again how easy it is to get the advantage of [Dapr](https://dapr.io/) and its [Dapr .NET SDK](https://github.com/dapr/dotnet-sdk) to ease our developer work and focus on the functionalities of our application and delegate the infrastructure work to Dapr, in that case, the service location again.

You can get access to the code of this blog post on GitHub in the [ServiceToService folder](https://github.com/laurentkempe/daprPlayground/tree/master/ServiceToService)
<p></p>
{% githubCard user:laurentkempe repo:daprPlayground align:left %}

