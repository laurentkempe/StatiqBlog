---
title: "Get rid of extra pixels in Vista Toolbars"
permalink: /2008/01/11/Get-rid-of-extra-pixels-in-Vista-Toolbars/
date: 1/11/2008 10:49:18 PM
updated: 1/11/2008 10:49:18 PM
disqusIdentifier: 20080111104918
tags: ["Vista"]
alias:
 - /post/Get-rid-of-extra-pixels-in-Vista-Toolbars.aspx/index.html
---
When you use *Lock  the Taskbar* in Windows Vista the toolbars shift from some pixels before locking and it is really annoying!

The alternative is to not lock the Taskbar and add the following registry setting to avoir the possibility of moving the toolbars:
<!-- more -->

![](http://farm3.static.flickr.com/2408/2185496536_0be96ce00a_o.jpg)

> HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer
> 
> TaskbarNoDragToolbar
> Prevents users from rearranging toolbars. If you enable this setting the user will not be able to drag or drop toolbars to the taskbar.
> 
> From [http://jalaj.net/2007/11/28/tweaking-registry-for-task-bar-in-vista/](http://jalaj.net/2007/11/28/tweaking-registry-for-task-bar-in-vista/ "http://jalaj.net/2007/11/28/tweaking-registry-for-task-bar-in-vista/")
