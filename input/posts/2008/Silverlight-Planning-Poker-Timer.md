---
title: "Silverlight Planning Poker Timer"
permalink: /2008/06/15/Silverlight-Planning-Poker-Timer/
date: 6/15/2008 7:07:47 PM
updated: 6/15/2008 7:07:47 PM
disqusIdentifier: 20080615070747
tags: ["Silverlight", "Scrum"]
alias:
 - /post/Silverlight-Planning-Poker-Timer.aspx/index.html
---
<div class="wlWriterHeaderFooter" style="float:right; margin:0px; padding:0px 0px 4px 8px;"><script type="text/javascript">digg_url = "http://weblogs.asp.net/lkempe/archive/2008/06/15/silverlight-planning-poker-timer.aspx";digg_title = "Silverlight Planning Poker Timer";digg_bgcolor = "#FFFFFF";digg_skin = "normal";</script><script src="http://digg.com/tools/diggthis.js" type="text/javascript"></script><script type="text/javascript">digg_url = undefined;digg_title = undefined;digg_bgcolor = undefined;digg_skin = undefined;</script></div>

The other day I was searching for a small project to [get started with Silverlight 2 development](http://weblogs.asp.net/lkempe/archive/2008/06/13/silverlight-2-beta-2-unable-to-start-debugging.aspx). 
<!-- more -->

In my today’s works at [Innoveo Solutions](http://www.innoveo.com/) I am spending quite some time evangelizing about [Scrum](http://en.wikipedia.org/wiki/Scrum_(development)) framework. I found some time ago the [Planning Poker Timer](http://aslakhellesoy.com/planning_poker_timer/index.html) by [Aslak Hellesøy](http://blog.aslakhellesoy.com/). That’s definitely looks like a great project to implement with Silverlight 2.

So my goal is as the following:

1.  A full re-implementation of the Planning Poker Timer using Silverlight 2 and C#
2.  *   You can set a [different time and precision](http://aslakhellesoy.com/?precision=10&time=180) in the URL. Use a precision of 10 to avoid distracting people
    *   Rings a bell and turns the screen red (unless you specify other colours) when time is up
    *   Make the colours [start at blue, go red at 4 seconds and black at 0 seconds](http://aslakhellesoy.com/?precision=1&time=8&initialColour=0000ff&colours[4]=ff0000&colours[0]=000000) or use your imagination to let people know time is running out
    *   You can make the timer [restart automatically](http://aslakhellesoy.com/?precision=1&time=5&restartAt=-2) at a certain time. Hitting the spacebar will also restart the timer (and make all of this text go away)
    *   Use it to keep folks focussed on time in other situations - like [lightning talks](http://en.wikipedia.org/wiki/Lightning_Talk), where you can use [these settings](http://aslakhellesoy.com/?precision=10&time=600&restartAt=-120&colours[120]=ff0000&colours[0]=000000)
    *   Make the text bigger to fill the whole screen. This is CMD+ or CTRL+ in most browsers - or via the menu if you're using IE    Turn the Silverlight Planning Poker Timer to a Vista Gadget  

So I fired up [Microsoft Expression Blend 2.5 June 2008 Preview](http://www.microsoft.com/downloads/details.aspx?FamilyID=32a3e916-e681-4955-bc9f-cfba49273c7c) and created a first very simple project with two TextBlock and used it in Visual Studio 2008.

After less than one hour I was able to:

*   Set a [different time and precision](http://aslakhellesoy.com/?precision=10&time=180) in the URL. Use a precision of 10 to avoid distracting people
*   Rings a bell and turns the screen red (unless you specify other colours) when time is up
*   Go full screen  

It is really impressive at which speed you can start to handle a project in Silverlight 2 with some .NET backgound. It was really clever from Microsoft to give the same development environment on the client that you have on the server side.
