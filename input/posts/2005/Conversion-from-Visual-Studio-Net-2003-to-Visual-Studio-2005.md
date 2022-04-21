---
title: "Conversion from Visual Studio .Net 2003 to Visual Studio 2005"
permalink: /2005/03/03/Conversion-from-Visual-Studio-Net-2003-to-Visual-Studio-2005/
date: 3/3/2005 8:45:00 AM
updated: 3/3/2005 8:45:00 AM
disqusIdentifier: 20050303084500
tags: ["Tech Head Brothers", ".NET Development", "Whidbey", "ASP.NET 2.0"]
alias:
 - /post/Conversion-from-Visual-Studio-Net-2003-to-Visual-Studio-2005.aspx/index.html
---
This evening (hum morning ;) I decided to give a try to the conversion 
wizard integrated in Visual Studio 2005 that let you import older 1.1 
projects.  
I choosed to import my whole website: [Tech Head Brothers](http://www.techheadbrothers.com "Tech Head Brothers"). You might 
see next the conversion report:  

<!-- more -->
![](http://membres.lycos.fr/lkempe//conversionreport.jpg)

And the Error List report of a build :

![](http://membres.lycos.fr/lkempe//Errorlist.jpg)

I am impressed about the conversion. Ok I have 2 errors and 133 Warnings, but 
the warnings are all due to deprecated classes or obsolete methods and the 
two errors are:

*   Error 134 Program '\Projects\Tech Head Brothers 
  Portal\Database\obj\Debug\Database.exe' does not contain a static 'Main' 
  method suitable for an entry point Database
*   Error 135 Program '\Projects\Tech Head Brothers 
  Portal\Docs\obj\Debug\Docs.exe' does not contain a static 'Main' method 
  suitable for an entry point Docs


Next step is to get the DB in SQL Server 2005 and make some tests. But that 
the task for another day.

Tomorrow I will be at the French DevDays 2005 in Strasbourg, hope to meet you 
there.
