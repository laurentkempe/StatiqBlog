---
title: "Tech Head Brothers new authoring tool, step 2"
permalink: /2008/02/16/Tech-Head-Brothers-new-authoring-tool-step-2/
date: 2/16/2008 10:30:03 PM
updated: 2/16/2008 10:30:03 PM
disqusIdentifier: 20080216103003
tags: ["Tech Head Brothers", "Visual Studio", "VSTO", ".NET Framework 3.5", "Office 2007"]
---
![](http://farm3.static.flickr.com/2005/2268217977_fd02a77b95_o.jpg) 

Today I had the time to go on a bit with the new version of [Tech Head Brothers](http://www.techheadbrothers.com/), as you can see on the picture.
<!-- more -->

I decided to use my last article as a test case for the tool.

I went from the Ribbon (Visual Editor) to Ribbon (XML), it is not anymore grahical edition of the Ribbon but I can do more than what I was able to do in the Visual Editor.

I started to implement some parsing logic, I first made a trial with an idea of [Sébastien Ros](http://www.dotnetguru2.org/sebastienros/) and Fabien Reinle but finally got back to LINQ to XML to parse the WordML.

The general idea is to output a well formed XML document out of the WordML document. 

The architecture I decided to use at the moment is to have special Word styles mapped to some part of my target XML structure. Doing so I can easily parse the WordML like this to get all pagaraph with Heading1 style:

```csharp
var sections = 
    from p in styledPara
    where p.Elements(w + "pPr")
           .Elements(w + "pStyle")
           .First()
           .Attribute(w + "val")
           .Value == "Heading1"
    select p;
```
