---
title: "Tech Head Brothers new authoring tool, step 3"
permalink: /2008/02/17/Tech-Head-Brothers-new-authoring-tool-step-3/
date: 2/17/2008 4:40:32 PM
updated: 2/17/2008 4:40:32 PM
disqusIdentifier: 20080217044032
tags: ["Tech Head Brothers", "VSTO", ".NET Framework 3.5", "Office 2007"]
alias:
 - /post/Tech-Head-Brothers-new-authoring-tool-step-3.aspx/index.html
---
I started working on the GUI defining the Ribbon.

Now it was time to work on the mapping of the WordML to my XML Schema.
<!-- more -->

Here is a first result of a Word document in which I extract the different section, title and paragraph that are injected in a well formed XML document using my XML Schema.

![](http://farm3.static.flickr.com/2129/2270982074_95feb4a007_o_d.jpg) 

Not bad after only some hours of work!

It is leveraging LINQ to XML to parse the WordML and build my targeted XML Document.

<u>Aurel, Redo</u>: What do you think?
