---
title: "“Unable to evaluate the expression” error in Visual Studio 2010 debugger"
permalink: /2010/11/23/e2809cUnable-to-evaluate-the-expressione2809d-error-in-Visual-Studio-2010-debugger/
date: 11/23/2010 8:04:46 PM
updated: 11/23/2010 8:04:46 PM
disqusIdentifier: 20101123080446
tags: ["Visual Studio", "ASP.NET MVC", "Async"]
alias:
 - /post/e2809cUnable-to-evaluate-the-expressione2809d-error-in-Visual-Studio-2010-debugger.aspx/index.html
---
Last week I had an issue in Visual Studio 2010 debugger in some projects only! When I was looking at the value of variables I was getting an error message saying “Unable to evaluate the expression”!

Searching on [Connect](https://connect.microsoft.com/) I have found that someone else had the same issue! Not really a good sign, “[Debuging (Evalutation) stops working at times - Unable to evaluate the expression](https://connect.microsoft.com/VisualStudio/feedback/details/622525/debuging-evalutation-stops-working-at-times-unable-to-evaluate-the-expression)”. I have the same configuration, same issue and can reproduce what is described:
<!-- more -->

> I'm on Windows 7 64bit and am using VS.NET 2010 Ultimate
> Never had a problem with VS.NET 2010 until recently. And the problem I am having is that when I create a new console app and am not able to:
> 1. Evalutate any variable values
> 2. Hover over any variables and see their values
> 3. Use the immidiate window to get any values. I see a message -"Unable to evaluate the expression"
> The debugger stops and the break points as expected but the above does not work.
> If I switch the Project Build platofrm target property from x86 to Any CPU, then everything works as expected. Switching it back to x86 gives to the same behavior as explained above.
> Other project types exhibit the same behavior sometimes. I have a new ASP.NET MVC project that are created and it exhibits the same behaviour. In fact with this project I am unable ot debug no matter what the build settings are.
> Older console app projects have this problem as well (they used ot work just fine earlier).

On last Friday, I re-installed two times Visual Studio 2010 without any success, uninstalled all tooling… Still the same issue. I tried several other things over the weekend which didn’t helped.

I finally found that I had installed ASP.NET MVC 3 RC and I remember reading the documentation saying that it had an issue with Async CTP, which cannot be installed together on the same machine.

So I un-installed ASP.NET MVC 3 RC and searched Async CTP which wasn’t listed. Something I did already when installing ASP.NET MVC 3 RC.

This morning I realized why I didn’t found the Async CTP when I installed ASP.NET MVC 3 RC! It is installed as an update! What’s the hell!

So from Control Panel you have to click on “View installed updates” to see it

![](http://farm5.static.flickr.com/4092/5201198234_cdc5eca598_o.png)

And now you can see it and un-install the Async CTP

![](http://farm5.static.flickr.com/4090/5201200424_f0cd50966e_o.png)

Then I followed the instruction of Drew Miller of the ASP.NET Team to uninstall all pieces of ASP.NET MVC 3 RC, “[How to Uninstall Microsoft ASP.NET MVC 3 RC](http://drew-prog.blogspot.com/2010/11/how-to-uninstall-microsoft-aspnet-mvc-3.html)”

Now my Visual Studio 2010 debugger is working like before!
