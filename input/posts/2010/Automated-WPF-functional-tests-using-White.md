---
title: "Automated WPF functional tests using White"
permalink: /2010/01/28/Automated-WPF-functional-tests-using-White/
date: 1/28/2010 4:50:57 AM
updated: 5/7/2010 7:53:18 AM
disqusIdentifier: 20100128045057
tags: ["WPF", "unit test", "white"]
alias:
 - /post/Automated-WPF-functional-tests-using-White.aspx/index.html
---
I’d like to introduce a tool that I have added for a month or two in my toolset. This tool is [White](http://white.codeplex.com/) from [ThoughtWorks](http://opensource.thoughtworks.com/). Here is the description of White: 

> ### [White: Automate windows applications](http://white.codeplex.com/)
<!-- more -->
> 
>  White supports all rich client applications, which are Win32, WinForm, **WPF **and SWT (java).
> It is **.NET based **and hence you wouldn't have use proprietary scripting language. You can use your favourite .NET language, IDE and tools for developing tests/automation programs.
> White provides consistent object oriented API for all kinds of applications. Also it hides all the complexity of Microsoft's UIAutomation library and windows messages (on which it is based).
> **(While WHITE is completely ready to be used, the documentation is still work under progress. Please do point out the areas which needs documentation.)**

When I found White with it’s version 0.19, I was a bit skeptical about the state of this piece of software. But it is really ready to be used.

By the way the latest [version 0.19](http://white.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=20372#ReleaseFiles) of White supports also [Silverlight](http://white.codeplex.com/wikipage?title=Silverlight&referringTitle=Home).

I had the need to automate some functional tests of a WPF application that I am working on. When such an application is growing it becomes more and more difficult to do functional tests manually, so it needs automation.

So what I was searching for was something that would let me write unit tests in C# with NUnit which automate a WPF application. It is not that I don’t want to learn another language but I needed to be efficient so I preferred to use something I knew. And what I have found is [White](http://white.codeplex.com/).

I have tested it with the [RadRibbonBar for WPF of Telerik](http://www.telerik.com/products/wpf/ribbonbar.aspx) and it works just great. btw Thanks to Telerik for the offered license, which my company [Innoveo Solutions](http://www.innoveo.com/) also ordered now.

If you are starting with automated functional testing I recommend you to read the [Functional Testing](http://white.codeplex.com/wikipage?title=Functional%20Testing). It explains step by step how to write automated functional tests with White. The basic can be also used with another tool.

It is funny to see you application running without you clicking around.

I still need to have this run by our build server, [TeamCity](http://www.jetbrains.com/teamcity/index.html). I have currently not done any investigation on that point. I plan to have those tests running at least nightly, but I have to see how the unit tests could be started on an environment with a desktop.
