---
title: "Unit Tests without leaving the keyboard"
permalink: /2009/10/08/Unit-Tests-without-leaving-the-keyboard/
date: 10/8/2009 4:24:54 PM
updated: 5/7/2010 7:46:47 AM
disqusIdentifier: 20091008042454
tags: ["Visual Studio", "ReSharper", "unit test", "TDD"]
alias:
 - /post/Unit-Tests-without-leaving-the-keyboard.aspx/index.html
---
I like the Roy Osherove blog: [Five Whys](http://5whys.com/), Leadership in software teams.

Follow up on those two posts “[How to measure programmer productivity using TDD Katas](http://feedproxy.google.com/~r/5whys/~3/EJ_zJ9h3pn0/how-to-measure-programmer-productivity-using-tdd-katas.html)” or “[Be Productive and Go Mouseless](http://feedproxy.google.com/~r/5whys/~3/3ugKivEOxAg/be-productive-and-go-mouseless.html)”. I would like to share a little keyboard shortcut which save me quite some time on my daily developments.
<!-- more -->

As you might know I am a fan of ReSharper and when I am doing Test Driven Development I hate having to switch from keyboard to mouse to run the test I just wrote. So searching for ReSharper Keyboard command I found **Resharper_UnitTest_ContextRun** which I assign to the **Ctrl+Shift+<**.

![](http://weblogs.asp.net/blogs/lkempe/3986142655_a489b88d41_o1_58609135.png) 

Now I can run the unit test I just wrote with just a combination of keys which are really near to each other so really easy!

The bonus of this command is that if I have the cursor in one unit test in a TestFixture class then it will run that particular test, but if I have the cursor out of any unit test method it will run all tests of that TextFixture. Very efficient!

Enjoy it!
