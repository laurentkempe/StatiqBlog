---
title: "Run NCover through MSBuild in Team City"
permalink: /2008/04/30/Run-NCover-through-MSBuild-in-Team-City/
date: 4/30/2008 5:05:10 AM
updated: 5/7/2010 7:45:42 AM
disqusIdentifier: 20080430050510
tags: ["Tech Head Brothers", "continuous integration", "Team City"]
alias:
 - /post/Run-NCover-through-MSBuild-in-Team-City.aspx/index.html
---
After one comment of Chris Walquist on one of [my last post about Team City integration of NCover](http://weblogs.asp.net/lkempe/archive/2008/03/30/integration-of-ncover-into-team-city-for-tech-head-brothers.aspx) here is the way I run NCover through MSBuild in Team City.

First I need to import the Task of NCover.
<!-- more -->

<span style="background: black; color: white"><Project</span><span style="background: black; color: #ff8000">ToolsVersion</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">3.5</span><span style="background: black; color: white">"</span><span style="background: black; color: #ff8000">DefaultTargets</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">Build</span><span style="background: black; color: white">"</span><span style="background: black; color: #ff8000">xmlns</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">http://schemas.microsoft.com/developer/msbuild/2003</span><span style="background: black; color: white">">       
  <UsingTask </span><span style="background: black; color: #ff8000">TaskName</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">NCoverExplorer.MSBuildTasks.NCover</span><span style="background: black; color: white">"</span><span style="background: black; color: #ff8000">AssemblyFile</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NCoverPath)\Build Task Plugins\NCoverExplorer.MSBuildTasks.dll</span><span style="background: black; color: white">" />       
  <UsingTask </span><span style="background: black; color: #ff8000">TaskName</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">NCoverExplorer.MSBuildTasks.NCoverExplorer</span><span style="background: black; color: white">"</span><span style="background: black; color: #ff8000">AssemblyFile</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NCoverPath)\Build Task Plugins\NCoverExplorer.MSBuildTasks.dll</span><span style="background: black; color: white">" />       
</span>

Then I define some properties:

<span style="background: black; color: white">    <!-- </span><span style="background: black; color: green">NCover </span><span style="background: black; color: white">-->
    <SolutionFolder>$(MSBuildProjectDirectory)\..\..</SolutionFolder>
    <TestsFolder>$(MSBuildProjectDirectory)\..\..\Tests</TestsFolder>
    <NCoverPath>C:\Program Files\NCover</NCoverPath>
    <NUnitPath>C:\Program Files\NUnit 2.4.6\bin</NUnitPath>
    <TestDll>TechHeadBrothers.Portal.DataAccess.Tests.dll TechHeadBrothers.Portal.DataAccess.dll</TestDll>
    <CoverageFile>..\..\Tests\Output\Coverage.xml</CoverageFile>
    <CoverageExclusions>Assembly=*.Tests</CoverageExclusions>
    <CoverageAssemblies>TechHeadBrothers.Portal.DataAccess;TechHeadBrothers.Portal.Domain;</CoverageAssemblies>
</span>

Then I have two targets:

