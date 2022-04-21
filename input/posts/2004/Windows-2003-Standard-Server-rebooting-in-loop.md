---
title: "Windows 2003 Standard Server rebooting in loop"
permalink: /2004/05/14/Windows-2003-Standard-Server-rebooting-in-loop/
date: 5/14/2004 4:58:00 AM
updated: 5/14/2004 4:58:00 AM
disqusIdentifier: 20040514045800
tags: ["Work"]
alias:
 - /post/Windows-2003-Standard-Server-rebooting-in-loop.aspx/index.html
---
I could go a bit further with my issue to have a Windows 2003 Standard server running. As mentioned here: [Windows 2003 crashes after patching - Beginning of another week of hell :-(](http://weblogs.asp.net/lkempe/archive/2004/05/11/129871.aspx), I had a hard time to fix this but with some patience and pushing Linux fan colleagues I got it to work for... 10 minutes.

So there is two things I found:
<!-- more -->

<ol>
<li>If your computer at reboot display an error message saying that the file winn\system32\config\system is corrupt then, you might fix the problem by booting in repair mode from the Windows installation CD, and <strong>copy</strong> the file from <strong>winnt\repair\system</strong> to <strong>winn\system32\config\system</strong>. A <strong>chkdsk c: /p /r</strong> fix it also for some time. 
<li>If you try to apply a Windows Update patch and it does not want to install then try one of those solutions listed in the Knowledge Base entry 822798: [You cannot install some updates or programs](http://support.microsoft.com/default.aspx?scid=kb;EN-US;822798)</li></li></ol>


Ok so what's up? Now that I have my server running, patched with today's 14 Windows 2003 Server patches, firewall activated on all network cards and Symantec Antivirus installed and up-to-date. I install [MBSA 1.2](http://www.microsoft.com/technet/security/tools/mbsahome.mspx), run it, everything ok. I do a new Windows Update to be sure, 1 new Intel network driver is ready to download, I download it, it install correctly. That's fine. Then I decide to make 2-3 reboots to see if everything is ok. Good. I get back from our server rooms and after 15 minutes I try ping the machine, I get an answer for the first packet but nothing for the second packet :-( I try to connect with Remote Desktop, no way. I go back in the server room to see that the server reboots in loop. It gets to the login dialog and then reboots. I start it in Safe Mode and it works correctly and for sure there is nothing in the Event Logs.

Any idea? I am really open, I mean I am so upset about this server.
