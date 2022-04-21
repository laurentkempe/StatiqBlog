---
title: "Build and Deployment automation, VCS Root and Labeling in TeamCity"
permalink: /2010/06/03/Build-and-Deployment-automation-VCS-Root-and-Labeling-in-TeamCity/
date: 6/3/2010 3:11:49 AM
updated: 6/3/2010 3:11:49 AM
disqusIdentifier: 20100603031149
tags: ["Team City", "Jobping", "Tech Head Brothers"]
alias:
 - /post/Build-and-Deployment-automation-VCS-Root-and-Labeling-in-TeamCity.aspx/index.html
---
As you might now from reading my blog I tend to automate as much as I can. 

Why? Because I hate to do repetitive tasks. First because it is boring, at least for me, and as a developer we have more interesting things to do. Second because executing repetitive tasks tend to be error prone. 
<!-- more -->

So last week I decided that it was enough for me to have to create manually a subversion tag for [Jobping](http://www.jobping.com "Job posting web site aimed specifically at job seekers and employers who work with Microsoft technologies") web site then also took the time to do the same for my portal [Tech Head Brothers](http://www.techheadbrothers.com/).

At [Jobping](http://www.jobping.com/ "Job posting web site aimed specifically at job seekers and employers who work with Microsoft technologies") we are using [TeamCity](http://www.jetbrains.com/teamcity/index.html) and Subversion. We use it as our Continuous Integration system but also to deploy to production server in one click. Something I promised to talk later on in more details.

So it was relatively easy to configure TeamCity so that after build/deployment process it tags our subversion.

First of all, I realized only the other day the way TeamCity works with [VCS root](http://confluence.jetbrains.net/display/TCD5/Configuring+VCS+Roots) 

> A VCS Root is a set of settings that defines how TeamCity communicates with a version control (SCM) system to monitor for changes and to get sources for a build.

We are using the convention of trunk, branches, tags in our Subversion server (which is the great [Visual SVN Server](http://www.visualsvn.com/server/)). In the past I always set the TeamCity VCS Root to our myproject/trunk url, and I have seen lots of people doing so on different websites/blogs…

At this point I realized that I missed a point with TeamCity VCS Root is that it contains Root in it’s name. So I might be using myproject/ in place of myproject/trunk as the VCS Root. But wait, if I do that TeamCity agent will make a checkout of my whole Subversion repository! 

And here comes the [VCS Checkout rules](http://confluence.jetbrains.net/display/TCD5/VCS+Checkout+Rules), which you can configure on every project which is using the VCS Root

![Build and Deployment automation, VCS Root and Labeling in TeamCity 1](https://farm2.staticflickr.com/1462/24579562215_0c560155da_o.png)

So in my case it looks like that

![Build and Deployment automation, VCS Root and Labeling in TeamCity 2](https://farm2.staticflickr.com/1513/24497267931_83cf76a446_o.png)

Which specify that for that particular build I want to checkout from my VCS Root extended with trunk to the build folder /. So with that I restrict the checkout to the trunk. Good

Now back to my first topic, which was to automate the subversion tag creation when our build is successful which is done using TeamCity [VCS Labeling](http://confluence.jetbrains.net/display/TCD5/VCS+Labeling).

Here is how we set it up for our staging build

![Build and Deployment automation, VCS Root and Labeling in TeamCity 3](https://farm2.staticflickr.com/1636/24471243112_136c3c3d4e_o.png)

For sure we have another one for our production build.

And here is the result in Subversion

![Build and Deployment automation, VCS Root and Labeling in TeamCity 4](https://farm2.staticflickr.com/1527/24579562315_bb8aa7385e_o.png)

Another thing that I will not forget as it is automated

One last issue that I had which you see only if you register to get info about failed build in TeamCity is the following

> ### [TeamCity, LABELING FAILED] Staging - CI Trunk, Unit Tests, Deploy #1072
> 
> Jetbrains.buildServer.vcs.VcsException:
> 
> **Labeling the path 'trunk' to 'tags/staging-1072' has failed with the error**: svn: MKACTIVITY of '/svn/!svn/act/7caf50f8-2801-0010-9e80-1fedd46c5a33': **403 Forbidden **

With this error message it was then easy to figure it out. I just had to modify the access right of the build user to have read/write access right and not only read.
