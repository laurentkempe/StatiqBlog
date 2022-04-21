---
title: "BlogThisUsingWriterPlugin coming soon"
permalink: /2006/08/17/BlogThisUsingWriterPlugin-coming-soon/
date: 8/17/2006 9:03:31 AM
updated: 8/17/2006 9:03:31 AM
disqusIdentifier: 20060817090331
tags: ["Tools", "ASP.NET"]
alias:
 - /post/BlogThisUsingWriterPlugin-coming-soon.aspx/index.html
---
OK I fixed my main issue. I wasn't able to have my plugin loaded by Jetbrains Omea, and it seems that it is linked with the .NET Framework 2.0 that I use in my plugin, even if Omea Reader is running using .NET Framework 2.0 somehow it can't verify the plugin.

I had to install MSBee so I don't need to install VS 03 again. And now back to the old days of command line I have to compile using such a command:
<!-- more -->

msbuild BlogThisUsingWriterPlugin.csproj /t:Rebuild /p:TargetFX1_1=true

After modifying my csproj MSBuild file to use MSBee

So this is how it look likes in Omea Reader

[![](http://www.techheadbrothers.com/images/blog/c09af96e7bd2_18F4/image0_thumb1.png)](http://www.techheadbrothers.com/images/blog/c09af96e7bd2_18F4/image03.png) 

And the result in Windows Live Writer:

[![](http://www.techheadbrothers.com/images/blog/c09af96e7bd2_18F4/image0_thumb2.png)](http://www.techheadbrothers.com/images/blog/c09af96e7bd2_18F4/image06.png)

Not bad.

That will be my second plugin for Live Writer and it will be also on Codeplex in [Windows Live Writer Plugins](http://www.codeplex.com/Wiki/View.aspx?ProjectName=WLWPlugins) project.

Thanks to [Christopher Frazier](http://www.chrisfrazier.net/blog/default.aspx) for the kind help compiling my project on VS 03.

Ok it is 2:01 AM, time to go to bed.
