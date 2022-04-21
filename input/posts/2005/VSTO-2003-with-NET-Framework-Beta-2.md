---
title: "VSTO 2003 with .NET Framework Beta 2"
permalink: /2005/04/23/VSTO-2003-with-NET-Framework-Beta-2/
date: 4/23/2005 4:40:00 AM
updated: 4/23/2005 4:40:00 AM
disqusIdentifier: 20050423044000
tags: ["Tech Head Brothers", "Tools", ".NET Development"]
alias:
 - /post/VSTO-2003-with-NET-Framework-Beta-2.aspx/index.html
---



I finally managed to have a first beta going to the [Tech Head Brothers](http://www.techheadbrothers.com/) authors of my 
publishing tool based on Word 2003, XML, Web Services... [Read 
more](http://weblogs.asp.net/lkempe/archive/2005/01/25/360227.aspx) and a [bit 
more](http://weblogs.asp.net/lkempe/archive/2004/11/03/251422.aspx).
<!-- more -->

So I have a cool setup for the tool that works for me on two machines, but 
when you start to distribute your software you face other challenges. On 
three authors computer it was not working, a VSTO security problem, for sure 
what else. After some discussions with them I realized they had the .NET 
Framework Beta 2 installed. And so ?

Here is the explanation from **Misha Shneerson** I had on the 
newsgroup:

> VSTO loader (aka OTKLoader) that shipped with Office 2003 RTM would always 
  load the latest runtime installed on the machine. Once you install .NET Fx 2.0 
  this runtime will be loaded. Each version of CLR runtime has its own CAS 
  policy settings. Note also that installinng .NET Fx 2.0 would not migrate 
  security policies from .NET Fx 1.1. So, the real problem is that your code 
  simply does not have permission to load once .NET Fx 2.0 is installed.
> 
> The brute force solution is of course to force loading v1.1 version of CLR 
  through the config file. But this will rule out the usage of VSTO 2005 since 
  VSTO 2005 runtime requires CLR 2.0 to run and you can not have both versions 
  of CLR running at the same process. Unfortunately, I should add.
> 
> So, I would suggest to manually migrate Fx 1.1 security policies to Fx 2.2. 
  I know this is painful, but this is the least bad solution for now.
> 
> I am not sure I can disclose the full details but before VS 2005 ships we 
  will see an **Office 2003 SP2** coming out. **The SP will 
  contain a patch for OTKLoader that would not require migrating security 
  policies from 1.1 to 2.0 even though the 2.0 version of CLR will be loaded on 
  the machine**.

The brute force solution is one I already used. You have to force Word 2003 
to load the .NET Framework you are interested in by creating a 
winword.exe.config and saving it in the folder of winword.exe. Thanks to Chuck 
Hartman to remind me that method:

<span style="COLOR: blue"><?</span><span style="COLOR: maroon">xml</span> <span style="COLOR: red">version</span>="<span style="COLOR: blue">1.0</span>" <span style="COLOR: red">encoding</span>="<span style="COLOR: blue">utf-8</span>" <span style="COLOR: blue">?></span>
<span style="COLOR: blue"><</span><span style="COLOR: maroon">configuration</span><span style="COLOR: blue">></span>
 <span style="COLOR: blue"><</span><span style="COLOR: maroon">startup</span><span style="COLOR: blue">></span>
  <span style="COLOR: blue"><</span><span style="COLOR: maroon">supportedRuntime</span> <span style="COLOR: red">version</span>="<span style="COLOR: blue">v1.1.4322</span>" /<span style="COLOR: blue">></span>
 <span style="COLOR: blue"><</span>/<span style="COLOR: maroon">startup</span><span style="COLOR: blue">></span>
<span style="COLOR: blue"><</span>/<span style="COLOR: maroon">configuration</span><span style="COLOR: blue">></span>

My first trial to migrate the security policies from 1.1 to 2.2 
was not sucessful, so I first sent the brute force solution to the different 
authors. Then I thought of a solution I saw some times ago on the blog of [Peter Torr](http://weblogs.asp.net/ptorr/) : [A useful 
regfile for VSTO](http://blogs.msdn.com/ptorr/archive/2004/07/16/184716.aspx).  
So I took his regfile and modified to run with .NET 
Framework 2.0:

Windows Registry Editor Version <span style="COLOR: maroon">5</span><span style="COLOR: maroon">.00</span> 

[HKEY_CLASSES_ROOT\dllfile\shell\FullTrust] 
@=<span style="COLOR: maroon">"Trust assembly Beta 2"</span> 

[HKEY_CLASSES_ROOT\dllfile\shell\FullTrust\command] 
@=<span style="COLOR: maroon">"C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50215\\caspol.exe -q -u -ag 1 -url \"%1\" FullTrust -n \"%1\""</span> 

[HKEY_CLASSES_ROOT\dllfile\shell\UnTrust] 
@=<span style="COLOR: maroon">"Remove assembly trust Beta 2"</span> 

[HKEY_CLASSES_ROOT\dllfile\shell\UnTrust\command] 
@=<span style="COLOR: maroon">"C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50215\\caspol.exe -q -u -rg \"%1\""</span> 
  
[HKEY_CLASSES_ROOT\exefile\shell\FullTrust] 
@=<span style="COLOR: maroon">"Trust assembly Beta 2"</span> 
  
[HKEY_CLASSES_ROOT\exefile\shell\FullTrust\command] 
@=<span style="COLOR: maroon">"C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50215\\caspol.exe -q -u -ag 1 -url \"%1\" FullTrust -n \"%1\""</span> 

[HKEY_CLASSES_ROOT\exefile\shell\UnTrust] 
@=<span style="COLOR: maroon">"Remove assembly trust Beta 2"</span> 

[HKEY_CLASSES_ROOT\exefile\shell\UnTrust\command] 
@=<span style="COLOR: maroon">"C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50215\\caspol.exe -q -u -rg \"%1\""</span> 

[HKEY_CLASSES_ROOT\Folder\shell\FullTrust] 
@=<span style="COLOR: maroon">"Trust folder Beta 2"</span> 

[HKEY_CLASSES_ROOT\Folder\shell\FullTrust\command] 
@=<span style="COLOR: maroon">"C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50215\\caspol.exe -q -u -ag 1 -url \"%1\"\\* FullTrust -n \"%1\""</span> 

[HKEY_CLASSES_ROOT\Folder\shell\UnTrust] 
@=<span style="COLOR: maroon">"Remove folder trust Beta 2"</span> 

[HKEY_CLASSES_ROOT\Folder\shell\UnTrust\command] 
@=<span style="COLOR: maroon">"C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50215\\caspol.exe -q -u -rg \"%1\""</span> 


Now it is running without the brute force solution, so it is much 
cleaner.

[ Currently Playing : Strategie D'un Pion - Iam - Revoir Un 
Printemps (04:36) ]
