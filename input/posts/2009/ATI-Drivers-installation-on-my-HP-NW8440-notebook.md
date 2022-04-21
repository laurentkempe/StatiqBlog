---
title: "ATI Drivers installation on my HP NW8440 notebook"
permalink: /2009/04/10/ATI-Drivers-installation-on-my-HP-NW8440-notebook/
date: 4/10/2009 4:32:53 PM
updated: 4/10/2009 4:32:53 PM
disqusIdentifier: 20090410043253
alias:
 - /post/ATI-Drivers-installation-on-my-HP-NW8440-notebook.aspx/index.html
---
From quite some time I had a crash when running the ATI Catalyst installer and I finally found what was the issue.

> Follow the steps in the Knowledge Base (KB) article to solve the problem with the Microsoft Visual Studio mfc80u.dll or mfc80.dll module
<!-- more -->
> 
> This problem was caused by **the Microsoft Visual Studio mfc80u.dll or mfc80.dll module**, which was created by **Microsoft Corporation**.
> 
> **Solution**
> 
> * * *
> 
> To fix this problem, follow the steps in this online Microsoft Support Knowledge Base (KB) article:
> 
> ![](http://wer.microsoft.com/Responses/include/images/arrow.gif)[KB961894: An application crashes after you install a product that updates the Mfc80.dll or Mfc80u.dll module](http://support.microsoft.com/kb/961894)[](#here)

I followed the KB and then I could run again the installer without any issue.

Another good news is that ATI release now installers for notebooks. We don’t need anymore to tweak there installer using [Mobility DotNET Final1.1.1.0](http://www.driverheaven.net/modtool.php). 

For example I could find the installer for my [FireGL V5200](http://support.amd.com/us/psearch/Pages/psearch.aspx?type=2.4.3&product=2.4.3.3.2.3.9&contentType=GPU+Download+Detail&ostype=Windows+Vista+-+32-Bit+Edition&keywords=&items=20).
