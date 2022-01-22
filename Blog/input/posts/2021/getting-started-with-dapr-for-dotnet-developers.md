---
title: 'Getting started with Dapr for .NET Developers'
permalink: /2021/03/09/getting-started-with-dapr-for-dotnet-developers/
date: 03/09/2021 16:53:56
disqusIdentifier: 20210309045356
coverSize: partial
tags: [Dapr, .NET]
coverCaption: 'Au bord de l''Ill, Illzach, France'
coverImage: 'https://live.staticflickr.com/65535/49667967123_728bd9d94c_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/65535/49667967123_a6f148a0a9_q.jpg'
---
[Dapr for .NET Developers](https://docs.microsoft.com/en-us/dotnet/architecture/dapr-for-net-developers/) is a great book, read in a weekend, to start gaining an understanding of what [Dapr](https://dapr.io/) is and especially for .NET Developers. In this post, we will see how you can use .NET to create a service and run it with Dapr and what does this provides.
<!-- more -->

# Introduction

**Dapr** for *Distributed Application Runtime* is a new way to build modern distributed applications. It simplifies cloud-native application development by letting you focus on your application’s core logic and keep your code simple and portable.

Dapr helps developers build event-driven, resilient distributed applications. Whether on-premises, in the cloud, or on an edge device, Dapr helps you tackle the challenges that come with building microservices and keeps your code platform agnostic.

[Dapr v1.0 announced](https://blog.dapr.io/posts/2021/02/17/announcing-dapr-v1.0/) on February 17, 2021 is now production ready.

# Pre-requisites

To be able to follow this simple example you will have to
* [Install the Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/).
* Install [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0).

# Creating a .NET web API project

Let's create a basic web API using a .NET template

{% codeblock Creating a .NET web API lang:powershell %}
mkdir WeatherForecastService
cd WeatherForecastService
dotnet new webapi
dotnet run
{% endcodeblock %}

We now have a simple web API which we can call using any web browser using the URL https://localhost:5001/weatherforecast. Nothing too special about it at the moment.

# Running our .NET web API with Dapr

Dapr uses the [sidecar pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/sidecar), so we will run our web API and expose it through a Dapr sidecar.

{% codeblock Running our web API with Dapr lang:powershell %}
dapr run --app-id weatherforecastservice --dapr-http-port 3500 --app-port 5001 --app-ssl -- dotnet run
{% endcodeblock %}

Let's break down this command to understand it:

1. **--app-id** gives an id to our application/service, used for service discovery, in this case **weatherforecastservice**
1. **--dapr-http-port** the HTTP port for Dapr to listen on, **3500**
1. **--app-port** the port your application is listening on, **5001** is the https port of our service
1. **--app-ssl** enables https when Dapr invokes the application
1. **-- dotnet run** is the way to run our web API

# Benefits of using Dapr

Before running our web API in dapr we could access our web API using
https://localhost:5001/weatherforecast
and it is still the case, but now we can also access it using its sidecar URL
http://localhost:3500/v1.0/invoke/weatherforecastservice/method/weatherforecast

![Calling web API through dapr sidecar](https://live.staticflickr.com/65535/51008538782_f782fc5b63_z.jpg)

With this last URI, we invoke the Dapr sidecar by using the native invoke API built into Dapr. In this case, we call the API with HTTP but you can also call it with gRPC. The way to call it is standardized in the following way

{% codeblock HTTP lang:powershell %}
http://localhost:<dapr-http-port>/v1.0/invoke/<app-id>/method/<method-name>
{% endcodeblock %}

So, having such a standard way of calling services using the **[service invocation](https://docs.dapr.io/developing-applications/building-blocks/service-invocation/service-invocation-overview/)** Dapr building block gives you access to built-in

* Service discovery
* Distributed tracing
* Metrics
* Error handling
* Encryption

One other great benefit is that you don't need to care in your application code about those things as those are externalized and handled by Dapr. So, you write code like you are used to and enrich your service with Dapr.

{% alert info %}
You might have some performance concerns and you can read more about this topic on [Dapr performance considerations](https://docs.microsoft.com/en-us/dotnet/architecture/dapr-for-net-developers/dapr-at-20000-feet#dapr-performance-considerations).
{% endalert %}

# Conclusion

We have touched only the surface of what Dapr can provide, nevertheless, we have seen how easy it is to expose your web API using Dapr and the benefits it can provide, e.g. service discovery.

