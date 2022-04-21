---
title: "Run old build using TeamCity"
permalink: /2010/07/10/Run-old-build-using-TeamCity/
date: 7/10/2010 12:59:35 AM
updated: 9/17/2010 9:56:52 PM
disqusIdentifier: 20100710125935
tags: ["Team City", "continuous integration", "innoveo solutions", "Jobping"]
alias:
 - /post/Run-old-build-using-TeamCity.aspx/index.html
---
At [Jobping](http://www.jobping.com) and [Innoveo](http://www.innoveo.com/) we are using [TeamCity](http://www.jetbrains.com/teamcity/index.html) from Jetbrains to automate our different builds.

Today I was asked by my colleague Roy the following interesting question “Can I rerun an old build?”
<!-- more -->

My first thought and question was, “do you have a tag for that old build?” The response was no. With a yes I would have proposed to use what I described in a previous post: [Build and Deployment automation, VCS Root and Labeling in TeamCity](http://www.laurentkempe.com/post/Build-and-Deployment-automation-VCS-Root-and-Labeling-in-TeamCity.aspx). In which we could change the [VCS Checkout rules](http://confluence.jetbrains.net/display/TCD5/VCS+Checkout+Rules)to point to that particular tag and run the build.

Then I searched the TeamCity online documentation and found about [History Build](http://confluence.jetbrains.net/display/TCD5/History+Build) which lead me to [Run Custom Build Dialog](http://confluence.jetbrains.net/display/TCD5/Run+Custom+Build+Dialog)which starts with the following:

> To open the dialog:
> 
> *   Click ellipsis on the Run button

I am using for a long time TeamCity, and I can’t believe that I never pressed that part of that button!

![](http://farm5.static.flickr.com/4137/4777341802_abe7bcf1a5_o_d.png)

Then you get access to the popup in which you can select an older build!

![](http://farm5.static.flickr.com/4095/4777341878_9ce2cf6264_z_d.jpg)

And run the build.
 Which is interesting for example when you have some of your builds which are deploying your web applications and you want to come back to a last stable version.   
