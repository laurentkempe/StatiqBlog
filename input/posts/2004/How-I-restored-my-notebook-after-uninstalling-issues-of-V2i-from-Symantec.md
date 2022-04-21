---
title: "How I restored my notebook after uninstalling issues of V2i from Symantec"
permalink: /2004/07/08/How-I-restored-my-notebook-after-uninstalling-issues-of-V2i-from-Symantec/
date: 7/8/2004 6:43:00 AM
updated: 7/8/2004 6:43:00 AM
disqusIdentifier: 20040708064300
tags: ["Infrastructure"]
alias:
 - /post/How-I-restored-my-notebook-after-uninstalling-issues-of-V2i-from-Symantec.aspx/index.html
---
<P>After uninstalling V2i from Symantec I was asked to reboot my notebook. The reboot took for ever so I decide after 30 minutes to switch it off. Problems started when I rebooted, lots of drivers where missing and it seems that I lost the plug and play features. One of the first driver I decided to recover was the Sound driver. This is the way I recover from that sound driver issue.</P>
<OL>
<LI>In services, check that WinAudio service is enabled, set to automatic, and running.</LI>
<LI>In Device Manager, System Devices check that Plug and Play Software Device Enumerator is installed and running</LI></OL>
<P>If not , then you need to reinstall it:</P>
<OL>
<LI>Copy machine.inf from %windir%\inf to your desktop</LI>
<LI>Remove line ExcludeFromSelect=*</LI>
<LI>Use Add new hardware wizard using the Have Disk option</LI>
<LI>Select the machine.inf you saved on the desktop</LI>
<LI>Install Plug and Play Software Device</LI></OL>
<P>If the sound card is listed in the device manager, then uninstall it and start a scan for hardware changes. Afer installing it again I got the sound back live.</P>
<P>I did the same for several other drivers that were missing on my notebook. And now it seems to be ok.</P>
