---
title: "Update of Tech Head Brothers site"
permalink: /2004/06/05/Update-of-Tech-Head-Brothers-site/
date: 6/5/2004 8:23:00 AM
updated: 6/5/2004 8:23:00 AM
disqusIdentifier: 20040605082300
tags: ["Tech Head Brothers", ".NET Development"]
alias:
 - /post/Update-of-Tech-Head-Brothers-site.aspx/index.html
---
I refactored quit a lot of code on my site [Tech Head Brothers](http://www.techheadbrothers.com "Tech Head Brothers") this week and went to bed late (around 1:30 AM each day). But now it is running better and faster. I removed the rendering to Pdf using [NFop](http://sourceforge.net/projects/nfop/) of the articles because it works locally on my TEST server but not on the PROD one.<br><br>One of the speed improvement is due to the complete reimplementation of a user control that download a RSS to publishnews on my site. The issue with the previous method was that the cache was buggy. Now what I am doing is to to use two values in cache, one is the RSS document (a XML document as a string) and that one never expire:
<font size="2">


<!-- more -->
Cache.Insert(CacheKey, xmlControl.Document.InnerXml, </font><font color="#0000ff" size="2">null</font><font size="2">, <strong>System.Web.Caching.Cache.NoAbsoluteExpiration</strong>, <strong>System.Web.Caching.Cache.NoSlidingExpiration</strong>);</font>

<font size="2">The second cache value is an expiration cache value :</font>
<font size="2"><font size="2">


Cache.Insert(ExpiredKey, "", </font><font color="#0000ff" size="2">null</font><font size="2">, DateTime.Now.AddSeconds(60), TimeSpan.Zero);</font>

<font size="2">I use that cache value to know if it is time to download again the RSS :</font>
<font size="2"><font size="2">


</font><font color="#808080" size="2">///</font><font color="#008000" size="2"> </font><font color="#808080" size="2"><summary>

</font><font size="2">


</font><font color="#808080" size="2">///</font><font color="#008000" size="2"> Check if the cache expired

</font><font size="2">


</font><font color="#808080" size="2">///</font><font color="#008000" size="2"> </font><font color="#808080" size="2"></summary>

</font><font size="2">


</font><font color="#808080" size="2">///</font><font color="#008000" size="2"> </font><font color="#808080" size="2"><returns></font><font color="#008000" size="2">true, if it expired otherwise false</font><font color="#808080" size="2"></returns>

</font><font size="2">


</font><font color="#0000ff" size="2">private</font><font size="2"> </font><font color="#0000ff" size="2">bool</font><font size="2"> isCacheStillValid()

{

</font><font color="#0000ff" size="2">    return</font><font size="2"> ((Cache.Get(ExpiredKey) != </font><font color="#0000ff" size="2">null</font><font size="2">));

}

If it is time to download again then I remove from the cache the XmlDocument after having it downloaded correctly:
<font size="2">


</font><font color="#008000" size="2">//Remove old version from cache

</font><font size="2">


Cache.Remove(CacheKey);

This way I always have the RSS in the cache, and remove it only if I have a new one downloaded.
</font></font></font></font>
