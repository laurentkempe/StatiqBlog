---
title: "Web Deployment Projects for VS08 released as CTP & Migration tips"
permalink: /2007/12/02/Web-Deployment-Projects-for-VS08-released-as-CTP-amp3b-Migration-tips/
date: 12/2/2007 4:59:02 PM
updated: 12/2/2007 4:59:02 PM
disqusIdentifier: 20071202045902
tags: ["Tech Head Brothers", "ASP.NET 2.0", "Visual Studio", "ASP.NET"]
---
My old build script wasn't working after the migration to this new version and you will find in this post the different adaptation that I had to do.

Add missing ToolsVersion as following:
<!-- more -->

```xml
<Project DefaultTargets="Build" 
         xmlns="http://schemas.microsoft.com/developer/msbuild/2003" 
         ToolsVersion="3.5">
```

Replace

```xml
<Import Project="$(MSBuildExtensionsPath)\Microsoft\WebDeployment\v8.0\Microsoft.WebDeployment.targets"/>
```

with
```xml
<Import Project="$(MSBuildExtensionsPath)\Microsoft\WebDeployment\v9.0\Microsoft.WebDeployment.targets"/>
```

I also had to add the following to the beginning of my AfterBuild task:
```xml
<Target Name="AfterBuild">
  <CreateItem Include="$(TempBuildDir)\**\*.*">
    <Output ItemName="CompiledFiles" TaskParameter="Include" />     
  </CreateItem>
  <Exec Command="if exist &quot;$(WDTargetDir)&quot; rd /s /q &quot;$(WDTargetDir)&quot;" />
  <Exec Command="if not exist &quot;$(WDTargetDir)&quot; md &quot;$(WDTargetDir)&quot;" />
  <Copy SourceFiles="@(CompiledFiles)" DestinationFolder="$(WDTargetDir)\%(CompiledFiles.SubFolder)%(CompiledFiles.RecursiveDir)" />
  <Exec Command="if exist &quot;$(TempBuildDir)&quot; rd /s /q &quot;$(TempBuildDir)&quot;" />
  ...

</Target>
```

Otherwise I ended up with TempBuildDir folder with a part of my solution. This is just a copy of what is defined in Microsoft.WebDeployment.targets.

Now everything works like before, I can:

1.  set Staging as my active solution configuration
2.  Run a compilation
3.  Zip the merged result of the build
4.  Start uploading using ssh to the target stage server

Nice!
