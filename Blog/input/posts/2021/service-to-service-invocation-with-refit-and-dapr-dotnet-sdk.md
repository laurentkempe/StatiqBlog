---
title: 'Service to service invocation with Refit and Dapr .NET SDK'
permalink: /2021/03/18/service-to-service-invocation-with-refit-and-dapr-dotnet-sdk/
date: 03/18/2021 16:20:00
disqusIdentifier: 20210318042000
coverSize: partial
tags: [.NET, ASP.NET Core, Dapr]
coverCaption: 'Anse Caffard, Martinique, France'
coverImage: 'https://live.staticflickr.com/2672/32885107145_a73724e5cf_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/2672/32885107145_f9923f29b0_q.jpg'
---
In the last post, we have seen how to [call a service from another service using the Dapr .NET SDK](https://laurentkempe.com/2021/03/16/service-to-service-invocation-with-dapr-dotnet-sdk/). In this one, we will have a look at a possible way to simplify the development of the client code using [Refit](https://reactiveui.github.io/refit/), the automatic type-safe REST library for .NET Core, Xamarin, and .NET.
<!-- more -->

# Introduction

Refit is a library heavily inspired by Square’s [Retrofit](http://square.github.io/retrofit) library, and it turns your REST API into a live interface! It generates for you an implementation of the interface that uses HttpClient to make its calls.
Coupled to the [Dapr .NET SDK](https://github.com/dapr/dotnet-sdk) it is a great candidate to reduce the amount of code you need to write to call your Dapr services. And we all know, less code means fewer bugs.

# Proxy service

The only difference from the previous blog post source code for the `IWeatherForecastClient` interface is that we decorate the interface with a *Get* attribute coming from Refit and specifying the method on the service which is called, here *weatherforecast*.

{% codeblock IWeatherForecastClient.cs lang:csharp %}
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace WeatherForecastProxyService
{
    public interface IWeatherForecastClient
    {
        [Get("/weatherforecast")]
        Task<IEnumerable<WeatherForecast>> GetWeatherForecast(int count);
    }
}
{% endcodeblock %}

As previously, we inject the interface in our web API controller of our first service, so that it can call the second service.

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

Finally, we need to configure the ASP.NET IOC container, so that it can inject the client based on `IWeatherForecastClient` created by Refit into the controller. Again, we leverage the Dapr .NET SDK to create the HttpClient using the Dapr appid *backend* which was used to start our backend service through Dapr. This is the way that the HttpClient knows how to call our *backend* service and decoupling it from its real address, letting Dapr handle the resolution of its location.

{% codeblock Startup.cs lang:csharp %}
    public void ConfigureServices(IServiceCollection services)
    {
        ...

        services.AddSingleton(
            _ => RestService.For<IWeatherForecastClient>(
                     DaprClient.CreateInvokeHttpClient("backend")));
    }
{% endcodeblock %}

Here is the same result

![Calling the proxy using Refit](https://live.staticflickr.com/65535/51034623513_011b0ca0c1_c.jpg)

# Conclusion

In this post, we used [Refit](https://reactiveui.github.io/refit/) to simplify the development of the client code needed to call a Dapr service.

You can get access to the code of this blog post on GitHub in the [ServiceToService Refit](https://github.com/laurentkempe/daprPlayground/tree/master/ServiceToServiceRefit) folder.
<p></p>
{% githubCard user:laurentkempe repo:daprPlayground align:left %}

