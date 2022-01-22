---
title: Upload files to DropBox from PowerShell
tags:
  - PowerShell
  - continuous integration
permalink: /2016/04/07/Upload-files-to-DropBox-from-PowerShell/
disqusIdentifier: 20160407214132
coverSize: partial
coverCaption: 'Le rocher du diamant depuis le morne larcher, Martinique'
coverImage: 'https://farm2.staticflickr.com/1704/25727719735_62100cdbf1_h.jpg'
thumbnailImage: 'https://farm2.staticflickr.com/1704/25727719735_baf17e5561_q.jpg'
date: 2016-04-07 21:41:32
---
When I [migrated our build to Cake (C# Make)](http://laurentkempe.com/2016/04/05/Moving-to-Cake-CSharp-Make/) we had one requirement which was to upload some of the build output's artifacts to DropBox at the end of the build.
<!-- more -->
I searched for that kind of capabilities in Cake reference documentation but couldn't find anything out of the box.

I could have gone to write my own add-in but I found the [PowerShell](http://cakebuild.net/addins/category/powershell) one, great! 
So I decided that I could write a little PowerShell script to achieve that upload to DropBox.

Here it is

{% gist 9e71a307e1d216d17e5adf1589e51c5e dropbox-upload.ps1 %}

The script is getting one environment variable *DropBoxAccessToken*, which represents the DropBox access token because I don't want to see the access token logged into our TeamCity logs.

One gotcha from [DropBox REST API upload documentation](https://www.dropbox.com/developers/documentation/http/documentation#files-upload) which hit me was how to upload to a business account? In fact, it is really easy, you just need to create an application for your business account with permission full!

Warning, I do not consider myself as a PowerShell expert, I am more of a kind of PowerShell hacker, so take this script with all precautions.
