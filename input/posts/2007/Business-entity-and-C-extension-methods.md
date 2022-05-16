---
title: "Business entity and C# extension methods"
permalink: /2007/11/01/Business-entity-and-C-extension-methods/
date: 11/1/2007 12:56:23 AM
updated: 11/1/2007 12:56:23 AM
disqusIdentifier: 20071101125623
tags: ["Note to self"]
---
In place of writing something like:

```csharp
protected string GetUrl(Information info)
{
    if (info is News)
        return ResolveUrl(string.Format("~/news.aspx/{0}", info.NormalizedTitle));
    else if (info is Publication)
        return ResolveUrl(string.Format("~/publications.aspx/{0}", info.NormalizedTitle));
    else
        return string.Empty;
}
```

<!-- more -->

I would use on the presentation layer only an extension method on my business entity class of type Information to add a GetUrl method!

It would keep the business entity light on the different other layers, and add web responsibilities on the presentation layer.

And would end like so:

```html
<a href="<%# ((Information)Container.DataItem)).GetUrl() %>" title="<%#Eval("Title")%>">
    <%#Eval("Title")%>
</a>
```

Something to test when I will install Visual Studio 2008!
