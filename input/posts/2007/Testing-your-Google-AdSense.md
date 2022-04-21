---
title: "Testing your Google AdSense"
permalink: /2007/10/12/Testing-your-Google-AdSense/
date: 10/12/2007 6:43:44 AM
updated: 10/12/2007 6:43:44 AM
disqusIdentifier: 20071012064344
tags: ["Note to self"]
alias:
 - /post/Testing-your-Google-AdSense.aspx/index.html
---
This trick is coming from my friend [Nix](http://blogs.codes-sources.com/nix).

If you are developing a site containing [Google Adsense](http://www.google.com/adsense/) you might know that clicking on your own ad is not allowed.
<!-- more -->

To be able to test and click on your own ad and still follow [Google AdSense Program Policies](https://www.google.com/adsense/support/bin/answer.py?answer=48182&sourceid=aso&subid=ww-ww-et-asui&medium=link), just add following to your pages: 

<span style="color: #0000ff"><</span><span style="color: #800000">script</span> <span style="color: #ff0000">type</span>=<span style="color: #0000ff">"text/javascript"</span><span style="color: #0000ff">></span>**google_adtest** = 'on';<span style="color: #0000ff"></</span><span style="color: #800000">script</span><span style="color: #0000ff">></span> 
 <form id="aspnetForm" name="aspnetForm" action="http://blogs.codes-sources.com/nix/archive/2007/10/03/adsense-comment-viter-de-faire-un-clicks-incorrects-sur-vos-propres-annonces.aspx" method="post"> <div id="page"> <div id="wrapper"> <div id="main"> <div class="narrowcolumn" id="content"> <div class="post"> <div class="post-content">**Don't forget to remove it on your production environment! ;)**</div></div></div></div></div></div></form>
