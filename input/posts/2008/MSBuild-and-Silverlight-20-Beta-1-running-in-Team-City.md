---
title: "MSBuild and Silverlight 2.0 Beta 1 running in Team City"
permalink: /2008/05/20/MSBuild-and-Silverlight-20-Beta-1-running-in-Team-City/
date: 5/20/2008 5:07:11 AM
updated: 5/20/2008 5:07:11 AM
disqusIdentifier: 20080520050711
tags: ["Silverlight", "continuous integration", "MSBuild"]
---
If you want to integrate [Silverlight 2.0 Beta 1](http://silverlight.net/GetStarted/) in your [Continuous Integration](http://en.wikipedia.org/wiki/Continuous_Integration) system, in my case [Team City](http://www.jetbrains.com/teamcity)), it is better to read the readme of [Silverlight 2.0 Beta 1 SDK here](http://www.microsoft.com/silverlight/resources/readme.aspx?v=2.0&sdk=true).

The important part in our case is the following one:
<!-- more -->

> **Using MSbuild to build a solution in a command line doesn’t copy the Silverlight project output to the linked web project**
> 
> We don’t have complete support for command line msbuild usage to build solutions with Silverlight projects which also includes 64 bits MSBuild support.

So basically I made a copy manually from the source of the Silverlight project to the output path of my MSBuild project:

```xml
<!--
Using MSbuild to build a solution in a command line doesn’t copy the Silverlight project output
to the linked web project. We don’t have complete support for command line msbuild usage to
build solutions with Silverlight projects which also includes 64 bits MSBuild support.
-->

<Message Condition=" '\((Configuration)|\)(Platform)' == 'Staging|AnyCPU' "
         Text="### HACK Silverlight MSBUILD ###" />

<MakeDir Condition=" '\((Configuration)|\)(Platform)' == 'Staging|AnyCPU' "
         Directories="$(OutputPath)\ClientBin" ContinueOnError="true" />

<Copy Condition=" '\((Configuration)|\)(Platform)' == 'Staging|AnyCPU' "
      SourceFiles="\((MSBuildProjectDirectory)\..\..\Sources\VideoPlayer\ClientBin\VideoPlayer.xap"
      DestinationFiles="\)(OutputPath)\ClientBin\VideoPlayer.xap" />
```

Now my web application is compiled, deployed and works!
