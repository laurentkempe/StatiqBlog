---
title: "Using windows 7 symbolic link to organize your DropBox"
permalink: /2010/09/28/Using-windows-7-symbolic-link-to-organize-your-DropBox/
date: 9/28/2010 3:32:55 AM
updated: 9/28/2010 3:32:55 AM
disqusIdentifier: 20100928033255
tags: ["Tools", "Windows 7"]
alias:
 - /post/Using-windows-7-symbolic-link-to-organize-your-DropBox.aspx/index.html
---
If you are a user of the fantastic [DropBox](http://www.dropbox.com/referrals/NTIyMjQ1MTk) tool you might know that you have to organize your folders under a top root folder. This was really an issue for me which I wanted to solve because I like to organize my files in a different manner.

I used the **mklink** command from a Windows Command
<!-- more -->

E:\My Dropbox>mklink
Creates a symbolic link.

MKLINK [[/D] | [/H] | [/J]] Link Target

        /D      Creates a directory symbolic link.  Default is a file
                symbolic link.
        /H      Creates a hard link instead of a symbolic link.
        /J      Creates a Directory Junction.
        Link    specifies the new symbolic link name.
        Target  specifies the path (relative or absolute) that the new link
                refers to.

I used it this way to create a symbolic link between a folder in “My Dropbox” folder on a folder outside:



E:\My Dropbox>mklink /D "Logs" "L:\Logs"
symbolic link created for Logs <<===>> L:\Logs

So now everything which is on my *L:\Logs *folder is synchronized using DropBox.

You can also use this trick to move folder around on your system after having those under the DropBox root folder. Before that I always stop DropBox and restart it after the creation of the symbolic link.

Very helpful!
