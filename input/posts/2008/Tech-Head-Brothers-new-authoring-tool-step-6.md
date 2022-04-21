---
title: "Tech Head Brothers new authoring tool, step 6"
permalink: /2008/02/27/Tech-Head-Brothers-new-authoring-tool-step-6/
date: 2/27/2008 5:54:51 AM
updated: 2/27/2008 5:54:51 AM
disqusIdentifier: 20080227055451
tags: ["Tech Head Brothers", "Visual Studio", "VSTO", "Office 2007"]
alias:
 - /post/Tech-Head-Brothers-new-authoring-tool-step-6.aspx/index.html
---
Another day another step!

Tonight I wanted to work on the list and on the optimization a bit because I did use to much time the parsing of the whole WordML document.
<!-- more -->

I also tried the deployment through clickonce and two of my fellow tested it. Thanks to [Sébastien Pertus](http://www.dotmim.com/blogs/mim/) and [Grégory Renard (alias Rédo)](http://blogs.codes-sources.com/redo) for the quick test.

Here is the result; ***left window Word 2007*** and ***right window Internet Explorer*** after clicking on the preview (Pré-visualisation):

![](http://farm3.static.flickr.com/2408/2294699090_b540092455_o.jpg) 

As you can see the rendering is quite the same with pictures, bullet list with bold and link! and subection titles. 

The preview is done very fast now and works using LINQ to XML to parse the WordML and doing a projection to [Tech Head Brothers](http://www.techheadbrothers.com/) well formed XML, then a XSLT is run to produce the HTML. 

And is fast again now after the optimization. 

So the next steps will be:  

1.  <strike>Projection of hyperlink</strike> <strike>Projection of picture</strike> Projection of numbered list and <strike>bullet list</strike>  Fixing bugs found by Sébastien and Rédo Picture caption Giving the possibility to insert source code (**but got an idea how to do it now**). Projection of the source code  Adding the possibility to the author to post the article directly from Word to [Tech Head Brothers](http://www.techheadbrothers.com/) web site using secured web services made out of WCF 

See you in the next post about my trip to a better community publishing tool for my portal [Tech Head Brothers](http://www.techheadbrothers.com/) using Word 2007, WordML, C#, LINQ, VSTO 3...
