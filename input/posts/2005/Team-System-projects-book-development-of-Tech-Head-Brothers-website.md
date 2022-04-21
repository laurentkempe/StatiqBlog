---
title: "Team System projects: book, development of Tech Head Brothers website"
permalink: /2005/07/16/Team-System-projects-book-development-of-Tech-Head-Brothers-website/
date: 7/16/2005 6:05:00 AM
updated: 7/16/2005 6:05:00 AM
disqusIdentifier: 20050716060500
tags: ["Tech Head Brothers", "ASP.NET 2.0", "Team System"]
alias:
 - /post/Team-System-projects-book-development-of-Tech-Head-Brothers-website.aspx/index.html
---
First project about Team System is a book that I started with my friend 
[Kader 
Yildirim](http://www.techheadbrothers.com/DesktopDefault.aspx?tabindex=7&tabid=19&id=5). We had a first contact with one editor and were disapointed a bit 
because we were told that there is no 'market' for such a book. I really think 
that there is, because Team System is an awesome tool. Nevermind we will 
continue to search an editor. By the way if you are interested, please feel free 
to [contact me](mailto:laurent.kempe@techheadbrothers.com). One side 
note, the book will be written in French.

The second project with my brother [Mathieu](http://myaustraliantrip.blogspot.com/) is to 
update our website [Tech Head 
Brothers](http://www.techheadbrothers.com/) to use ASP.NET 2. Currently [Mathieu](http://www.techheadbrothers.com/DesktopDefault.aspx?tabindex=7&tabid=19&id=3) 
is located in Sydney, Australia and will soon move to Perth, Australia because 
he found a new job there. So to work on such condition we need really good tools 
to communicate, [Groove Virtual 
Office](http://www.groove.net/home/index.cfm), [Skype](http://www.skype.com/) and [MSN 
Messenger](http://www.techheadbrothers.com/DesktopDefault.aspx?tabindex=7&tabid=19&id=3). Ok with those tools we can have in real time discussions, share 
idea, even share documents in online/offline mode. But we missed a Software 
Development Lifecycle Tool (SDLC). At the time of the website version written in 
ASP.NET 1.0/1.1, we had tried several solutions from Visual SourceSafe over 
Internet, over VPN, then we moved to CVS because it was too slow. We also had [Draco.NET](http://draconet.sourceforge.net/)...
<!-- more -->

Now our solution is the following one:

1.  One Windows 2003 Server with Virtual Server and Team System installed on 
  it (Thanks to Fox). We used the Visual Studio 2005 Team System Beta 2 VPC 
  provided during the Tech Ed 2005 in Amsterdam.
2.  A512/128 Kb ADSL internet connection access to the server with a 
  dynamic ip, thanks to [DynDNS](http://www.dyndns.org)
3.  Mathieu is using a custom installation of Visual Studio 2005 Beta 2 
  installion in a Virtual PC on his notebook
4.  I am also using a VPC with Visual Studio 2005 Beta 2 installed


After one week of usage I have to say that it is working not so bad. For sure 
we have issues because of the adsl connection that get disconnected and DynDNS 
taking a bit time to update to the new IP. But I am ashtonished about the way it 
works and I like the source repository and the work items.
