---
title: "Visual Studio 2005 Team Suite installation"
permalink: /2005/12/10/Visual-Studio-2005-Team-Suite-installation/
date: 12/10/2005 8:59:00 AM
updated: 12/10/2005 8:59:00 AM
disqusIdentifier: 20051210085900
tags: [".NET Framework 2.0"]
alias:
 - /post/Visual-Studio-2005-Team-Suite-installation.aspx/index.html
---



It was a nightmare to have this installed and working!!! For all previous 
versions, since the alpha version, I used a Virtual PC. And now that the product 
went RTM I decided that it was time to install it on my productive machine.
<!-- more -->

I had not installed any piece of alpha nor beta software on my productive 
environment before starting the installation of Visual Studio 2005 Team 
Suite.

I ran the setup with default options except the location of the folder to 
install. Everything went fine till the end and then I had a strange message 
about Office 2003 missing SP2 and PIA... It was proposed that I 
run VSTOR.EXE manually.

Running VSTOR.EXE I get an error message saying that I need to uninstall 
"Visual Studio .NET prerequisites - English", but I don't had that as an option 
in the Add/Remove.  

From the support page ([http://msdn.microsoft.com/vstudio/express/support/uninstall/](http://msdn.microsoft.com/vstudio/express/support/uninstall/ "http://msdn.microsoft.com/vstudio/express/support/uninstall/")) 
I downloaded the uninstall tool, even if I never installed beta version before. 
Running it, it does uninstall SQL Server 2005 Express, then I get this 
error:  

The following task did not complete successfully:   
<font class="name_link" color="#800080"><u>Uninstall Visual Studio 
components</u></font>

I then decided to uninstall and try to re-install, I did it two 
times with some operations in between. No way still not working. I started to be 
really really pissed off.

To cooooool down I decided to get some music: [Jack 
Johnson - In Between Dreams](http://www.amazon.fr/exec/obidos/ASIN/B000BNUT7A/techheadbroth-21/402-5617210-5443358), really really cool. Thanks [Mathieu](http://myaustraliantrip.blogspot.com).

Then I went with regedit to 
*HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\  
*and 
started to uninstall stuff with *msiexec /x {guid}*

I finally managed to be able to install VSTOR.EXE after 
installing .Net Framework 2. Then a full re-install  of Visual Studio 2005 
Team Suite without SQL Server 2005 Express that I installed by hand and also 
with several trial before being successful.

I finally have a development environment again and I can go on 
with the development of Tech Head Brothers in ASP.NET 2.0 and the new version of 
the publishing tool based on Word 2003.

[ Currently Playing : Better Together - Jack Johnson - In Between 
Dreams (03:28) ]
