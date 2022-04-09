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

```shell {data-file=docker-machine_create.log data-gist=b46c5bfa339e1fd42b35d87191432c41}
C:\Users\Laurent
> docker-machine create --driver azure --azure-location westeurope --azure-subscription-id ADDYOURS hipchatconnect
Running pre-create checks...
(hipchatconnect) Completed machine pre-create checks.
Creating machine...
(hipchatconnect) Querying existing resource group.  name="docker-machine"
(hipchatconnect) Resource group "docker-machine" already exists.
(hipchatconnect) Configuring availability set.  name="docker-machine"
(hipchatconnect) Configuring network security group.  name="hipchatconnect-firewall" location="westeurope"
(hipchatconnect) Querying if virtual network already exists.  name="docker-machine-vnet" location="westeurope"
(hipchatconnect) Configuring subnet.  name="docker-machine" vnet="docker-machine-vnet" cidr="192.168.0.0/16"
(hipchatconnect) Creating public IP address.  name="hipchatconnect-ip" static=false
(hipchatconnect) Creating network interface.  name="hipchatconnect-nic"
(hipchatconnect) Creating storage account.  name="vhdsomfr2ogy52o" location="westeurope"
(hipchatconnect) Creating virtual machine.  name="hipchatconnect" location="westeurope" size="Standard_A2" username="docker-user" osImage="canonical:UbuntuServer:15.10:latest"
Waiting for machine to be running, this may take a few minutes...
Detecting operating system of created instance...
Waiting for SSH to be available...
Detecting the provisioner...
Provisioning with ubuntu(systemd)...
Installing Docker...
Copying certs to the local machine directory...
Copying certs to the remote machine...
Setting Docker configuration on the remote daemon...
Checking connection to Docker...
Docker is up and running!
To see how to connect your Docker Client to the Docker Engine running on this virtual machine, run: C:\Program Files\Docker\Docker\Resources\bin\docker-machine.exe env hipchatconnect
```

After a couple of minutes, we can check on the Azure cloud portal, All resources menu, that Azure created everything for us

![Azure ressources screenshot](https://c1.staticflickr.com/8/7006/27177992480_1b3056ce8f_o.jpg)

Finally, we need to get the configuration to talk to that machine, and it's Docker daemon

```shell {data-file=docker-machine_env.log data-gist=b46c5bfa339e1fd42b35d87191432c41}
> docker-machine env hipchatconnect
$Env:DOCKER_TLS_VERIFY = "1"
$Env:DOCKER_HOST = "tcp://x.y.z.y:2376"
$Env:DOCKER_CERT_PATH = "C:\Users\Laurent\.docker\machine\machines\hipchatconnect"
$Env:DOCKER_MACHINE_NAME = "hipchatconnect"
# Run this command to configure your shell:
# & "C:\Program Files\Docker\Docker\Resources\bin\docker-machine.exe" env hipchatconnect | Invoke-Expression
```

Then execute the last line which will configure our local environment  

> & "C:\Program Files\Docker\Docker\Resources\bin\docker-machine.exe" env hipchatconnect | Invoke-Expression

Now each docker commands will be executed by the machine hosted on Azure cloud.
Easy and awesome!

Next step is to have a Docker container to deploy! We will use the container from my previous post "[ASP.NET Core RC2, Docker and HipChat Connect add-on](http://laurentkempe.com/2016/05/16/ASP-NET-Core-RC2-Docker-and-HipChat-Connect-add-on/)".

First, we need to build an image using the following command

```shell {data-file=gist  docker_build.log data-gist=b46c5bfa339e1fd42b35d87191432c41}
> docker build -t hipchatconnect .
Sending build context to Docker daemon 1.441 MB
Step 1 : FROM microsoft/dotnet
latest: Pulling from microsoft/dotnet

8b87079b7a06: Pull complete
a3ed95caeb02: Pull complete
1bb8eaf3d643: Pull complete
3e04171ce2e5: Pull complete
bd09d56bccd6: Pull complete
5d7408f25e6a: Pull complete
Digest: sha256:5ffec15e54ce30c40dc40d81222d87495b675d81d68c68eda09558d62ac211aa
Status: Downloaded newer image for microsoft/dotnet:latest
---> ceef90b6765e
Step 2 : ENV ASPNETCORE_URLS "http://*:5000"
---> Running in fd2186f53b53
---> e97d74f73f17
Removing intermediate container fd2186f53b53
Step 3 : ENV ASPNETCORE_ENVIRONMENT "Development"
---> Running in 5e768a476c06
---> cfcafa6ec0b4
Removing intermediate container 5e768a476c06
Step 4 : COPY . /app
---> c306c31742cc
Removing intermediate container 1bdb65029a8a
Step 5 : WORKDIR /app
---> Running in a41ce7e35415
---> d074cc6cb3fb
Removing intermediate container a41ce7e35415
Step 6 : RUN dotnet restore
---> Running in e93f545f01f1
log  : Restoring packages for /app/project.json...
info :   GET https://www.myget.org/F/aspnetrc2/api/v3/flatcontainer/microsoft.netcore.app/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.netcore.app/index.json
info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.aspnetcore.mvc/index.json
...
NuGet Config files used:
/app/nuget.config
/root/.nuget/NuGet/NuGet.Config

Feeds used:
https://www.myget.org/F/aspnetrc2/api/v3/index.json
https://api.nuget.org/v3/index.json

Installed:
206 package(s) to /app/project.json
---> e3a436d91604
Removing intermediate container e93f545f01f1
Step 7 : EXPOSE 5000
---> Running in d7cee657215f
---> 9ee89d4fb68d
Removing intermediate container d7cee657215f
Step 8 : ENTRYPOINT dotnet run
---> Running in 333a6914a1d8
---> b1685a1c81f8
Removing intermediate container 333a6914a1d8
Successfully built b1685a1c81f8
SECURITY WARNING: You are building a Docker image from Windows against a non-Windows Docker host. All files and directories added to build context will have '-rwxr-xr-x' permissions. It is recommended to double check and reset permissions for sensitive files and directories.
```

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
