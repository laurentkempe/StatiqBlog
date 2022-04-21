---
title: "New milestone reached in the development of the authoring tool for Tech Head Brothers French portal"
permalink: /2004/08/24/New-milestone-reached-in-the-development-of-the-authoring-tool-for-Tech-Head-Brothers-French-portal/
date: 8/24/2004 6:41:00 AM
updated: 8/24/2004 6:41:00 AM
disqusIdentifier: 20040824064100
tags: ["Tech Head Brothers", ".NET Development"]
alias:
 - /post/New-milestone-reached-in-the-development-of-the-authoring-tool-for-Tech-Head-Brothers-French-portal.aspx/index.html
---
In this version we now have:

*   a toolbar hosting Preview, Zip, Post plugin
<!-- more -->
*   a dropdown connecting to a web service at first click to get back the articles categories, used before posting
*   a web service to post the article using DIME


Internal changes:

*   everything is configured in a config file now
*   XML Schema is associated with Word at runtime, read from config file
*   All source code colorization is done through configured properties


Here is a picture of Word 2003 with the toolbar:

![](http://perso.wanadoo.fr/laurent.kempe/images/thbpublisher.png)

I am still facing an issue. I am able to read configuration files from the assembly, but when I want to deserialize one part of my configuration file I get an exception saying: "There is an error in XML document" and the inner exception is "Security error". It seems to be a French problem cause I could find someone having the same issue in the newsgroup, [here](http://groups.google.com/groups?q=vsto+deserialize&hl=en&lr=&ie=UTF-8&selm=e1gzbqcrDHA.2588%40tk2msftngp13.phx.gbl&rnum=1). I emailed [Peter Torr](http://blogs.msdn.com/ptorr) that replied to this newsgroup message, I hope ot get an answer and a solution ;)
