---
title: "Windows Backup, Xbox 360 media extender and 0x80070002 error"
permalink: /2009/11/03/Windows-Backup-Xbox-360-media-extender-and-0x80070002-error/
date: 11/3/2009 6:57:20 PM
updated: 11/3/2009 6:57:20 PM
disqusIdentifier: 20091103065720
tags: ["Windows 7", "media extender", "xbox 360"]
alias:
 - /post/Windows-Backup-Xbox-360-media-extender-and-0x80070002-error.aspx/index.html
---
Yesterday I bought a new Western Digital external hard drive of 1TB for 109CHF, incredible how cheap it is now.

Then I configured my Windows 7 backup to use this new hard drive, very easy and the backup in less than 2 minutes.
<!-- more -->

At the end the backup failed because of drive having errors, then after a Error-checking everything was fine.

So I restarted the backup and got again an error:

Backup encountered a problem while backing up file C:\Users\Mcx1-xx-PC\Contacts. Error:(The system cannot find the file specified. (0x80070002))     
Backup encountered a problem while backing up file C:\Users\Mcx1-xx-PC\Searches. Error:(The system cannot find the file specified. (0x80070002))      
Backup encountered a problem while backing up file C:\Users\Mcx2-xx-PC\Contacts. Error:(The system cannot find the file specified. (0x80070002))      
Backup encountered a problem while backing up file C:\Users\Mcx2-xx-PC\Searches. Error:(The system cannot find the file specified. (0x80070002))

Searching on the Internet I found a [forum post](http://social.technet.microsoft.com/Forums/en-US/w7itprogeneral/thread/bff71aac-c78e-42bd-a959-116019c93478/) with the following solution:

> This is a known issue with Media center users. For such users, Windows does not create "Contacts" and "Searches" directories which Windows backup expect to be present. As a workaround, please do the following:
> 1. Launch Change backup settings.
> 2. Choose the same backup target and proceed to next page.
> 3. Select "Let me choose".
> 4. Expand the user Mcx1-NEO node in the treeview. Expand "Additional locations".
> 5. Exclude the Contacts and Searches directories for the user.
> 6. Proceed with configuration and save settings.

[![4071003833_d19380d9ab_o[1]](http://weblogs.asp.net/blogs/lkempe/4071003833_d19380d9ab_o1_thumb_762C3546.png "4071003833_d19380d9ab_o[1]")](http://weblogs.asp.net/blogs/lkempe/4071003833_d19380d9ab_o1_0EDC1E31.png) 

Restarted backup and now it works!
