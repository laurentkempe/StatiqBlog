---
title: "BDD web application testing using easyB and Sahi"
permalink: /2010/11/08/BDD-web-application-testing-using-easyB-and-Sahi/
date: 11/8/2010 6:21:00 PM
updated: 11/8/2010 6:21:01 PM
disqusIdentifier: 20101108062100
tags: ["BDD", "Web Application Testing"]
alias:
 - /post/BDD-web-application-testing-using-easyB-and-Sahi.aspx/index.html
---
I already talked about the way we are testing our web application at [Jobping](http://www.jobping.com/) in the following posts “[ASP.NET MVC 2, MSpec and Watin](http://www.laurentkempe.com/post/ASPNET-MVC-2-MSpec-and-Watin.aspx) ” and [”Automated functional tests using Watin and MSpec](http://www.laurentkempe.com/post/Automated-functional-tests-using-Watin-and-MSpec.aspx)”.

The other day I landed on the DZone page “[Automated Browser Testing: What's in Your Toolkit?](http://agile.dzone.com/polls/automated-browser-testing)” In the list of around 10 tools I knew some of them but there were 3 I didn’t knew. So I decided to go on and read about those 3. In this list there were [Sahi](http://sahi.co.in/) which got me with those three sentences:
<!-- more -->

> *   Powerful Recorder works on any Browser
> *   Robust object identification **without brittle XPaths **
> *   **Implicit waits **- even for complex AJAX applications

This is supposed to solve the issue we currently have with Selenium, even if we use Screen Objects to encapsulate our Selenium tests. It is just a pain to work in our highly AJAX application and for sure the XPath are brittle.  

So I decided to try it. I started by watching the video in the “[Get Started](http://sahi.co.in/static/sahi_tutorial.html)” section and in less than 10 minutes I had my first test running.

WOW ! Impressive.

Going further I wanted to know if it could be used with a BDD framework. We currently are using JBehave, but reading about it, it was clear that it would be more effort for me to try it. So I went back to [easyb](http://easyb.org/)  which I tried out a couple month ago.

I used Sahi Java Driver to output some Java code which I used directly in my easyb story.

I was surprised and impressed about the easiness I went through the implementation.

Looking forward for the next steps and where we will end with this.
