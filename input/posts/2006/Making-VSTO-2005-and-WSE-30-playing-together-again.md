---
title: "Making VSTO 2005 and WSE 3.0 playing together again"
permalink: /2006/08/13/Making-VSTO-2005-and-WSE-30-playing-together-again/
date: 8/13/2006 12:08:00 AM
updated: 5/7/2010 7:45:40 AM
disqusIdentifier: 20060813120800
tags: ["Web Services", "ASP.NET", "VSTO"]
alias:
 - /post/Making-VSTO-2005-and-WSE-30-playing-together-again.aspx/index.html
---
Today I am working on the security of my publishing web services for the next release of Tech Head Brothers Portal. The goal is to be able to post content on the portal using [THBAuhtoring](http://www.codeplex.com/Wiki/View.aspx?ProjectName=THBAuthoring "THBAuthoring") (you might get the source from [Codeplex](http://www.codeplex.com/ "Codeplex")) a Word 2003 solution I developed. I wanted to build my proxy class like so:

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Consolas">


<!-- more -->
<span style="COLOR: teal">PublishServiceWse</span> service = <span style="COLOR: blue">new</span> <span style="COLOR: teal">PublishServiceWse</span>();
</div>


and then I was getting the following error:

<em>{"WSE032: There was an error loading the microsoft.web.services3 configuration section."}</em>

The solution went with a crazy idea: t<strong>he trick is simple give full trust to your .config file.</strong>
