---
title: "Wowwwwww effect of my new Word 2007 authoring tool"
permalink: /2008/08/22/Wowwwwww-effect-of-my-new-Word-2007-authoring-tool/
date: 8/22/2008 6:59:58 AM
updated: 8/22/2008 6:59:58 AM
disqusIdentifier: 20080822065958
tags: ["Tech Head Brothers", "VSTO", ".NET Framework 3.5", "Office 2007", "Open XML", "Linq", "WordML"]
alias:
 - /post/Wowwwwww-effect-of-my-new-Word-2007-authoring-tool.aspx/index.html
---
I have reached now another step on the new authoring tool for [Tech Head Brothers](http://www.techheadbrothers.com/) portal by having some authors installing this new version and starting beta testing it. Has you can see, with the following [twit](http://www.twitter.com/), it seems that the tool is on the good way:

From [Rédo](http://twitter.com/Redo) ([french blog](http://blogs.codes-sources.com/redo))
<!-- more -->

> Just try the new beta tool of publishing for [http://www.techheadbrothers...](http://www.techheadbrothers.com/) .... that's the Wouawwww effect !!! ... sorry, it's NDA ;p [<abbr>about 23 hours</abbr> ago](http://twitter.com/Redo/statuses/893763572)

From [Jon Galloway](http://twitter.com/jongalloway) ([blog](http://weblogs.asp.net/jgalloway))

> @[laurentkempe](http://twitter.com/laurentkempe) Like I said, I'd pay money for it, and I'm very cheap. Very cool. [<abbr>about 15 hours</abbr> ago](http://twitter.com/jongalloway/statuses/894115079) from web [in reply to laurentkempe](http://twitter.com/laurentkempe/statuses/894105384)

This tool let all authors write articles using a customized Word 2007, preview it offline in Internet Explorer as it will be seen on the portal and finally post the article to the portal directly from Word 2007. It is a replacement of [THBAuthoring](http://www.codeplex.com/THBAuthoring), my old tool found on [CodePlex](http://www.codeplex.com/).

To achieve this goal I am using quite some technologies. First Word 2007, Ribbon, .NET Framework 3.5 SP1, VSTO 3.0, OpenXML, WordML, XML Schema, XSLT. Than for sure C#, Linq to XML, Linq to Object.

For the installation I use ClickOnce. That’s a great improvement! I had so much issues with [THBAuthoring](http://www.codeplex.com/THBAuthoring).

From an author point of view you are using a almost normal Word 2007 document with different predefined styles, bold, list… There is two exceptions:

1.  Source code
2.  Zip with sample code  

The usage scenario for the inserting Source Code into the Word document is as following:

1.  Copy the code into the clipboard from Visual Studio 2008/2005
2.  Position the cursor into the Word 2007 document where you want to insert the Source Code
3.  Click on the button Source Code on the custom Ribbon
4.  That’s it, easy no?  

This brings clear text of the source code into the document and colored source code into the HTML preview.

The usage scenario for attaching the Zip with the sample code is also easy:

1.  Click on the button Zip in the custom Ribbon
2.  In the opening File selector dialog browse to your zip file and select one
3.  That’s it, easy no?  

Finally there is another usage scenario; inserting pictures:

1.  From Windows Explorer copy a picture file
2.  Position the cursor in the Word 2007 document
3.  Paste
4.  That’s it, easy no?  

So, how does it work technically!

It works by doing a projection of the WordML to a well formed Tech Head Brothers XML document defined by an XML Schema. Then it uses a XSLT to produce HTML.

I hope to find the time to make a little video soon to demonstrate the way it is used!

Here is a picture of the tool

![](http://farm3.static.flickr.com/2086/2327067069_1e37deaa06_o.jpg)
