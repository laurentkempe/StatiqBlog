---
title: "Exchange 2003 almost working"
permalink: /2004/04/27/Exchange-2003-almost-working/
date: 4/27/2004 6:43:00 AM
updated: 4/27/2004 6:43:00 AM
disqusIdentifier: 20040427064300
tags: ["Infrastructure"]
alias:
 - /post/Exchange-2003-almost-working.aspx/index.html
---
I continued the installation of Exchange 2003 and I am almost done with it. Currently I am able to send email, to get email from my different internet pop emails using [Pullmail](http://www.swsoft.co.uk/index.asp?page=freesoftware). But I can't get directly emails for my domain. As I have a dynamic ip i used [DynDns](http://www.dyndns.org/) and changed the MX record of my DNS provider to point to a CNAME with the domain name used in DynDns. It seems that the problem is coming from there, but I could not fix it, and I don't have any idea.

I want to thanks [Benoit Hamet](http://www.hametbenoit.info/) for his wonderful support. I used also a really good article: [Hosting Your Own SMTP Mail Using Exchange 2000](http://www.msexchange.org/tutorials/MF002.html) by [Mark Fugatt](http://www.msexchange.org/Mark_Fugatt/). Thanks Mark.
