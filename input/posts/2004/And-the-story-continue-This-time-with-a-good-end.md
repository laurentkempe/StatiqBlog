---
title: "And the story continue... This time with a good end"
permalink: /2004/05/14/And-the-story-continue-This-time-with-a-good-end/
date: 5/14/2004 5:36:00 AM
updated: 5/14/2004 5:36:00 AM
disqusIdentifier: 20040514053600
tags: ["Work"]
alias:
 - /post/And-the-story-continue-This-time-with-a-good-end.aspx/index.html
---
After all servers problems from the last days, now I have an issue with IIS 5 on my notebook. I am currently working on a project for one of our subsidiaries in which we have to integrate a backend calculator application. In that project I have to restart IIS with the command <em>iisreset</em> to be able to compile my code and to deploy it, otherwise my dll is locked by IIS. I did it for sometime, and today I worked on the project and when I decided to restart IIS then I get an error message: "<strong>No such interface supported</strong>". What the hell is this? Come on I need to work. I searched a couple of hours on the web trying different things then I end up uninstalling IIS from my machine rebooting and manually deleting files that where lying on my hard drive. Then after the 128th reboot of the last two weeks I reinstalled it and it worked. :-) :-) :-) 

<strong><font size="2">Now for the really cool thing</font></strong> (there is always blue sky after a storm ;) I found a weblog entry by Steven M. Cohn: [Multiple IIS Virtual Servers on XP Pro](http://weblogs.asp.net/stevencohn/articles/59782.aspx). I really encourage you to read it if you develop ASP.NET websites on Windows XP Pro. Now I have multiple IIS virtual servers running on my notebook and I can switch from one to the other. Awesome.
<!-- more -->

On the third Virtual Server I had to install [Microsoft SOAP Toolkit 3.0](http://www.microsoft.com/downloads/details.aspx?FamilyId=C943C0DD-CEEC-4088-9753-86F052EC8450&displaylang=en) with there command <em>SOAPVDIR.CMD</em>. Reading the script, I realized that there is an option I never saw before -s that let you define the server. So in my case I had to run:

SOAPVDIR.CMD UPDATE myapp -S:3

At least the end of the day was positive.
