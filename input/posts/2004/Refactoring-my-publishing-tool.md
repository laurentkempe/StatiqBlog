---
title: "Refactoring my publishing tool"
permalink: /2004/05/20/Refactoring-my-publishing-tool/
date: 5/20/2004 10:49:00 PM
updated: 5/20/2004 10:49:00 PM
disqusIdentifier: 20040520104900
tags: [".NET Development"]
alias:
 - /post/Refactoring-my-publishing-tool.aspx/index.html
---
I started some time ago to work on a new release of the tool I developed to publish content on my web site [Tech Head Brothers](http://www.techheadbrothers.com "Tech Head Brothers"). This new release will add the possibility for the author to directly post there article to the website.<br>This solution is using:

*   [Word 2003](http://msdn.microsoft.com/office/understanding/word/) with it XML features
<!-- more -->
*   XML Schema for the definition of the content
*   XSLT for the presentation. Transformation to HTML, PDF using nFOP (FOP is not working properly at the moment), and soon WordML.
*   [Microsoft Visual Studio Tools for the Microsoft Office System](http://msdn.microsoft.com/vstudio/office/default.aspx)
*   [Web Services Enhancements (WSE)](http://msdn.microsoft.com/webservices/building/wse/default.aspx), Dime
*   [SharpZipLib](http://www.icsharpcode.net/OpenSource/SharpZipLib/Default.aspx)


The authors write there articles using Word 2003 and can visualize on there computer there article the way it will be displayed on [Tech Head Brothers](http://www.techheadbrothers.com "Tech Head Brothers"). Then when they are ready, they save the Word document and are asked if they want to publish it. The software then parse the XML published by Word 2003 and replace the references to source code files by the real source code with colorization. It also replaces references to images with a new XML tag representing the image. Then it zip everything; the article as a XML file, the images, if there is a zip with source code... And finally using WSE and DIME attachment the article is posted. The website gets the DIME attachment, unpack the zip, save the files on the server, and add an entry to the database. That's it.

I have done all the refactoring by hand. And that's such a pain. I am now used to do that with Whidbey and it refactoring tools. So I have a bit searched on the internet some tools that will match my need concerning refactoring and I could find some:

*   [.NET Refactoring](http://dotnetrefactoring.com/)
*   [C# Refactory](http://www.xtreme-simplicity.net/)
*   [ReSharper](http://www.jetbrains.com/resharper/index.html)
*   [NETRefactor 2.0](http://www.knowdotnet.com/articles/netrefactorproducthome.html)


I tried several of those in the past but now I really want to use one, but it is impossible cause I get an error message when I add an addin to Visual Studio .NET 2003, for exemple yith [.NET Reflector 4](http://www.aisto.com/roeder/DotNet/):

![](http://perso.wanadoo.fr/laurent.kempe/images/vserror.png)

Any one as an idea?
