---
title: .NET Core console app running on Raspberry Pi
permalink: /2017/04/03/Dotnet-Core-app-running-on-raspberry-pi/
date: 2017-04-03 21:40:07
tags: [".NET Core", "Raspberry Pi", "Linux"]
disqusIdentifier: 20170403214007
coverSize: partial
coverCaption: 'Promenade Grand Anse Le diamant'
coverImage: https://c1.staticflickr.com/4/3808/33008049230_fcb508209e_h.jpg
thumbnailImage: https://c1.staticflickr.com/4/3808/33008049230_89efeeb73d_q.jpg
---

Now that there is a distribution of .NET Core on a Linux distro running on my [Raspberry Pi 3](https://www.raspberrypi.org/products/raspberry-pi-3-model-b/), I had to try it.

In this post, I will explain all the steps I had to execute to be able to run my first Hello World application.
<!-- more -->
First, you will need to install on your Pi the [Ubuntu MATE distribution](https://ubuntu-mate.org/raspberry-pi/). Currently, the RASPBIAN distribution, which is the mostly used, is not supported but it seems that it will be soon the case according to this [Github issue on dotnet / core](https://github.com/dotnet/core/issues/447).

Download [Ubuntu MATE 16.04.2 LTS for Raspberry Pi](https://ubuntu-mate.org/download/), then extract the image out of the archive.

To install it on your microSDHC, the best is to use [Win32 Disk Imager](https://chocolatey.org/packages/win32diskimager) which you can install using Chocolatey with the command '*choco install win32diskimager*'.

Then just write the image you extracted on your microSDHC using Win32 Disk Imager, like this:

![](https://c1.staticflickr.com/3/2822/32957708953_4ff95b973b_o.png)

Wait a couple of minutes then plug the microSDHC in your Pi and boot it, then follow the installation instruction and create your user.

Currently, there is no .NET Core SDK running on ARM processor, the CPU architecture of the Pi. So you will have to write and compile your program from another machine, in my case I run on Windows.

Now to simplify all the operations I installed a SSH server on the Pi to make it easy to access it remotely and copy files from my Surface.

To install the SSH server, run a shell, and run the following commands

<div style="clear:both;"></div>{% gist 75b6b70d1bcdc3b4caa030160dbcb018 installSSSHServer %}

Then we will need to install some .NET Core native prerequisites for Ubuntu as explained on the [Github dotnet / core documentation](https://github.com/dotnet/core/blob/master/Documentation/prereqs.md).

<div style="clear:both;"></div>{% gist cf534c59a094654491e28b0d1701738a prerequisites %}

You will need the IP of your Pi, so use the command '*ifconfig*' which will output something like that

![](https://c2.staticflickr.com/4/3816/32957721773_5f33093414_o.png)

Then back to your PC, install [Putty](https://chocolatey.org/packages/putty) using '*choco install putty*' and [WinSCP](https://chocolatey.org/packages/winscp) using '*choco install winscp*'.

Configure both Putty and WinSCP to connect to your Pi using the IP address you've got from the ifconfig command

Now that we have the Pi running the right Linux distribution on it and we can connect to it easily from our computer, it is time to create a first .NET Core project. For sure, we will start with the famous Hello World.

[Install .NET Core 2.0 SDK](https://github.com/dotnet/cli/tree/master), I used the Windows x64 version 2.0.0-preview1-005685 which I downloaded as a zip. Unpack it on your hard drive in a folder named dotnet, then create another folder at the same level called hello and create a new console application:

<div style="clear:both;"></div>{% gist 0b60cae1c6c10ccab6ed3f167e766cb5 create_project %}

Edit the file hello.csproj created in the folder. I use [Visual Studio Code](https://code.visualstudio.com/) for that, which again can be installed using [Chocolatey](https://chocolatey.org/packages/VisualStudioCode) '*choco install visualstudiocode*'. You will need to update the RuntimeFrameworkVersion with the version that you can find on the page .NET Core Runtime & Host Setup Repo / [Daily Builds](https://github.com/dotnet/core-setup#daily-builds) then **find Ubuntu 16.04 (arm32)** and get the version, in my **case 2.0.0-preview1-005685**.

<div style="clear:both;"></div>{% gist 809deab02a08a52ee17f3cb03a7b39c1 hello.csproj %}

Then run, and publish

<div style="clear:both;"></div>{% gist 98813ec107d827fd3ef3cb6178d36caf run_publish %}

Finally, you will get the publish results in the folder **.\hello\bin\Debug\netcoreapp2.0\ubuntu.16.04-arm** which you can easily upload to your Pi using WinSCP previously installed.

You need then to set the hello executable to have the execution rights on the Pi, which you can also do easily from WinSCP

![](https://c1.staticflickr.com/3/2806/33821762845_f3033ff9db_o.png)

The final step is to run you hello executable on the Pi, which you can do using Putty over SSH then, or by having your Pi connected to a monitor, mouse, and keyboard!

Here is the result

![Result](https://c2.staticflickr.com/4/3856/32957716323_e196bc13f0_o.png)
