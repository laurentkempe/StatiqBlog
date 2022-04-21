---
title: "Devices and Printers empty on Windows 7"
permalink: /2009/09/06/Devices-and-Printers-empty-on-Windows-7/
date: 9/6/2009 9:16:31 PM
updated: 5/7/2010 7:52:09 AM
disqusIdentifier: 20090906091631
tags: ["Windows 7"]
alias:
 - /post/Devices-and-Printers-empty-on-Windows-7.aspx/index.html
---
For some days I had the following issue on my Windows 7 installation: clicking on the start orb then on Devices and Printers would open the window, show a progress bar and then nothing else, a completely empty folder.

I tried a restore that didn’t solved the issue but it reminded me that I installed some days ago a Bluetooth software for a Bluetooth headset I received as a Microsoft MVP gift. This gave me the direction to search for.
<!-- more -->

I ended up seeing in the Services that the “Bluetooth Support Service” was stopped and that it Statup type was set to disabled. Re-enabling it fixed my issue and now I have again the full list of Devices and Printers shown in the dialog.
