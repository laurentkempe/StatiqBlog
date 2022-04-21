---
title: "Tech Head Brothers new authoring tool, step 8"
permalink: /2008/08/09/Tech-Head-Brothers-new-authoring-tool-step-8/
date: 8/9/2008 6:26:19 AM
updated: 8/9/2008 6:26:19 AM
disqusIdentifier: 20080809062619
tags: ["Tech Head Brothers", "VSTO", "WCF", "Office 2007"]
alias:
 - /post/Tech-Head-Brothers-new-authoring-tool-step-8.aspx/index.html
---
For three night now I focused on one scenario for the Tech Head Brothers authoring tool:

*   As an author I want to be able to copy source code from Visual Studio and insert it into the authored Word document
<!-- more -->
*   As an author I want to preview a document with source code inserted in Internet Explorer  

This might not look really difficult at first, but I had some difficulties to fulfill the requirements of those two user story.

The first one was almost already implemented [in step 7](http://weblogs.asp.net/lkempe/archive/2008/03/12/tech-head-brothers-new-authoring-tool-step-7.aspx), using CustomXML capabilities of WordML.

![](http://farm4.static.flickr.com/3228/2745205622_92d3d8e762_o.png)The biggest issue I had was to figure out why when I was inserting CustomXML, the WordML format of the hyperlink was changing. It took me the most time to realize that inserting CustomXML was not the issue. The issue was proofing. Somehow inserting CustomXML was activating the auto proofing and then I got some error like on the following picture. The red underlined Brothers which is a link was the issue. In fact this simple thing change the WordML representation of the hyperlink.

As I had something working I had to find a way other than adding a second way to parse Hyperlinks. I came to the solution of disabling proofing then saving the document before making my projection to Tech Head Brothers XML. Then for sure re-enabling the proofing. Nice and easy!

The next issue was to be able at the time of parsing the WordML to identify that source code was at at the current position in the WordML. I turned and turned the problem, than asked some clever WordML guys (Julien, Pierre) and some generally good advisor (Mitsu, Cyril). Nothing! I finally came to a solution tonight:

1.  Getting the Id of the PlainTextContentControl from the WordML
2.  Searching for this Control in the Controls collection
3.  Finally using the plainTextContentControl.XMLMapping.CustomXMLPart.XML property I had access to my CustomXML  

I couldn’t find any way to do that parsing the WordML and honestly tonight I don’t know how it is possible to go from the WordML to the CustomXML.

The result is here!

**Copying source code from Visual Studio**

![](http://farm4.static.flickr.com/3089/2744413383_26bf4a4025_o.png) 

**Inserting it in Word 2007**

![](http://farm4.static.flickr.com/3001/2744414779_d4d7af5829_o.png) 

**Final result in Internet Explorer**

![](http://farm4.static.flickr.com/3043/2744419063_c55860a60c_o.png)  

There is still a little bug with the spacing that will solved tomorrow or later!

Now the next steps will be: 

1.  <strike>Projection of hyperlink</strike>
2.  <strike>Projection of picture</strike>
3.  Projection of numbered list and <strike>bullet list</strike>
4.  <strike>Fixing bugs found by Sébastien and Rédo</strike> 
5.  <strike>Picture caption</strike>
6.  <strike>Giving the possibility to insert source code </strike>
7.  <strike>Projection of the source code</strike> 
8.  Adding the possibility to the author to post the article directly from Word to [Tech Head Brothers](http://www.techheadbrothers.com/) web site using secured web services made out of WCF 
9.  Adding all validation of the content
