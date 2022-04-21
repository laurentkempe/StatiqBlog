---
title: "Tech Head Brothers new authoring tool, step 7"
permalink: /2008/03/12/Tech-Head-Brothers-new-authoring-tool-step-7/
date: 3/12/2008 8:11:45 AM
updated: 3/12/2008 8:11:45 AM
disqusIdentifier: 20080312081145
tags: ["VSTO", "WCF", "Office 2007"]
alias:
 - /post/Tech-Head-Brothers-new-authoring-tool-step-7.aspx/index.html
---
I finally got time to go on with the new [Tech Head Brothers](http://www.techheadbrothers.com/) publishing tool!

This time I wanted to go on with source code and I reached a quite good status.
<!-- more -->

The usage scenario is as following:

1.  In Visual Studio the author select source code that he wants to publish on his article
2.  The author switch to Word 2007 an click on Source Code button and the code is automatically inserted 

As you can see on the following pictures!

![](http://farm3.static.flickr.com/2088/2327071967_28780de9c0_o.jpg)

![](http://farm3.static.flickr.com/2086/2327067069_1e37deaa06_o.jpg) 

 Behind the scene:

1.  I get the code from the clipboard, 
2.  Build up a XML document with one tag representing the source code in html and inline css and another of pure text
3.  Insert a CustomXML part into the Word 2007 document
4.  Insert a PlainTextContentControl into the document with read only rights
5.  Uses XML mapping to display the text tag into the PlainTextContentControl 

Great! I look forward to see the projection of the source code to HTML and preview it into the browser!

So the next steps will be:  

1.  <strike>Projection of hyperlink</strike>  <strike>Projection of picture</strike>  Projection of numbered list and <strike>bullet list</strike>  Fixing bugs found by Sébastien and Rédo  <strike>Picture caption</strike>  <strike>Giving the possibility to insert source code </strike> Projection of the source code  Adding the possibility to the author to post the article directly from Word to [Tech Head Brothers](http://www.techheadbrothers.com/) web site using secured web services made out of WCF Adding all validation of the content
