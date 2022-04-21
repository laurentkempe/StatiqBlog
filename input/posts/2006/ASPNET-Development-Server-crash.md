---
title: "ASP.NET Development Server crash"
permalink: /2006/08/03/ASPNET-Development-Server-crash/
date: 8/3/2006 6:09:00 PM
updated: 8/3/2006 6:09:00 PM
disqusIdentifier: 20060803060900
tags: ["ASP.NET 2.0", "Visual Studio", ".NET"]
alias:
 - /post/ASPNET-Development-Server-crash.aspx/index.html
---
This morning I had a strange crash of ASP.NET Development Server, it is the first time that it happens and I was really stressed about it.

With debugger I got: "**<font size="2" color="#000080">An unhandled exception of type 'System.StackOverflowException' occurred in Unknown Module."</font>**
<!-- more -->

I thought of third parties assemblies but that wasn't it, so I finally thought of infinite loop and pointed out that I was doing the following in my last changes:
<div style="font-size: 10pt; background: white; color: black; font-family: Consolas">

<span style="color: blue">public</span> <span style="color: blue">virtual</span> <span style="color: blue">string</span> **ChromeTemplateFile**

{

    <span style="color: blue">get</span>

    {

        <span style="color: blue">if</span> (<span style="color: teal">WebPartManager</span>.GetCurrentWebPartManager(Page).DisplayMode == <span style="color: teal">WebPartManager</span>.BrowseDisplayMode)

            <span style="color: blue">return</span> BrowseDisplayModeChromeTemplateFile;

        <span style="color: blue">else</span>

            <span style="color: blue">return</span> **ChromeTemplateFile**;

    }

    <span style="color: blue">set</span> { <span style="color: blue">throw</span> <span style="color: blue">new</span> <span style="color: teal">ApplicationException</span>(<span style="color: maroon">"Not Implemented"</span>); }

}

</div>

Bingo, one miss tipping of a properties name and you end up in an infinite loop. Thanks Valentin for the nice msn talks ;-)
