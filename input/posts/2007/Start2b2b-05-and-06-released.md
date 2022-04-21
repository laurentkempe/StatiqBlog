---
title: "Start++ 0.5 and 0.6 released"
permalink: /2007/05/23/Start2b2b-05-and-06-released/
date: 5/23/2007 4:45:38 AM
updated: 5/23/2007 4:45:38 AM
disqusIdentifier: 20070523044538
tags: ["Tools"]
alias:
 - /post/Start2b2b-05-and-06-released.aspx/index.html
---
> Start++ is an enhancement for the Start Menu in Windows Vista. It also extends the Run box and the command-line with customizable commands.  For example, typing "w Windows Vista" will take you to the Windows Vista page on Wikipedia!

See the full description [on this page](http://brandontools.com/content/StartPlusPlus.aspx).
<!-- more -->

What is new from 0.4.6 to 0.6

**Fixes since 0.4.6**

*   Double-quotes in web searches should work properly now.  UI hook should be more reliable  Fixed some erroneous idle CPU usage when running in the background.  Misc stability improvements. 

New Features  

*   Search Startlets can now be scripted using JavaScript!   Fancy new scripting UI. 

**Changes since 0.5**

*   The included "play" scripts were broken for most people, they are now fixed.  Start++ no longer attempts to hook the Start Menu UI on non-English versions of Windows (support for other language versions will come eventually).  Fixed focus issues when launching commands or elevated apps (hopefully once and for all).  Running a Startlet from the Start Menu no longer starts a new shortlived Start++ process to execute it in, if Start++ is running in the background.  The embedded Start Menu UI shouldn't jump around anymore the first time it's shown.  The "mail" and "play" scripts now show friendly error messages if no results are found.  Script authors can now get at the parameters the user typed in as well as the full search query  Added "Donation / License" tab.  Other misc fixes. 

It is a great tool even if the version 0.4.6 was buggy and crashed often on my machine.
