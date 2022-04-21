---
title: "PDF generation"
permalink: /2003/08/08/PDF-generation/
date: 8/8/2003 4:27:00 AM
updated: 8/8/2003 4:27:00 AM
disqusIdentifier: 20030808042700
tags: ["Tech Head Brothers"]
alias:
 - /post/PDF-generation.aspx/index.html
---
New features on the new version of my website. I worked out a little more the pdf document generator using [nFOP](http://nfop.sourceforge.net/). Now the pictures that are displayed on the web pages are also displayed on the pdf version of the different articles. I had to work also on the XSLT file transforming my XML articles to XSL-FO.

I still have a stange bug with the link in the pdf. The mouse pointer is changed to show that there is a link but at the top of the text. As much as the text is at the bottom of the document as much as the link is at the top of the text. Anyone is using [nFOP](http://nfop.sourceforge.net/)?
<!-- more -->

**Update**  
You might check a sample rendering [here](http://techheadbrothers.europe.webmatrixhosting.net/DesktopModules/PrintArticle.aspx?AId=27&Render=pdf). The article is in french, but you'll get the idea.
