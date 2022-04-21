---
title: "How to connect to SQL Azure"
permalink: /2009/08/21/How-to-connect-to-SQL-Azure/
date: 8/21/2009 6:49:16 PM
updated: 8/21/2009 6:49:16 PM
disqusIdentifier: 20090821064916
tags: ["Azure", "SQL Azure"]
alias:
 - /post/How-to-connect-to-SQL-Azure.aspx/index.html
---
**From Microsoft SQL Server Management Studio** (tips from the [SQL Azure — Getting Started](http://social.msdn.microsoft.com/Forums/en-US/ssdsgetstarted/threads "SQL Azure — Getting Started") forum, [this post](http://social.msdn.microsoft.com/Forums/en-US/ssdsgetstarted/thread/aca1d494-0b52-4661-b022-86c4101ba6ca)) :

> Please be aware that SSMS is not supported with SQL Azure yet. However, you can use the following workaround for the time being:
<!-- more -->
> 
> 1. Click Cancel in Connect to Server Dialog
> 2. Click New Query which will bring up the new connection dialog.
> 3. Enter your credentials. By default, you will connect to Master database. If you want to connect to specific database, click options and enter the database name. Please be aware that USE <database> is not supported.
> 4. Now click Connect and you will get an error "Unable to apply connection settings. The detailed error message is: ‘ANSI_NULLS’ is not a recognized SET option. "
> 5. Click Ok.
> 
> After this step, you should be connected to the desired database and you can start running queries.
> Thanks,
> Abi

Here are the information you need to enter into Microsoft SQL Server Management Studio. You will get those from 

[![3842366844_c64803c044_o[1]](http://weblogs.asp.net/blogs/lkempe/3842366844_c64803c044_o1_thumb_4C39FECF.png "3842366844_c64803c044_o[1]")](http://weblogs.asp.net/blogs/lkempe/3842366844_c64803c044_o1_2FFCE27C.png) 

You will get this error message, just click OK

[![3842373284_54932c057f_o[1]](http://weblogs.asp.net/blogs/lkempe/3842373284_54932c057f_o1_thumb_317D3CE9.png "3842373284_54932c057f_o[1]")](http://weblogs.asp.net/blogs/lkempe/3842373284_54932c057f_o1_6CB118B4.png) 

According to [Stan Kitsis – MSFT](http://social.msdn.microsoft.com/Profile/en-US/?user=Stan%20Kitsis%20-%20MSFT&referrer=http%3a%2f%2fsocial.msdn.microsoft.com%2fForums%2fen-US%2fssdsgetstarted%2fthread%2f73a35b8d-28d8-442e-9589-27d1c38ece6f&rh=2E4GwI8mKjrhaaGn4hTAEWcMRxrCexEDmfuOVZ5mzwU%3d&sp=forums)<abbr> (MSFT</abbr><abbr>, Moderator) ([link](http://social.msdn.microsoft.com/Forums/en-US/ssdsgetstarted/thread/73a35b8d-28d8-442e-9589-27d1c38ece6f)): </abbr>

> I'm not aware of a way to get around this message.  We are planning to enable support for SET ANSI_NULLS in the next service update so hopefully the experience will be better.
> Stan
> 
> * * *
> Program Manager, SQL Azure

I also tried to run aspnet_regsql.exe without any success, there are still lots of features which seems to be not supported at the moment.
