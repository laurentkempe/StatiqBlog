---
title: "Draco.net on Windows 2003 with CVS and Visual Studio .Net Build"
permalink: /2003/12/10/Draconet-on-Windows-2003-with-CVS-and-Visual-Studio-Net-Build/
date: 12/10/2003 9:01:00 AM
updated: 12/10/2003 9:01:00 AM
disqusIdentifier: 20031210090100
tags: ["Tools"]
alias:
 - /post/Draconet-on-Windows-2003-with-CVS-and-Visual-Studio-Net-Build.aspx/index.html
---
I finally managed :-) to have [Draco.net](http://draconet.sourceforge.net) working on Windows 2003 Server with cvs. I always had a [100% cpu usage](http://www.mail-archive.com/draconet-users@lists.sourceforge.net/msg00069.html) when Draco.net detected a change and that was due to a cvs process.

The solution I used was to define  <strong><cvsroot>:sspi:SERVERNAME:/MyREPO</cvsroot> </strong>like that. Before I had <strong><cvsroot>:sspi:User@SERVERNAME:E:/MyREPO</cvsroot></strong>, and it was not working with that configuration.
<!-- more -->

I use CVSNT 2.0.13 and Draco.net 1.5-beta-2.

I use that continous integration tools to develop with my brother our web site. The good thing is that when he change something in the cvs repository I get an email with the changes listed and the status of a build. This ease our communication concerning release. Cool !!!!
