---
title: "Yahoo release a web site evaluation tool called YSlow"
permalink: /2007/08/19/Yahoo-release-a-web-site-evaluation-tool-called-YSlow/
date: 8/19/2007 11:07:30 PM
updated: 8/19/2007 11:07:30 PM
disqusIdentifier: 20070819110730
tags: ["Tools", "ASP.NET 2.0", "ASP.NET"]
alias:
 - /post/Yahoo-release-a-web-site-evaluation-tool-called-YSlow.aspx/index.html
---
[YSlow](http://developer.yahoo.com/yslow/) is a new web tool published by Yahoo. It let you test a total of 13 rules against your web site to check if it is efficient.

YSlow is an addin to Firefox integrated in the web development tool [Firebug](http://www.getfirebug.com/).
<!-- more -->

You might read the rules that are verified on this page : [Thirteen Simple Rules for Speeding Up Your Web Site](http://developer.yahoo.com/performance/rules.html) and the documentation is on this page : [YSlow User Guide](http://developer.yahoo.com/yslow/help/).

There is also a screencast on this page : [YSlow Podcast Interview and Screencast Demo](http://developer.yahoo.net/blog/archives/2007/08/yslow-podcast-screencast.html).

#### Performance view

YSlow analyze the web page and generate a global mark and one mark per rules tested.

![](http://www.techheadbrothers.com/content/ec901840-2744-4bf0-b5d1-caf13c6ddcb8/yslow-pic1.jpg) 

This is the gui of YSLow showing the result of the analysis of [Tech Head Brothers](http://www.techheadbrothers.com/) main page. You can see the global mark of C (not so bad ;) and the different marks per rule. For example a D for the rule "Make fewer HTTP requests". You can opne up each section that didn't get a A to see what might be better.

![](http://www.techheadbrothers.com/content/ec901840-2744-4bf0-b5d1-caf13c6ddcb8/yslow-pic4.jpg) 

We can see on the previous picture that YSlow identified that we are doing 11 requests for different JavaScript files. When you click on the rule you get a new web page with the explanation of the rule.

#### Stats view

YSlow compute the total size of a web page with and without the use of the cache and also give you some information on cookies.

![](http://www.techheadbrothers.com/content/ec901840-2744-4bf0-b5d1-caf13c6ddcb8/yslow-pic2.jpg) 

You can see clearly the impact of the cache on your web application. On the previous screen shot you see that the browser needs to download 127.3 Kb with 21 HTTP requests without the cache and this numbers drops to 28.2 Kb and 5 HTTP requests with.

#### Components view

You get also a complete list of all the components of your page, included their type, URL, expiration date, gzip status, loading time , size and ETag.

![](http://www.techheadbrothers.com/content/ec901840-2744-4bf0-b5d1-caf13c6ddcb8/yslow-pic3.jpg) 

#### Menu tools

The last menu let's you display the whole Javascript and CSS used by your web page to get a global view. It gives you also a web page with the result of the test on one page. The last part is [JSLint](http://jslint.com/) checking the Code Conventions for the JavaScript Programming Language.

![](http://www.techheadbrothers.com/content/ec901840-2744-4bf0-b5d1-caf13c6ddcb8/yslow-pic5.jpg)  

[YSlow](http://developer.yahoo.com/yslow/YSlow) is a tool that might be usefull when you come to the optimization of your web site.
