---
title: "MSBuildProjectDirectory issue in Web Deployment Project! Any idea Welcomed!"
permalink: /2008/04/04/MSBuildProjectDirectory-issue-in-Web-Deployment-Project!-Any-idea-Welcomed!/
date: 4/4/2008 3:31:50 AM
updated: 4/4/2008 3:31:50 AM
disqusIdentifier: 20080404033150
tags: ["Tech Head Brothers", "continuous integration", "Team City"]
alias:
 - /post/MSBuildProjectDirectory-issue-in-Web-Deployment-Project!-Any-idea-Welcomed!.aspx/index.html
---
I go crazy with a stupid error!

I try to have my web deployment project use the property [MSBuildProjectDirectory](http://msdn2.microsoft.com/en-us/ms164309.aspx) like this:
<!-- more -->
  <table style="background-color: black" cellspacing="0" cellpadding="2" width="600" border="0"><tbody>     <tr>       <td valign="top" width="600">         

<span style="background: black; color: white">  <Target </span><span style="background: black; color: #ff8000">Name</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">NDepend</span><span style="background: black; color: white">">
    <Message </span><span style="background: black; color: #ff8000">Text</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">#--------- Executing NDepend ---------#</span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">Importance</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">normal</span><span style="background: black; color: white">"/>
    <Message </span><span style="background: black; color: #ff8000">Text</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">NDepend ProjectFilePath: $(NDependProjectFilePath)</span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">Importance</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">normal</span><span style="background: black; color: white">"/>
    <Message </span><span style="background: black; color: #ff8000">Text</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">NDepend OutDir: $(NDpendOutputDir)</span><span style="background: black; color: white">" </span><span style="background: black; color: #ff8000">Importance</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">normal</span><span style="background: black; color: white">"/>
    <NDependTask </span><span style="background: black; color: #ff8000">NDependConsoleExePath</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(NDependPath)</span><span style="background: black; color: white">"
           </span><span style="background: black; color: #ff8000">ProjectFilePath</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(**MSBuildProjectDirectory**)..\..\Tests\NDepend\TechHeadBrothers.Portal.xml</span><span style="background: black; color: white">"
           </span><span style="background: black; color: #ff8000">OutDir</span><span style="background: black; color: white">="</span><span style="background: black; color: lime">$(MSBuildProjectDirectory)..\..\Tests\Output\NDependOut</span><span style="background: black; color: white">" />
  </Target>
</span>

        [](http://11011.net/software/vspaste)</td>
    </tr>
  </tbody></table>



And with Visual Studio 2008 I have the following error:

Error    1    The NDepend project file T:\_Projects\_handsup\Portal_HandsUP\Deployment**\TechHeadBrothers.Portal.csproj_deploy**..\..\Tests\NDepend\TechHeadBrothers.Portal.xml can't be found.    

So I am getting the correct folder but the filename of the project file too. Weird.

In the documentation there are saying that 

> **MSBuildProjectDirectory**
> 
> The absolute path of the directory where the project file is located, for example, C:\MyCompany\MyProduct.

By I am getting

> **MSBuildProjectFullPath**
> 
> The absolute path and complete file name of the project file, for example, C:\MyCompany\MyProduct\MyApp.proj

Any idea would be greatly appreciated!

**<u>Update</u>**: In fact I mixed two things folder and files. TechHeadBrothers.Portal.csproj_deploy is a folder and I thought it was a file :-( too bad

Now the second issue is that there is a bug in NDepend MSBuild task that I communicated to Patrick (the author) and he fixed and I could test it. It will be released next week. I will post about my integration of NDepend with Team City soon.
