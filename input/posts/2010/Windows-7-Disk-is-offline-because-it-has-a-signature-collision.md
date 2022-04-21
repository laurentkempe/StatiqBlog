---
title: "Windows 7, Disk is offline because it has a signature collision"
permalink: /2010/04/03/Windows-7-Disk-is-offline-because-it-has-a-signature-collision/
date: 4/3/2010 2:56:31 AM
updated: 9/25/2013 9:57:57 PM
disqusIdentifier: 20100403025631
tags: ["Windows 7"]
alias:
 - /post/Windows-7-Disk-is-offline-because-it-has-a-signature-collision.aspx/index.html
---
When I switched on today my second hard drive I had the following issue displayed 

> The disk is offline because it has a signature collision with another disk that is online
<!-- more -->

![4483826563_d4d4d4a9c0_o[1]](/images/4483826563_d4d4d4a9c0_o%5B1%5D.png "4483826563_d4d4d4a9c0_o[1]") 

Running Diskpart as administrator proved that it was the issue

![4483838795_d1c9bdf038_o[1]](/images/4483838795_d1c9bdf038_o%5B1%5D.png "4483838795_d1c9bdf038_o[1]") 

As you can see disk 2 and disk 4 both have the 00024A91 signature hence the collision.

Following the advise [here](http://www.howtohaven.com/system/change-disk-signature.shtml), I changed the signature of the drive decreasing from 1 unit.

![4484504524_c039b4f771_o[1]](/images/4484504524_c039b4f771_o%5B1%5D.png "4484504524_c039b4f771_o[1]") 

Than I brought back the disk online using Disk Management 

![4483885215_d8941f5c7f_o[1]](/images/4483885215_d8941f5c7f_o%5B1%5D.png "4483885215_d8941f5c7f_o[1]") 

And everything went back to normal

![4483892519_916117bcb0_o[1]](/images/4483892519_916117bcb0_o%5B1%5D.png "4483892519_916117bcb0_o[1]")
