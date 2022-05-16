---
title: "ASP.NET Ajax RC and Google Adsense"
permalink: /2006/12/22/ASPNET-Ajax-RC-and-Google-Adsense/
date: 12/22/2006 7:25:49 PM
updated: 5/7/2010 7:45:39 AM
disqusIdentifier: 20061222072549
tags: ["ASP.NET AJAX", "ASP.NET"]
---
During the integration of ASP.NET Ajax RC on [Tech Head Brothers](http://www.techheadbrothers.com/ "Tech Head Brothers") I had issues with some javascript failing both in ASP.NET Ajax and Adsense. After a short online discussion with [Cyril](http://blogs.codes-sources.com/cyril/default.aspx), he could manage to get a fix, and to identify the issue in the ASP.NET Ajax RC. You might read more about it on the [forum](http://forums.asp.net/thread/1501276.aspx), or on his [french blog](http://blogs.codes-sources.com/cyril/archive/2006/12/17/atlas-et-google-adsense-bug-avec-la-methode-date-parse.aspx).

Adsense uses the native method Date.parse of JavaScript. ASP.NET Ajax RC overload this method changing a bit its behavior. Luckily the old method is saved in Date._jsParse. Using this code just before your first insert of the adsense script will solve the issue:
<!-- more -->

```javascript
<script type="text/javascript"><!--
    Date.__cyril_parse = Date.parse; 
    Date.parse = function(s){
        try {
            return Date.__cyril_parse(s);
        } catch (e){
            var d = Date._jsParse(s);
            if (d) {
                return d; 
            } else {
                throw e;
            } 
        }
    }
//--></script>
```

**Thanks Cyril!!!**

**<font color="#ff0000">This is a critical bug that must be corrected for the RTM!!</font>**
