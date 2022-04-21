---
title: "Integration of VisualSVN (TortoiseSVN) and JIRA"
permalink: /2008/03/27/Integration-of-VisualSVN-(TortoiseSVN)-and-JIRA/
date: 3/27/2008 1:08:13 AM
updated: 3/27/2008 1:08:13 AM
disqusIdentifier: 20080327010813
tags: ["Tools", "innoveo solutions"]
alias:
 - /post/Integration-of-VisualSVN-(TortoiseSVN)-and-JIRA.aspx/index.html
---
The aim is to be able to associate a bug/feature described into [JIRA](http://www.atlassian.com/software/jira/) to source code changes made into the [Subversion](http://subversion.tigris.org/) repository.

First of all you need to install the [JIRA Subversion Plugin](http://confluence.atlassian.com/display/JIRAEXT/JIRA+Subversion+Plugin) on you JIRA server.
<!-- more -->

Then you need to have [VisualSVN](http://www.visualsvn.com/) installed and configured with your project checkout and opened in Visual Studio. You might do the same directly with [TortoiseSVN](http://tortoisesvn.tigris.org/).

Right click on your solution in solution explorer and choose [VisualSVN](http://www.visualsvn.com/) /properties:

![](http://farm3.static.flickr.com/2366/2363492405_f7c8bc7865_o.jpg) 

Add then two properties [bugtraq:url and bugtraq:message](http://tortoisesvn.net/issuetracker_integration) as here:

![](http://farm4.static.flickr.com/3187/2363498947_b40658fdff_o.jpg) 

Now when you commit your code to Subversion using [VisualSVN](http://www.visualsvn.com/) or [TortoiseSVN](http://tortoisesvn.tigris.org/) you will get access to the textbox Bug-ID/Issue-Nr:

![](http://farm3.static.flickr.com/2324/2363510725_b66cb3bd27_o.jpg) 

And in JIRA you will see linked to your bug/feature the Subversion commits associated to it:

![](http://farm3.static.flickr.com/2275/2364353160_e111f553c7_o.jpg) 

A very nice feature to have in a development process!
