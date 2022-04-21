---
title: "Displaying application version on TeamCity home page"
permalink: /2009/11/12/Displaying-application-version-on-TeamCity-home-page/
date: 11/12/2009 6:00:06 PM
updated: 11/12/2009 6:00:06 PM
disqusIdentifier: 20091112060006
tags: ["Team City"]
alias:
 - /post/Displaying-application-version-on-TeamCity-home-page.aspx/index.html
---
I wanted today to display the application version in front of our builds, something I already did on the past but never with the VCS version.

In fact I used the same [BUILD_VCS_NUMBER_<simplified VCS root name>](http://www.jetbrains.net/confluence/display/TCD4/Predefined+Properties) that I described here: [Build multiple ClickOnce deployment packages using MSBuild and Team City](http://weblogs.asp.net/lkempe/archive/2009/11/03/build-multiple-clickonce-deployment-packages-using-msbuild-and-team-city.aspx)
<!-- more -->

Go in the General Settings of your build and adapt as following:

[![4097914182_2e086aabc1_o[1]](http://weblogs.asp.net/blogs/lkempe/4097914182_2e086aabc1_o1_thumb_56C71DFD.png "4097914182_2e086aabc1_o[1]")](http://weblogs.asp.net/blogs/lkempe/4097914182_2e086aabc1_o1_2739253E.png) 

When your software is built it will now display the version:

[![4097158651_22d00c0ac1_o[1]](http://weblogs.asp.net/blogs/lkempe/4097158651_22d00c0ac1_o1_thumb_2101B2D6.png "4097158651_22d00c0ac1_o[1]")](http://weblogs.asp.net/blogs/lkempe/4097158651_22d00c0ac1_o1_54AA1F34.png)
