---
title: "Build ClickOnce deployment packages using MSBuild and Team City"
permalink: /2009/10/27/Build-ClickOnce-deployment-packages-using-MSBuild-and-Team-City/
date: 10/27/2009 8:25:26 PM
updated: 10/27/2009 8:25:26 PM
disqusIdentifier: 20091027082526
alias:
 - /post/Build-ClickOnce-deployment-packages-using-MSBuild-and-Team-City.aspx/index.html
---
The other day I was requested to automate our build process to issue different [ClickOnce](http://msdn.microsoft.com/en-us/library/t71a733d(VS.80).aspx) setup for the same application. The main difference was some configuration files pointing to different back end web services.

To start I had to create new build configurations on [Team City](http://www.jetbrains.com/teamcity/index.html) which used the following settings for the Build Runner:
<!-- more -->

1.  **Targets**: Rebuild Publish 
2.  **Configuration**:** **One per build configurations; e.g** **DeployClickOnce, integrationDeployClickOnce   

[![Targets and Configuration](http://weblogs.asp.net/blogs/lkempe/4049721154_08aa444fd1_o1_thumb_12F817BA.png "Targets and Configuration")](http://weblogs.asp.net/blogs/lkempe/4049721154_08aa444fd1_o1_1C3452FB.png) 

Then in my Visual Studio 2008 solution I created several Solution configuration reflecting the different configurations that I needed during my deployment, e.g. DeployClickOnce

[![Visual Studio 2008 solutions](http://weblogs.asp.net/blogs/lkempe/4048977891_8fef52e1df_o1_thumb_4F606697.png "Visual Studio 2008 solutions")](http://weblogs.asp.net/blogs/lkempe/4048977891_8fef52e1df_o1_2D580AD1.png) 

Then using the project properties from the solution explorer in Visual Studio I had to set all Publish options I was interested in; Publish Location, Installation Folder Url, Install Mode and Settings, Prerequisites…

[![4048988539_2a9f77285f_o[1]](http://weblogs.asp.net/blogs/lkempe/4048988539_2a9f77285f_o1_thumb_614C5E8A.png "4048988539_2a9f77285f_o[1]")](http://weblogs.asp.net/blogs/lkempe/4048988539_2a9f77285f_o1_1B4BC744.png)

The issue now is that the Publish Version automatically increment the revision with each publish. But this doesn’t work with our continuous integration server Team City as it would need to checkin the modified file back to subversion. SO a different approach was needed.

The solution I used is to use the [Build Number](http://www.jetbrains.net/confluence/display/TCD4/Predefined+Properties#PredefinedProperties-buildNumber) offered by Team City, so I had to modify the MSBuild script to use the the BUILD_NUMBER. To do that, right click in the Solution Explorer on our project and select Edit Project File:

[![4049748852_2ea06972ca_o[1]](http://weblogs.asp.net/blogs/lkempe/4049748852_2ea06972ca_o1_thumb_238F5101.png "4049748852_2ea06972ca_o[1]")](http://weblogs.asp.net/blogs/lkempe/4049748852_2ea06972ca_o1_650A035A.png) 

Then you will face the your MSBuild script, and you will have to search for the configuration that we defined some steps before:
  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:68c8585d-4f35-4bb1-bc7f-3e5d6368c615" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> 

1.  <span style="color:#0000ff"><</span><span style="color:#a31515">PropertyGroup</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Condition</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff"> '$(Configuration)|$(Platform)' == 'DeployClickOnce|AnyCPU' </span>"<span style="color:#0000ff">></span> </div> </div> </div>  

Then before the end of the closing PropertyGroupd tag add the following lines:
  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:79a05921-b262-4485-a7bb-7cce56f4a693" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> 

1.    <span style="color:#0000ff"><!--</span><span style="color:#008000"> ClickOnce getting build number from Team City </span><span style="color:#0000ff">--></span>
2.    <span style="color:#0000ff"><</span><span style="color:#a31515">ApplicationRevision</span><span style="color:#0000ff">></span>$(BUILD_NUMBER)<span style="color:#0000ff"></</span><span style="color:#a31515">ApplicationRevision</span><span style="color:#0000ff">></span>
3.    <span style="color:#0000ff"><</span><span style="color:#a31515">InstallUrl</span><span style="color:#0000ff">></span>http://myserver.com/<span style="color:#0000ff"></</span><span style="color:#a31515">InstallUrl</span><span style="color:#0000ff">></span>
4.  <span style="color:#0000ff"></</span><span style="color:#a31515">PropertyGroup</span><span style="color:#0000ff">></span> </div> </div> </div>  

**ApplicationRevision **will overwrite the Application revision using the BUILD_NUMBER defined by Team City.

**InstallUrl **is another configuration that we want to override because we want to create multiple ClickOnce setup installed from different urls. So for DeployClickOnce you will have an InstallURL and for integrationDeployClickOnce you will have another one.

Now we are ready for the final step in which we want to exchange some configuration files related to the different ClickOnce builds and Publish the output to an IIS server so that our testers can access the different ClickOnce package for the different stages.

ClickOnce secure the different files that are created with checksums so that they cannot be mitigated during the installment transfer. So the only option we have to be able to exchange our configuration files is before compilation. So we had a Target to our MSBuild script:
  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:416854f2-ad0a-41c8-8f7f-714d3c50093d" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> 

1.  <span style="color:#0000ff"><</span><span style="color:#a31515">Target</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Name</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">BeforeCompile</span>"<span style="color:#0000ff">></span>
2.    <span style="color:#0000ff"><</span><span style="color:#a31515">CallTarget</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Targets</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">ExchangeDefaultSettings</span>"<span style="color:#0000ff"> </span><span style="color:#ff0000">ContinueOnError</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">false</span>"<span style="color:#0000ff"> /></span>
3.    <span style="color:#0000ff"><</span><span style="color:#a31515">CallTarget</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Targets</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">ExchangeAppConfig</span>"<span style="color:#0000ff"> </span><span style="color:#ff0000">ContinueOnError</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">false</span>"<span style="color:#0000ff"> /></span>
4.  <span style="color:#0000ff"></</span><span style="color:#a31515">Target</span><span style="color:#0000ff">></span> </div> </div> </div>  

The BeforeCompile target will be called before each build and will exchange our App.config and another settings file containing stage dependant configuration.

Here is the simple target which exchanges the App.config stored in a configs folder:
  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:c416415d-7f09-45ef-86d7-5b284b6bb880" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> 

1.  <span style="color:#0000ff"><</span><span style="color:#a31515">Target</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Name</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">ExchangeAppConfig</span>"<span style="color:#0000ff">></span>
2.    <span style="color:#0000ff"><</span><span style="color:#a31515">Message</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Text</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">####### CONFIG Exchange $(Configuration)|$(Platform)  ---------#</span>"<span style="color:#0000ff"> /></span>
3.    <span style="color:#0000ff"><</span><span style="color:#a31515">Copy</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Condition</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff"> '$(Configuration)' == 'DeployClickOnce' </span>"<span style="color:#0000ff"> </span>
4.          <span style="color:#0000ff"></span><span style="color:#ff0000">SourceFiles</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">$(SolutionFolder)\Sources\Application\configs\localhost.App.config</span>"<span style="color:#0000ff"> </span>
5.          <span style="color:#0000ff"></span><span style="color:#ff0000">DestinationFiles</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">$(SolutionFolder)\Sources\Application\App.config</span>"<span style="color:#0000ff"> </span>
6.          <span style="color:#0000ff"></span><span style="color:#ff0000">ContinueOnError</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">false</span>"<span style="color:#0000ff"> /></span>
7.    <span style="color:#0000ff"><</span><span style="color:#a31515">Copy</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Condition</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff"> '$(Configuration)' == 'integrationDeployClickOnce' </span>"<span style="color:#0000ff"> </span>
8.          <span style="color:#0000ff"></span><span style="color:#ff0000">SourceFiles</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">$(SolutionFolder)\Sources\Application\configs\integration.App.config</span>"<span style="color:#0000ff"> </span>
9.          <span style="color:#0000ff"></span><span style="color:#ff0000">DestinationFiles</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">$(SolutionFolder)\Sources\Application\App.config</span>"<span style="color:#0000ff"> </span>
10.          <span style="color:#0000ff"></span><span style="color:#ff0000">ContinueOnError</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">false</span>"<span style="color:#0000ff"> /></span>
11.  <span style="color:#0000ff"></</span><span style="color:#a31515">Target</span><span style="color:#0000ff">></span> </div> </div> </div>  

The ExchangeDefaultSettings Target works the same.

So before any compilation of our solution using the DeployClickOnce, integrationDeployClickOnce solution configuration the App.config and the default settings file are exchanged. So after the compilation they will be correct according to the stage that we target.

The final step is to Publish the ClickOnce package created to the IIS server. As we defined the **Targets**: Rebuild Publish, there will be a Rebuild and then a Publish phase in our build script. So now we have to take care of the Publish target.

So we add a Target Publish as here:
  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:0cd3c3b1-a3d4-4711-a329-267c0e87cd04" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> 

1.  <span style="color:#0000ff"><</span><span style="color:#a31515">Target</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Name</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">Publish</span>"<span style="color:#0000ff">></span>
2.    <span style="color:#0000ff"><</span><span style="color:#a31515">CallTarget</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Condition</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff"> '$(Configuration)' == 'DeployClickOnce' </span>"<span style="color:#0000ff"> </span>
3.                <span style="color:#0000ff"></span><span style="color:#ff0000">Targets</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">DeployClickOnce</span>"<span style="color:#0000ff"> </span>
4.                <span style="color:#0000ff"></span><span style="color:#ff0000">ContinueOnError</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">false</span>"<span style="color:#0000ff"> /></span>
5.    <span style="color:#0000ff"><</span><span style="color:#a31515">CallTarget</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Condition</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff"> '$(Configuration)' == 'integrationDeployClickOnce' </span>"<span style="color:#0000ff"> </span>
6.                <span style="color:#0000ff"></span><span style="color:#ff0000">Targets</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">DeployClickOnce</span>"<span style="color:#0000ff"> </span>
7.                <span style="color:#0000ff"></span><span style="color:#ff0000">ContinueOnError</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">false</span>"<span style="color:#0000ff"> /></span>
8.  <span style="color:#0000ff"></</span><span style="color:#a31515">Target</span><span style="color:#0000ff">></span> </div> </div> </div>  

Which just call a Target DeployClickOnce for the configuration we are interested in: DeployClickOnce and integrationDeployClickOnce.

The DeployClickOnce Target is responsible to xcopy the packages created by the Publish Target to the different IIS path used to host our ClickOnce deployment setup:
  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:833c691d-8a00-4ec6-86af-582367848b49" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> 

1.  <span style="color:#0000ff"><!--</span><span style="color:#008000"> Deploy Click Once</span><span style="color:#0000ff">--></span>
2.  <span style="color:#0000ff"><</span><span style="color:#a31515">Target</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Name</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">DeployClickOnce</span>"<span style="color:#0000ff">></span>
3.    <span style="color:#0000ff"><</span><span style="color:#a31515">Message</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Text</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">####### Deploy ClickOnce $(Configuration)|$(Platform)  ---------#</span>"<span style="color:#0000ff"> /></span>
4.    <span style="color:#0000ff"><</span><span style="color:#a31515">Exec</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Command</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff">xcopy /E /Y $(ClickOnceSrc)\*.* $(ClickOnceDestination)</span>"<span style="color:#0000ff"> /></span>
5.  <span style="color:#0000ff"></</span><span style="color:#a31515">Target</span><span style="color:#0000ff">></span> </div> </div> </div>  

This is achieved by using two variables **ClickOnceSrc** and **ClickOnceDestination** which are also defined per solution configuration like the **ApplicationUrl** and **InstallUrl**. The destination is a folder on a IIS server which already has a manually customized Publish.htm file.
  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:71ce7464-b98f-44e5-8d30-8f9bd908a5cf" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> 

1.  <span style="color:#0000ff"><</span><span style="color:#a31515">PropertyGroup</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Condition</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff"> '$(Configuration)|$(Platform)' == 'DeployClickOnce|AnyCPU' </span>"<span style="color:#0000ff">></span>
2.    <span style="color:#0000ff"><</span><span style="color:#a31515">ClickOnceSrc</span><span style="color:#0000ff">></span>$(TestsFolder)\Output\$(OutputPath)app.publish<span style="color:#0000ff"></</span><span style="color:#a31515">ClickOnceSrc</span><span style="color:#0000ff">></span>
3.    <span style="color:#0000ff"><</span><span style="color:#a31515">ClickOnceDestination</span><span style="color:#0000ff">></span>E:\Inetpub\Application<span style="color:#0000ff"></</span><span style="color:#a31515">ClickOnceDestination</span><span style="color:#0000ff">></span> </div> </div> </div>  

And
  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:6b8dc035-17de-4b4c-b11f-120a7f8168ca" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> 

1.  <span style="color:#0000ff"><</span><span style="color:#a31515">PropertyGroup</span><span style="color:#0000ff"> </span><span style="color:#ff0000">Condition</span><span style="color:#0000ff">=</span>"<span style="color:#0000ff"> '$(Configuration)|$(Platform)' == 'integrationDeployClickOnce|AnyCPU' </span>"<span style="color:#0000ff">></span>
2.    <span style="color:#0000ff"><</span><span style="color:#a31515">ClickOnceSrc</span><span style="color:#0000ff">></span>$(TestsFolder)\Output\$(OutputPath)app.publish<span style="color:#0000ff"></</span><span style="color:#a31515">ClickOnceSrc</span><span style="color:#0000ff">></span>
3.    <span style="color:#0000ff"><</span><span style="color:#a31515">ClickOnceDestination</span><span style="color:#0000ff">></span>E:\Inetpub\Application\integration<span style="color:#0000ff"></</span><span style="color:#a31515">ClickOnceDestination</span><span style="color:#0000ff">></span> </div> </div> </div>  

Now you have two build configurations which output two valid ClickOnce setup packages using different stage dependant configurations that your tester can install directly from your ClickOnce web site. And if you have configured the automatic update of the application through the ClickOnce Application Updates then the applciation will be updated automatically when a tester start you application.

Enjoy!
