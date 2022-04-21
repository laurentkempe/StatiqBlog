---
title: "Best Practices for Representing XML in the .NET Framework - Summary"
permalink: /2004/04/02/Best-Practices-for-Representing-XML-in-the-NET-Framework-Summary/
date: 4/2/2004 2:17:00 AM
updated: 4/2/2004 2:17:00 AM
disqusIdentifier: 20040402021700
tags: [".NET Development"]
alias:
 - /post/Best-Practices-for-Representing-XML-in-the-NET-Framework-Summary.aspx/index.html
---
Thats a summary of the [article from Dare Obasanjo on MSDN](http://msdn.microsoft.com/xml/default.aspx?pull=/library/en-us/dnexxml/html/xml03172004.asp). 
<h4>Classes with Fields or Properties that Hold XML</h4>If a class has a field or property that is an XML document or fragment, it should provide mechanisms for manipulating the property as both a string and as an <b>XmlReader</b>. 
<h4>Methods that Accept XML Input or Return XML as Output</h4>Methods that accept or return XML should favor returning <b>XmlReader</b> or <b>XPathNavigator</b> unless the user is expected to be able to edit the XML data, in which case <b>XmlDocument</b> should be used. 
<h4>Converting an Object to XML</h4>If an object wants to provide an XML representation of itself for serialization purposes, then it should use the <b>XmlWriter</b> if it needs more control of the XML serialization process than is provided by the <b>XmlSerializer</b>. If the object wants to provide an XML representation of itself that enables it to participate fully as a member of the XML world, such as allow XPath queries or XSLT transformations over the object, then it should implement the <b>IXPathNavigable</b> interface.
