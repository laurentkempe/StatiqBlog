---
title: "ModalPopup and Silverlight"
permalink: /2007/06/16/ModalPopup-and-Silverlight/
date: 6/16/2007 6:27:14 AM
updated: 6/16/2007 6:27:14 AM
disqusIdentifier: 20070616062714
tags: ["ASP.NET AJAX", "Silverlight", "ASP.NET AJAX Control Toolkit "]
alias:
 - /post/ModalPopup-and-Silverlight.aspx/index.html
---
If you place on a web page a [ModalPopup](http://ajax.asp.net/ajaxtoolkit/ModalPopup/ModalPopup.aspx) and a [Silverlight](http://silverlight.net/) control, you might end up with the ModalPopup hided by the Silverlight control.

To avoid this at the time you call Sys.Silverlight.createObjectEx you need to give the following property to true:
<!-- more -->

isWindowless:'true',       // Determines whether to display control in Windowless mode. 

And then everything works again and you see your ModalPopup. 
