---
title: 'Accessing Dapr secrets building block using Dapr .NET SDK'
permalink: /2021/04/06/accessing-dapr-secrets-building-block-using-dapr-dotnet-sdk/
date: 04/06/2021 16:49:45
disqusIdentifier: 20210406044945
coverSize: partial
tags: [.NET, Dapr]
coverCaption: 'Barcelona, Spain'
coverImage: 'https://live.staticflickr.com/7191/7038530105_abaf41225f_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/7191/7038530105_ce4260f771_q.jpg'
---
In all [previous posts](https://laurentkempe.com/tags/Dapr/), we were looking at the Dapr service invocation building block. We have seen how to expose and call HTTP and gRPC services using it. In this post, we will see how Dapr ease developers life when it comes to deal with secrets, thanks to the secrets management building block.
<!-- more -->

# Introduction

The little application used to illustrate the Dapr secrets management building block is leveraging [Twitter](https://twitter.com/laurentkempe) and [Tweetinvi](https://github.com/linvi/tweetinvi) an intuitive Twitter C# library for the REST and Stream API. To be able to run the application you will need to [create a Twitter app](https://developer.twitter.com/en/portal/apps/new). When you have created the application you will need to get the API key & secret, the Access token & secret as described on the [Tweetinvi Getting Started, Steps to create my first credentials](https://linvi.github.io/tweetinvi/dist/intro/getting-started.html). Those four pieces of information are the ones which we want to keep secret.

# Dapr secrets management building block

The secrets management building block is a building block that comes with a new capability which we didn't talk about in the previous posts, the components.

{% alert info %}
Dapr uses a modular design where functionality is delivered as a component. Each component has an interface definition. All of the components are pluggable so that you can swap out one component with the same interface for another.

A building block can use any combination of components.
{% endalert %}

This is one of the great power of Dapr. Here is the interface declaration for the secrets management building block which each component needs to implement.

{% codeblock SecretStore lang:protobuf %}
type SecretStore interface {
  // Init authenticates with the actual secret store and performs other init operation
  Init(metadata Metadata) error

  // GetSecret retrieves a secret using a key and returns a map of decrypted string/string values
  GetSecret(req GetSecretRequest) (GetSecretResponse, error)

  // BulkGetSecrets retrieves all secrets in the store and returns a map of decrypted string/string values
  BulkGetSecret(req BulkGetSecretRequest) (BulkGetSecretResponse, error)
}
{% endcodeblock %}

Currently, we can have access transparently to all of those components to handle secrets:

* [Local file storage (for development, do not use this in production)](https://docs.dapr.io/operations/components/setup-secret-store/supported-secret-stores/file-secret-store/)
* [Azure KeyVault](https://docs.dapr.io/operations/components/setup-secret-store/supported-secret-stores/azure-keyvault/)
* [AWS Secret manager](https://docs.dapr.io/operations/components/setup-secret-store/supported-secret-stores/aws-secret-manager/)
* [Kubernetes](https://docs.dapr.io/operations/components/setup-secret-store/supported-secret-stores/kubernetes-secret-store/)
* [Hashicorp Vault](https://docs.dapr.io/operations/components/setup-secret-store/supported-secret-stores/hashicorp-vault/)
* [GCP Secret Manager](https://docs.dapr.io/operations/components/setup-secret-store/supported-secret-stores/gcp-secret-manager/)

In this post, we are using local file storage to store the Twitter secrets, which should be used only for development. This could be easily replaced by one of the other components for production.

# Client

## Components folder

This is the first time we are seeing the components folder in which you can define components for your application. In today's post, we are defining a local secret store for local development and this is the only thing you would need to change when you would be ready to go into production with your application. The super nice part is that you could go to Azure, AWS, Google Cloud Platform, or any places in which secret components exist just by the change of a configuration file.

{% codeblock local-secret-store.yaml lang:yaml %}
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: local-secret-store
  namespace: default
spec:
  type: secretstores.local.file
  metadata:
  - name: secretsFile
    value: ./client/components/secrets.json
  - name: nestedSeparator
    value: ":"
{% endcodeblock %}

In this configuration we are defining the *name* of the component, **local-secret-store**  to be able to reference it from our code. Then, the type of component *secretstores.local.file* and where the secret file is stored.

And here is the local secrets file.

{% alert warning %}
You should not push this file to your code repository, as it contains your secrets!
{% endalert %}

{% codeblock secrets.json lang:json %}
{
  "twitterSecrets": {
    "consumerKey": "TOADD",
    "consumerSecret": "TOADD",
    "accessToken": "TOADD",
    "accessSecret": "TOADD"
  }
}
{% endcodeblock %}

## Starting Dapr sidecar

You can use the start.ps1 PowerShell script if you have Windows Terminal installed, and it will start the Dapr sidecar. It executes the following:

> dapr.exe run --dapr-grpc-port 50001 --components-path .\client\components\

We just define the default gRPC port which the Dapr .NET SDK will be using in our application to connect to the secrets building block and the local file storage component to get the secrets. We also need to tell Dapr in which folder our components are defined.

## Application

Our super simple application is again a [C# 9 top-level statements application](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/top-level-statements) using the [Dapr .NET SDK](https://docs.dapr.io/developing-applications/sdks/dotnet/) to access the Dapr secrets building block and the local file storage component by calling one of its API *GetBulkSecretAsync*. Then we build a *twitterClient* using Tweetinvi getting information from the authenticated user, of which you provided the secrets, to display her description.

{% codeblock Program.cs lang:csharp %}
using System;
using System.Linq;
using Dapr.Client;
using Tweetinvi;

Console.WriteLine("Hello Dapr secrets!");

var daprClient = new DaprClientBuilder().Build();

var twitterSecrets = daprClient.GetBulkSecretAsync("local-secret-store").Result;

var consumerKey = twitterSecrets["twitterSecrets:consumerKey"].Values.First();
var consumerSecret = twitterSecrets["twitterSecrets:consumerSecret"].Values.First();
var accessToken = twitterSecrets["twitterSecrets:accessToken"].Values.First();
var accessSecret = twitterSecrets["twitterSecrets:accessSecret"].Values.First();

var twitterClient =
    new TwitterClient(consumerKey, consumerSecret, accessToken, accessSecret);

var user = await twitterClient.Users.GetAuthenticatedUserAsync();
Console.WriteLine(user.Description);
{% endcodeblock %}

Here you can see the result:

> Hello Dapr secrets!
> Team Leader, Distinguished Solution Architect with a passion for shipping high quality products by empowering development team and culture

# Conclusion

We have seen that Dapr provides a very easy and nice way to retrieve secrets that will be used by your application. From a security point of view, this provides the advantage to not store your secrets in your application code or on system environment variables. It also provides an abstraction on top of a set of secrets stores which let you develop your application without being tight to concrete implementation and let you choose at the time of deployment which secrets store your want to use, without changing one line of code of your application.

You can get access to the code of this blog post on GitHub in the [Secrets folder](https://github.com/laurentkempe/daprPlayground/tree/master/Secrets).
<p></p>
{% githubCard user:laurentkempe repo:daprPlayground align:left %}
