---
title: 'ASP.NET Core RC2, Docker and HipChat Connect add-on'
date: 2016-05-16 20:37:03
tags: ["ASP.NET Core RC2", "Docker", "HipChat Connect"]
permalink: /2016/05/16/ASP-NET-Core-RC2-Docker-and-HipChat-Connect-add-on/
disqusIdentifier: 20160516203703
coverSize: partial
coverCaption: 'Anse caffard, Martinique'
coverImage: 'https://farm2.staticflickr.com/1580/25532266625_c2d799e8ba_h.jpg'
thumbnailImage: 'https://farm2.staticflickr.com/1580/25532266625_a9c8d2b5b5_q.jpg'
---
This weekend ASP.NET Core RC2 was starting to show up! And it finally was [released today](https://blogs.msdn.microsoft.com/webdev/2016/05/16/announcing-asp-net-core-rc2/). Get it fresh from [here](https://www.microsoft.com/net/core). We had here a long three days weekend with quite awful gray clouds and cold weather for the season, so a perfect excuse to get started!
<!-- more -->
The first project I wanted to port to ASP.NET Core RC2 is something I began to work on some time ago when [Atlassian HipChat](https://www.hipchat.com/) announced their new [Connect](https://developer.atlassian.com/hipchat/about-hipchat-connect) framework!

I had it working with [NancyFx](https://github.com/NancyFx/Nancy); it is quite small and hacky at the moment, but at least an interesting little project to port on a weekend. The second part I wanted to have is to be able to make it run in a [Docker](https://www.docker.com/) container so that I will be able to deploy it on our Linux server at work.

So I installed the [ASP.NET Core Tooling Preview](https://blogs.msdn.microsoft.com/visualstudio/2016/05/16/announcing-updated-web-development-tools-for-asp-net-core-rc2/) for Visual Studio 2015 and created a new ASP.NET Core Web Application (.NET Core) in C#, for sure!

![New Project](https://farm8.staticflickr.com/7477/26962278712_2de5a67090_o.png)

picked up Web API

![New ASP.NET Core Web Application (.NET Core)](https://farm8.staticflickr.com/7649/26988369191_7b0369cf04_o.png)

Finally, I started the port which took me something like two to three hours!

I ended up with the following code for the *Program.cs* file. The interesting part is the **UseUrls()** which I didn't have while trying to make it run with Docker, then it wasn't bound to the right network, and the application wasn't accessible outside of the Docker container.

{% gist 38b53ab6c53b15a9630580b6115d2067 Program.cs %}

Then I had some difficulties to have [CORS](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing) working the way I wanted, but in fact, it ended up being an issue of returning JSON from my HipChat Connect GetGlance method. So it is quite easy to configure it in the *Configure()* method.

{% gist 38b53ab6c53b15a9630580b6115d2067 Startup.cs %}

Next step was to port from NancyFx module to ASP.NET Core RC2 controller, which was quite natural with the *Route*, *HttpGet*, *HttpPost*, *FromBody* and *FromQuery* attributes. The main point of interest is the **ValidateToken()** method which validates a JWT token using a **SymmetricSecurityKey**, and that wasn't straight!

{% gist 38b53ab6c53b15a9630580b6115d2067 HipChatConnectController.cs %}

To be able to test the HipChat Connect add-on, I needed to be able to expose my application from my local development machine to the internet so that I can add the add-on to one HipChat room and for that I used [ngrok](https://ngrok.com/)!

Using the same ngrok command I used for NancyFx with ASP.NET Core RC2 gave me as a result "*Http Bad Request error while calling your end point!*"

> ngrok http -bind-tls=true 8080

To be able to make it work with ASP.NET Core RC2 I had to fine tune the command so that the host header is adapted, then it worked!

> ngrok http -bind-tls=true -host-header="localhost:52060" 52060

And to finish, I wanted to have the project running in a Docker container using [Docker for Windows](http://laurentkempe.com/2016/04/30/Docker-for-Windows-Beta-review/). To achieve that goal I used the following *Dockerfile*

{% gist 38b53ab6c53b15a9630580b6115d2067 Dockerfile %}

Built the Docker image with

> docker build -t hipchatconnect .

Then started the Docker container with

> docker run -d -p 5000:5000 --name hipchatconnect hipchatconnect

Checked that I could access my first ASP.NET Core RC2 project running in Docker with the following url:

> http://docker:5000/hipchat/atlassian-connect.json

You might be also interested to read the following post ["Docker and .NET Core CLR Release Candidate 2"](https://blog.docker.com/2016/05/docker-net-core-clr-rc2/) by [Mano Marks](https://blog.docker.com/author/mano/).

To expose the container using ngrok I had to use:

> ngrok http -bind-tls=true -host-header="docker:5000" docker:5000

After adding the add-on to one of our room, the final result is a [HipChat Connect Glance](https://developer.atlassian.com/hipchat/getting-started#GettingStarted-AddstatustoHipChatroomsviaGlances) showing the number of our TeamCity builds and their states.

![HipChat Connect add-on based on ASP.NET Core RC2](https://farm8.staticflickr.com/7598/26989288911_6a7439863d_o.png)

As a conclusion, to that especially long post, I am so happy that I could finally play with the ASP.NET Core RC2 bits, run a little Web application on my Windows 10 machine but also in a Linux Docker container using Docker for Windows! I love those two technologies and see a bright future for both of them. I am also delighted that Microsoft made .NET Core an open source project.
