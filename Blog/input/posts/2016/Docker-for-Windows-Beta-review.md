---
title: Docker for Windows Beta review
date: 2016-04-30 11:27:50
tags: ["Docker", "Windows", "Visual Studio"]
permalink: /2016/04/30/Docker-for-Windows-Beta-review/
disqusIdentifier: 20160430112750
coverSize: partial
coverCaption: 'Plage de Quarciole, Corse'
coverImage: 'https://farm1.staticflickr.com/470/19744907834_1883db9671_h.jpg'
thumbnailImage: 'https://farm1.staticflickr.com/470/19744907834_2a1fb48523_q.jpg'
---
I have been playing with all Docker tools for quite some time now! Started with the command line and all its commands like docker start,  stop, ps, etc... Then I tried Kitematic and even compiled it from the source before it was supporting Windows! Then Docker Toolbox.
<!-- more -->
Two weeks ago I got access to Docker for Windows Beta, so first I uninstalled all the previous things I installed like VirtualBox, boot2docker, Docker Toolbox, even if it is supported to run in parallel with Docker for Windows. I was delighted to read that it supports Hyper-V which is a much better solution for us on Windows. Especially developers who need to run phone emulators which most of the time use Hyper-V! No more reboot tricks to turn it on or off.

My first impressions are very positive! The overall experience is much easier, and you can get started very fast. Downloading and running your first Docker images is done in some minutes now!

You now get a nice icon in the Windows notification area 

![Docker for Windows Tray icon](https://farm2.staticflickr.com/1673/26661310031_9bc569e24a_o.png)

Currently, it lets you control only a few things like Memory allocation, Automatical start, Checks for update but also, the interesting Manage shared drives

![Docker for Windows Settings](https://farm2.staticflickr.com/1510/26661257341_11374f368c_o.png)

Till now the updates are coming at a good pace!

You can also use the entry Dashboard to download Kitematic which will let you control Docker through an interface and makes it even easier to download images and start/stop containers!

![Kitematic](https://farm2.staticflickr.com/1515/26701373396_487ce0187a_o.png)

And the other great experience is that now you just start your command line and type your docker commands, and it just works!

![Docker on the Command Line](https://farm2.staticflickr.com/1538/26660797101_605058c25f_o.png)

Another nice improvement is that now you can access your containers with a simple URL like http://docker.local:32769/ 

Overall a great new experience and to me, it goes in the right direction.
