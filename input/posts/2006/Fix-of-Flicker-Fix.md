---
title: "Fix of Flicker Fix"
permalink: /2006/05/27/Fix-of-Flicker-Fix/
date: 5/27/2006 6:33:00 PM
updated: 5/7/2010 7:46:43 AM
disqusIdentifier: 20060527063300
tags: ["ASP.NET 2.0", "ASP.NET AJAX", "ASP.NET"]
alias:
 - /post/Fix-of-Flicker-Fix.aspx/index.html
---
If you read my post about [Flicker Fix](/lkempe/archive/2006/04/29/444392.aspx) that was included and removed from the first distribution of <u><font color="#006ff7">CSS friendly control adapters beta</font></u> you might have heard that it created a security hole.

Having a handler or other reading a file that you might specify the path in a parameter is a really <strong>really</strong> bad idea. It lets for example read possibility to your web.config file to anybody just browsing your site. If your connection string to the db is in clear then... too bad.
<!-- more -->

Now there is a [fix to that issue posted on that page](http://www.groovybits.com/leftoverbits/flickerfix.aspx).

I like the way it is implemented with a base rule of security, define what is allowed. So basically you define a key in your appconfig saying which pictures are accessible through the handler and in you css you reference this key. Nice.
