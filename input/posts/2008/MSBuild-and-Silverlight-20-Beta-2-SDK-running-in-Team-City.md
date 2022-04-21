---
title: "MSBuild and Silverlight 2.0 Beta 2 SDK running in Team City"
permalink: /2008/06/07/MSBuild-and-Silverlight-20-Beta-2-SDK-running-in-Team-City/
date: 6/7/2008 10:57:52 PM
updated: 6/7/2008 10:57:52 PM
disqusIdentifier: 20080607105752
tags: ["Tech Head Brothers", "Silverlight", "continuous integration", "Team City", "MSBuild"]
alias:
 - /post/MSBuild-and-Silverlight-20-Beta-2-SDK-running-in-Team-City.aspx/index.html
---
First you need to un-install the Silverlight 2.0 Beta 1 SDK from the build server! Then you can download the [Microsoft Silverlight Tools Beta 2 for Visual Studio 2008](http://www.microsoft.com/downloads/details.aspx?FamilyId=50A9EC01-267B-4521-B7D7-C0DBA8866434&displaylang=en) and extract it on the server, there you will find the file **silverlight_sdk.msi**, that will allow you to install Silverlight 2.0 Beta 2 SDK.

Now if you followed my post [MSBuild and Silverlight 2.0 Beta 1 running in Team City](http://weblogs.asp.net/lkempe/archive/2008/05/19/msbuild-and-silverlight-2-0-beta-1-running-in-team-city.aspx), then you know about the issue:
<!-- more -->

> **Using MSbuild to build a solution in a command line doesn’t copy the Silverlight project output to the linked web project**

This issue is still there so [my fix is still in my MSBuild file](http://weblogs.asp.net/lkempe/archive/2008/05/19/msbuild-and-silverlight-2-0-beta-1-running-in-team-city.aspx).
