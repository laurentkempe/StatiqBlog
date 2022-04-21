---
title: "Flicker Fix"
permalink: /2006/04/29/Flicker-Fix/
date: 4/29/2006 8:53:00 AM
updated: 4/29/2006 8:53:00 AM
disqusIdentifier: 20060429085300
alias:
 - /post/Flicker-Fix.aspx/index.html
---
If you read my last post "[First 
experience with CSS friendly control adapters beta](http://weblogs.asp.net/lkempe/archive/2006/04/29/444390.aspx)" you might have realized 
that I am referencing in my CSS file a http handler:

background: 
url(../../**PersistantImage.ashx**?theme=Default&file=Rounded.gif)
<!-- more -->

This is a gem coming with the adapters project. It avoids the flickering you 
might have with pure CSS menu using images. This flickering is caused by the 
client browser failing to cache images used in hover styles (CSS). 

[ Currently Playing : Upside Down - Jack Johnson and Friends - 
Sing-A-Longs and Lullabies for the film Curious George (03:29) 
]
