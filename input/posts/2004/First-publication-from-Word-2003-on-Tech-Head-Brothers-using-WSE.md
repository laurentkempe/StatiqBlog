---
title: "First publication from Word 2003 on Tech Head Brothers using WSE"
permalink: /2004/12/09/First-publication-from-Word-2003-on-Tech-Head-Brothers-using-WSE/
date: 12/9/2004 7:15:00 AM
updated: 5/7/2010 7:46:46 AM
disqusIdentifier: 20041209071500
tags: [".NET Development"]
alias:
 - /post/First-publication-from-Word-2003-on-Tech-Head-Brothers-using-WSE.aspx/index.html
---
<P>This morning at 1:30 AM, I reached a new milestone in the development of the publishing tool I am developing for Tech Head Brothers web site. I was able to publish content directly from Word 2003 using a Web Service secured by WS-Policy and a X509 certificate.</P>
<P>Yesterday, finally, I was able to create a X509 certificate working correctly with WSE SP2. After lots and lots of trials using makecert without any success, I tried openssl under cygwin. And guess what, the certificate was working correctly. That was the main issue I had since a while.</P>
<P>Now I am searching the way to install the public part of the certificate during the setup of the application. Any idea ?</P>
