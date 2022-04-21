---
title: "[Note] - Fiddler localhost"
permalink: /2007/06/05/Note-Fiddler-localhost/
date: 6/5/2007 6:21:49 PM
updated: 5/7/2010 7:45:48 AM
disqusIdentifier: 20070605062149
tags: ["Tools", "Note"]
alias:
 - /post/Note-Fiddler-localhost.aspx/index.html
---
That's just a reminder for me about [Fiddler](http://www.fiddler2.com/) and localhost/127.0.0.1:

> <table cellspacing="0" cellpadding="20" width="100%" border="0" unselectable="on"> <tbody> <tr> <td width="100%"><font size="2">
<!-- more -->
> 
> #### I don't see IE7 or .NET traffic sent to <u>*localhost*</u> or <u>*127.0.0.1.*</u>
> 
>  <blockquote>
> 
> IE7 and the .NET Framework are hardcoded not to send requests for Localhost through proxies.  Fiddler runs as a proxy.  The workaround is to use your machine name as the hostname instead of Localhost or 127.0.0.1.
> 
> So, for instance, rather than hitting http://**localhost**:8081/mytestpage.aspx, instead visit http://**machinename**:8081/mytestpage.aspx.  Alternatively, you can use http://**<span class="style4">localhost.</span>**:8081/mytestpage.aspx (note the trailing dot after *localhost*).  Alternatively, you can customize your Rules file like so:
> <font color="#0000ff" size="2">
> 
> static<font size="2"> </font>function<font size="2"> OnBeforeRequest</font><font color="#ff0000" size="2">(</font>oSession<font color="#ff0000" size="2">:</font><font size="2">Fiddler</font><font color="#ff0000" size="2">.</font><font size="2">Session</font><font color="#ff0000" size="2">)</font><font size="2">{
> </font>  if</font><font size="2"> </font><font color="#ff0000" size="2">(</font><font color="#0000ff" size="2">oSession</font><font color="#ff0000" size="2">.</font><font color="#ff00ff" size="2">host</font><font color="#ff0000" size="2">.</font><font size="2">ToUpper</font><font color="#ff0000" size="2">()</font><font size="2"> </font><font color="#ff0000" size="2">==</font><font size="2"> </font><font color="#4682b4" size="2">"MYAPP"</font><font color="#ff0000" size="2">)</font><font size="2"> { </font><font color="#0000ff" size="2">oSession</font><font color="#ff0000" size="2">.</font><font color="#ff00ff" size="2">host</font><font size="2"> </font><font color="#ff0000" size="2">=</font><font size="2"> </font><font color="#4682b4" size="2">"127.0.0.1:8081"</font><font color="#ff0000" size="2">;</font><font size="2"> }</font>
> <font color="#0000ff" size="2">**}**</font>
> 
> ...and then navigate to [http://myapp](http://www.fiddler2.com/), which will act as an alias for 127.0.0.1:8081.
> </blockquote></font></td></tr></tbody></table>
