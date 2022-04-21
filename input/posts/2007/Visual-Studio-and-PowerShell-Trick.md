---
title: "Visual Studio and PowerShell Trick"
permalink: /2007/11/30/Visual-Studio-and-PowerShell-Trick/
date: 11/30/2007 3:30:45 AM
updated: 11/30/2007 3:30:45 AM
disqusIdentifier: 20071130033045
tags: ["Visual Studio", "innoveo solutions", "white"]
alias:
 - /post/Visual-Studio-and-PowerShell-Trick.aspx/index.html
---
This end of afternoon I worked on the mapping part of my new project for [innoveo solutions](http://www.innoveo.com/), a blog engine.

Till now I was using the XML Persistence Engine of [Euss](http://euss.evaluant.com/) but it was time to go to a real mapping and a database.
<!-- more -->

![](http://farm3.static.flickr.com/2130/2073540103_dcff049d11_o.jpg) 

I ended up with some .bat file in my solution. 

For example *diffMapping.bat* uses [WinMerge](http://winmerge.org/) to compare the mapping I currently use to one that I just generated out of my Domain assembly.

Then I wanted to be able to **run this .bat script from the IDE**, without having it in Visual Studio 2005 External Tools menu (not tried with 2008 currently). So I tried the right click on the .bat Open With, then gave the path of cmd.exe. That's was not working, it just opened a cmd prompt.

Then I **pointed Open With to PowerShell exe and it works**!!! **It will run you .bat file from Solution Explorer**. You can even define it as the default program for .bat and double clicking on a .bat file in Solution Explorer will run it. 

Nice no ?!
