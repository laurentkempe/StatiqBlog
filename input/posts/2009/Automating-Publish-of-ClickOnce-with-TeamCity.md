---
title: "Automating Publish of ClickOnce with TeamCity"
permalink: /2009/11/11/Automating-Publish-of-ClickOnce-with-TeamCity/
date: 11/11/2009 8:24:47 PM
updated: 11/11/2009 8:24:47 PM
disqusIdentifier: 20091111082447
tags: ["continuous integration", "Team City", "MSBuild", "ClickOnce"]
alias:
 - /post/Automating-Publish-of-ClickOnce-with-TeamCity.aspx/index.html
---
The other day I published different posts about the way I automated our build process at [Innoveo Solutions](http://www.innoveo.com/) to generate different ClickOnce setup using [TeamCity](http://www.jetbrains.com/teamcity/index.html): 

[Build multiple ClickOnce deployment packages using MSBuild and Team City](http://weblogs.asp.net/lkempe/archive/2009/11/03/build-multiple-clickonce-deployment-packages-using-msbuild-and-team-city.aspx)      
<!-- more -->
[Building ClickOnce with TeamCity](http://weblogs.asp.net/lkempe/archive/2009/11/02/building-clickonce-with-teamcity.aspx)      
[ClickOnce certificate and TeamCity](http://weblogs.asp.net/lkempe/archive/2009/11/02/clickonce-certificate-and-teamcity.aspx)      
[Build ClickOnce deployment packages using MSBuild and Team City](http://weblogs.asp.net/lkempe/archive/2009/10/27/build-clickonce-deployment-packages-using-msbuild-and-team-city.aspx)

Yesterday I was asked to solve one minor issue. At ClickOnce publishing time the publish.htm file was not generated so the ClickOnce version number on the web page wasn’t shown. The publish.htm file is a static file on the targeted deploy directory and IIS uses that file. The file contains a hard coded version 2.0.0.x.

So from a user perspective it was difficult to know if there were a new version. So I was asked to show the correct version.

I knew from past research a way to handle this from the following post: [How To: Generate publish.htm with MSBuild](http://blogs.msdn.com/mwade/archive/2009/02/28/how-to-generate-publish-htm-with-msbuild.aspx)

But I went to a more pragmatic solution, as I already had the [MSBuild Community Tasks](http://msbuildtasks.tigris.org/).

I made a copy of Publish.htm to Publish.htm.ori on each targeted deploy directory.

Then I modified my MSBuild script to do the following:

1.  Copy Publish.html.ori to Publish.htm
2.  Use FileUpdate of [MSBuild Community Tasks](http://msbuildtasks.tigris.org/) to search the 2.0.0.x string and replace it with the version  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:75c64077-df36-4b1f-aa27-b6be7cb8c39c" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> 

1.  <span style="color:#0000ff"><!--</span><span style="color:#008000"> Deploy Click Once</span><span style="color:#0000ff">--></span>
2.  <span style="color:#0000ff"><</span><span style="color:#a31515">Target</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Name</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">DeployClickOnce</span>"<span style="color:#0000ff">></span>
3.    <span style="color:#0000ff"><</span><span style="color:#a31515">Message</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Text</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">####### Deploy ClickOnce $(Configuration)|$(Platform)  ---------#</span>"<span style="color:#0000ff"> /></span>
4.    <span style="color:#0000ff"><</span><span style="color:#a31515">Exec</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Command</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">xcopy /E /Y $(ClickOnceSrc)\*.* $(ClickOnceDestination)</span>"<span style="color:#0000ff"> /></span>
5.    <span style="color:#0000ff"><</span><span style="color:#a31515">Copy</span><span style="color:#0000ff"> </span><span style="color:#ff0000">SourceFiles</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">$(ClickOnceDestination)\Publish.htm.ori</span>"<span style="color:#0000ff"> </span><span style="color:#ff0000">DestinationFiles</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">$(ClickOnceDestination)\Publish.htm</span>"<span style="color:#0000ff"> /></span>
6.    <span style="color:#0000ff"><</span><span style="color:#a31515">FileUpdate</span>
7.      <span style="color:#0000ff"></span><span style="color:#ff0000">Files</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">$(ClickOnceDestination)\Publish.htm</span>"
8.      <span style="color:#0000ff"></span><span style="color:#ff0000">Regex</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">2.0.0.x</span>"
9.      <span style="color:#0000ff"></span><span style="color:#ff0000">ReplacementText</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">$(FullVersion)</span>"<span style="color:#0000ff"> /></span>
10.  <span style="color:#0000ff"></</span><span style="color:#a31515">Target</span><span style="color:#0000ff">></span> </div> </div> </div>  

and the FullVersion is defined as this, using TeamCity [BUILD_VCS_NUMBER](http://www.jetbrains.net/confluence/display/TCD4/Predefined+Properties), which is Latest VCS revision:
  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:b9120155-0f8c-4e96-bcd7-88d1207c1621" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> 

1.  <span style="color:#0000ff"><</span><span style="color:#a31515">Major</span><span style="color:#0000ff">></span>2<span style="color:#0000ff"></</span><span style="color:#a31515">Major</span><span style="color:#0000ff">></span>
2.  <span style="color:#0000ff"><</span><span style="color:#a31515">Minor</span><span style="color:#0000ff">></span>0<span style="color:#0000ff"></</span><span style="color:#a31515">Minor</span><span style="color:#0000ff">></span>
3.  <span style="color:#0000ff"><</span><span style="color:#a31515">Build</span><span style="color:#0000ff">></span>0<span style="color:#0000ff"></</span><span style="color:#a31515">Build</span><span style="color:#0000ff">></span>
4.  <span style="color:#0000ff"><</span><span style="color:#a31515">Revision</span><span style="color:#0000ff">></span>$(BUILD_VCS_NUMBER_app_Trunk)<span style="color:#0000ff"></</span><span style="color:#a31515">Revision</span><span style="color:#0000ff">></span>
5.  <span style="color:#0000ff"><</span><span style="color:#a31515">FullVersion</span><span style="color:#0000ff">></span>$(Major).$(Minor).$(Build).$(Revision)<span style="color:#0000ff"></</span><span style="color:#a31515">FullVersion</span><span style="color:#0000ff">>  </</span><span style="color:#a31515">PropertyGroup</span><span style="color:#0000ff">></span> </div> </div> </div>  

And now the Publish webpage display the version correctly!

[![4094558371_70b24140cc_o[1]](http://weblogs.asp.net/blogs/lkempe/4094558371_70b24140cc_o1_thumb_5F82F1DE.png "4094558371_70b24140cc_o[1]")](http://weblogs.asp.net/blogs/lkempe/4094558371_70b24140cc_o1_670E614B.png)
