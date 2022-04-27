---
title: "VisualSVN and Pending Changes"
permalink: /2009/11/30/VisualSVN-and-Pending-Changes/
date: 11/30/2009 7:09:37 PM
updated: 11/30/2009 7:09:37 PM
disqusIdentifier: 20091130070937
tags: ["VisualSVN"]
---
I am using and enjoying for some time now [VisualSVN](http://www.visualsvn.com/visualsvn/) plugin in Visual Studio 2008. I just discovered a nice feature that is not part of the normal menu but that you can easily add to; the pending changes window:

![](/images/2009/VisualSVN-and-Pending-Changes-1.png)
<!-- more -->

It let’s you quickly see the files that you have modified since your last commit, and double click to open those files.

Then you can use the normal shortcut VisualSVN.NextDifference, Ctrl+Shift+Down Arrow or VisualSVN.PreviousDifference, Ctrl+Shift+Up Arrow to navigate the changes into that file that you just opened:

![](/images/2009/VisualSVN-and-Pending-Changes-2.png)
Notice the yellow visual helper on the left !

To have access to the Pending Changes window in Visual Studio 2008, just click on the menu Tools / Customize and then select VisualSVN in Categories, then search for Pending Changes in Commands and drag it to the VisualSVN menu

![VisualSVN Pending Changes](/images/2009/VisualSVN-and-Pending-Changes-3.png)

By the way I also use VisualSVN that I highly recommend ! It is really easy to install and configure on a Windows machine.
