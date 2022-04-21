---
title: "Building ClickOnce targeting .NET Framework 4 with TeamCity"
permalink: /2010/10/21/Building-ClickOnce-targeting-NET-Framework-4-with-TeamCity/
date: 10/21/2010 5:54:59 AM
updated: 10/21/2010 5:56:12 AM
disqusIdentifier: 20101021055459
tags: ["Team City", "ClickOnce"]
alias:
 - /post/Building-ClickOnce-targeting-NET-Framework-4-with-TeamCity.aspx/index.html
---
I already talked about this topic in a previous post, [Building ClickOnce with TeamCity](http://www.laurentkempe.com/post/Building-ClickOnce-with-TeamCity.aspx), which was about .NET framework 3.5. This time I will talk about .NET framework 4.

As always when you change from one .NET framework version to another, there are some more things to install on our build server than just the .NET Framework.
<!-- more -->

So this time I had to copy from my local machine (in fact it was Robert doing it, Thanks Robert) the folder

> C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A

To the build server folder

> C:\Program Files\Microsoft SDKs\Windows\v7.0A

Take care of the “x86”, due to my 64bits Windows 7 which is removed on the server because it is a 32bits Windows 2003 R2.

After that I created all the registry keys needed

> <font size="1">Windows Registry Editor Version 5.00</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A]
> "InstallationFolder"="C:\\Program Files\\Microsoft SDKs\\Windows\\v7.0A\\"
> "ProductVersion"="7.0.30319"
> "ProductName"="Microsoft Windows SDK for Visual Studio 2010"</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A\WinSDK-NetFx35Tools]
> "ProductVersion"="7.0.30319"
> "ComponentName"="Windows SDK .NET Framework 3.5 Multi-targeting Utilities"
> "InstallationFolder"="C:\\Program Files\\Microsoft SDKs\\Windows\\v7.0A\\Bin\\"</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A\WinSDK-NetFx35Tools-x64]
> "ProductVersion"="7.0.30319"
> "ComponentName"="Windows SDK .NET Framework 3.5 Multi-targeting Utilities (x64)"
> "InstallationFolder"="C:\\Program Files\\Microsoft SDKs\\Windows\\v7.0A\\Bin\\x64\\"</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A\WinSDK-NetFx35Tools-x86]
> "ProductVersion"="7.0.30319"
> "ComponentName"="Windows SDK .NET Framework 3.5 Multi-targeting Utilities"
> "InstallationFolder"="C:\\Program Files\\Microsoft SDKs\\Windows\\v7.0A\\Bin\\"</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A\WinSDK-NetFx40Tools]
> "ProductVersion"="7.0.30319"
> "ComponentName"="Windows SDK Tools for .NET Framework 4.0"
> "InstallationFolder"="C:\\Program Files\\Microsoft SDKs\\Windows\\v7.0A\\Bin\\NETFX 4.0 Tools\\"</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A\WinSDK-NetFx40Tools-x64]
> "ProductVersion"="7.0.30319"
> "ComponentName"="Windows SDK Tools for .NET Framework 4.0 (x64)"
> "InstallationFolder"="C:\\Program Files\\Microsoft SDKs\\Windows\\v7.0A\\Bin\\NETFX 4.0 Tools\\x64\\"</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A\WinSDK-NetFx40Tools-x86]
> "ProductVersion"="7.0.30319"
> "ComponentName"="Windows SDK Tools for .NET Framework 4.0"
> "InstallationFolder"="C:\\Program Files\\Microsoft SDKs\\Windows\\v7.0A\\Bin\\NETFX 4.0 Tools\\"</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A\WinSDK-SDKTools]
> "InstallationFolder"="C:\\Program Files\\Microsoft SDKs\\Windows\\v7.0A\\Bin\\"
> "ProductVersion"="7.0.30319"
> "ComponentName"="Windows Common Utilities"</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A\WinSDK-Win32Tools]
> "InstallationFolder"="C:\\Program Files\\Microsoft SDKs\\Windows\\v7.0A\\Bin\\"
> "ProductVersion"="7.0.30319"
> "ComponentName"="Windows Utilities for Win32 Development"</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A\WinSDK-WindowsHeadersLibs]
> "InstallationFolder"="C:\\Program Files\\Microsoft SDKs\\Windows\\v7.0A\\"
> "ProductVersion"="7.0.30319"
> "ComponentName"="Windows Headers and Libraries"</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v7.0A\WinSDK-WinSDKIntellisenseRefAssys]
> "InstallationFolder"="C:\\Program Files\\Reference Assemblies\\"
> "ProductVersion"="7.0.30319"
> "ComponentName"="Windows Intellisense and Reference Assemblies"</font>

You can copy this in a .reg file and apply it directly

Finally I also created the following keys, which you also might copy in a .reg file

> <font size="1">Windows Registry Editor Version 5.00</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\GenericBootstrapper]</font>
> 
> <font size="1">[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\GenericBootstrapper\4.0]
> "Path"="C:\\Program Files\\Microsoft SDKs\\Windows\\v7.0A\\Bootstrapper\\"</font>

And here is the result

![](http://farm2.static.flickr.com/1320/5100316380_6e7c15ae6f_o.png)
