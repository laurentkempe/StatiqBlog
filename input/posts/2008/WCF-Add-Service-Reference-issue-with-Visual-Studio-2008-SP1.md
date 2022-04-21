---
title: "WCF Add Service Reference issue with Visual Studio 2008 SP1"
permalink: /2008/09/10/WCF-Add-Service-Reference-issue-with-Visual-Studio-2008-SP1/
date: 9/10/2008 5:00:51 AM
updated: 9/10/2008 5:00:51 AM
disqusIdentifier: 20080910050051
tags: ["Visual Studio", "WCF", ".NET Framework 3.5"]
alias:
 - /post/WCF-Add-Service-Reference-issue-with-Visual-Studio-2008-SP1.aspx/index.html
---
![](http://farm4.static.flickr.com/3148/2844017150_6ffb53b2ac_o.png) The other day I faced a strange issue when I wanted to add a service reference to the [Tech Head Brothers](http://www.techheadbrothers.com/) authoring tool. The code generated wasn’t compiling with errors located in the file *Reference.cs*. 

Drilling down the issue I figured out that the default namespace, *TechHeadBrothers.Word*, was the issue. It was just added at some place and at other not.
<!-- more -->

![](http://farm4.static.flickr.com/3054/2844022854_d08c534a81_o.png) 

I made then a quick test with another assembly which had no dot in it’s name. That’s was it! The issue was the dot into the Assembly name, Default namespace.

Don’t use . in the name of your Assembly name, Default namespace to get back to normal state, weird!
