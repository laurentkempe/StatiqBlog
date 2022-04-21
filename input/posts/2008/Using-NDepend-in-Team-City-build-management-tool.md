---
title: "Using NDepend in Team City build management tool"
permalink: /2008/04/25/Using-NDepend-in-Team-City-build-management-tool/
date: 4/25/2008 6:23:00 PM
updated: 4/25/2008 6:23:00 PM
disqusIdentifier: 20080425062300
tags: ["Tech Head Brothers", "continuous integration", "Team City", "NDepend"]
alias:
 - /post/Using-NDepend-in-Team-City-build-management-tool.aspx/index.html
---
In my effort to bring a good development environment for the next version of [Tech Head Brothers](http://www.techheadbrothers.com/) portal, in which we should be (at the moment) three to develop, I went on with the integration of [NDepend](http://www.ndepend.com/), the wonderful tool of [Patrick Smacchia](http://codebetter.com/blogs/patricksmacchia/), with the just as well [JetBrains](http://www.jetbrains.com/) [Team City](http://www.jetbrains.com/teamcity) build management server.

As said in [my last post](http://weblogs.asp.net/lkempe/archive/2008/04/23/continuous-integration-and-nightly-build-with-team-city.aspx) I have defined three builds type
<!-- more -->

1.  Continuous Integration - running compilation, unit testing, code coverage and deployment on staging 
2.  Nightly build - to find duplicates in the code 
3.  Nightly build - running compilation, unit testing, code coverage and code analyze   

[NDepend](http://www.ndepend.com/) is integrated in the 3rd point during this nightly build for the code analyze.

My solution in Visual Studio is organized as the following:

![](http://farm3.static.flickr.com/2153/2440714404_9ae11201b6_o.jpg) 

In which you can see

1.  a [Visual Studio 2008 Web Deployment Projects](http://www.microsoft.com/downloads/details.aspx?FamilyId=0AA30AE8-C73B-4BDD-BB1B-FE697256C459&displaylang=en) which I customized to carry out the different build tasks for the 3 different built types (Continuous Integration, Nightly duplicates and Nightly full analyze): ***TechHeadBrothers.Portal.csproj_deploy***, this is just a MSBuild script 
2.  The configuration file for NDepend: ***TechHeadBrothers.Portal.xml***   

The next thing I used is to define new configuration:

![](http://farm4.static.flickr.com/3104/2439888971_055ba9bd48_o.jpg) 

Nightly and Staging configuration are new one based on the Debug configuration. This will help me to control my MSBuild script with the two build types I am interested about: Continuous Integration = Staging, Nightly = Nightly (obvious ;).

Let's take a look at the MSBuild script with the new "Open Project File" menu in Visual Studio 2008:

![](http://farm3.static.flickr.com/2064/2439915841_f882f8b84a_o.jpg) 

First I am importing the MSBuild tasks exposed by NDepend like this:

<span style="background: black; color: white">  <UsingTask </span><span style="background: black; color: #ff8000">AssemblyFile</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NDependPath)\MSBuild\NDepend.Build.MSBuild.dll</span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">TaskName</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">NDependTask</span><span style="background: black; color: white">" />
</span>

Then defining some properties that will ease the build script writting later on:

<span style="background: black; color: white">    <!-- </span><span style="background: black; color: green">NDepend </span><span style="background: black; color: white">-->
    <NDependPath>C:\Program Files\_Tools\Development\ndepend</NDependPath>
    <NDependProjectFilePath>$(TestsFolder)\NDepend\TechHeadBrothers.Portal.xml</NDependProjectFilePath>
    <NDependInDirs>"$(SolutionFolder)\Sources\TechHeadBrothers.Portal\bin";"C:\Windows\Microsoft.NET\Framework\v2.0.50727";</NDependInDirs>
    <NDpendOutputDir>$(TestsFolder)\Output\NDependOut</NDpendOutputDir>
  </PropertyGroup>
</span>

Then I define a Target for NDepend:

<span style="background: black; color: white">  <Target </span><span style="background: black; color: #ff8000">Name</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">NDepend</span><span style="background: black; color: white">">
    <Message </span><span style="background: black; color: #ff8000">Text</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">#--------- Executing NDepend ---------#</span><span style="background: black; color: white">" />
    <NDependTask </span><span style="background: black; color: #ff8000">NDependConsoleExePath</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NDependPath)</span><span style="background: black; color: white">" 
                 </span><span style="background: black; color: #ff8000">ProjectFilePath</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NDependProjectFilePath)</span><span style="background: black; color: white">" 
                 </span><span style="background: black; color: #ff8000">InDirsDotComaSeparated</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NDependInDirs)</span><span style="background: black; color: white">" 
                 </span><span style="background: black; color: #ff8000">OutDir</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NDpendOutputDir)</span><span style="background: black; color: white">" />
  </Target>
</span>

Then in the after build target I call the NDepend target if the configuration is the Nightly one:

<span style="background: black; color: white">  <Target </span><span style="background: black; color: #ff8000">Name</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">AfterBuild</span><span style="background: black; color: white">">
    <CallTarget </span><span style="background: black; color: #ff8000">Condition</span><span style="background: black; color: white">=" </span><span style="background: black; color: lime">'$(Configuration)' == 'Staging' </span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">Targets</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">SummaryCoverage</span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">ContinueOnError</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">false</span><span style="background: black; color: white">" />
    <CallTarget </span><span style="background: black; color: #ff8000">Condition</span><span style="background: black; color: white">=" </span><span style="background: black; color: lime">'$(Configuration)' == 'Nightly' </span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">Targets</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">FullCoverage</span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">ContinueOnError</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">false</span><span style="background: black; color: white">" />
    <CallTarget </span><span style="background: black; color: #ff8000">Condition</span><span style="background: black; color: white">=" </span><span style="background: black; color: lime">'$(Configuration)' == 'Nightly' </span><span style="background: black; color: white">" 
                </span><span style="background: black; color: #ff8000">Targets</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">NDepend</span><span style="background: black; color: white">" 
                </span><span style="background: black; color: #ff8000">ContinueOnError</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">false</span><span style="background: black; color: white">" />
</span>

Now what we want to have is the possibility to see the report generated by NDepend on the Team City portal like this:

![](http://farm3.static.flickr.com/2148/2440756652_20b1fb6381_o.jpg) 

To achieve this like for [NCover](http://www.ncover.com/) ([Integration of NCover into Team City for Tech Head Brothers](http://weblogs.asp.net/lkempe/archive/2008/03/30/integration-of-ncover-into-team-city-for-tech-head-brothers.aspx)) we have to configure Team City to deal with the artifacts that NDepend creates, so in the settings of your build you need to define the following:

![](http://farm4.static.flickr.com/3216/2439932051_29fffef7de_o.jpg)   

Then you need to edit on the build server the file ***main-config.xml*** and add the following configuration:

<report-tab title="Code Coverage Summary" basePath="" startPage="CoverageSummary.html" />
    
<report-tab title="Code Coverage" basePath="Coverage" startPage="index.html" />

    
**<report-tab title="NDepend" basePath="NDepend" startPage="NDependReport.html" /> **

Beware not to have two title with the same value!

You will get access also to all created artifacts through the artifacts tab of Team City:

![](http://farm4.static.flickr.com/3131/2440767352_9dcc1abac7_o.jpg) 

<u>Remark</u>: It would be really nice to be able to have a RSS Feed that would expose the artifacts created! I hope to see this feature in a next release of Team City!

Happy build management!
