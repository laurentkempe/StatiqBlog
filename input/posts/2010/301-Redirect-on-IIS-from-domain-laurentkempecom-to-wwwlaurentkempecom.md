---
title: "301 Redirect on IIS, from domain laurentkempe.com to www.laurentkempe.com"
permalink: /2010/04/23/301-Redirect-on-IIS-from-domain-laurentkempecom-to-wwwlaurentkempecom/
date: 4/23/2010 5:46:08 PM
updated: 4/23/2010 5:48:09 PM
disqusIdentifier: 20100423054608
tags: ["IIS"]
alias:
 - /post/301-Redirect-on-IIS-from-domain-laurentkempecom-to-wwwlaurentkempecom.aspx/index.html
---
I got an email the other day from [Didier](http://didierbeck.com/), than a message on my facebook page from [Antoine](http://www.facebook.com/aemond) about an issue with my blog [www.laurentkempe.com](http://www.laurentkempe.com) and my .NET portal [www.techheadbrothers.com](http://www.techheadbrothers.com) which were not redirecting correctly from laurentkempe.com and techheadbrothers.com to the ones with www in front.

So I fired up IIS manager and fixed this using the following easy way:
<!-- more -->

1.  Create a web site for the one without the www; e.g. laurentkempe.com, configure the web site content directory to the path of the one you have in [www.laurentkempe.com](http://www.laurentkempe.com) 
2.  Configure the web site to listen on laurentkempe.com, if needed
![4544683393_41bcd8b345_o[1]](/images/4544683393_41bcd8b345_o%5B1%5D.png "4544683393_41bcd8b345_o[1]")         
3.  Then on the Home directory you will have to set the redirection url using ‘When connecting to this resource the content should come from’ set to ‘A redirection to a URL"’.  And check ‘A permanent redirection for this resource’.
![4544689855_4f1bbd5979_o[1]](/images/4544689855_4f1bbd5979_o%5B1%5D.png "4544689855_4f1bbd5979_o[1]")   

You are done! And you can control that using a tool like [HttpWatch Professional](http://www.httpwatch.com/) and for sure from your browser!

![4545330398_37c2b3747b_o[1]](/images/4545330398_37c2b3747b_o%5B1%5D.png "4545330398_37c2b3747b_o[1]")
