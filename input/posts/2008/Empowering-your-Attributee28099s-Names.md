---
title: "Empowering your Attribute’s Names"
permalink: /2008/06/18/Empowering-your-Attributee28099s-Names/
date: 6/18/2008 8:54:06 PM
updated: 6/18/2008 8:54:06 PM
disqusIdentifier: 20080618085406
tags: ["Lucene.Net"]
alias:
 - /post/Empowering-your-Attributee28099s-Names.aspx/index.html
---
Today I was facing the following issue. I have an indexing system (read more [here](http://weblogs.asp.net/lkempe/archive/2007/11/16/indexing-and-searching-business-entities-using-lucene-net-framework-part-1.aspx), [here](http://weblogs.asp.net/lkempe/archive/2008/03/07/indexing-and-searching-business-entities-using-lucene-net-framework-part-2.aspx) and [here](http://weblogs.asp.net/lkempe/archive/2008/03/07/indexing-and-searching-business-entities-using-lucene-net-framework-part-3.aspx)) using [Lucene.Net](http://incubator.apache.org/lucene.net/) that is working quite good. The drawback of the current implementation was that I couldn’t filter any to be indexed property decorated with the attribute. For example if I had a string property of my domain holding HTML, I wasn’t able to remove the HTML out of the string before indexing it. 

I scratch my head some time, and finally came to a pragmatic solution. 
<!-- more -->

For this pragmatic solution I used meta information in the name of the attribute. So the attribute decorate my code with a meta information and the name of the attribute contains by itself also meta information. This means that I enforce some rules in the naming of the attribute.

For example the first rule is the following, if the name of the attribute contains Html in whatever form, uppercase, lowercase, etc. the framework will realize that the property contains Html and that it needs to remove the Html tags out of the string on decode it.

If none of the naming rules match then it will use the content of the property directly.

So as a user of the indexing framework, I write one attribute named HTMLSearchableAttribute and decorate my properties and then the framework will clean automatically this string property before indexing the cleaned content with [Lucene.Net](http://incubator.apache.org/lucene.net/).
<div class="wlWriterHeaderFooter" style="text-align:left; margin:0px; padding:4px 0px 4px 0px;"><script type="text/javascript">digg_url = "http://weblogs.asp.net/lkempe/archive/2008/06/18/empowering-your-attribute-s-names.aspx";digg_title = "Empowering your Attribute’s Names";digg_bgcolor = "#FFFFFF";digg_skin = "compact";</script><script src="http://digg.com/tools/diggthis.js" type="text/javascript"></script><script type="text/javascript">digg_url = undefined;digg_title = undefined;digg_bgcolor = undefined;digg_skin = undefined;</script></div>
