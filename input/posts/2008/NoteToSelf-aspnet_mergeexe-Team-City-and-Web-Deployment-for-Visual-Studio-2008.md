---
title: "NoteToSelf: aspnet_merge.exe, Team City and Web Deployment for Visual Studio 2008"
permalink: /2008/03/01/NoteToSelf-aspnet_mergeexe-Team-City-and-Web-Deployment-for-Visual-Studio-2008/
date: 3/1/2008 2:50:20 AM
updated: 5/10/2010 7:50:39 PM
disqusIdentifier: 20080301025020
tags: ["Note to self"]
alias:
 - /post/NoteToSelf-aspnet_mergeexe-Team-City-and-Web-Deployment-for-Visual-Studio-2008.aspx/index.html
---
I had to modify the Microsoft.WebDeployment.targets file to be able to compile through Team City the Web Deployment 2008 project !

> **<!-- Changed KEL ExePath="$(FrameworkSDKDir)bin" --> **
<!-- more -->
> 
>     <Target Name="AspNetMerge" Condition="'$(UseMerge)' == 'true'" DependsOnTargets="$(MergeDependsOn)">
>         <AspNetMerge
> **          ExePath="C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin"
> **          ApplicationPath="$(TempBuildDir)"
>           KeyFile="$(_FullKeyFile)"
>           DelaySign="$(DelaySign)"
>           Prefix="$(AssemblyPrefixName)"
>           SingleAssemblyName="$(SingleAssemblyName)"
>           Debug="$(DebugSymbols)"
>           Nologo="$(NoLogo)"
>           ContentAssemblyName="$(ContentAssemblyName)"
>           ErrorStack="$(ErrorStack)"
>           RemoveCompiledFiles="$(DeleteAppCodeCompiledFiles)"
>           CopyAttributes="$(CopyAssemblyAttributes)"
>           AssemblyInfo="$(AssemblyInfoDll)"
>           MergeXmlDocs="$(MergeXmlDocs)"
>           ErrorLogFile="$(MergeErrorLogFile)"
>           />
> 
>         <CreateItem Include="$(TempBuildDir)**\*.*">
>             <Output ItemName="PrecompiledOutput" TaskParameter="Include" />
>         </CreateItem>
>     </Target>
