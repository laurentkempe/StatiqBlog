---
title: "ASP.NET Development Server crash"
permalink: /2006/08/03/ASPNET-Development-Server-crash/
date: 8/3/2006 6:09:00 PM
updated: 8/3/2006 6:09:00 PM
disqusIdentifier: 20060803060900
tags: ["ASP.NET 2.0", "Visual Studio", ".NET"]
---
This morning I had a strange crash of ASP.NET Development Server, it is the first time that it happens and I was really stressed about it.

With debugger I got: "**An unhandled exception of type 'System.StackOverflowException' occurred in Unknown Module."**
<!-- more -->

I thought of third parties assemblies but that wasn't it, so I finally thought of infinite loop and pointed out that I was doing the following in my last changes:

````csharp
public virtual string ChromeTemplateFile
{
    get
    {
        if (WebPartManager.GetCurrentWebPartManager(Page).DisplayMode == WebPartManager.BrowseDisplayMode)
            return BrowseDisplayModeChromeTemplateFile;
        else
            return ChromeTemplateFile;
    }
    set { throw new ApplicationException("Not Implemented"); }
}
````

Bingo, one miss tipping of a properties name and you end up in an infinite loop. Thanks Valentin for the nice msn talks ;-)
