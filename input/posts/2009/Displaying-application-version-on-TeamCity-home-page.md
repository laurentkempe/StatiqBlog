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

![](/images/2009/Displaying-application-version-on-TeamCity-home-page-1.png)

When your software is built it will now display the version:

![](/images/2009/Displaying-application-version-on-TeamCity-home-page-2.png)