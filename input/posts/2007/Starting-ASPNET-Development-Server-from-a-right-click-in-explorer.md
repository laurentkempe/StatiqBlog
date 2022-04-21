---
title: "Starting ASP.NET Development Server from a right click in explorer"
permalink: /2007/07/07/Starting-ASPNET-Development-Server-from-a-right-click-in-explorer/
date: 7/7/2007 12:18:37 AM
updated: 5/7/2010 7:46:47 AM
disqusIdentifier: 20070707121837
tags: ["Tools", "ASP.NET 2.0", "Visual Studio", "ASP.NET"]
alias:
 - /post/Starting-ASPNET-Development-Server-from-a-right-click-in-explorer.aspx/index.html
---
**<u>Update</u>: **Following the comment of [Jon Galloway](http://weblogs.asp.net/jgalloway)

I know that there are other solutions doing this but I was just asked about how to do it today (hey Christine ;) and had this registry file stored somewhere for a while waiting for a blog post.
<!-- more -->

Save this to a file with an extension .reg, e.g. "asp.net web server here.reg":

Windows Registry Editor Version 5.00  

[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Folder\shell\VS2005 WebServer]  
@="ASP.NET 2.0 Web Server Here"  

[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Folder\shell\VS2005 WebServer\command]  
@="C:\\Windows\\Microsoft.NET\\Framework\\v2.0.50727\\Webdev.WebServer.exe /port:9081 /path:\"%1\"" 

You might also change the port used to start the ASP.NET Development Server. If this option is not specified then the port 80 is used.  

Then double click on the .reg file to save the settings in the registry.  

Now you will have access to the following right click menu in explorer and you can browse your site without running Visual Studio:  

![](http://www.techheadbrothers.com/images/blog/asp.netserverhere2.gif) 

**<u>Update:</u>** This is not needed anymore

You will need to copy [hstart.exe](http://www.ntwind.com/software/utilities/hstart.html) to your C:\Windows\system32\ first, or change the path used in the .reg file. This little tool (3kb); Hidden start, by [NTWind Software](http://www.ntwind.com/) is really good for "small startup manager that **allows console applications to be started without any windows in the background**.". Exactly what is need in our case. 
