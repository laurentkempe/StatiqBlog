---
title: "Setting correctly your configuration for Web Deployment Projects VS08"
permalink: /2007/12/02/Setting-correctly-your-configuration-for-Web-Deployment-Projects-VS08/
date: 12/2/2007 5:50:03 PM
updated: 12/2/2007 5:50:03 PM
disqusIdentifier: 20071202055003
tags: ["ASP.NET 2.0", "Visual Studio", "ASP.NET"]
alias:
 - /post/Setting-correctly-your-configuration-for-Web-Deployment-Projects-VS08.aspx/index.html
---
I read on [MSDN Forums](http://forums.asp.net/t/1151526.aspx) that it is not possible to use other configuration than Release and Debug. This is wrong.

I am using it for Tech Head Brothers Portal, what you have to take care of is how you set your configuration. On the following picture you see how I defined a new Staging configuration and set the Web Deployment Project *WebApplication.csproj_deploy *to build using this configuration.
<!-- more -->

![](http://farm3.static.flickr.com/2203/2079573615_8fa38ad83d_o.jpg) 

Then you are able to use it this way in your MSBuild:

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">PropertyGroup</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">Condition</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)"> '$(Configuration)|$(Platform)' == 'Staging|AnyCPU' </span>"<span style="color: rgb(0,0,255)">>
    <</span><span style="color: rgb(163,21,21)">DebugSymbols</span><span style="color: rgb(0,0,255)">></span>true<span style="color: rgb(0,0,255)"></</span><span style="color: rgb(163,21,21)">DebugSymbols</span><span style="color: rgb(0,0,255)">>
    <</span><span style="color: rgb(163,21,21)">OutputPath</span><span style="color: rgb(0,0,255)">></span>.\Staging<span style="color: rgb(0,0,255)"></</span><span style="color: rgb(163,21,21)">OutputPath</span><span style="color: rgb(0,0,255)">>
    <</span><span style="color: rgb(163,21,21)">EnableUpdateable</span><span style="color: rgb(0,0,255)">></span>true<span style="color: rgb(0,0,255)"></</span><span style="color: rgb(163,21,21)">EnableUpdateable</span><span style="color: rgb(0,0,255)">>
    <</span><span style="color: rgb(163,21,21)">UseMerge</span><span style="color: rgb(0,0,255)">></span>true<span style="color: rgb(0,0,255)"></</span><span style="color: rgb(163,21,21)">UseMerge</span><span style="color: rgb(0,0,255)">>
    <</span><span style="color: rgb(163,21,21)">SingleAssemblyName</span><span style="color: rgb(0,0,255)">></span>THB.Portal<span style="color: rgb(0,0,255)"></</span><span style="color: rgb(163,21,21)">SingleAssemblyName</span><span style="color: rgb(0,0,255)">>
</</span><span style="color: rgb(163,21,21)">PropertyGroup</span><span style="color: rgb(0,0,255)">></span>
[](http://11011.net/software/vspaste)
