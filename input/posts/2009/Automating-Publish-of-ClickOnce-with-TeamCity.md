---
title: "Automating Publish of ClickOnce with TeamCity"
permalink: /2009/11/11/Automating-Publish-of-ClickOnce-with-TeamCity/
date: 11/11/2009 8:24:47 PM
updated: 11/11/2009 8:24:47 PM
disqusIdentifier: 20091111082447
tags: ["continuous integration", "Team City", "MSBuild", "ClickOnce"]
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
2.  Use FileUpdate of [MSBuild Community Tasks](http://msbuildtasks.tigris.org/) to search the 2.0.0.x string and replace it with the version 

```xml
<!-- Deploy Click Once-->
<Target Name="DeployClickOnce">

  <Message Text="####### Deploy ClickOnce $(Configuration)|$(Platform)  ---------#" />

  <Exec Command="xcopy /E /Y $(ClickOnceSrc)*.* $(ClickOnceDestination)" />

  <Copy SourceFiles="$(ClickOnceDestination)\Publish.htm.ori"
        DestinationFiles="$(ClickOnceDestination)\Publish.htm" />

  <FileUpdate Files="$(ClickOnceDestination)\Publish.htm"
              Regex="2.0.0.x"
              ReplacementText="$(FullVersion)" />

</Target>
```

and the FullVersion is defined as this, using TeamCity [BUILD_VCS_NUMBER](http://www.jetbrains.net/confluence/display/TCD4/Predefined+Properties), which is Latest VCS revision:

```xml
<Major>2</Major>
<Minor>0</Minor>
<Build>0</Build>
<Revision>$(BUILD_VCS_NUMBER_app_Trunk)</Revision>
<FullVersion>\((Major).\)(Minor).\((Build).\)(Revision)</FullVersion>
```  

And now the Publish webpage display the version correctly!

![Automating Publish of ClickOnce with TeamCity version](/images/2009/Automating-Publish-of-ClickOnce-with-TeamCity.png)
