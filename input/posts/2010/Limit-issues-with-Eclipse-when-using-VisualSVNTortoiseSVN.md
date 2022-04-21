---
title: "Limit issues with Eclipse when using VisualSVN/TortoiseSVN"
permalink: /2010/01/07/Limit-issues-with-Eclipse-when-using-VisualSVNTortoiseSVN/
date: 1/7/2010 9:56:00 PM
updated: 1/7/2010 9:56:00 PM
disqusIdentifier: 20100107095600
alias:
 - /post/Limit-issues-with-Eclipse-when-using-VisualSVNTortoiseSVN.aspx/index.html
---
In some rare case I have to use Eclipse configured to access our [Innoveo Solutions](http://www.innoveo.com/) svn server. I also for sure have Visual Studio 2008 with VisualSvn installed which install TortoiseSvn.

Today I faced the following crazy issue, from a file browse window opened through Eclipse, I saw that one file was modified so I did from that file browse window a svn revert using TortoiseSvn which ended to a crazy situation in Eclipse. It was totally messed up.
<!-- more -->

So to avoid this, I configured [TortoiseSvn Exclude Paths](http://tortoisesvn.net/docs/nightly/TortoiseSVN_en/ch05s25.html) not to show me this folder and subfolder with icon overlays:

![](/images/4253062219_8a93a90c4d_o1_250CF5EA.png) 

I hope this will save me from those crazy situations!
