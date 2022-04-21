---
title: "Remove .svn folders from your builds"
permalink: /2007/11/15/Remove-svn-folders-from-your-builds/
date: 11/15/2007 10:31:32 PM
updated: 11/15/2007 10:31:32 PM
disqusIdentifier: 20071115103132
tags: ["innoveo solutions", "continuous integration"]
alias:
 - /post/Remove-svn-folders-from-your-builds.aspx/index.html
---
When you use [Subversion](http://subversion.tigris.org/) you will have on your file system some .svn folders, normally there are hidden.

I use [Visual Studio 2005 Web Deployment Projects](http://msdn2.microsoft.com/en-us/asp.net/aa336619.aspx) in my build process to build the application then package it.
<!-- more -->

If the .svn folders are hidden, you forget about those folders but with the build and packaging they will be there in your package. So to remove those folders I juste added this line in my MSBuild file:

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">ExcludeFromBuild</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">Include</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">$(SourceWebPhysicalPath)\**\.svn\**\*.*</span>"<span style="color: rgb(0,0,255)"> /></span>
[](http://11011.net/software/vspaste)
