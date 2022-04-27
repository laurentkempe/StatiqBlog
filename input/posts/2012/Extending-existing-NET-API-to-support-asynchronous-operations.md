---
title: "Extending existing .NET API to support asynchronous operations"
permalink: /2012/01/05/Extending-existing-NET-API-to-support-asynchronous-operations/
date: 1/5/2012 7:10:53 AM
updated: 11/13/2012 11:05:11 AM
disqusIdentifier: 20120105071053
coverImage: https://farm6.staticflickr.com/5057/5517072642_bc446224c7_b.jpg
coverSize: partial
thumbnailImage: https://farm6.staticflickr.com/5057/5517072642_bc446224c7_q.jpg
coverCaption: "Palmier sur la plage de la grande anse, Le Diamant, Martinique"
---
<!-- [![Palmier sur la plage de la grande anse du diamant](http://farm6.staticflickr.com/5057/5517072642_bc446224c7_m.jpg)](http://www.flickr.com/photos/laurentkempe/5517072642/ "Palmier sur la plage de la grande anse du diamant by Laurent Kempé, on Flickr") -->

The other day I needed a way in a project I am working on to turn a .NET API, [RestSharp](http://restsharp.org/) to name it, so that I could use it in an asynchronous way.
<!-- more -->

The goal was to have the API returning a **Task<TResult>** so that I could use it if I want with the [Async CTP](http://msdn.microsoft.com/en-us/vstudio/gg316360). I also wanted to have something running both on .NET 4.0 and on Windows Phone 7.5.

To achieve this goal I used 

> ### [TaskCompletionSource<TResult> Class](http://msdn.microsoft.com/en-us/library/dd449174.aspx)
> 
> **.NET Framework 4**

and defined two helper extension methods on the RestClient class of RestSharp as this:

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

public static class RestClientExtensions
{
    public static Task<TResult> ExecuteTask<TResult>(this RestClient client,
                                                     RestRequest request) where TResult : new()
    {
        var tcs = new TaskCompletionSource<TResult>();

        WaitCallback
            asyncWork = _ =>
                            {
                                try
                                {
                                    client.ExecuteAsync<TResult>(request,
                                                                 response => tcs.SetResult(response.Data));
                                }
                                catch (Exception exc)
                                {
                                    tcs.SetException(exc);
                                }
                            };

        return ExecuteTask(asyncWork, tcs);
    }

    public static Task<TResult> ExecuteTask<T, TResult>(this RestClient client,
                                                        RestRequest request,
                                                        Func<T, TResult> adapter) where T : new()
    {
        var tcs = new TaskCompletionSource<TResult>();

        WaitCallback
            asyncWork = _ =>
                            {
                                try
                                {
                                    client.ExecuteAsync<T>(request,
                                                           response =>
                                                           tcs.SetResult(adapter.Invoke(response.Data)));
                                }
                                catch (Exception exc)
                                {
                                    tcs.SetException(exc);
                                }
                            };

        return ExecuteTask(asyncWork, tcs);
    }

    private static Task<TResult> ExecuteTask<TResult>(WaitCallback asyncWork, TaskCompletionSource<TResult> tcs)
    {
        ThreadPool.QueueUserWorkItem(asyncWork);

        return tcs.Task;
    }
}
```

Now I can write asynchronous code like this:

```csharp
public Task<List<Project>> GetAllProjects()
{
    var request = new RestRequest { Resource = "/httpAuth/app/rest/projects", RootElement = "project" };
    request.AddHeader("Accept", "application/json");

    return _restClient.ExecuteTask<List<Project>>(request);
}
```

So TaskCompletionSource<T> is a nice class to know about!

~~You might also watch this short video by [Phil Pennington](http://channel9.msdn.com/Niners/philpenn) to get a better idea about it:~~

<iframe style="width: 512px; height: 288px" src="http://channel9.msdn.com/Blogs/philpenn/TaskCompletionSourceTResult/player?w=512&h=288" frameborder="0" scrolling="no"></iframe>
