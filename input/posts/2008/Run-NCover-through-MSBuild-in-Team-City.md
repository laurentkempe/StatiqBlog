---
title: "Run NCover through MSBuild in Team City"
permalink: /2008/04/30/Run-NCover-through-MSBuild-in-Team-City/
date: 4/30/2008 5:05:10 AM
updated: 5/7/2010 7:45:42 AM
disqusIdentifier: 20080430050510
tags: ["Tech Head Brothers", "continuous integration", "Team City"]
---
After one comment of Chris Walquist on one of [my last post about Team City integration of NCover](http://weblogs.asp.net/lkempe/archive/2008/03/30/integration-of-ncover-into-team-city-for-tech-head-brothers.aspx) here is the way I run NCover through MSBuild in Team City.

First I need to import the Task of NCover.
<!-- more -->

```xml
<ProjectToolsVersion="3.5" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <UsingTask TaskName="NCoverExplorer.MSBuildTasks.NCover"
             AssemblyFile="$(NCoverPath)\Build Task Plugins\NCoverExplorer.MSBuildTasks.dll" />
  <UsingTask TaskName="NCoverExplorer.MSBuildTasks.NCoverExplorer"
             AssemblyFile="$(NCoverPath)\Build Task Plugins\NCoverExplorer.MSBuildTasks.dll" />
```

Then I define some properties:

```xml
<!-- NCover -->
<SolutionFolder>$(MSBuildProjectDirectory)\..\..</SolutionFolder>
<TestsFolder>$(MSBuildProjectDirectory)\..\..\Tests</TestsFolder>
<NCoverPath>C:\Program Files\NCover</NCoverPath>
<NUnitPath>C:\Program Files\NUnit 2.4.6\bin</NUnitPath>
<TestDll>TechHeadBrothers.Portal.DataAccess.Tests.dll TechHeadBrothers.Portal.DataAccess.dll</TestDll>
<CoverageFile>..\..\Tests\Output\Coverage.xml</CoverageFile>
<CoverageExclusions>Assembly=*.Tests</CoverageExclusions>
<CoverageAssemblies>TechHeadBrothers.Portal.DataAccess;TechHeadBrothers.Portal.Domain;</CoverageAssemblies>
```

Then I have two targets:

```xml
<Target Name="FullCoverage">
    <Message Text="#--------- Executing NCover ---------#" />
    <NCover ToolPath="$(NCoverPath)" 
            CommandLineExe="$(NUnitPath)\nunit-console.exe" 
            CommandLineArgs="$(TestDll)" 
            WorkingDirectory="$(TestsFolder)\Output\bin\Debug" 
            ProjectName="$(ProjectName)" 
            CoverageFile="$(CoverageFile)" 
            FileExclusionPatterns="$(CoverageExclusions)" 
            LogFile="Coverage.log" 
            AssemblyList="$(CoverageAssemblies)" />
    <!-- Summary Page -->
    <NCoverExplorer ToolPath="$(NCoverPath)" 
                    ProjectName="$(ProjectName)" 
                    OutputDir="$(TestsFolder)\Output" 
                    CoverageFiles="$(CoverageFile)" 
                    SatisfactoryCoverage="80" 
                    ReportType="ModuleClassSummary" 
                    HtmlReportName="CoverageSummary.html" />
    <!-- Full HTML Report -->
    <NCoverExplorer ToolPath="$(NCoverPath)" 
                    ProjectName="$(ProjectName)" 
                    OutputDir="$(TestsFolder)\Output\Coverage" 
                    CoverageFiles="$(CoverageFile)" 
                    SatisfactoryCoverage="80" 
                    ReportType="FullCoverageReport" 
                    HtmlReportName="Coverage.html" />
  </Target>

```

And finally in the target AfterBuild ([more on that in this post](http://weblogs.asp.net/lkempe/archive/2008/04/25/using-ndepend-in-team-city-build-management-tool.aspx)) I call target SummaryCoverage or FullCoverage according to the configuration:

```xml
<Target Name="AfterBuild">
    <CallTarget Condition="'$(Configuration)' == 'Staging'" Targets="SummaryCoverage" ContinueOnError="false" />
    <CallTarget Condition="'$(Configuration)' == 'Nightly'" Targets="FullCoverage" ContinueOnError="false" />
```

That's it!

Then Chris asked the following:

> 1) How did you get your "tests" tab to show up as well? Did you have to run the tests with NUnitLauncher, and then run again for coverage? I read that NUnitLauncher can't be used to profile, since it kicks off a separate process.
> 
> 2) How did you get the "classes", "header", "index", etc. files? I just get the summary.html. I see options for this in ncover.console.exe but not in the <NCoverExplorer> target.
> 
> 3) Did you use wildcard expressions to pass a list of test assemblies to NCover? If so, would like to see how you did that, too.

1) I get the test tab configuring Team City to run my unit tests. That has nothing to do with NCover. And yes doing it so seems to run two time all unit tests. Here is a screen shot of my team city runner configuration:

![](http://farm4.static.flickr.com/3083/2452076205_63938cdba0_o.jpg) 

2) Read this post "[Using NDepend in Team City build management tool](http://weblogs.asp.net/lkempe/archive/2008/04/25/using-ndepend-in-team-city-build-management-tool.aspx)" that talks about for [NDepend](http://www.ndepend.com/) but shows configuration for [NCover](http://www.ncover.com/) too.

3) The answer is on top of this post!

Hope it helps!
