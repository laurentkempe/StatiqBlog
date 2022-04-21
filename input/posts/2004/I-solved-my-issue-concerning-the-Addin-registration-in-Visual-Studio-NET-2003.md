---
title: "I solved my issue concerning the Addin registration in Visual Studio .NET 2003"
permalink: /2004/06/06/I-solved-my-issue-concerning-the-Addin-registration-in-Visual-Studio-NET-2003/
date: 6/6/2004 10:43:00 PM
updated: 6/6/2004 10:43:00 PM
disqusIdentifier: 20040606104300
tags: ["Tools", ".NET Development"]
alias:
 - /post/I-solved-my-issue-concerning-the-Addin-registration-in-Visual-Studio-NET-2003.aspx/index.html
---
I had a problem to register new addins in Visual Studio .NET 2003 that I exposed there : [Refactoring my publishing tool](/lkempe/archive/2004/05/20/135838.aspx). When I was starting Visual Studio .NET 2003 I got this dialog:

![](http://perso.wanadoo.fr/laurent.kempe/images/vserror.png)
<!-- more -->

Now it is over. If you are facing the same issue here is how I solved it:

I installed the extensibility.dll in the CAG manually:

<strong>gacutil /i "C:\Program Files\Microsoft Visual Studio .NET 2003\Common7\IDE\PublicAssemblies\extensibility.dll"</strong>

Then reinstall or repair the addin installation with its setup. Thats it.
