---
title: 'Dapr binding building block by simple example'
permalink: /2021/10/19/dapr-binding-building-block-by-simple-example/
date: 10/19/2021 13:32:05
disqusIdentifier: 20211019013205
coverSize: partial
tags: [.NET, Dapr]
coverCaption: 'Lomont, Haute-Saône, Franche-Comté, France'
coverImage: 'https://live.staticflickr.com/65535/46976463834_99a78b828f_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/65535/46976463834_cd93cdcab2_q.jpg'
---
Till now, we have seen two [Dapr building blocks](https://laurentkempe.com/tags/Dapr/) which are the [service to service invocation building block](https://docs.dapr.io/developing-applications/building-blocks/service-invocation/service-invocation-overview/) and the [secrets building block](https://docs.dapr.io/developing-applications/building-blocks/secrets/secrets-overview/). The secret building block serves to protect things like a database connection string, an API key... so that they're never disclosed outside of the application. The service to service invocation building block serves to make calls between services in your distributed application easy. In this post, we will introduce a third one which is the [bindings building block](https://docs.dapr.io/developing-applications/building-blocks/bindings/bindings-overview/). The bindings building block enables your distributed application to handle external events or invoke external services.
<!-- more -->
# Introduction

Today's applications often need to be called from other external applications or call external services.

{% alert info %}
A binding provides a bi-directional connection to an external cloud/on-premise service or system. Dapr allows you to invoke the external service through the Dapr binding API, and it allows your application to be triggered by events sent by the connected service.
{% endalert %}

What is the difference between just being called or calling yourself the external service? Like for previous building blocks the benefits are mostly similar
<p></p>

* Focus on your business logic and avoid implementation details of how to interact with an external system keeping your code free from other SDKs or third parties libraries
* Being able to swap between Dapr bindings for different environments without any code change
* Having not to care about the handling of retries and failure recoveries

# Sample application

We will implement a very simple application that will poll an external [Time service](http://worldtimeapi.org/) on a configured interval to get the current time in that place. To achieve that we will use two [Dapr bindings](https://docs.dapr.io/developing-applications/building-blocks/bindings/bindings-overview/)
<p></p>

1. An input [Cron binding](https://docs.dapr.io/reference/components-reference/supported-bindings/cron/)
1. An output [HTTP binding](https://docs.dapr.io/reference/components-reference/supported-bindings/http/)

As previously those are configured in the **dapr/components** folder.

{% codeblock scheduleHttpJobCron.yaml lang:yaml %}
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: scheduleHttpJob
  namespace: default
spec:
  type: bindings.cron
  version: v1
  metadata:
    - name: schedule
      value: "@every 10s"
{% endcodeblock %}

The Dapr Cron binding named *scheduleHttpJob* will be triggering each 10 seconds.

{% codeblock httpJob.yaml lang:yaml %}
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: httpJob
  namespace: default
spec:
  type: bindings.http
  version: v1
  metadata:
    - name: url
      value: http://worldtimeapi.org/api/timezone/Europe/Paris
{% endcodeblock %}

The Dapr Http binding named *httpJob* will call the WorldTimeAPI service to get the current time in Paris.

Now that we have the Dapr configurations done, we need to have some code that will be called when the *scheduleHttpJob* is triggered.

To tie together the two Dapr bindings we create an ASP.NET controller that will be called by the *scheduleHttpJob* binding using an HTTP POST on the route *scheduleHttpJob*. The controller will use the *httpJob* binding to call the external service.

{% codeblock ScheduledHttpJobController.cs lang:csharp %}
[Route("scheduleHttpJob")]
[ApiController]
public class ScheduledHttpJobController : ControllerBase
{
    private readonly ILogger<ScheduledHttpJobController> _logger;
    private readonly DaprClient _daprClient;

    public ScheduledHttpJobController(ILogger<ScheduledHttpJobController> logger,
                                      DaprClient daprClient)
    {
        _logger = logger;
        _daprClient = daprClient;
    }

    [HttpPost]
    public async Task HttpJob()
    {
        _logger.LogInformation($"{nameof(ScheduledHttpJobController)} called 😎");

        var response =
            await _daprClient.InvokeBindingAsync(new BindingRequest("httpJob", "get"));
        var timeData = JsonSerializer.Deserialize<TimeData>(response.Data.Span);

        _logger.LogInformation($"⏰ in Paris {timeData?.utc_datetime}");
    }
}
{% endcodeblock %}

# Starting the application

A start.ps1 script is provided in the GitHub repository. It will start the ASP.NET application and the Dapr sidecar.

And here are the results displayed after starting the application and waiting ten seconds that the cron input binding triggers and call the ASP.NET controller

{% codeblock %}
== APP == info: Worker.Controllers.ScheduledHttpJobController[0]
== APP ==       ScheduledHttpJobController called 😎
== APP == info: Worker.Controllers.ScheduledHttpJobController[0]
== APP ==       ⏰ in Paris 10/19/2021 1:29:04 PM
{% endcodeblock %}

# Conclusion

We have seen that Dapr provides a great way to integrate with external systems using its binding building block. By leveraging Dapr building blocks your code becomes independent from the external systems you are integrating with keeping your code free from SDKs or third parties libraries. We will see that in a future post showing the integration of an external system using GraphQL.

You can get access to the code of this blog post on GitHub in the [CronHttpBindings folder](https://github.com/laurentkempe/daprPlayground/tree/master/CronHttpBindings).
<p></p>

{% githubCard user:laurentkempe repo:daprPlayground align:left %}
