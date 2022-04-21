---
title: "VisualSVN and Pending Changes"
permalink: /2009/11/30/VisualSVN-and-Pending-Changes/
date: 11/30/2009 7:09:37 PM
updated: 11/30/2009 7:09:37 PM
disqusIdentifier: 20091130070937
tags: ["VisualSVN"]
alias:
 - /post/VisualSVN-and-Pending-Changes.aspx/index.html
---
I am using and enjoying for some time now [VisualSVN](http://www.visualsvn.com/visualsvn/) plugin in Visual Studio 2008. I just discovered a nice feature that is not part of the normal menu but that you can easily add to; the pending changes window:

[![4146938714_6faf5320fe_o[1]](http://weblogs.asp.net/blogs/lkempe/4146938714_6faf5320fe_o1_thumb_165F0655.png "4146938714_6faf5320fe_o[1]")](http://weblogs.asp.net/blogs/lkempe/4146938714_6faf5320fe_o1_34010721.png)  
<!-- more -->

It let’s you quickly see the files that you have modified since your last commit, and double click to open those files.

Then you can use the normal shortcut VisualSVN.NextDifference, Ctrl+Shift+Down Arrow or VisualSVN.PreviousDifference, Ctrl+Shift+Up Arrow to navigate the changes into that file that you just opened:

[![4146948196_958c3396c3_o[1]](http://weblogs.asp.net/blogs/lkempe/4146948196_958c3396c3_o1_thumb_5CAC469D.png "4146948196_958c3396c3_o[1]")](http://weblogs.asp.net/blogs/lkempe/4146948196_958c3396c3_o1_3722C93C.png) 

Notice the yellow visual helper on the left !

To have access to the Pending Changes window in Visual Studio 2008, just click on the menu Tools / Customize and then select VisualSVN in Categories, then search for Pending Changes in Commands and drag it to the VisualSVN menu

![VisualSVN Pending Changes](http://weblogs.asp.net/blogs/lkempe/4146920714_b359ef6262_o1_0235C3FF.png "VisualSVN Pending Changes")

By the way I also use VisualSVN that I highly recommend ! It is really easy to install and configure on a Windows machine.
