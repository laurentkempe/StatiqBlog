---
title: "Starting IIS Express from a right click in explorer"
permalink: /2011/04/13/Starting-IIS-Express-from-a-right-click-in-explorer/
date: 4/13/2011 7:36:54 AM
updated: 4/13/2011 7:41:20 AM
disqusIdentifier: 20110413073654
tags: ["ASP.NET MVC", "IIS Express"]
alias:
 - /post/Starting-IIS-Express-from-a-right-click-in-explorer.aspx/index.html
---
After publishing this for [Starting ASP.NET Development Server from a right click in explorer](http://www.laurentkempe.com/post/Starting-ASPNET-Development-Server-from-a-right-click-in-explorer.aspx) it is time to do it for IIS Express.

Tonight trying out Orchard 1.1 I just wanted to start by right clicking and getting IIS Express fired so that I can test the 1.1 version.
<!-- more -->

So I just modified my old Windows Registry .reg file and adapted it to IIS Express: 

> Windows Registry Editor Version 5.00
> 
> [HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Folder\shell\VS2005 WebServer]
> @="IIS Express Here"
> 
> [HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Folder\shell\VS2005 WebServer\command]
> @="C:\\Program Files (x86)\\IIS Express\\iisexpress.exe /path:\"%1\""

Take care that this is a file for 64 bits Windows OS, adapt to your path. Then you can double click on the .reg file to save the settings in the registry.

Now you will have access to the IIS Express Here right click menu in explorer and you can browse your site without running Visual Studio!

You might also read more for example on the other start param on [Running a Site using IIS Express from the Command Line](http://learn.iis.net/page.aspx/870/running-iis-express-from-the-command-line/)
