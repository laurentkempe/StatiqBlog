---
title: "Jobping announcing StickyBeak"
permalink: /2010/06/26/Jobping-announcing-StickyBeak/
date: 6/26/2010 8:25:21 PM
updated: 6/26/2010 8:25:21 PM
disqusIdentifier: 20100626082521
tags: ["Jobping", "ASP.NET", "ASP.NET MVC"]
alias:
 - /post/Jobping-announcing-StickyBeak.aspx/index.html
---
As said when we launched [Jobping](http://www.jobping.com) [Open Source Short Urls Service](http://blog.jobping.com/2010/05/jobping-open-source-short-urls-service.html), we at [Jobping](http://www.jobping.com) are committed to open source.

So last week we announced  [StickyBeak](http://stickybeak.codeplex.com/), you might read more on the blog of [Mark](http://markkemper1.blogspot.com/) - [Introduction to StickyBeak](http://markkemper1.blogspot.com/2010/06/introduction-to-stickybeak.html)
<!-- more -->

[StickyBeak](http://stickybeak.codeplex.com/) is a logging tool for asp.net websites which logs information such as date, http method, url, User.Identity.Name, IP Address, unique session Id, unique browser Id, header values, querystring values, posted form values and cookie values for every request.

Here is a screenshot of the administration page,which lets you see the logged activity on your site.

![](http://farm5.static.flickr.com/4096/4734666739_ecdf9215bc_o.png)

[StickyBeak](http://stickybeak.codeplex.com/) is a complementary tool to the excellent [elmah](http://code.google.com/p/elmah/) from [Atif Aziz](http://www.raboof.com/)

We are using elmah on [Jobping](http://www.jobping.com) to log exceptions that might happen on the site, but we also wanted a raw record of each request made to our site, to make our troubleshooting life easier.

You can download the source and binaries from [StickyBeak on CodePlex](http://stickybeak.codeplex.com/).
