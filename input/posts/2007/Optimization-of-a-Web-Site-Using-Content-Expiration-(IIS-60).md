---
title: "Optimization of a Web Site - Using Content Expiration (IIS 6.0)"
permalink: /2007/07/25/Optimization-of-a-Web-Site-Using-Content-Expiration-(IIS-60)/
date: 7/25/2007 5:54:46 PM
updated: 5/7/2010 7:53:14 AM
disqusIdentifier: 20070725055446
tags: ["Tech Head Brothers", "ASP.NET AJAX", "ASP.NET AJAX Control Toolkit "]
alias:
 - /post/Optimization-of-a-Web-Site-Using-Content-Expiration-(IIS-60).aspx/index.html
---
As you might know, I am running the French .NET portal [Tech Head Brothers](http://www.techheadbrothers.com/) and I am always searching for good way to optimize it and make it run better.

Following the post of [Jeremy Zawodny's blog](http://jeremy.zawodny.com/blog/): "[How To Add Good Expires Headers to Images in Apache 1.3](http://jeremy.zawodny.com/blog/archives/009272.html)" I decided to check this on my server running [Tech Head Brothers](http://www.techheadbrothers.com/) on IIS 6.0.
<!-- more -->

You can read more about the way to configure this option on [technet](http://technet.microsoft.com/en-us/default.aspx) "[Using Content Expiration (IIS 6.0)](http://www.microsoft.com/technet/prodtechnol/WindowsServer2003/Library/IIS/0fc16fe7-be45-4033-a5aa-d7fda3c993ff.mspx?mfr=true)".

After several tests I must say that yes it increase the performance of the site. I used the wonderful [httpwatch 4.2](http://www.httpwatch.com/default.htm) to run the tests.

The other optimization I did in the past:

1.  added compression to the site to optimize the load time of all the [ASP.NET AJAX](http://ajax.asp.net/) and [Control Toolkit](http://ajax.asp.net/ajaxtoolkit) scripts and the page html
2.  modified the site to use less external files, I had multiple css files so migrated to almost one
3.  usage of the [AJAX Control Toolkit - ToolkitScriptManager](http://weblogs.asp.net/lkempe/archive/2007/06/08/ajax-control-toolkit-toolkitscriptmanager.aspx "AJAX Control Toolkit - ToolkitScriptManager")
