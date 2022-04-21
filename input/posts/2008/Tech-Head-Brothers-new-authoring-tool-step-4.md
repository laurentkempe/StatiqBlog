---
title: "Tech Head Brothers new authoring tool, step 4"
permalink: /2008/02/19/Tech-Head-Brothers-new-authoring-tool-step-4/
date: 2/19/2008 6:22:40 AM
updated: 2/19/2008 6:22:40 AM
disqusIdentifier: 20080219062240
tags: ["Tech Head Brothers", "Visual Studio", "VSTO", ".NET Framework 3.5", "Office 2007"]
alias:
 - /post/Tech-Head-Brothers-new-authoring-tool-step-4.aspx/index.html
---
I took some time this evening to work a bit on the new version of the publishing tool for [Tech Head Brothers](http://www.techheadbrothers.com/).

Tonight goal was to be able to have a first preview of the HTML rendering in offline mode.
<!-- more -->

In this scenario an author launch the [Tech Head Brothers](http://www.techheadbrothers.com/) Word 2007 template, write the content of his article, then click on preview ("pré-visualisation" on the picture) ribbon button. This is a full offline scenario.

Behind the scene, the C# code associated with the Word 2007 template will do a projection of WordML to my own XML document format using a predefined XML Schema. This projection is done using LINQ to XML. Then it will make a XSLT transformation on that projection result, save the result as a html file and launch Internet Explorer to see the result, as you can see on the picture:

![](http://farm3.static.flickr.com/2299/2275499640_bbf637aa2f_o.jpg) 

I am still missing some projection code for hyperlink, list, pictures and source code. But it is a very good start after 3-4 evening of work.

So the next steps will be:

1.  Projection of hyperlink
2.  Projection of picture
3.  Projection of numbered list and bullet list
4.  Giving the possibility to insert source code
5.  Projection of the source code
6.  Adding the possibility to the author to post the article directly from Word to [Tech Head Brothers](http://www.techheadbrothers.com/) web site using secured web services made out of WCF 

I really like LINQ (hidden message to a friend at MS ;)
