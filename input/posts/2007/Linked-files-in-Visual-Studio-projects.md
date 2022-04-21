---
title: "Linked files in Visual Studio projects"
permalink: /2007/11/30/Linked-files-in-Visual-Studio-projects/
date: 11/30/2007 4:10:00 AM
updated: 11/30/2007 4:10:00 AM
disqusIdentifier: 20071130041000
tags: ["Visual Studio", "innoveo solutions"]
alias:
 - /post/Linked-files-in-Visual-Studio-projects.aspx/index.html
---
<div> 

![](http://farm3.static.flickr.com/2222/2073605741_5a6f80845e_o.jpg) Following my post "[Visual Studio and PowerShell Trick](http://weblogs.asp.net/lkempe/archive/2007/11/29/visual-studio-and-powershell-trick.aspx)" I had another little issue, I have one mapping file, mapping.xml, that I want to use on my website and with my data access layer unit tests project.
<!-- more -->

I used to solve this using the **linked file possibility of Visual Studio**. Point the target folder, and click Add Existing item, browse to your source folder, choose the original file, and at the bottom of the dialog you will have the Add button showing a dropdown, you juste then need to use Add As Link.

![](http://farm3.static.flickr.com/2291/2074403176_8a394271f1_o.jpg) 

Now I have the same mapping file used in both locations.
</div>  
 <div> 

And I can run and watch the results of my unit tests happily!

![](http://farm3.static.flickr.com/2376/2074407694_fb539878e6_o.jpg)
</div>
