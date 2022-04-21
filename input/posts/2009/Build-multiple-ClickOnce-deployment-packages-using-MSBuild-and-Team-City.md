---
title: "Build multiple ClickOnce deployment packages using MSBuild and Team City"
permalink: /2009/11/03/Build-multiple-ClickOnce-deployment-packages-using-MSBuild-and-Team-City/
date: 11/3/2009 5:10:52 PM
updated: 11/3/2009 5:10:52 PM
disqusIdentifier: 20091103051052
tags: ["continuous integration", "Team City", "MSBuild", "ClickOnce"]
alias:
 - /post/Build-multiple-ClickOnce-deployment-packages-using-MSBuild-and-Team-City.aspx/index.html
---
The other day I posted about [Build ClickOnce deployment packages using MSBuild and Team City](http://weblogs.asp.net/lkempe/archive/2009/10/27/build-clickonce-deployment-packages-using-msbuild-and-team-city.aspx), and there were something that I didn’t liked in my way of doing it.

I have multiple ClickOnce deployment packages created using TeamCity and MSBuild but each ClickOnce packages have their own Application Revision due to the usage of TeamCity [BUILD_NUMBER server build property](http://www.jetbrains.net/confluence/display/TCD4/Predefined+Properties).
<!-- more -->

So I changed to use TeamCity [BUILD_VCS_NUMBER_<simplified VCS root name>](http://www.jetbrains.net/confluence/display/TCD4/Predefined+Properties)
  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:938d7efb-f5b5-4dca-90a2-68001b35ba46" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> 

1.  <span style="color:#0000ff"><!--</span><span style="color:#008000"> ClickOnce getting build number from Team City </span><span style="color:#0000ff">--></span>
2.  <span style="color:#0000ff"><</span><span style="color:#a31515">ApplicationRevision</span><span style="color:#0000ff">></span>$(BUILD_VCS_NUMBER_MyApplication_Trunk)<span style="color:#0000ff"></</span><span style="color:#a31515">ApplicationRevision</span><span style="color:#0000ff">></span> </div> </div> </div>  

And now all my ClickOnce packages have the same Application Revision which is better !
