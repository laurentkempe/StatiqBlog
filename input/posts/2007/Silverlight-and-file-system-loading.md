---
title: "Silverlight and file system loading"
permalink: /2007/09/12/Silverlight-and-file-system-loading/
date: 9/12/2007 8:04:51 AM
updated: 9/12/2007 8:04:51 AM
disqusIdentifier: 20070912080451
tags: ["Silverlight", "Silverlight Streaming"]
alias:
 - /post/Silverlight-and-file-system-loading.aspx/index.html
---
I am currently working with [Silverlight](http://silverlight.net/Default.aspx) and [Silverlight Streaming](http://silverlight.live.com/) and had an issue over the weekend I am sure I will not be the last one to have so:

**Don't try to load a Silverlight application from the file system**, I mean double clicking an html file, prefer to have a little web application or even use "[Starting ASP.NET Development Server from a right click in explorer](http://weblogs.asp.net/lkempe/archive/2007/07/06/starting-asp-net-development-server-from-a-right-click-in-explorer.aspx)" to test your application.
<!-- more -->

The security used in both cases is different so it makes the difference.

Thanks to [Mathieu](http://www.techheadbrothers.com/Auteurs.aspx/mathieu-kempe) for the support in the development and [Jon Galloway](http://weblogs.asp.net/jgalloway/) for the support in troubleshooting the issue.
