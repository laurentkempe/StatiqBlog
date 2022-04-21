---
title: "Starting with Mercurial distributed source control management tool on Windows"
permalink: /2010/11/06/Starting-with-Mercurial-distributed-source-control-management-tool-on-Windows/
date: 11/6/2010 10:24:47 PM
updated: 11/6/2010 10:24:47 PM
disqusIdentifier: 20101106102447
tags: ["Mercurial"]
alias:
 - /post/Starting-with-Mercurial-distributed-source-control-management-tool-on-Windows.aspx/index.html
---
I am starting to use [Mercurial](http://mercurial.selenic.com/)for my personal projects. At the moment I use it only locally to be able to experience it but also to be able to try thing and revert easily to previous state.

To get started I downloaded
<!-- more -->

*   [Mercurial for Windows](http://mercurial.selenic.com/downloads/)
*   [TortoiseHG for Windows 64bits](http://mercurial.selenic.com/downloads/)
*   [VisualHG](http://visualhg.codeplex.com/)  

This let me work directly from a PowerShell window, Windows Explorer or Visual Studio. 

To add a project to Mercurial from the PowerShell I used the following commands:

> PS E:\Personal\Projects\_Spikes\DynApplication> **hg init**

Then I created in the same folder a file named **.hgignore,** excluding file or folder which I don’t want to version in my C# project:

> syntax: glob
> 
> *.csproj.user
> obj/
> bin/
> *.ncb
> *.suo
> _ReSharper.*
> *.ReSharper.user
> *.TeamCity.user

You are ready then to add files of your project to Mercurial with

> PS E:\Personal\Projects\_Spikes\DynApplication> **hg add**

Then you need to commit the files

> PS E:\Personal\Projects\_Spikes\DynApplication> **hg commit –m “Initital commit”**

That’s it your files are under Mercurial source control.

To get started with Mercurial I recommend you to read [Hg Init: a Mercurial tutorial](http://hginit.com/index.html)!

If you need some hosting there is [bitbucket](http://bitbucket.org/), which has been bought by [Atlassian](http://www.atlassian.com/) recently. And it is free for 5 users. For more user you will have to pay, but it is a reasonable price.      
You might also use [Codeplex](http://www.codeplex.com/) for your open source project [which supports Mercurial](http://codeplex.codeplex.com/wikipage?title=Source%20Control&referringTitle=Documentation) !