<span style="background: black; color: white">  <Target </span><span style="background: black; color: #ff8000">Name</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">FullCoverage</span><span style="background: black; color: white">">
    <Message </span><span style="background: black; color: #ff8000">Text</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">#--------- Executing NCover ---------#</span><span style="background: black; color: white">" />
    <NCover </span><span style="background: black; color: #ff8000">ToolPath</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NCoverPath)</span><span style="background: black; color: white">" 
            </span><span style="background: black; color: #ff8000">CommandLineExe</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NUnitPath)\nunit-console.exe</span><span style="background: black; color: white">" 
            </span><span style="background: black; color: #ff8000">CommandLineArgs</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(TestDll)</span><span style="background: black; color: white">" 
            </span><span style="background: black; color: #ff8000">WorkingDirectory</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(TestsFolder)\Output\bin\Debug</span><span style="background: black; color: white">" 
            </span><span style="background: black; color: #ff8000">ProjectName</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(ProjectName)</span><span style="background: black; color: white">" 
            </span><span style="background: black; color: #ff8000">CoverageFile</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(CoverageFile)</span><span style="background: black; color: white">" 
            </span><span style="background: black; color: #ff8000">FileExclusionPatterns</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(CoverageExclusions)</span><span style="background: black; color: white">" 
            </span><span style="background: black; color: #ff8000">LogFile</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">Coverage.log</span><span style="background: black; color: white">" 
            </span><span style="background: black; color: #ff8000">AssemblyList</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(CoverageAssemblies)</span><span style="background: black; color: white">" />
    <!-- </span><span style="background: black; color: green">Summary Page </span><span style="background: black; color: white">-->
    <NCoverExplorer </span><span style="background: black; color: #ff8000">ToolPath</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NCoverPath)</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">ProjectName</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(ProjectName)</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">OutputDir</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(TestsFolder)\Output</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">CoverageFiles</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(CoverageFile)</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">SatisfactoryCoverage</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">80</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">ReportType</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">ModuleClassSummary</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">HtmlReportName</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">CoverageSummary.html</span><span style="background: black; color: white">" />
    <!-- </span><span style="background: black; color: green">Full HTML Report </span><span style="background: black; color: white">-->
    <NCoverExplorer </span><span style="background: black; color: #ff8000">ToolPath</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NCoverPath)</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">ProjectName</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(ProjectName)</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">OutputDir</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(TestsFolder)\Output\Coverage</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">CoverageFiles</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(CoverageFile)</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">SatisfactoryCoverage</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">80</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">ReportType</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">FullCoverageReport</span><span style="background: black; color: white">" 
                    </span><span style="background: black; color: #ff8000">HtmlReportName</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">Coverage.html</span><span style="background: black; color: white">" />
  </Target>
</span>

And finally in the target AfterBuild ([more on that in this post](http://weblogs.asp.net/lkempe/archive/2008/04/25/using-ndepend-in-team-city-build-management-tool.aspx)) I call target SummaryCoverage or FullCoverage according to the configuration:

<span style="background: black; color: white">  <Target </span><span style="background: black; color: #ff8000">Name</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">AfterBuild</span><span style="background: black; color: white">">
    <CallTarget </span><span style="background: black; color: #ff8000">Condition</span><span style="background: black; color: white">=" </span><span style="background: black; color: lime">'$(Configuration)' == 'Staging' </span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">Targets</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">SummaryCoverage</span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">ContinueOnError</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">false</span><span style="background: black; color: white">" />
    <CallTarget </span><span style="background: black; color: #ff8000">Condition</span><span style="background: black; color: white">=" </span><span style="background: black; color: lime">'$(Configuration)' == 'Nightly' </span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">Targets</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">FullCoverage</span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">ContinueOnError</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">false</span><span style="background: black; color: white">" />
</span>

That's it!

Then Chris asked the following:

> 1) How did you get your "tests" tab to show up as well?  Did you have to run the tests with NUnitLauncher, and then run again for coverage?  I read that NUnitLauncher can't be used to profile, since it kicks off a separate process.
> 
> 2) How did you get the "classes", "header", "index", etc. files?  I just get the summary.html.  I see options for this in ncover.console.exe but not in the <NCoverExplorer> target.
> 
> 3) Did you use wildcard expressions to pass a list of test assemblies to NCover?  If so, would like to see how you did that, too.

1) I get the test tab configuring Team City to run my unit tests. That has nothing to do with NCover. And yes doing it so seems to run two time all unit tests. Here is a screen shot of my team city runner configuration:

![](http://farm4.static.flickr.com/3083/2452076205_63938cdba0_o.jpg) 

2) Read this post "[Using NDepend in Team City build management tool](http://weblogs.asp.net/lkempe/archive/2008/04/25/using-ndepend-in-team-city-build-management-tool.aspx)" that talks about for [NDepend](http://www.ndepend.com/) but shows configuration for [NCover](http://www.ncover.com/) too.

3) The answer is on top of this post!

Hope it helps!
