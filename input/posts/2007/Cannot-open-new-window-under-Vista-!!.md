---
title: "Cannot open new window under Vista !!"
permalink: /2007/05/23/Cannot-open-new-window-under-Vista-!!/
date: 5/23/2007 5:27:05 AM
updated: 5/23/2007 5:27:05 AM
disqusIdentifier: 20070523052705
alias:
 - /post/Cannot-open-new-window-under-Vista-!!.aspx/index.html
---
I was experiencing for some time a quite strange effect on my no so old installation of Windows Vista. After some dyas of usage without any reboot I sometime was unable to open any new window, it could be an explorer, internet explorer 7 tab, a menu of tray icon, and this without any error message. I first thought it was an issue with the video driver, but after too much annoyance I searched the web and found a post "[Max Num. of Open Windows under XP/2003/VISTA - Resolved !](http://weblogs.asp.net/israelio/archive/2007/02/07/max-num-of-open-windows-under-xp-2003-vista-resolved.aspx)' explaining the issue and a possible fix.

> From [Ohad's WebLog](http://weblogs.asp.net/israelio/)
<!-- more -->
> 
> When I'm working (it happens from now and then...) I always tend to open lots and lots of explorer windows as I Google for answers to questions that come along while coding...
> 
> When I used XP and Win 2003 it always bothered me that as soon as I opened 15-17 Internet explorer windows my system got stuck so hard that even right click context menu wouldn't seem to show up.
> When I migrated to Vista I though that finally someone in MS will fix this limitation but I was disappointed to find out that it still there...
> 
> Well after looking and looking... searching and hacking ... I found the answer !
> 
> **Its all about the desktop heap !**
> 
> <u>Following is Desktop Heap Tweak:</u>
> 
> Because it is an undocumented registry tweak **<font color="#ff0000">I take no responsibility regarding implementing this tweak ! It worked for me and if you choose to tweak your system you are doing it on your own risk</font>.**
> 
> Longtime Windows users are familiar with the desktop heap, a memory space that Windows allocates for desktop objects such as, well, windows.
> 
> Each open window or other desktop object uses up a certain amount of the desktop heap. In older versions of Windows the desktop heap was very small, and objects weren't always disposed from the heap correctly.
> 
> This was a good part of the reason for the Incredible Shrinking Resource Heap problem that plagued the 16 and hybrid 16/32-bit versions of Windows.
> 
> NT fixed this problem by devoting a far larger chunk of memory to the desktop heap -- but the fact that it had a far better memory manager than Win 3.x or Win9x, and a pure 32-bit architecture, didn't hurt either.
> 
> The desktop heap limit affects the NT/VISTA family of systems, Hitting the limit is manifested as either a DLL initialization error for USER32.dll or an out of memory error.
> 
> Fortunately, the limit it tweakable, the default settings are low enough that the limit is easily hit.
> To tweak the limit, take a look in the registry at **<font color="#008000">HLKM/System/CurrentControlSet/Control/Session Manager/SubSystems</font>**
> 
> (cranked up a bit if you find yourself manipulating a lot of desktop objects.)
> 
> Within that key is a subkey called Windows, which contains in it, among other things, the value "**<font color="#008080">SharedSection=1024,3072</font>**"
> 
> Changing the SharedSection entry to "**<font color="#008080">1024,3072,512</font>**" (note the comma and the value)increases the size of the "hidden" desktop heap.
> 
> If that doesn't work, try increasing the second of the comma delimited values (e.g.<font color="#008080"> **3072 -> 4096**</font>) which is the size limit of any particular desktop heap.
> 
> <font color="#ff0000">Update: just to clarify for some pepole... on vista your default is 1024,3072,512 changing is to 1024,4096,512 will make the difference</font>

I did the modification and for the moment didn't had any issue, but I will wait one more week to be sure that it works good for me.
