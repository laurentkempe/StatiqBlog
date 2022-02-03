---
title: Debugging into ASP.NET Core 2.0 source code
permalink: /2017/09/26/Debugging-into-ASP.NET-Core-2.0-source-code/
disqusIdentifier: 20170926204411
coverSize: partial
tags:
  - ASP.NET Core
  - Visual Studio 2017
coverCaption: 'Tiahura, Moorea, French Polynesia'
coverImage: 'https://farm5.staticflickr.com/4385/36202322654_cd05ca5740_h.jpg'
thumbnailImage: 'https://farm5.staticflickr.com/4385/36202322654_09850a5c86_q.jpg'
date: 2017-09-26 20:44:11
---
I am just back from 2.5 months unpaid leave which I used to spend time with my family. So, I almost did not spend time with software development. One exception is that I wanted to be able to configure Visual Studio 2017 to be able to debug into ASP.NET Core 2.0 source code.
<!-- more -->
Something which should have been simple didn't work for long because Microsoft wasn't publishing the PDBs. There were other ways to achieve that goal but it was fairly complex, so I decided to wait. Now, this has changed with the release of ASP.NET Core 2.0.

But first, why would you want to step into the source code of ASP.NET Core 2.0?
In my case, I find it very helpful to understand how the framework is working. So it is a great way to learn the internals of the framework and I am most particularly interested about the compilation of Razor pages.

Finally, the team uploaded the PDBs of ASP.NET Core 2.0 on the Microsoft Symbol Servers. And it is coming with a bonus feature of Visual Studio 2017 15.3; Source Link Support for Windows PDB File Format, which makes everything easy again.

{% blockquote Microsoft https://www.visualstudio.com/en-us/news/releasenotes/vs2017-relnotes#source-link-support-for-windows-pdb-file-format
 Visual Studio 2017 version 15.3 Release Notes %}

Source Link is now supported for Windows PDB file format (in addition to Portable PDBs). Compilers that support it can put the necessary information in the Windows PDB file format and the debugger can retrieve source files based on that information.
{% endblockquote %}

In this post, I will show you the fastest way to setup Visual Studio 2017 15.3 so that you can debug into ASP.NET Core 2.0 source code!

To start you can either use your current application or create a new ASP.NET Core 2.0 Web application.
I have Visual Studio 2017.3 default collection settings set to Web Development.

Open you Startup class, and set a breakpoint on the ConfigureServices method, then if you start a debugging session with F5 you will end up in this situation

![](https://farm5.staticflickr.com/4343/37372298705_65a2054a8d_c.jpg)

In the Call Stack window, we see only our code and [External Code]

Open Visual Studio options dialog, even without stopping your debugging session, using menu **Tools / Options** and choose **Debugging**, then untick **Enable Just My Code**. Click OK. Now the Call Stack Window show you much more like this

![](https://farm5.staticflickr.com/4427/37372901855_bc43c1e579_c.jpg)

We see now, our code is called from *Microsoft.AspNetCore.Hosting.dll*

Right click on Microsoft.AspNetCore.Hosting.dll and choose **Load Symbols**

![](https://farm5.staticflickr.com/4406/37287703876_296032f829_c.jpg)

Then the following dialog will be shown, give it some time to download the symbols from Microsoft Symbol Servers

![](https://farm5.staticflickr.com/4417/23483587158_1b3d16d4fc_z.jpg)

Now you should see the *Microsoft.AspNetCore.Hosting.dll* in white in the Call Stack window and right-clicking it, you can now choose **Go To Source Code**

![](https://farm5.staticflickr.com/4478/36625825424_87d424b7ef_c.jpg)

Finally the new Source Link dialog popup and let you download the source automatically 

![](https://farm5.staticflickr.com/4470/23483589828_59452c0da4_z.jpg)

You finally end up in the ASP.NET Core 2 source code, can set breakpoints and inspect some of the variables!

![](https://farm5.staticflickr.com/4502/23483591168_c99dd6953f_c.jpg)
