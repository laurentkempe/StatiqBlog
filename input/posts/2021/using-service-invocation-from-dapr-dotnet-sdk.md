---
title: 'Using Service Invocation from Dapr .NET SDK'
permalink: /2021/03/11/using-service-invocation-from-dapr-dotnet-sdk/
date: 03/11/2021 16:51:49
disqusIdentifier: 20210311045149
coverSize: partial
tags: [.NET, Dapr]
coverCaption: 'Moorea, Polynésie française'
coverImage: 'https://live.staticflickr.com/4403/36848905226_5406f5d177_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/4403/36848905226_74956b7bea_q.jpg'
---
In the previous post "[Getting started with Dapr for .NET Developers](https://laurentkempe.com/2021/03/09/getting-started-with-dapr-for-dotnet-developers/)" we have seen how easy it was to expose a web API written in .NET and the power of exposing it through Dapr sidecar. In this post, we are looking at the different possible ways to invoke that service.
<!-- more -->

# Introduction

[Dapr](https://dapr.io/) through its [Service Invocation building block](https://docs.dapr.io/developing-applications/building-blocks/service-invocation/service-invocation-overview/) makes it easy to have your application reliably and securely communicate with other applications using the standard HTTP protocols.

It is possible to invoke service by just leveraging the HTTP protocol and a simple [.NET HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0).

# Calling Dapr sidecar with .NET HttpClient

{% codeblock Calling Dapr sidecar with .NET HttpClient lang:csharp %}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using WeatherForecastServiceGrpcClient;

var client = new HttpClient();

var weatherForecasts =
    await client.GetFromJsonAsync<List<WeatherForecast>>(
        "http://localhost:3500/v1.0/invoke/weatherforecastservice/method/weatherforecast");

foreach (var weatherForecast in weatherForecasts)
{
    Console.WriteLine(
        $"Date:{weatherForecast.Date}, TemperatureC:{weatherForecast.TemperatureC}, Summary:{weatherForecast.Summary}");
}
{% endcodeblock %}

And the results looks like this

{% codeblock Results lang:powershell %}
Date:3/10/2021 9:21:30 PM, TemperatureC:10, Summary:Sweltering
Date:3/11/2021 9:21:30 PM, TemperatureC:-18, Summary:Sweltering
Date:3/12/2021 9:21:30 PM, TemperatureC:17, Summary:Freezing
Date:3/13/2021 9:21:30 PM, TemperatureC:-17, Summary:Bracing
Date:3/14/2021 9:21:30 PM, TemperatureC:-12, Summary:Chilly
{% endcodeblock %}

This has the advantage that your code depends only on .NET, no third-party dependencies. The drawback is that you don't get any help to build the URI to call the service.

You can get some help using the [Dapr .NET SDK](https://github.com/dapr/dotnet-sdk).

# Calling Dapr sidecar with Dapr HttpClient

{% codeblock Calling Dapr sidecar with Dapr HttpClient lang:csharp %}
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using Dapr.Client;
using WeatherForecastServiceGrpcClient;

var client = DaprClient.CreateInvokeHttpClient();

var weatherForecasts =
    await client.GetFromJsonAsync<List<WeatherForecast>>(
        "http://weatherforecastservice/weatherforecast");

foreach (var weatherForecast in weatherForecasts)
{
    Console.WriteLine(
        $"Date:{weatherForecast.Date}, TemperatureC:{weatherForecast.TemperatureC}, Summary:{weatherForecast.Summary}");
}
{% endcodeblock %}

We get exactly the same output result but this time we just had to craft an easy URI *"http://weatherforecastservice/weatherforecast"* composed only of the Dapr **app-id** and the **method-name** of our service.

# Calling Dapr sidecar with DaprClient

{% codeblock Calling Dapr sidecar with Dapr .NET SDK lang:csharp %}
using System;
using System.Collections.Generic;
using System.Net.Http;
using Dapr.Client;
using WeatherForecastServiceGrpcClient;

using var client = new DaprClientBuilder().Build();

var weatherForecasts =
    await client.InvokeMethodAsync<List<WeatherForecast>>(
        HttpMethod.Get, "weatherforecastservice", "weatherforecast");
            
foreach (var weatherForecast in weatherForecasts)
{
    Console.WriteLine(
        $"Date:{weatherForecast.Date}, TemperatureC:{weatherForecast.TemperatureC}, Summary:{weatherForecast.Summary}");
}
{% endcodeblock %}

Using the DaprClient we only had to specify the **HTTP method**, the Dapr **app-id** and the **method-name** of our service.

# Conclusion

We have seen the different ways developers can use .NET with and without Dapr .NET SDK to call a service exposed through a Dapr sidecar. For some more in-depth reading about that topic, I recommend you to read ["The Dapr service invocation building block"](https://docs.microsoft.com/en-us/dotnet/architecture/dapr-for-net-developers/service-invocation) from the [Dapr for .NET Developers book](https://docs.microsoft.com/en-us/dotnet/architecture/dapr-for-net-developers/).

