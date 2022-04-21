---
title: "Bringing Linq to Euss ORM"
permalink: /2008/05/16/Bringing-Linq-to-Euss-ORM/
date: 5/16/2008 1:53:12 AM
updated: 5/16/2008 1:53:12 AM
disqusIdentifier: 20080516015312
tags: ["Tech Head Brothers", "NDepend", "Euss", "ORM", "Linq"]
alias:
 - /post/Bringing-Linq-to-Euss-ORM.aspx/index.html
---
[Euss stand for Evaluant Universal Storage Services](http://euss.evaluant.com/), it's a great open source (MIT License) Object-Relational mapping framework.

I use Euss on [Tech Head Brothers](http://www.techheadbrothers.com/) portal since a long time now.
<!-- more -->

Four days ago I went to [Sébastien Ros](http://www.dotnetguru2.org/sebastienros/), the architect of Euss, with a first draft implementation of IQueryable<T> for Euss. Almost everything was already implemented but it was missing the IQueryable<T>. Today Sébastien came back to me with a first implementation that I could quickly integrate.

As you can see on the following screenshots, made with [NDepend](http://www.ndepend.com/), I was then able to remove the dependency on Euss from my Service Layer.

Before with dependency from TechHeadBrothers.Portal.Services to Evaluant.Uss

![](http://farm3.static.flickr.com/2051/2495212684_3c820458eb_o.jpg) 

After without dependency from TechHeadBrothers.Portal.Services to Evaluant.Uss

![](http://farm4.static.flickr.com/3229/2495250970_a703e36eef_o.jpg) 

Great new step! 
