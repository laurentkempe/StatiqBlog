---
title: "Update of Tech Head Brothers site"
permalink: /2004/06/05/Update-of-Tech-Head-Brothers-site/
date: 6/5/2004 8:23:00 AM
updated: 6/5/2004 8:23:00 AM
disqusIdentifier: 20040605082300
tags: ["Tech Head Brothers", ".NET Development"]
---
I refactored quit a lot of code on my site [Tech Head Brothers](http://www.techheadbrothers.com "Tech Head Brothers") this week and went to bed late (around 1:30 AM each day). But now it is running better and faster. I removed the rendering to Pdf using [NFop](http://sourceforge.net/projects/nfop/) of the articles because it works locally on my TEST server but not on the PROD one.<br><br>One of the speed improvement is due to the complete reimplementation of a user control that download a RSS to publishnews on my site. The issue with the previous method was that the cache was buggy. Now what I am doing is to to use two values in cache, one is the RSS document (a XML document as a string) and that one never expire:
<!-- more -->

```csharp
Cache.Insert(CacheKey, xmlControl.Document.InnerXml, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
```

The second cache value is an expiration cache value :

```csharp
Cache.Insert(ExpiredKey, "", null, DateTime.Now.AddSeconds(60), TimeSpan.Zero);
```

I use that cache value to know if it is time to download again the RSS :

```csharp
///
/// Check if the cache expired
///
/// true, if it expired otherwise false
private bool isCacheStillValid()
{
    return ((Cache.Get(ExpiredKey) != null));
}
```

If it is time to download again then I remove from the cache the XmlDocument after having it downloaded correctly:

```csharp
//Remove old version from cache
Cache.Remove(CacheKey);
```

This way I always have the RSS in the cache, and remove it only if I have a new one downloaded.
