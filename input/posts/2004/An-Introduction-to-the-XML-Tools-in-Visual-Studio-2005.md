---
title: "An Introduction to the XML Tools in Visual Studio 2005"
permalink: /2004/07/29/An-Introduction-to-the-XML-Tools-in-Visual-Studio-2005/
date: 7/29/2004 6:17:00 PM
updated: 7/29/2004 6:17:00 PM
disqusIdentifier: 20040729061700
tags: ["Whidbey"]
alias:
 - /post/An-Introduction-to-the-XML-Tools-in-Visual-Studio-2005.aspx/index.html
---
Msdn published an [interesting article](http://msdn.microsoft.com/XML/BuildingXML/XMLinNETFramework/default.aspx?pull=/library/en-us/dnxmlnet/html/xmltools.asp) concerning the new XML possibilities of the IDE Visual Studio 2005.

<u>Summary</u>:
<!-- more -->

<ul>
<li>Checks the <strong>well-formedness</strong> errors in the XML and provides live feedback using red squiggly under the sequence of characters that is the cause of the errors and  through the error list.</li>
<li><strong>XML</strong> File using schema are <strong>validated</strong> at design time and  the XML editor provides context-sensitive Intellisense.</li>
<li><strong>Inferring a Schema from an XML Document</strong>.  The inferred schema is the most restrictive it can be, based on the XML used to infer it.</li>
<li><strong>Attaching an XML Schema to an XML Document</strong>.  The user already has the schema and an XML file, and now needs to figure out how to associate the two.</li>
<li><strong>Editing XSLT</strong>.  XSLT errors and standard XML syntax errors are displayed with the red squigglies and in the Error List.  Intellisense. Intellisense list is also sensitive to the namespaces that are being used. </li>
<li><strong>Debugging XSLT Files </strong>from the XML Editor itself, or from a CLR language program that uses an XSLTCommand object from System.XML.</li></ul>


The debugging part for the XSLT and intellisense are really cool features.
