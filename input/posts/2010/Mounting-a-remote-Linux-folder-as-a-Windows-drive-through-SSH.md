---
title: "Mounting a remote Linux folder as a Windows drive through SSH"
permalink: /2010/11/27/Mounting-a-remote-Linux-folder-as-a-Windows-drive-through-SSH/
date: 11/27/2010 3:39:41 AM
updated: 11/27/2010 3:39:41 AM
disqusIdentifier: 20101127033941
tags: ["Tools"]
alias:
 - /post/Mounting-a-remote-Linux-folder-as-a-Windows-drive-through-SSH.aspx/index.html
---
There are some times in which you need to come to some extreme solutions. Having two days of trials without success to have a portlet running in a local Tomcat with Day portal I came to the following solution.

Shortly why I came to such a solution? I needed to work on some CSS on a portal solution, and the development cycle was taking too long. I had to commit to svn, run a teamcity build which deployed a war to weblogic to finally be able to test my CSS changes. Far too long.
<!-- more -->

So first idea was to have our portlet working locally in Day portal deployed on Tomcat. After many attempt to have this running, I have stopped with this idea.

Next idea was to work directly on the Linux file system, and modifying the CSS files which would be reloaded automatically by weblogic. Good idea, it was working but I needed more, I needed my development environment.

So I searched a solution to be able to mount a remote Linux folder as Windows drive through SSH. And I quickly found a solution! Thanks to [Dokan SSHFS](http://dokan-dev.net/en/download/)

I finally could fire [JetBrains WebStorm](http://www.jetbrains.com/webstorm/) on my Windows notebook, edit CSS files which are located on a remote Linux server, save the file and refresh my page on the browser. Straight solution, certainly not the best but at least I could work.

Dokan SSHFS is a great tool! It took me 10 minutes to install it and configure it.

You must take care of:

*   Dokan SSHFS supports only OpenSSH key format. So I used puttygen to convert my key.
*   Dokan SSHFS doesn’t work with Dokan 0.5, you have to use the updated files [found on the web page](http://dokan-dev.net/en/download/#sshfs)  

The rest was just easy! Well done.
