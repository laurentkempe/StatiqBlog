---
title: 'gRPC and C# 8 Async stream cancellation'
permalink: /2019/09/25/gRPC-and-csharp-8-Async-stream-cancellation/
disqusIdentifier: 20190925220612
coverSize: partial
tags:
  - gRPC
  - C#
  - ASP.NET Core
coverCaption: 'Purcaraccia, Corse, France'
coverImage: 'https://live.staticflickr.com/65535/48795276492_5b02cadc71_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/65535/48795276492_f1c5396f9c_q.jpg'
date: 2019-09-25 22:06:12
---
In the previous post "[gRPC and C# 8 Async stream](https://laurentkempe.com/2019/09/18/gRPC-and-csharp-8-Async-stream/)", we looked at how gRPC server stream and C# 8 Async stream work great together. In this post, we are looking at the way we can, from the client, stop the server to stream results back.
<!-- more -->
# Server

I need to extend a bit the original code of the *GreeterService* to take care of the client request to stop the streaming. This is done through the usage of *ServerCallContext* which exposes a *CancellationToken*.

{% alert info %}
Note: we don't need to change anything to our greet.proto file! The cancellation capability is coming out of the box from the framework.
{% endalert %}

Then we need to check if the client requests the cancellation using *IsCancellationRequested* and in that case, stop writing back into the responseStream. Easy!

{% codeblock GreeterService.cs lang:csharp %}
public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger< GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override async Task SayHello(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
    {
        var contextCancellationToken = context.CancellationToken;

        foreach (var n in Enumerable.Range(1, 10))
        {
            if (contextCancellationToken.IsCancellationRequested) return;

            await responseStream.WriteAsync(new HelloReply
            {
                Message = $"Hello {request.Name} {n}"
            });

            await Task.Delay(200);
        }
    }
}
{% endcodeblock %}

# Client

Imagine that we want the client to stop getting results after getting 5 responses from the server. The client needs to signal to the server to stop sending responses.

As we did for the server, we need to use cancellation token for that, which we simply pass to the *ResponseStream.ReadAllAsync* method.

Then we need to surround our code with *try...catch* catching RpcException with a status code of *StatusCode.Cancelled*. Easy too!

{% codeblock Program.cs lang:csharp %}
static class Program
{
    static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("https://localhost:5001");
        var client = new Greeter.GreeterClient(channel);

        var replies = client.SayHello(new HelloRequest {Name = "Laurent"});

        var tokenSource = new CancellationTokenSource();
        var n = 0;

        try
        {
            await foreach (var reply in replies.ResponseStream.ReadAllAsync(tokenSource.Token))
            {
                Console.WriteLine(reply.Message);

                if (++n == 5)
                {
                    tokenSource.Cancel();
                }
            }
        }
        catch (RpcException e) when (e.Status.StatusCode == StatusCode.Cancelled)
        {
            Console.WriteLine("Streaming was cancelled from the client!");
        }
    }
}
{% endcodeblock %}

# Conclusion

We have seen that it is easy to cancel gRPC streaming from the client using C# 8 Async streams and cancellation token.

You can get all the code on GitHub
{% githubCard user:laurentkempe repo:grpcAsyncStreamCancellation align:left %}

Thanks, Sam Macpherson for asking the question on Twitter.

{% twitter https://twitter.com/ichpuchtli/status/1174567298226503680 %}
