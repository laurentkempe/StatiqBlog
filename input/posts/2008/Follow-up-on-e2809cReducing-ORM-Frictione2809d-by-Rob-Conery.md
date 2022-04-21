---
title: "Follow up on “Reducing ORM Friction” by Rob Conery"
permalink: /2008/11/05/Follow-up-on-e2809cReducing-ORM-Frictione2809d-by-Rob-Conery/
date: 11/5/2008 7:20:24 AM
updated: 11/5/2008 7:20:24 AM
disqusIdentifier: 20081105072024
tags: ["unit test", "Euss", "ORM", "Agile"]
alias:
 - /post/Follow-up-on-e2809cReducing-ORM-Frictione2809d-by-Rob-Conery.aspx/index.html
---
In my development process I do use what Rob is describing in his post “[Crazy Talk: Reducing ORM Friction](http://blog.wekeroad.com/blog/crazy-talk-reducing-orm-friction/)” with some slight differences.

For example I developed [Tech Head Brothers](http://www.techheadbrothers.com/) portal this way, as [Innoveo Solutions](http://www.innoveo.com) web site. I use TDD and Domain Driven Development and I keep the mapping as one of the last step for my implementation.
<!-- more -->

I do have a generic Repository interface as following:
  <div style="font-family: courier new; background: white; color: black; font-size: 10pt">   

<span style="color: blue">using</span> System;

<span style="color: blue">using</span> System.Collections.Generic;

<span style="color: blue">using</span> System.Linq;

<span style="color: blue">namespace</span> TechHeadBrothers.Portal.Infrastructure.Interfaces

{

    <span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"><summary></span>

    <span style="color: gray">///</span><span style="color: green"> IRepository exposes all methods to access the data repository</span>

    <span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"></summary></span>

    <span style="color: blue">public</span> <span style="color: blue">interface</span> <span style="color: #2b91af">IRepository</span>

    {

        <span style="color: blue">void</span> InitializeRepository();

        <span style="color: blue">bool</span> Save<T>(T entity) <span style="color: blue">where</span> T : <span style="color: blue">class</span>;

        <span style="color: blue">bool</span> SaveAll<T>(<span style="color: #2b91af">IList</span><T> entities) <span style="color: blue">where</span> T : <span style="color: blue">class</span>;

        <span style="color: blue">bool</span> Delete<T>(<span style="color: blue">string</span> id) <span style="color: blue">where</span> T : <span style="color: blue">class</span>;

        T Find<T>(<span style="color: blue">string</span> id) <span style="color: blue">where</span> T : <span style="color: blue">class</span>;

        <span style="color: #2b91af">IQueryable</span><T> Find<T>();

        <span style="color: #2b91af">IQueryable</span><T> DetachedFind<T>();

        <span style="color: #2b91af">IQueryable</span><T> Find<T>(System.Linq.Expressions.<span style="color: #2b91af">Expression</span><<span style="color: #2b91af">Func</span><T, <span style="color: blue">bool</span>>> expression);

        <span style="color: blue">int</span> Count<T>();

        <span style="color: blue">int</span> Count<T>(System.Linq.Expressions.<span style="color: #2b91af">Expression</span><<span style="color: #2b91af">Func</span><T, <span style="color: blue">bool</span>>> expression);

    }

}

 </div>  

My ORM mapping tool of choice is [Euss](http://www.codeplex.com/euss/). And here comes the slight difference, I do have one implementation of my interface leveraging Euss, and that’s it. All different possibilities are handled by Euss. During my work on the definition of the domain I took the habit to use an [Euss](http://www.codeplex.com/euss/) XML Engine or an [Euss](http://www.codeplex.com/euss/) Memory Engine. I use those two engine for my unit test and my real application.

Following the [lean principle](http://www.poppendieck.com/ilsd.htm) I postpone the choice of the data repository till the last minute, when I know more about the real need. So it really happen that I stay with an XML Engine so that all my data are stored in an XML file. If I need more I go to an Euss SQL Mapper Engine and then define the mapping.

So I moved to the ORM framework the different implementations.

Now I am still free to go to another ORM, or something else, by using the interface IRepository.

I used several time this technique and I am currently happy about it.
