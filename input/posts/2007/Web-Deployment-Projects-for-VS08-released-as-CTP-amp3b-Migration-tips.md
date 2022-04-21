---
title: "Web Deployment Projects for VS08 released as CTP & Migration tips"
permalink: /2007/12/02/Web-Deployment-Projects-for-VS08-released-as-CTP-amp3b-Migration-tips/
date: 12/2/2007 4:59:02 PM
updated: 12/2/2007 4:59:02 PM
disqusIdentifier: 20071202045902
tags: ["Tech Head Brothers", "ASP.NET 2.0", "Visual Studio", "ASP.NET"]
alias:
 - /post/Web-Deployment-Projects-for-VS08-released-as-CTP-amp3b-Migration-tips.aspx/index.html
---
My old build script wasn't working after the migration to this new version and you will find in this post the different adaptation that I had to do.

Add missing ToolsVersion as following:
<!-- more -->
[](http://11011.net/software/vspaste)

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">Project</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">DefaultTargets</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">Build</span>"<span style="color: rgb(0,0,255)"> 
         </span><span style="color: rgb(255,0,0)">xmlns</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">http://schemas.microsoft.com/developer/msbuild/2003</span>"<span style="color: rgb(0,0,255)"> 
         </span>**<span style="color: rgb(255,0,0)">ToolsVersion</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">3.5</span>"**<span style="color: rgb(0,0,255)">></span>

Replace

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">Import</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">Project</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">$(MSBuildExtensionsPath)\Microsoft\WebDeployment\v8.0\Microsoft.WebDeployment.targets</span>"<span style="color: rgb(0,0,255)">/></span>

with

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">Import</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">Project</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">$(MSBuildExtensionsPath)\Microsoft\WebDeployment\v9.0\Microsoft.WebDeployment.targets</span>"<span style="color: rgb(0,0,255)">/></span>

[](http://11011.net/software/vspaste)I also had to add the following to the beginning of my AfterBuild task:

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">Target</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">Name</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">AfterBuild</span>"<span style="color: rgb(0,0,255)">>
  <</span><span style="color: rgb(163,21,21)">CreateItem</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">Include</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">$(TempBuildDir)\**\*.*</span>"<span style="color: rgb(0,0,255)">>
    <</span><span style="color: rgb(163,21,21)">Output</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">ItemName</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">CompiledFiles</span>"<span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">TaskParameter</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">Include</span>"<span style="color: rgb(0,0,255)"> />     
  </</span><span style="color: rgb(163,21,21)">CreateItem</span><span style="color: rgb(0,0,255)">>
  <</span><span style="color: rgb(163,21,21)">Exec</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">Command</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">if exist </span><span style="color: rgb(255,0,0)">&quot;</span><span style="color: rgb(0,0,255)">$(WDTargetDir)</span><span style="color: rgb(255,0,0)">&quot;</span><span style="color: rgb(0,0,255)"> rd /s /q </span><span style="color: rgb(255,0,0)">&quot;</span><span style="color: rgb(0,0,255)">$(WDTargetDir)</span><span style="color: rgb(255,0,0)">&quot;</span>"<span style="color: rgb(0,0,255)"> />
  <</span><span style="color: rgb(163,21,21)">Exec</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">Command</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">if not exist </span><span style="color: rgb(255,0,0)">&quot;</span><span style="color: rgb(0,0,255)">$(WDTargetDir)</span><span style="color: rgb(255,0,0)">&quot;</span><span style="color: rgb(0,0,255)"> md </span><span style="color: rgb(255,0,0)">&quot;</span><span style="color: rgb(0,0,255)">$(WDTargetDir)</span><span style="color: rgb(255,0,0)">&quot;</span>"<span style="color: rgb(0,0,255)"> />
  <</span><span style="color: rgb(163,21,21)">Copy</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">SourceFiles</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">@(CompiledFiles)</span>"<span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">DestinationFolder</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">$(WDTargetDir)\%(CompiledFiles.SubFolder)%(CompiledFiles.RecursiveDir)</span>"<span style="color: rgb(0,0,255)"> />
  <</span><span style="color: rgb(163,21,21)">Exec</span><span style="color: rgb(0,0,255)"> </span><span style="color: rgb(255,0,0)">Command</span><span style="color: rgb(0,0,255)">=</span>"<span style="color: rgb(0,0,255)">if exist </span><span style="color: rgb(255,0,0)">&quot;</span><span style="color: rgb(0,0,255)">$(TempBuildDir)</span><span style="color: rgb(255,0,0)">&quot;</span><span style="color: rgb(0,0,255)"> rd /s /q </span><span style="color: rgb(255,0,0)">&quot;</span><span style="color: rgb(0,0,255)">$(TempBuildDir)</span><span style="color: rgb(255,0,0)">&quot;</span>"<span style="color: rgb(0,0,255)"> />
</span><span style="color: rgb(0,0,255)">  ...</span>

<span style="color: rgb(0,0,255)"></</span><span style="color: rgb(163,21,21)">Target</span><span style="color: rgb(0,0,255)">></span>

[](http://11011.net/software/vspaste)Otherwise I ended up with TempBuildDir folder with a part of my solution. This is just a copy of what is defined in Microsoft.WebDeployment.targets.

Now everything works like before, I can:

1.  set Staging as my active solution configuration
2.  Run a compilation
3.  Zip the merged result of the build
4.  Start uploading using ssh to the target stage server


Nice!
