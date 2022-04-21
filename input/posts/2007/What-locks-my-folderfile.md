---
title: "What locks my folder/file?"
permalink: /2007/10/10/What-locks-my-folderfile/
date: 10/10/2007 6:39:49 PM
updated: 10/10/2007 6:39:49 PM
disqusIdentifier: 20071010063949
tags: ["Tools"]
alias:
 - /post/What-locks-my-folderfile.aspx/index.html
---
I guess you already had the following problem; you want to delete a folder or a file and the operating system just tell you that it is impossible because one application locks it. For sure you have no clue which is the bad application doing it!

In the past I was using a little application (which I can't remember the name) but I tend to remove tools from my system if another can achieve the same result. Now to do that I use [Process Explorer](http://www.microsoft.com/technet/sysinternals/utilities/processexplorer.mspx) from [Windows Sysinternals](http://www.microsoft.com/technet/sysinternals/default.mspx), this tool replace my Task Manager and add lots of features.
<!-- more -->

So to use Process Explorer to find which application locks you file/folder use the menu Find / Find Handle or DLL:

![](http://farm3.static.flickr.com/2382/1531410448_5ebf379e74_o.jpg) 

Then type the filename or folder name in the search box and click search.

In the following example I search a PDF that is locked by Foxit Reader.exe:

![](http://farm3.static.flickr.com/2041/1530580795_06c16f059a_o.jpg) 

Enjoy it!
