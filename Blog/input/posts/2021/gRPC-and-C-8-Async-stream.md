---
title: 'gRPC and C# 8 Async stream'
permalink: /2019/09/18/gRPC-and-csharp-8-Async-stream/
disqusIdentifier: 20190918192300
coverSize: partial
tags:
  - gRPC
  - C#
  - ASP.NET Core
coverCaption: 'Purcaraccia, Corse, France'
coverImage: 'https://live.staticflickr.com/65535/48736714892_a7c405d990_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/65535/48736714892_5d8efce2bd_q.jpg'
date: 2019-09-18 19:23:00
---
[gRPC](https://grpc.io/) and its idea to describe an API in a standardized file, which can generate both client and server code to interact in different languages is a compelling idea.
In this post, I would like to have a quick look at the experience you would have with gRPC streaming capability and the new [C# 8 async streams](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/async-streams), which sounds like a perfect match.
<!-- more -->
I am using the [.NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x) to create the solution with two projects, one for the server called grpcAsyncStreamServer and one for the client grpcAsyncStreamClient. By the way, do you know you can get [tab completion of .NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/enable-tab-autocomplete) commands? Very handy.

# Server

 I am using the default gRPC .NET Core template to generate the server part. The template creates a greet.proto file which I will duplicate between the server and the client project. As I want to experience with the [streaming capability of gRPC](https://www.grpc.io/docs/guides/concepts/), I am modifying the greet.proto file adding the keyword stream in front of the SayHello HelloReply response.

{% alert info %}
Server streaming RPC
A server-streaming RPC is similar to our simple example, except **the server sends back a stream of responses after getting the client’s request message**. After sending back all its responses, the server’s status details (status code and optional status message) and optional trailing metadata are sent back to complete on the server side. **The client completes once it has all the server’s responses**.
{% endalert %}

 Here is how the greet.proto looks like.

{% codeblock greet.proto %}
syntax = "proto3";

option csharp_namespace = "grpcAsyncStreamServer";

package Greet;

// The greeting service definition.
service Greeter {
  // Sends a greeting
  rpc SayHello (HelloRequest) returns (stream HelloReply);
}

// The request message containing the user's name.
message HelloRequest {
  string name = 1;
}

// The response message containing the greetings.
message HelloReply {
  string message = 1;
}
{% endcodeblock %}

I modify the code of the GreeterService class coming from the template to stream 10 HelloReply back to the client, with a small delay of 200ms in between each string returned to simulate some work.

{% codeblock GreeterService.cs lang:csharp %}
public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override async Task SayHello(HelloRequest request, IServerStreamWriter<HelloReply> responseStream,                                            ServerCallContext context)
    {
        foreach (var x in Enumerable.Range(1, 10))
        {
            await responseStream.WriteAsync(new HelloReply
            {
                Message = $"Hello {request.Name} {x}"
            });

            await Task.Delay(200);
        }
    }
}
{% endcodeblock %}

# Client

For the client, I am creating a new Console application and copying the greet.proto into a new folder which I name Protos. Then I need to add the dependencies that you can see on the following csproj, so that at compilation time, the proto file is projected to C# code which I can use in my code. That's nice because it means that you also get after a first compilation IntelliSense auto-completion.

{% codeblock grpcAsyncStreamClient.csproj lang:xml %}
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp3.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Google.Protobuf" Version="3.9.1" />
    <PackageReference Include="Grpc.Core" Version="2.23.0" />
    <PackageReference Include="Grpc.Net.Client" Version="0.2.23" />
    <PackageReference Include="Grpc.Tools" Version="2.23.0" />
  </ItemGroup>

  <ItemGroup>
    <Protobuf Include="Protos\greet.proto">
      <GrpcServices>Client</GrpcServices>
      <Access>Public</Access>
      <ProtoCompile>True</ProtoCompile>
      <CompileOutputs>True</CompileOutputs>
      <OutputDir>obj\Debug\netcoreapp3.0\</OutputDir>
      <Generator>MSBuild:Compile</Generator>
    </Protobuf>
  </ItemGroup>
  
</Project>
{% endcodeblock %}

We can now use the new C# 8 Async stream to iterate on the server answers asynchronously.

{% alert info %}
C# has support for iterator methods and async methods, but no support for a method that is both an iterator and an async method. We should rectify this by allowing for await to be used in a new form of async iterator, one that returns an IAsyncEnumerable<T> or IAsyncEnumerator<T> rather than an IEnumerable<T> or IEnumerator<T>, with IAsyncEnumerable<T> consumable in a new await foreach. An IAsyncDisposable interface is also used to enable asynchronous cleanup.
{% endalert %}

The code is greatly readable with this.

{% codeblock Program.cs lang:csharp %}
static class Program
{
    static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("https://localhost:5001");
        var client = new Greeter.GreeterClient(channel);

        var replies = client.SayHello(new HelloRequest { Name = "Laurent" });

        await foreach (var reply in replies.ResponseStream.ReadAllAsync())
        {
            Console.WriteLine(reply.Message);
        }
    }
}
{% endcodeblock %}

# Conclusion

We have seen that C# 8 Async streams are nicely integrating with gRPC streaming capabilities. And it makes code more readable!

You can get all the code on GitHub
{% githubCard user:laurentkempe repo:grpcAsyncStream align:left %}
