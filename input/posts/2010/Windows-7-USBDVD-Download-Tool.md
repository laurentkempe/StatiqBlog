---
title: "Windows 7 USB/DVD Download Tool"
permalink: /2010/08/27/Windows-7-USBDVD-Download-Tool/
date: 8/27/2010 9:18:30 AM
updated: 8/27/2010 9:19:47 AM
disqusIdentifier: 20100827091830
tags: ["Windows 7"]
alias:
 - /post/Windows-7-USBDVD-Download-Tool.aspx/index.html
---
Tonight during the preparation of an usb stick using [Windows 7 USB/DVD Download](http://store.microsoft.com/Help/ISO-Tool) I got an error message about bootsect just at the end of the creation of bootable usb stick. Following the troubleshoot link I arrived on the following

> **When creating a bootable USB device, I am getting an error about bootsect**
<!-- more -->
> To make the USB device bootable, you need to run a tool named bootsect.exe. In some cases, this tool needs to be downloaded from your Microsoft Store account. This may happen if you're trying to create a 64-bit bootable USB device from a 32-bit version of Windows. To download bootsect:
> 
> 1.  Login to your [Microsoft Store account](https://store.microsoft.com/account) to view your purchase history
> 2.  Look for your Windows 7 purchase.
> 3.  Next to Windows 7, there is an "Additional download options" drop-down menu.
> 4.  In the drop-down menu, select "32-bit ISO."
> 5.  Right-click the link, and then save the bootsect.exe file to the location where you installed the Windows 7 USB/DVD Download Tool (e.g. C:\Users\username\AppData\Local\Apps\Windows 7 USB DVD Download Tool).
> 6.  Once the file has been saved, go back to the Windows 7 USB/DVD Download tool to create your bootable USB device.

Now my problem is that I haven’t ordered my iso on Microsoft Store but downloaded it from MSDN, so I can’t download bootsect from Microsoft Store.

I could find bootsect.exe in a Windows 7 32 bit iso which came also from MSDN. Then after following the help documentation I successfully created a bootable usb stick with Windows 7 64 bits.

Tomorrow evening I will start the re-installation of my machine with a fresh Windows 7 64 bits (running 32 bits till now) and a new [Seagate Momentus XT, 7200rpm, 32MB, 2.5", 500GB, SATA-II](http://www.seagate.com/www/en-us/products/laptops/laptop-hdd/)
