---
title: "Indexing and searching business entities using Lucene.Net Framework, part 1"
permalink: /2007/11/16/Indexing-and-searching-business-entities-using-LuceneNet-Framework-part-1/
date: 11/16/2007 5:59:02 PM
updated: 5/7/2010 7:46:50 AM
disqusIdentifier: 20071116055902
tags: ["Tools", "ASP.NET 2.0", "Interoperability", "innoveo solutions", "C#", "Architecture", "Generics", "Reflection"]
alias:
 - /post/Indexing-and-searching-business-entities-using-LuceneNet-Framework-part-1.aspx/index.html
---
*Conception using generics and reflection of a search engine to index and search content in your business entities without being intrusive.*  

### [Introduction]()
<!-- more -->

Today, one of the functionality that almost all web sites implements is a method to index content and give it users the possibility to search that content spread into its web pages. It is one of the simplest ways to improve the user experience on your web site.  

Blogs brought categories/tags giving the possibility to label the information. However this advantageous method isn’t always sufficient. It is advisable to then use a real content indexing method.  

In this set of posts I propose to take a look at the indexing and searching method I implemented on the web site of [innoveo solutions](http://www.innoveo.com/), my new company. I hope also to bring soon this system to my web site [Tech Head Brothers](http://www.techheadbrothers.com/).  

Both web sites, innoveo solutions and Tech Head Brothers, were developed using [Domain Driven Design](http://www.dotnetrocks.com/default.aspx?showNum=236). So, we started by defining a domain model with our business entities. In this layer we do not concentrate on technical aspects for example like persistence. On the other hand we do concentrate on the domain we want to address.  

One of the main ideas is to avoid being intrusive in the domain model with any inheritance of technical classes or to link this layer with any technical frameworks.  

To achieve this goal we will use an [O/R mapping tool (Euss)](http://euss.evaluant.com/) for the business entities persistence as well as the [Lucene.Net framework](http://incubator.apache.org/lucene.net/) for the indexing part.  

Following quite some discussions (Thanks [Didier](http://www.didierbeck.com/) ;) in which we asked us if we would better use a service offered by one of the searching big players on the Internet, we finally decided to keep the control of our searching tool.  

Wanting to be independent of any database and services like Full-Text indexing, or from services like Indexing Services, we decided to use Lucene.Net to avoid having to re-implement everything from scratch.  

In the following posts, I will present an introduction of Lucene.Net; we will see the architecture I have chosen for the indexing and searching framework; the implementation details of that framework and finally an example of integration into a data access layer.

*This post is cross-posted on [innoveo blog](http://blog.innoveo.com/archive.aspx/2007/12/12/indexing-and-searching-business-entities-using-lucene-net-framework-part-1) **and in French on my .NET community portal *[*Tech Head Brothers*](http://www.techheadbrothers.com/Articles.aspx/indexer-rechercher-entites-metier-aide-framework-lucene-net)*.*
