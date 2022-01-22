---
title: 'Calling Dapr service with gRPC'
permalink: /2021/03/25/calling-dapr-service-with-grpc/
date: 03/25/2021 18:07:50
disqusIdentifier: 20210325060750
coverSize: partial
tags: [Dapr, gRPC, .NET]
coverCaption: 'Illzach, France'
coverImage: 'https://live.staticflickr.com/65535/51063269481_b31f5a9d09_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/65535/51063269481_4d617ff8ac_q.jpg'
---
In previous posts, we focused on Dapr service invocation using the HTTP protocol. Dapr, through its service invocation, can also reliably and securely communicate with other applications using gRPC. We will have a look at this other capability in this post.
<!-- more -->
# Introduction

We want to show how you can avoid having a dependency on another third party other than the one dealing with the transport protocol. That's one of the advantages of Dapr, you can integrate with it just by relying on HTTP and/or gRPC. In the previous cases, with HTTP, we could do it without any dependencies. In the case of gRPC, we need to bring at least this dependency so that we can call Dapr services.

# Creating the gRPC client

We are creating a standard console application in which we want to leverage the proto file delivered with the Dapr project to generate the C# Darp client.

We can get the [Dapr proto files](https://github.com/dapr/dapr/tree/master/dapr/proto) from the Dapr project on Github. Those files are included in the project in a Dapr folder at the root of the project. We need to include some nuget packages to be able to generate C# code from the proto files as we can see on the **GrpcClient.csproj** file.

{% codeblock GrpcClient.csproj lang:xml %}
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Google.Protobuf" Version="3.15.6" />
    <PackageReference Include="Google.Api.CommonProtos" Version="2.2.0" />
    <PackageReference Include="Grpc" Version="2.36.1" />
    <PackageReference Include="Grpc.Core" Version="2.36.1" />
    <PackageReference Include="Grpc.Net.Client" Version="2.36.1" />
    <PackageReference Include="Grpc.Tools" Version="2.36.1">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <Protobuf Include=".\dapr\proto\common\v1\common.proto"
              ProtoRoot=".\"
              GrpcServices="Client" />
    <Protobuf Include=".\dapr\proto\runtime\v1\dapr.proto"
              ProtoRoot=".\"
              GrpcServices="Client"
              Access="Public"
              ProtoCompile="True"
              CompileOutputs="True"
              Generator="MSBuild:Compile"
              OutputDir="obj\Debug\net5.0\">
    </Protobuf>
  </ItemGroup>

</Project>
{% endcodeblock %}

# Using the generated gRPC client

Looking at the **dapr.proto** we see in which C# namespace the client code will be generated.

{% codeblock dapr.proto lang:proto %}
option csharp_namespace = "Dapr.Client.Autogen.Grpc.v1";
{% endcodeblock %}

We can now use the generated client and specify which service we want to call using the `InvokeServiceRequest` specifying the `Id` which is the *app-id* of the Dapr sidecar so in our case *proxy*. Then, we specify that we want to call the method *weatherforecastproxy*, with an HTTP Get and pass the `QueryString`.

Then we call `daprClient.InvokeService` to invoke the Dapr service through the sidecars, get the response as JSON which we deserialize, and display the results.

{% codeblock Program.cs lang:csharp %}
using System;
using System.Collections.Generic;
using System.Text.Json;
using Dapr.Client.Autogen.Grpc.v1;
using Grpc.Core;

var channel = new Channel("127.0.0.1:3500", ChannelCredentials.Insecure);
var daprClient = new Dapr.Client.Autogen.Grpc.v1.Dapr.DaprClient(channel);
var request = new InvokeServiceRequest
{
    Id = "proxy",
    Message = new InvokeRequest
    {
        Method = "weatherforecastproxy",
        HttpExtension = new HTTPExtension
        {
            Verb = HTTPExtension.Types.Verb.Get,
            Querystring = "count=2"
        }
    }
};

var invokeResponse = daprClient.InvokeService(request);
var json = invokeResponse.Data.Value.ToStringUtf8();
Console.WriteLine(json);

var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
var weatherForecasts =
    JsonSerializer.Deserialize<List<WeatherForecast>>(json, jsonSerializerOptions);
foreach (var weatherForecast in weatherForecasts)
{
    Console.WriteLine($"Date: {weatherForecast.Date}, {weatherForecast.Summary}, {weatherForecast.TemperatureC}°");
}
{% endcodeblock %}

Notice that we connect to the *app-id* **proxy** on the URI **127.0.0.1:3500** which is the gRPC port of our proxy service. We are invoking the **weatherforecastproxy** method on the service. The proxy service is an HTTP service that we are calling with gRPC thanks to Dapr sidecar. That proxy service is then calling the **backend** service also exposed through HTTP but accessible through gRPC again thanks to the Dapr sidecar.

{% alert info %}
[All calls between Dapr sidecars go over gRPC](https://docs.dapr.io/developing-applications/building-blocks/service-invocation/service-invocation-overview/#service-invocation) for performance. Only calls between services and Dapr sidecars can be either HTTP or gRPC
{% endalert %}

# Starting proxy and backend Dapr sidecar

You can use the start.ps1 PowerShell script if you have Windows Terminal installed, and it will display side by side both outputs in a new full-screen window. On the left is the proxy sidecar output and on the right the backend. There are some slight differences from the previous start.ps1 because we want to specify the gRPC port of the first sidecar, which is set to 3500.

Here is the output of our client.

{% codeblock %}
[{"date":"2021-03-26T17:52:47.5849398+01:00","temperatureC":4,"temperatureF":39,"summary":"Scorching"},{"date":"2021-03-27T17:52:47.5850893+01:00","temperatureC":22,"temperatureF":71,"summary":"Bracing"}]
Date: 3/26/2021 5:52:47 PM, Scorching, 4°
Date: 3/27/2021 5:52:47 PM, Bracing, 22°
{% endcodeblock %}

# Conclusion
We have seen that Dapr provides a way to communicate with other applications using gRPC and without taking any dependencies to any third-party framework.

You can get access to the code of this blog post on GitHub in the [GrpcServiceToService folder](https://github.com/laurentkempe/daprPlayground/tree/master/GrpcServiceToService).
<p></p>
{% githubCard user:laurentkempe repo:daprPlayground align:left %}

