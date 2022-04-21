---
title: "Watch Several TeamCity Servers With Windows Tray Notifier"
permalink: /2009/11/05/Watch-Several-TeamCity-Servers-With-Windows-Tray-Notifier/
date: 11/5/2009 4:49:39 AM
updated: 11/5/2009 4:49:39 AM
disqusIdentifier: 20091105044939
tags: ["Team City"]
alias:
 - /post/Watch-Several-TeamCity-Servers-With-Windows-Tray-Notifier.aspx/index.html
---
Browsing [TeamCity](http://www.jetbrains.com/teamcity/index.html) documentation this evening I discovered that it is possible to [Watch Several TeamCity Servers With Windows Tray Notifier](http://www.jetbrains.net/confluence/display/TCD5/How+To...#HowTo...-WatchSeveralTeamCityServersWithWindowsTrayNotifier)

> TeamCity Tray Notifier is used normally to watch builds and receive notifications from a single TeamCity server.
<!-- more -->
> 
> In situations, when you have more than one TeamCity server, and want to monitor them with Windows Tray Notifier simultaneously, you need to start a separate instance of Tray Notifier for each of the servers from the command line with the <tt>/allowMultiple</tt> option:
> 
> *   navigate to TeamCity Tray notifier installation folder (by default, it's <tt>C:\Program Files\JetBrains\TeamCity</tt> and run the command:
> 
> JetBrains.TrayNotifier.exe /allowMultiple
> 
> Optionally, for each of the Tray Notifier instances you can specify explicitly the URL of the the server to connect to with the <tt>/server</tt> option. Otherwise, for each further tray notifier instance you will need to log out and change server's URL via UI.
> 
> JetBrains.TrayNotifier.exe /allowMultiple /server:http://myTeamCityServer
> 
> See also [details](http://jetbrains.net/tracker/issue/TW-4230#comment=27-14194) in the issue tracker.

So now I am able to have two TeamCity Tray notifier open:

[![](http://weblogs.asp.net/blogs/lkempe/4075997580_e7abf7683e_o1_thumb_29F29315.png)](http://weblogs.asp.net/blogs/lkempe/4075997580_e7abf7683e_o1_5C2A092C.png) 

What I also like is to be able to start programs by typing the Windows key and then some text, here for example “te”

[![4076003432_6c2b8f449f_o[1]](http://weblogs.asp.net/blogs/lkempe/4076003432_6c2b8f449f_o1_thumb_2BEF4644.png "4076003432_6c2b8f449f_o[1]")](http://weblogs.asp.net/blogs/lkempe/4076003432_6c2b8f449f_o1_01638B34.png) 

To achieve this I created three shortcuts with the different servers configuration and then I placed the shortcuts in my folder C:\Users\Laurent\AppData\Roaming\Microsoft\Internet Explorer\Quick Launch\User Pinned\StartMenu in which it is indexed.

Nice!
