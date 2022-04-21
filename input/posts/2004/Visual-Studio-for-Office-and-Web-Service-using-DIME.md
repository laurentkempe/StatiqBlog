---
title: "Visual Studio for Office and Web Service using DIME"
permalink: /2004/04/29/Visual-Studio-for-Office-and-Web-Service-using-DIME/
date: 4/29/2004 12:00:00 AM
updated: 4/29/2004 12:00:00 AM
disqusIdentifier: 20040429120000
tags: ["Tech Head Brothers"]
alias:
 - /post/Visual-Studio-for-Office-and-Web-Service-using-DIME.aspx/index.html
---
I started yesterday to work on the second release of the publishing tool that we are using to write articles on my site [Tech Head Brothers](http://www.techheadbrothers.com "Tech Head Brothers"). The first version what packing everything needed (XML, pictures, source zip) into one zip file that the author was emailing me for publishing. The new idea is to have a Web Service on the web site that directly accept this zip file as a Dime attachment. So the author can directly post his article to the web site without passing it to me. I am so lazy ;-)

I have this web service developped and the code in the VSTO project. Then I faced an issue during debugging. In fact when the debugger reach the point of calling the web service then I get back to the Word 2003 document, and can't continue to debug and the web service is not called. If I remove the line of code that attach the Dime attachment to the request:<br>srv.RequestSoapContext.Attachments.Add(attachment);<br>and call the service then it works, but for sure that's not what I want.
<!-- more -->

For sure I have added Microsoft.Web.Services.dll with .NET Configuration 1.1 so that it has Full Trust. I think I am missing something in here but can't determine what. Any clue?

I took the code used in the VSTO project and made a Winform project that worked correctly, so it is really a security issue.
