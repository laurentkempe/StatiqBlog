---
title: "Back to correct speed"
permalink: /2004/11/16/Back-to-correct-speed/
date: 11/16/2004 6:02:00 AM
updated: 11/16/2004 6:02:00 AM
disqusIdentifier: 20041116060200
tags: ["Infrastructure"]
alias:
 - /post/Back-to-correct-speed.aspx/index.html
---
Over the weekend I realized that my notebook became quite slow and was always reading something on the HD, even without any memory pressure. When I was to some music, starting an application was really corrupting the sound. So after some I realized that my Primary IDE controller went from DMA back to the old PIO mode. To solve this issue I reinstalled the the primary IDE port using the Device Manager, and after two reboots it went back to DMA and a acceptable speed.

You might find more information about that issue here: [IDE ATA and ATAPI disks use PIO mode after multiple time-out or CRC errors occur](http://support.microsoft.com/?kbid=817472).
<!-- more -->

Looking to the cause in the KnowledgeBase: "After the Windows IDE/ATAPI Port driver (Atapi.sys) receives a cumulative total of six time-out or cyclical redundancy check (CRC) errors, the driver reduces the communications speed (the transfer mode) from the highest Direct Memory Access (DMA) mode to lower DMA modes in steps. If the driver continues to receive time-out or CRC errors, the driver eventually reduces the transfer mode to the slowest mode (PIO mode)." makes me thing that I need to take care about my HD, so I made a backup.
