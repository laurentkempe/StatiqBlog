---
title: "Improved my development workflow"
permalink: /2012/01/20/Improved-development-workflow/
date: 1/20/2012 12:31:06 AM
disqusIdentifier: 20120120123106
coverImage: https://farm6.staticflickr.com/5176/5519564098_94ba4c29b3_b.jpg
coverSize: partial
thumbnailImage: https://farm6.staticflickr.com/5176/5519564098_94ba4c29b3_q.jpg
coverCaption: "Fleur d'hibiscus à la villa cannelle, Le Diamant, Martinique"
tags: ["DVCS", "TDD", "BDD"]
---
<!-- [![Fleur d'hibiscus à la villa cannelle](http://farm6.staticflickr.com/5176/5519564098_94ba4c29b3_m.jpg)](http://www.flickr.com/photos/laurentkempe/5519564098/ "Fleur d'hibiscus à la villa cannelle by Laurent Kempé, on Flickr") -->
For some time now I slightly modified my development workflow and I have seen a great improvement in my developers life:

*   it started by using Git Svn in front of our central Svn   
<!-- more -->

*   then I introduced [NCrunch](http://www.ncrunch.net/) in my TDD/BDD   

> NCrunch is an automated parallel continuous testing tool for Visual Studio .NET. It intelligently takes responsibility for running automated tests so that you don't have to, and it gives you a huge amount of useful information about your tests (such as code coverage) inline in your IDE while you work.

I am a fan of [Test Driven Development](http://en.wikipedia.org/wiki/Test-driven_development) and [Behavior Driven Development](http://en.wikipedia.org/wiki/Behavior_Driven_Development), I think there are great tools in a developer toolset in quite some circumstances.

I am also a great fan of [JetBrains ReSharper](http://www.jetbrains.com/resharper/) which I use every development day and I couldn’t work as efficiently without it. But…

So what’s the point? [NCrunch](http://www.ncrunch.net/) propose only an automated parallel continuous run of your test. Big deal, it saves you only some keystrokes! Yeah I believed that too, but the difference was quite impacting.

I don’t need to think anymore about running the tests I just run through my TDD thinking only of my tests and code, no distraction just seeing the result of my changes directly aligned with my code.

What I really liked about [NCrunch](http://www.ncrunch.net/) is:

*   the possibility to run only a part of the tests 
*   the feedback given in the code window 
*   let me focus on the code 
*   the risk/progress bar 
*   the impact it had on my productivity   

I really encourage you to try [NCrunch](http://www.ncrunch.net/) which is free during the beta period.

> Supported testing frameworks are:
> 
> *   NUnit
> *   MS Test
> *   Xunit
> *   MbUnit
> *   MSpec
> 
> The Xunit and MbUnit test runners are provided with thanks to [The Gallio Project](http://www.gallio.org/).
> 
> Supported languages and .NET versions are:
> 
> *   C#
> *   VB.NET
> *   F#
> *   .NET Framework v2.0 and above
> *   Visual Studio 2008
> *   Visual Studio 2010
> *   Visual Studio 11 Developer Preview
