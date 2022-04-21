---
title: "Building ClickOnce with TeamCity"
permalink: /2009/11/03/Building-ClickOnce-with-TeamCity/
date: 11/3/2009 7:52:25 AM
updated: 11/3/2009 7:52:25 AM
disqusIdentifier: 20091103075225
tags: ["continuous integration", "Team City", "ClickOnce"]
alias:
 - /post/Building-ClickOnce-with-TeamCity.aspx/index.html
---
[Migrating our TeamCity](http://weblogs.asp.net/lkempe/archive/2009/11/02/teamcity-migration-tip.aspx) server today I got the following error on the new server:

error MSB3147: Could not find required file 'setup.bin' in …
<!-- more -->

I for sure fixed that on the older server, and the fix was to have the SDK on the build server. The issue is that I don’t want to install Visual Studio to do that, so here is how I achieved it

1.  Copy my whole c:\Program Files\Microsoft SDKs\Windows\v6.0a folder to the server
2.  Created a registry key        
Key: HKEY_LOCAL_MACHINE\Software\Microsoft\GenericBootstrapper\3.5\         
Value: Path         
Type: REG_SZ         
Data: C:\Program Files\Microsoft SDKs\Windows\v6.0a\Bootstrapper\  

Then the error was gone and I had my build server ready again to build ClickOnce setup.
