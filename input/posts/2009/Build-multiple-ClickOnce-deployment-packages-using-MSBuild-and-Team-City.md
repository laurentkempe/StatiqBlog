---
title: "Build multiple ClickOnce deployment packages using MSBuild and Team City"
permalink: /2009/11/03/Build-multiple-ClickOnce-deployment-packages-using-MSBuild-and-Team-City/
date: 11/3/2009 5:10:52 PM
updated: 11/3/2009 5:10:52 PM
disqusIdentifier: 20091103051052
tags: ["continuous integration", "Team City", "MSBuild", "ClickOnce"]
---
The other day I posted about [Build ClickOnce deployment packages using MSBuild and Team City](http://weblogs.asp.net/lkempe/archive/2009/10/27/build-clickonce-deployment-packages-using-msbuild-and-team-city.aspx), and there were something that I didn’t liked in my way of doing it.

I have multiple ClickOnce deployment packages created using TeamCity and MSBuild but each ClickOnce packages have their own Application Revision due to the usage of TeamCity [BUILD_NUMBER server build property](http://www.jetbrains.net/confluence/display/TCD4/Predefined+Properties).
<!-- more -->

So I changed to use TeamCity [BUILD_VCS_NUMBER_<simplified VCS root name>](http://www.jetbrains.net/confluence/display/TCD4/Predefined+Properties)

````xml
<ApplicationRevision>$(BUILD_VCS_NUMBER_MyApplication_Trunk)</ApplicationRevision>
````

And now all my ClickOnce packages have the same Application Revision which is better !
