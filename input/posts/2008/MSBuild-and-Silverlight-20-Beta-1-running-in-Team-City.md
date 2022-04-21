---
title: "MSBuild and Silverlight 2.0 Beta 1 running in Team City"
permalink: /2008/05/20/MSBuild-and-Silverlight-20-Beta-1-running-in-Team-City/
date: 5/20/2008 5:07:11 AM
updated: 5/20/2008 5:07:11 AM
disqusIdentifier: 20080520050711
tags: ["Silverlight", "continuous integration", "MSBuild"]
alias:
 - /post/MSBuild-and-Silverlight-20-Beta-1-running-in-Team-City.aspx/index.html
---
If you want to integrate [Silverlight 2.0 Beta 1](http://silverlight.net/GetStarted/) in your [Continuous Integration](http://en.wikipedia.org/wiki/Continuous_Integration) system, in my case [Team City](http://www.jetbrains.com/teamcity)), it is better to read the readme of [Silverlight 2.0 Beta 1 SDK here](http://www.microsoft.com/silverlight/resources/readme.aspx?v=2.0&sdk=true).

The important part in our case is the following one:
<!-- more -->

> **Using MSbuild to build a solution in a command line doesn’t copy the Silverlight project output to the linked web project**
> 
> We don’t have complete support for command line msbuild usage to build solutions with Silverlight projects which also includes 64 bits MSBuild support.

So basically I made a copy manually from the source of the Silverlight project to the output path of my MSBuild project:
  <div style="font-size: 10pt; background: black; color: white; font-family: consolas">   

    <!--<span style="color: green">HACK Fix the missing copy of ClientBin </span>-->

    <!--<span style="color: green">http://www.microsoft.com/silverlight/resources/readme.aspx?v=2.0&sdk=true </span>-->

    <!--<span style="color: green">Using MSbuild to build a solution in a command line doesn’t copy the Silverlight project output to the linked web project</span>

<span style="color: green">        We don’t have complete support for command line msbuild usage to build solutions with Silverlight projects which also includes 64 bits MSBuild support.</span>

<span style="color: green">    </span>-->

    <Message <span style="color: #ff8000">Condition</span>="<span style="color: lime"> '$(Configuration)|$(Platform)' == 'Staging|AnyCPU' </span>"  <span style="color: #ff8000">Text</span>="<span style="color: lime">### HACK Silverlight MSBUILD ###</span>" />

    <MakeDir <span style="color: #ff8000">Condition</span>="<span style="color: lime"> '$(Configuration)|$(Platform)' == 'Staging|AnyCPU' </span>" <span style="color: #ff8000">Directories</span>="<span style="color: lime">$(OutputPath)\ClientBin</span>" <span style="color: #ff8000">ContinueOnError</span>="<span style="color: lime">true</span>" />

    <Copy <span style="color: #ff8000">Condition</span>="<span style="color: lime"> '$(Configuration)|$(Platform)' == 'Staging|AnyCPU' </span>" <span style="color: #ff8000">SourceFiles</span>="<span style="color: lime">$(MSBuildProjectDirectory)\..\..\Sources\VideoPlayer\ClientBin\VideoPlayer.xap</span>" <span style="color: #ff8000">DestinationFiles</span>="<span style="color: lime">$(OutputPath)\ClientBin\VideoPlayer.xap</span>" />

    <!--<span style="color: green">EndHACK </span>-->
 </div>  

Now my web application is compiled, deployed and works!
