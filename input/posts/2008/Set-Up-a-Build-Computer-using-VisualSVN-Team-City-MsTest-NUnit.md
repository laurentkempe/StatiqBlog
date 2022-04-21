---
title: "Set Up a Build Computer using VisualSVN, Team City, MsTest, NUnit"
permalink: /2008/03/21/Set-Up-a-Build-Computer-using-VisualSVN-Team-City-MsTest-NUnit/
date: 3/21/2008 8:55:32 AM
updated: 3/21/2008 8:55:32 AM
disqusIdentifier: 20080321085532
tags: ["Tech Head Brothers", "innoveo solutions", "continuous integration", "Team City", "unit test"]
alias:
 - /post/Set-Up-a-Build-Computer-using-VisualSVN-Team-City-MsTest-NUnit.aspx/index.html
---
When I started to work on the new version of [Tech Head Brothers](http://www.techheadbrothers.com/) I decided to use the new version of the unit testing framework from Microsoft. Before making this decision I read that it was much better and faster in several places. As I also wanted to get code coverage I thought it was a good idea.

I also had to setup a version control system and decided to use subversion with the facility of the free [VisualSVN Server](http://www.visualsvn.com/server/). Very simple way to setup in less than 10 min (including download time) a subversion repository on a Windows server. I highly recommend it ! I am also now, for some months, using [VisualSVN](http://www.visualsvn.com/features.html) that provides a seamless integration between subversion and Visual Studio 2005 and 2008. VisualSVN Limited offered a license of VisualSVN to me and my brother. Thanks a lot, really appreciated! And now at [innoveo solutions](http://www.innoveo.com/), my company, we are also using it.
<!-- more -->

Then came the time to have a continuous integration server. After having [CC.NET](http://ccnet.thoughtworks.com/) installed and used at [innoveo solutions](http://www.innoveo.com/) I decided to give a try to [JetBrains Team City](http://www.jetbrains.com/teamcity/index.html) for my personal use  on [Tech Head Brothers](http://www.techheadbrothers.com/) development. The installation and configuration with subversion was straight and achieved in less than two hours. So I had a version control system and continuous integration process setup in less than 2 hours and a half. Isn't that incredible?

But as you can see on the following picture I had and still had an issue!

![](http://farm4.static.flickr.com/3109/2347957993_dae77fb72d_o.jpg) 

My unit tests aren't running at all! And you know what? What I feared the most was actually true!

As you can read it on this [msdn](http://msdn2.microsoft.com/) article: [How to: Set Up a Build Computer](http://msdn2.microsoft.com/en-us/library/ms181712.aspx) 

> **Important Note:**
> 
> In order to run tests during the build, Test Edition must be installed on the build computer. In order to run unit testing, code coverage, or code analysis, Visual Studio Team System Development Edition must be installed on the build computer.

Or on this forum post "[Strange MSTest.exe problems with Build Server 2008 RTM](http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=2694058&SiteID=1)" 

> We are indeed hoping to address this issue in a future release, but for now running unit tests with Team Build does require VS Team Suite or VS Team Tester on the build machine.

So no what I need to do as a next step is to convert my Visual Studio unit tests to a framework, like [NUnit](http://www.nunit.org/), that doesn't needs a development environment installed on a build server. What a waste of time! 

At [innoveo](http://www.innoveo.com/) we are successfully using [NUnit](http://www.nunit.org) and [Rhino Mocks](http://www.ayende.com/default.aspx) with [Team City](http://www.jetbrains.com/teamcity/index.html)!
