---
title: "[Note] - Fiddler localhost"
permalink: /2007/06/05/Note-Fiddler-localhost/
date: 6/5/2007 6:21:49 PM
updated: 5/7/2010 7:45:48 AM
disqusIdentifier: 20070605062149
tags: ["Tools", "Note"]
---
That's just a reminder for me about [Fiddler](http://www.fiddler2.com/) and localhost/127.0.0.1:

<!-- more -->
> 
> #### I don't see IE7 or .NET traffic sent to <u>*localhost*</u> or <u>*127.0.0.1.*</u>
> 
> 
> IE7 and the .NET Framework are hardcoded not to send requests for Localhost through proxies.  Fiddler runs as a proxy.  The workaround is to use your machine name as the hostname instead of Localhost or 127.0.0.1.
> 
> So, for instance, rather than hitting http://**localhost**:8081/mytestpage.aspx, instead visit http://**machinename**:8081/mytestpage.aspx.  Alternatively, you can use http://**localhost.**:8081/mytestpage.aspx (note the trailing dot after *localhost*).  Alternatively, you can customize your Rules file like so:

```javascript
static function OnBeforeRequest(oSession :  Fiddler.Session) 
{
   if (oSession.host.ToUpper() == "MYAPP") { oSession.host = "127.0.0.1:8081"; } 
} 
```
  
> ...and then navigate to [http://myapp](http://www.fiddler2.com/), which will act as an alias for 127.0.0.1:8081.
