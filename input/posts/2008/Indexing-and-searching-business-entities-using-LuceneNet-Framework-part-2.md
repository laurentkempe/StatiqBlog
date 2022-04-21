---
title: "Indexing and searching business entities using Lucene.Net Framework, part 2"
permalink: /2008/03/07/Indexing-and-searching-business-entities-using-LuceneNet-Framework-part-2/
date: 3/7/2008 6:16:32 PM
updated: 3/7/2008 6:16:32 PM
disqusIdentifier: 20080307061632
tags: ["Tools", "ASP.NET 2.0", "Interoperability", "innoveo solutions", "C#", "Architecture", "Generics", "Reflection"]
alias:
 - /post/Indexing-and-searching-business-entities-using-LuceneNet-Framework-part-2.aspx/index.html
---
*![](http://farm3.static.flickr.com/2032/2105387404_33d2e9ed92_o.gif) *

*Conception using generics and reflection of a search engine to index and search content in your business entities without being intrusive.*
<!-- more -->

Part 1 is available following this link [Indexing and searching business entities using Lucene.Net Framework, part 1](http://weblogs.asp.net/lkempe/archive/2007/11/16/indexing-and-searching-business-entities-using-lucene-net-framework-part-1.aspx)

### Lucene.Net presentation

[Lucene.Net](http://incubator.apache.org/lucene.net/) is an open source project coming from the Java world currently incubating at the Apache Software Foundation (ASF). It is a source code port on the .NET platform using C#, done class-by-class, API-per-API, of the indexing and searching engine algorithms of Java [Lucene](http://lucene.apache.org/java/docs/index.html).  

Apache Lucene is an efficient indexing and searching engine for text data. However it is not offering integrated support for document like [Office Word](http://wiki.apache.org/lucene-java/LuceneFAQ#head-37523379241b88fd90bcd1de81b74e7ec8843f72) or [PDF](http://wiki.apache.org/lucene-java/LuceneFAQ#head-c45f8b25d786f4e384936fa93ce1137a23b7e422), you need to use extensions able to extract the text content of a document in order to be able index it. This is also mandatory for markup documents like [HTML](http://wiki.apache.org/lucene-java/LuceneFAQ#head-e7d23f91df094d7baeceb46b04d518dc426d7d2e).  

Lucene.Net follows scrupulously the APIs defined in the classes of the original Lucene Java version. The API names as well as the class names are preserved with the intention to follow naming guidelines of the C# language. For example, the method *Hits.length()* of the Java implementation is written *Hits.Length()* in its C# version.  

Like the port of the APIs and the classes in C#, the algorithm of the Java version of Lucene is also ported in the C# version. This means that an index created using the Java version of Lucene is 100% compatible with it C# version, in reading, writing and updating. Therefore two processes, one written in Java and the other in C#, could achieve concurrent searches using the same index.  

You might consult the documentation of the last stable version, version 2.0, [on the following page](http://incubator.apache.org/lucene.net/docs/2.0/Lucene.Net/). To download the last stable version [browse to this page](http://incubator.apache.org/lucene.net/download/). To get more information about Lucene I recommend using the [pages dedicated to the Java version of Lucene](http://lucene.apache.org/java/docs/index.html) which are much more consistent.  

#### Lucene.Net Architecture

 <center>![Lucene.Net Architecture](http://farm3.static.flickr.com/2412/2316420682_b2fe668382_o.jpg)</center> 

The lower layer is the data access layer (Storage). Then, the upper layer is about accessing the index files (data access). This layer is used by the indexing system and the searching system. On top of those we find a layer for searching and a search request parser layer used by the searching part of Lucene.Net. Identically we found a parser layer and a document layer used for the indexation part of Lucene.Net. 

To get more information about Lucene I recommend reading the presentation on [Lucene website](http://lucene.apache.org/java/docs/features.html).  

Now that we got a better view on what is Lucene.Net about we will see in the next part how we will use it to index the properties of our business entities.  

*This post is cross-posted on [innoveo blog](http://blog.innoveo.com/archive.aspx/2008/3/7/indexing-and-searching-business-entities-using-lucene-net-framework-part-2) **and in French on my .NET community portal *[*Tech Head Brothers*](http://www.techheadbrothers.com/Articles.aspx/indexer-rechercher-entites-metier-aide-framework-lucene-net)*.*
