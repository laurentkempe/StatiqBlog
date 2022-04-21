---
title: "O/R frameworks"
permalink: /2004/08/15/OR-frameworks/
date: 8/15/2004 8:04:00 AM
updated: 8/15/2004 8:04:00 AM
disqusIdentifier: 20040815080400
tags: [".NET Development"]
alias:
 - /post/OR-frameworks.aspx/index.html
---
This evening, I mean morning, it is already almost 1 AM, I read the [article](http://www.theserverside.net/articles/showarticle.tss?id=NHibernate) about [NHibernate](http://nhibernate.sourceforge.net/) from Justin Gehtland on [TheServerSide.NET](http://www.theserverside.net). Btw it is a good introduction. I am playing now for some time with O/R frameworks. And i must say that I appreciate tools like [NHibernate](http://nhibernate.sourceforge.net/) but also tools like [Data Tier Modeler](http://www.evaluant.com/en/solutions/dtm/default.aspx). This tool is great to use for new projects, but what about projects that already have a database? In this case you might use tools like NHibernate. What I really like in DTM is that you use a UML tool to model your domain. Then the tool consume this model and generate all the plumbing needed to store your objects states. So you really deal with your domain objects and do not need to create mapping files or things out of your domain. There is another project that Ikeep an eye on is [Neo](http://neo.codehaus.org/), it is really similar to DTM. If you look at the ppt presentations of both you will see the idea are the same.
