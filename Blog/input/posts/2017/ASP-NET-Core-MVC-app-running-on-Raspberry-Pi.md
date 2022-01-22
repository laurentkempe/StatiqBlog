---
title: ASP.NET Core MVC app running on Raspberry Pi
permalink: /2017/04/14/ASPNET-Core-MVC-app-running-on-raspberry-pi/
date: 2017-04-14 17:26:06
tags: [".NET Core", "Raspberry Pi", "Linux"]
disqusIdentifier: 20170414172606
coverSize: partial
coverCaption: 'Promenade Grand Anse Le Diamant'
coverImage: https://c1.staticflickr.com/4/3952/33263293801_059bf0b71b_h.jpg
thumbnailImage: https://c1.staticflickr.com/4/3952/33263293801_287339c443_q.jpg
---

After running a first console app on my Raspberry Pi 3, I had to try ASP.NET Core and API. Two weeks ago when I posted [".NET Core console app running on Raspberry Pi"](http://laurentkempe.com/2017/04/03/Dotnet-Core-app-running-on-raspberry-pi/), I could make API working, but I had no chance with MVC. Today it worked!

<!-- more -->

This post will describe all steps I had to go through to have an ASP.NET Core MVC application running on my Raspberry Pi 3. I will not repeat the steps needed to install the Ubuntu MATE on the Pi, neither how to install the different tools, e.g., SSH Server, Putty, WinSCP... to have an efficient development environment, you can check the previous post for that.

Again you will need to install the .NET Core 2.0 SDK on your Windows machine. This time I used [Windows x64 2.0.0-preview1-005791](https://github.com/dotnet/cli/tree/master) which I downloaded as a zip. I unzipped it, and then I added it to the System Path.

So now when I run dotnet with the help flag, I see the following, and I am sure to run the correct version

<div style="clear:both;"></div>{% gist 38e29bb3942d167a252d13e56d8a45a9 dotnetHelp %}

Next, we will create the [ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/) project using the following

<div style="clear:both;"></div>{% gist 38e29bb3942d167a252d13e56d8a45a9 createProject %}

Now we have to adapt the **mvc.csproj** like this

<div style="clear:both;"></div>{% gist 38e29bb3942d167a252d13e56d8a45a9 mvc.csproj %}

We removed the *PackageTargetFallback* and added *RuntimeFrameworkVersion*, *RuntimeIdentifiers*.

To get access on the network to our ASP.NET Core MVC application we must first adapt the generated **Program.cs** file, to add the line **.UseUrls("http://*:8000")**

<div style="clear:both;"></div>{% gist 38e29bb3942d167a252d13e56d8a45a9 Program.cs %}

This code change will instruct the framework to bind to all network cards available on the PI, and thus make the web application accessible from your network.

Then we need to run the restore command

<div style="clear:both;"></div>{% gist 38e29bb3942d167a252d13e56d8a45a9 dotnetRestore %}

Then we publish 

<div style="clear:both;"></div>{% gist 38e29bb3942d167a252d13e56d8a45a9 dotnetPublish %}

We use WinSCP to copy all the files create in the folder *C:\@Projects\pi\mvc\bin\Debug\netcoreapp2.0\ubuntu.16.04-arm32\publish\* to the Raspberry Pi. Then we run the application from Putty

<div style="clear:both;"></div>{% gist 38e29bb3942d167a252d13e56d8a45a9 dotnetMVC %}

Now we are ready to display our first web page using ASP.NET Core MVC running on the Raspberry Pi 3. The first time your browse the site, it will be slow because the Raspberry Pi needs to compile the Razor Page, but you will finally end in front of

![ASP.NET Core MVC on Raspberry PI](https://c1.staticflickr.com/3/2818/33902220761_b539cfd3fa_o.png)

Enjoy!
