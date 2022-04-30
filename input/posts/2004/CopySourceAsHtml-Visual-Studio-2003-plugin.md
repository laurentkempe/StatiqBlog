---
title: "CopySourceAsHtml - Visual Studio 2003 plugin"
permalink: /2004/11/09/CopySourceAsHtml-Visual-Studio-2003-plugin/
date: 11/9/2004 6:36:00 AM
updated: 11/9/2004 6:36:00 AM
disqusIdentifier: 20041109063600
tags: ["Tools", ".NET Development"]
---
Ever wanted to copy paste some code from Visual Studio .NET 2003 to your blog tool (e.g. [Sauce Reader](http://www.synop.com/Products/SauceReader/)) and keep the colorization ?<br>Here is the solution, [CopySourceAsHtml](http://www.jtleigh.com/people/colin/blog/archives/2004/11/copysourceashtm_3.html), an awesome plugin from [Colin Coller](http://www.jtleigh.com/people/colin/blog/). The cool point is that if "<em>VS.NET can highlight it, CSAH can copy it, and your code should look the same in your browser as it does in your editor</em>". I was a bit disappointed not finding the context menu in the editor for other source then C# but you might add a keyboard shortcups as described on this [page](http://www.jtleigh.com/people/colin/blog/archives/2004/10/copysourceashtm_1.html).

Even better, Colin provides the source. I guess I will soon integrate his colorization way to [Tech Head Brothers](http://www.techheadbrothers.com "Tech Head Brothers") Word 2003 [publishing tool](http://weblogs.asp.net/lkempe/archive/2004/08/23/219122.aspx).
<!-- more -->

Here a sample output of a C# code:

```csharp
#region GuiBuilder

//http://support.microsoft.com/default.aspx?scid=kb;EN-US;303018

private bool setupCommandBar()
{
    ThisApplication.CustomizationContext = ThisDocument;

    // Add a button to the command bar.
    oCommandBar = ThisApplication.CommandBars.Add("Tech Head Brothers", oMissing, oMissing, true);

    AddButton(ref oCommandBar,
              ref oButtonNew,
              new _CommandBarButtonEvents_ClickEventHandler(oButtonNew_Click),
              "New Document",
              12);
```

XSLT:

```xml
<xml version="1.0" encoding="ISO-8859-1" ?>
<xsl:stylesheet xmlns:thb="http://www.techheadbrothers.com/WordFormat30.xsd" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="3.0">
    <xsl:output method="html" version="4.0" encoding="iso-8859-1" indent="yes" />
    <xsl:param name="readcounter" />
```
