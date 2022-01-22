---
title: 'Deploying Docker containers running ASP.NET Core RC2 to Microsoft Azure Cloud'
permalink: /2016/06/08/Deploying-Docker-containers-running-ASPNET-Core-RC2-to-Microsoft-Azure-Cloud/
date: 2016-06-08 13:45:01
tags: ["Microsoft Azure", "Docker", "ASP.NET Core RC2", "HipChat Connect"]
disqusIdentifier: 20160608134501
coverSize: partial
coverCaption: 'Bondi to Bronte coastal walk, Australia'
coverImage: 'https://c6.staticflickr.com/3/2837/9199431893_80c0511daf_h.jpg'
thumbnailImage: 'https://c6.staticflickr.com/3/2837/9199431893_f8b65b3e48_q.jpg'
---
Following my previous post on [ASP.NET Core RC2, Docker and HipChat Connect add-on](https://laurentkempe.com/2016/05/16/ASP-NET-Core-RC2-Docker-and-HipChat-Connect-add-on/) I wanted to learn the next step which is to deploy my Docker container on the Microsoft Azure Cloud!
<!-- more -->

Due to a [known issue](https://docs.docker.com/machine/drivers/azure/) and to avoid to have issues with credentials you need to follow the instructions on the following page "[Creating a Work or School identity in Azure Active Directory to use with Windows VMs](https://azure.microsoft.com/en-us/documentation/articles/virtual-machines-windows-create-aad-work-id/)"

>KNOWN ISSUE: There is a known issue with Azure Active Directory causing stored credentials to expire within hours rather than 14 days when the user logs in with personal Microsoft Account (formerly Live ID) instead of an Active Directory account. Currently, there is no ETA for resolution, however in the meanwhile, you can [create an AAD account](https://azure.microsoft.com/documentation/articles/virtual-machines-windows-create-aad-work-id/) and login with that as a workaround.

Now to get ready to deploy we need first the following:

1. Get an Azure account on [Azure](https://azure.microsoft.com)
2. Get Azure subscription id from Subscriptions link

![Azure subscription id screenshot](https://c5.staticflickr.com/8/7435/27355205412_b5b2f751fa_o.jpg)

Then we need to create a machine on the Azure cloud which will host our Docker containers. For that we will use Docker Machine, you can read more about it on this web page "[Use Docker Machine with the Azure driver](https://azure.microsoft.com/en-us/documentation/articles/virtual-machines-linux-docker-machine/)". 

Using Docker for Windows, I execute the following docker-machine command to create the machine

{% gist b46c5bfa339e1fd42b35d87191432c41 docker-machine_create.log %}

After a couple of minutes, we can check on the Azure cloud portal, All resources menu, that Azure created everything for us

![Azure ressources screenshot](https://c1.staticflickr.com/8/7006/27177992480_1b3056ce8f_o.jpg)

Finally, we need to get the configuration to talk to that machine, and it's Docker daemon

{% gist b46c5bfa339e1fd42b35d87191432c41 docker-machine_env.log %}

Then execute the last line which will configure our local environment  

> & "C:\Program Files\Docker\Docker\Resources\bin\docker-machine.exe" env hipchatconnect | Invoke-Expression

Now each docker commands will be executed by the machine hosted on Azure cloud.
Easy and awesome!

Next step is to have a Docker container to deploy! We will use the container from my previous post "[ASP.NET Core RC2, Docker and HipChat Connect add-on](http://laurentkempe.com/2016/05/16/ASP-NET-Core-RC2-Docker-and-HipChat-Connect-add-on/)".

First, we need to build an image using the following command

{% gist b46c5bfa339e1fd42b35d87191432c41 docker_build.log %}

Then we need to get the public IP of our machine on the Azure portal 

![Azure public ip](https://c6.staticflickr.com/8/7370/26847519653_66bc0cc208_o.jpg)

We can run the container on Azure cloud with

> docker run -d -p 80:5000 -e NGROK_URL='http://40.68.122.128' hipchatconnect

To be able to connect to our connector on the HTTP port 80, we will need to configure the firewall

![Azure firewall configuration](https://c3.staticflickr.com/8/7375/27420164226_eaa9aa3d94_o.jpg)

and finally, connect to it!

![Docker container running in Azure Cloud result](https://c6.staticflickr.com/8/7515/27383357301_ef760d740b_o.jpg)

What a great experience to be able to create a machine in the Azure Cloud from the command line and deploy our software so quickly!

Now that it works we will need to tackle the next issue, which is to expose the container through https because [HipChat Connect](https://developer.atlassian.com/hipchat/about-hipchat-connect) needs it so that the add-on can be installed in [HipChat](https://www.hipchat.com/). But that's for another post.
