---
title: "Sending your Git branch changes as an email attachment"
permalink: /2012/04/13/Sending-your-Git-branch-changes-as-an-email-attachment/
date: 4/13/2012 9:23:11 AM
updated: 4/13/2012 7:28:11 PM
disqusIdentifier: 20120413092311
coverImage: https://farm8.staticflickr.com/7046/6829846914_d0c6a8da4a_h.jpg
coverSize: partial
thumbnailImage: https://farm8.staticflickr.com/7046/6829846914_6d316434c6_q.jpg
coverCaption: "Anse Noire, Martinique"
tags: ["DVCS", "Git", "PowerShell"]
alias:
 - /post/Sending-your-Git-branch-changes-as-an-email-attachment.aspx/index.html
---
<!-- [![IMG_1586](http://farm8.staticflickr.com/7046/6829846914_6d316434c6_m.jpg)](http://www.flickr.com/photos/laurentkempe/6829846914/ "IMG_1586 by Laurent Kempé, on Flickr") -->
The other day I wanted to send per email some code to a friend which doesn’t use [Git](http://git-scm.com/). He is using Svn and I use [Git Svn](http://schacon.github.com/git/git-svn.html) in front of this Svn repository. Why I do that? Don’t get me started…

So he couldn’t pull from my repo and we were kind of stuck. Really?!? For sure not, here was the goal I set as I am sure this will happen some other time: having the computer work for me. What a strange idea you would say! Yeah, the computer working for you. At the end aren’t we here to make the cool things and let the computer do the boring things?
<!-- more -->

Ok so I came up with a quick hack. I wanted to have a way to send all the new, or modified, files of my current Git branch per email as a zip attachment. Guess what it was quite easy even for a PowerShell beginner like me.

First of all I needed first to be able to determine on which Git branch I was curently. I googled and find the following

> function Get-GitBranch {
>     $symbolicref = git symbolic-ref HEAD
>     $branch = $symbolicref.substring($symbolicref.LastIndexOf("/") +1)
>     return $branch
> }

Then I wanted to be able to Zip all the files, but to achieve I had to determine which were the files to Zip. this is done using the following 

> git diff --name-only HEAD..master

And making a Zip out of the list of files is done like this

> function Zip-GitBranch([string]$zipFilename) {
> 
>     $branch = Get-GitBranch
>    
>     if (!$zipFilename) {
>         $zipFilename = [string]::Format(".\{0}.zip", $branch)
>     }
> 
>     $files = git diff --name-only HEAD..master
> 
>     foreach($file in $files) {
>          & 'C:\Program Files\7-Zip\7z.exe' a $zipFilename $file
>     }
>    
>     return $zipFilename
> }

*<font face="Georgia">Finally I wanted to be able to send the zip as an attachment of an email using Outlook</font>*

> function MailZip-GitBanch($Recipient) {
>    
>     if (!$Recipient) {
>         Write-Host "You need to pass the email of the recipient as parameter"
>         return
>     }
>    
>     $branch = Get-GitBranch
>     $zipFilename = [string]::Format(".\{0}.zip", $branch)
>     $attachement = [IO.Path]::GetFullPath( $zipFilename )
> 
>     Zip-GitBranch($attachement)
>        
>     $ol = New-Object -comObject Outlook.Application
>     $Mail = $ol.CreateItem(0)
>     $Mail.Recipients.Add($Recipient)
>     $Mail.Subject = "Changes for the branch: " + $branch
>     $Mail.Body = "Check out the email attachement to see the changes made to the branch: " + $branch
>     $Mail.Attachments.Add($attachement)
>     $Mail.Send()
> }

*<font face="Georgia">Now I can type the following command to send my changes to my friend</font>*

> *<font face="Georgia">MailZip-GitBranch myemail@email.com</font>*

*<font face="Georgia">You can find the whole script on the [following gist](https://gist.github.com/2371417).</font>*
