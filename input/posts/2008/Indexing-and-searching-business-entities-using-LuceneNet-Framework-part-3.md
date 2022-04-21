---
title: "Indexing and searching business entities using Lucene.Net Framework, part 3"
permalink: /2008/03/07/Indexing-and-searching-business-entities-using-LuceneNet-Framework-part-3/
date: 3/7/2008 7:59:25 PM
updated: 3/7/2008 7:59:25 PM
disqusIdentifier: 20080307075925
tags: ["Tools", "ASP.NET 2.0", "Interoperability", "innoveo solutions", "C#", "Architecture", "Generics", "Reflection"]
alias:
 - /post/Indexing-and-searching-business-entities-using-LuceneNet-Framework-part-3.aspx/index.html
---
*![](http://farm3.static.flickr.com/2032/2105387404_33d2e9ed92_o.gif) *

*Conception using generics and reflection of a search engine to index and search content in your business entities without being intrusive.*
<!-- more -->

Part 1 and 2 are available following those links

1.  [Indexing and searching business entities using Lucene.Net Framework, part 1](http://weblogs.asp.net/lkempe/archive/2007/11/16/indexing-and-searching-business-entities-using-lucene-net-framework-part-1.aspx)  [Indexing and searching business entities using Lucene.Net Framework, part 2](http://weblogs.asp.net/lkempe/archive/2008/03/07/indexing-and-searching-business-entities-using-lucene-net-framework-part-2.aspx)  

### [Solution’s architecture]()

The main idea is to be able to define the business entity’s properties that must be indexed when this one is saved or updated in the chosen persistence system.

With the goal to be the less intrusive possible in our model we come fast to the idea that we need to extend our business entities with meta-data. The issue then is that at runtime it is needed to know which meta-data needs to searched in the entity in order to be able to index the content of the decorated property. 

As one of the goal is to have a Framework which manage the indexation and the searching of whatever business entity, we might have wrote a simple class inheriting from **System.Attribute** in an assembly separated from our domain. That would have the drawback of behind much intrusive in our domain. Another solution was needed.

As we have seen the developed Framework needs to know the meta-data, giving it the opportunity to index the content of the property at runtime. This means that at development time it is absolutely possible to generalize this information by using the generics of the .NET Framework 2. As we are talking about meta-data the only imposed thing is that our class inherits from **System.Attribute**.

The choice was made then to define a utility class in the domain assembly inheriting from **System.Attribute** which will serve us as a decorator of the entity’s properties needing to be indexed.

On the following picture you can see an example of the domain for an application to which we have added our attribute ***SearchableAttribute*** used to decorate the Post and Page classes:

![](http://farm3.static.flickr.com/2178/2316520178_1bd4bce729_o_d.jpg) 

The Visual Studio solution is organized as a Domain Driven Development solution:

![](http://farm3.static.flickr.com/2334/2316520202_3f74d3c57a_o_d.jpg) 

We have so defined the new attribute ***SearchableAttribute*** in the assembly *innoveo.Blog.Domain*.

Here is the description of the organization of our solution:

*   **innoveo.Blog.DAL**: Data access layer using [Euss](http://euss.evaluant.com/) OR/M mapping tool  **innoveo.Blog.Domain**: Assembly containing our domain business entities  **innoveo.Blog.Services**: Layer exposing the different business services  **innoveo.Blog.Web**: Web presentation & web services layer  **Blog**: The web application  

Here it is for our solution that will use our business entities indexing Framework. Let’s have a closer look now at the Framework itself!

#### [Indexing Framework]()

First here is the class diagram:

![](http://farm4.static.flickr.com/3051/2315711785_f322531748_o_d.jpg) 

The role of each class of our Framework is as following:  

*   **EntityIndexer** manage an index and index the business entities  **EntitySearcher** let you search business entities  **EntityDocument** is used by the class EntityIndexer in order to manage Lucene.Net Document  **IndexPath** is an utility class used to specify the location of index 

As you can see on the diagram we use the .NET Frameworks 2 generics this in order to allow us to search whatever attribute decorating our business entities. But also to be able to have a Framework that is not dependant of any entities. This brings a good flexibility at the usage time as it let you index whatever property of type string of whatever business entity. All of this is without being intrusive in our model.  

Now that we know about the architecture of our Framework it is time to look deeper in the details of the implementation.

*This post is cross-posted on [innoveo blog](http://blog.innoveo.com/archive.aspx/2008/3/7/indexing-and-searching-business-entities-using-lucene-net-framework-part-3) **and in French on my .NET community portal *[*Tech Head Brothers*](http://www.techheadbrothers.com/Articles.aspx/indexer-rechercher-entites-metier-aide-framework-lucene-net)*.*
