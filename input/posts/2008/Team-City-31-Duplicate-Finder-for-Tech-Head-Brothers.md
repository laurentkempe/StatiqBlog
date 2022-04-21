---
title: "Team City 3.1 Duplicate Finder for Tech Head Brothers"
permalink: /2008/04/01/Team-City-31-Duplicate-Finder-for-Tech-Head-Brothers/
date: 4/1/2008 6:04:49 AM
updated: 4/1/2008 6:04:49 AM
disqusIdentifier: 20080401060449
tags: ["Tech Head Brothers", "Team City"]
alias:
 - /post/Team-City-31-Duplicate-Finder-for-Tech-Head-Brothers.aspx/index.html
---
For the development of the next version of [Tech Head Brothers](http://www.techheadbrothers.com/) I wanted to have a continuous integration server running for our small team. I used for that [Team City 3](http://www.jetbrains.com/teamcity) from [JetBrains](http://www.jetbrains.com/). 

Continuous integration is very nice and works very good. Then I discovered another nice feature directly built in Team City: [Duplicate Finder](http://www.jetbrains.net/confluence/display/TCD3/Duplicates+Finder+%28.NET%29). Ok you might use other tools to do that, but it is there so just use it in two clicks.
<!-- more -->

![](http://farm3.static.flickr.com/2136/2378468328_7fd59d264c_o.jpg)

As the name tells it, the aim is to find duplicates of code in your solution.

We use it daily as a kind of "nightly build" to get a morning email about the possible code smell in our code.

 ![](http://farm4.static.flickr.com/3230/2377621109_ebd7f8e897_o.jpg) 

And this is the way it shows up the duplicates found.

![](http://farm3.static.flickr.com/2395/2377642227_56f8283142_o.jpg) 

I can see here that in VideoUrlRewrite and ArticleUrlRewrite classes I have so work to do :-)

Isn't that nice?
