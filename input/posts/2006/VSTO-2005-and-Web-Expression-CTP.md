---
title: "VSTO 2005 and Web Expression CTP"
permalink: /2006/10/05/VSTO-2005-and-Web-Expression-CTP/
date: 10/5/2006 6:17:08 AM
updated: 10/5/2006 6:17:08 AM
disqusIdentifier: 20061005061708
tags: ["Visual Studio", "VSTO"]
alias:
 - /post/VSTO-2005-and-Web-Expression-CTP.aspx/index.html
---
What? How can you link those two!!!???

I am working on [Tech Head Brothers Authoring](http://www.codeplex.com/Wiki/View.aspx?ProjectName=THBAuthoring) tool and it means that I need to provide a setup to all the authors. I did it following a real long tutorial in two parts:
<!-- more -->

*   [Deploying Visual Studio 2005 Tools for Office Solutions Using Windows Installer (Part 1 of 2)](http://msdn.microsoft.com/office/default.aspx?pull=/library/en-us/odc_vsto2005_ta/html/OfficeVSTOWindowsInstallerOverview.asp)
*   [Deploying Visual Studio 2005 Tools for Office Solutions Using Windows Installer: Walkthroughs (Part 2 of 2)](http://msdn.microsoft.com/office/default.aspx?pull=/library/en-us/odc_vsto2005_ta/html/OfficeVSTOWindowsInstallerWalkthrough.asp) 

And finally I had a setup that works on my machine. But as you know if it works on yours it doesn't mean that it works on all. Hey developers, an application doesn't need to run just on your machine ;-)

**What was the issue on author machines?**

The interesting part of the exception was:

> System.IO.FileNotFoundException: Impossible de charger le fichier ou l'assembly 'office, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c' ou une de ses dépendances. Le fichier spécifié est introuvable.

Office Version 12??? What the hell is this I use Office 2003 and VSTO 2005!!!

So I tried to add a reference to version 11 of office without any success because the version 12 was in the GAC. After some research I found that Web Expression was the one that installed the version 12 of the office DLL, and was causing the issue. Quick uninstall to find that VSTO was not able to create new VSTO projects in Visual Studio, so I tried uninstall/reinstall of VSTO for the same result. Will check that later.

Conclusion: NEVER INSTALL BETA SOFTWARE OUTSIDE A VPC.
