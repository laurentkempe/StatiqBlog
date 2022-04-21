---
title: "Business entity and C# extension methods"
permalink: /2007/11/01/Business-entity-and-C-extension-methods/
date: 11/1/2007 12:56:23 AM
updated: 11/1/2007 12:56:23 AM
disqusIdentifier: 20071101125623
tags: ["Note to self"]
alias:
 - /post/Business-entity-and-C-extension-methods.aspx/index.html
---
In place of writing something like:

<span style="color: rgb(0,0,255)">protected</span> <span style="color: rgb(0,0,255)">string</span> GetUrl(<span style="color: rgb(43,145,175)">Information</span> info)
{
    <span style="color: rgb(0,0,255)">if</span> (info <span style="color: rgb(0,0,255)">is</span> <span style="color: rgb(43,145,175)">News</span>)
        <span style="color: rgb(0,0,255)">return</span> ResolveUrl(<span style="color: rgb(0,0,255)">string</span>.Format(<span style="color: rgb(163,21,21)">"~/news.aspx/{0}"</span>, info.NormalizedTitle));
    <span style="color: rgb(0,0,255)">else</span> <span style="color: rgb(0,0,255)">if</span> (info <span style="color: rgb(0,0,255)">is</span> <span style="color: rgb(43,145,175)">Publication</span>)
        <span style="color: rgb(0,0,255)">return</span> ResolveUrl(<span style="color: rgb(0,0,255)">string</span>.Format(<span style="color: rgb(163,21,21)">"~/publications.aspx/{0}"</span>, info.NormalizedTitle));
    <span style="color: rgb(0,0,255)">else
</span>        <span style="color: rgb(0,0,255)">return</span> <span style="color: rgb(0,0,255)">string</span>.Empty;
}
<!-- more -->

I would use on the presentation layer only an extension method on my business entity class of type Information to add a GetUrl method!

It would keep the business entity light on the different other layers, and add web responsibilities on the presentation layer.

And would end like so:

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">a</span> <span style="color: rgb(255,0,0)">href</span><span style="color: rgb(0,0,255)">="</span><span style="background: rgb(255,238,98)"><%</span># ((Information)Container.DataItem)).GetUrl() <span style="background: rgb(255,238,98)">%><span style="color: rgb(0,0,255)"></span>"</span> <span style="color: rgb(255,0,0)">title</span><span style="color: rgb(0,0,255)">="</span><span style="background: rgb(255,238,98)"><%</span>#Eval("Title")<span style="background: rgb(255,238,98)">%><span style="color: rgb(0,0,255)"></span>">
</span>    <span style="background: rgb(255,238,98)"><%<span style="color: rgb(0,0,255)"></span>#</span>Eval(<span style="color: rgb(163,21,21)">"Title"</span>)<span style="background: rgb(255,238,98)">%>
<span style="color: rgb(0,0,255)"></span></</span><span style="color: rgb(163,21,21)">a</span><span style="color: rgb(0,0,255)">>
</span>

Something to test when I will install Visual Studio 2008!
