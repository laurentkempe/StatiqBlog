---
title: 'Build, ship and run ASP.NET Core on Microsoft Azure using Docker Cloud'
permalink: /2016/07/18/Build-ship-and-run-ASP-NET-Core-on-Microsoft-Azure-using-Docker-Cloud/
date: 2016-07-18 19:04:21
tags: ["ASP.NET Core", "Microsoft Azure", "Docker", "HipChat Connect"]
disqusIdentifier: 20160718190421
coverSize: partial
coverCaption: 'LO Ferré, Petite Anse, Martinique, France'
coverImage: 'https://c7.staticflickr.com/9/8689/16775792438_e45283970c_h.jpg'
thumbnailImage: 'https://c7.staticflickr.com/9/8689/16775792438_8366ee5732_q.jpg'
---
In the last posts, we looked at the code to [build a HipChat Connect add-on with ASP.NET Core](http://laurentkempe.com/2016/05/16/ASP-NET-Core-RC2-Docker-and-HipChat-Connect-add-on/), [run the solution in a Docker container](http://laurentkempe.com/2016/06/08/Deploying-Docker-containers-running-ASPNET-Core-RC2-to-Microsoft-Azure-Cloud/) and [secure the access to the application with a valid HTTPS certificate that we got from Let's Encrypt for free](http://laurentkempe.com/2016/06/20/Free-HTTPS-certificates-for-Docker-containers-running-ASPNET-Core-RC2-on-Microsoft-Azure/) and finally deploy all of this on Azure!
<!-- more -->

One of my priority while working on new projects is to put in place Continous Integration/Continuous Delivery so that developers can develop and integrate the result of their work in a fast, efficient and easy way.

We have now all the necessary components to create that Continuous Integration and Continuous Delivery pipeline!

The goal is to have something which operates like that

1. A developer merges something on the master branch of our Github repository
2. It triggers a build of a Docker container image using the Dockerfile versioned in Github, which internally compile our new version of our ASP.NET Core web application
3. In case of a successful build, it pushes the new Docker container image to a Docker repository
4. Finally, the new Docker image is deployed as a Docker container on Azure

To fulfill those requirements, we will use [Docker Cloud](https://cloud.docker.com/) which lets you

> Build, ship, and run - any app, anywhere

Exactly what we need!

> Automate Pipelines - Set up a fully automated CI/CD workflow in minutes, from git push to production.

And as we want to use Docker the next argument is also interesting

> A Single Docker Platform- Docker Cloud has everything you need to build, ship and run your Dockerized application.

So if you haven't any Docker account you will first need to create one to be able to log in Docker Cloud.

Then you will need to **link Docker Cloud to your provider**, [in our case Microsoft Azure](https://docs.docker.com/docker-cloud/infrastructure/link-azure/).

Now that Docker Cloud can operate Microsoft Azure, the next step is to **create a node**. A node is a Linux host or virtual machine used to deploy and run containers. It is the equivalent of what we did with the docker-machine command line on the post "[Deploying Docker containers running ASP.NET Core RC2 to Microsoft Azure Cloud](http://laurentkempe.com/2016/06/08/Deploying-Docker-containers-running-ASPNET-Core-RC2-to-Microsoft-Azure-Cloud/)". You can get all the instruction on the page "[Deploy Your First Node](https://docs.docker.com/docker-cloud/getting-started/your_first_node/)".

In our case we created the following node:

![Docker Cloud node](https://c4.staticflickr.com/9/8141/27745012403_54637b1580_o.jpg)

Currently, we have a machine, a node in Docker Cloud wording, so we are ready to deploy our containers on that node. We could **create a service**, which is a container, or a group of containers from the same Docker repository. But we are more interested in **creating a stack**,  which specifies a group of services that make up an application, similar to Docker Compose. As we have seen in "[Free HTTPS certificates for Docker containers running ASP.NET Core RC2 on Microsoft Azure](http://laurentkempe.com/2016/06/20/Free-HTTPS-certificates-for-Docker-containers-running-ASPNET-Core-RC2-on-Microsoft-Azure/)," our application is currently made of two components served as two containers: 
1. NGINX, reverse proxy and setting up Let's Encrypt certificates
2. Kestrel delivering our ASP.NET Core 1.0 hipchat connect web application

To create our stack, we are re-using our docker-compose.yml file set up in the previous post

<div style="clear:both;"></div>{% gist d529fcdf54724a900533f26fa4a768c3 docker-compose.yml %}

which we modify slightly to describe our stack
<div style="clear:both;"></div>{% gist 29165d3e6874cf4cc27d83ead5b8bd28 stackfile.yml %}

The only difference, except the reordering, is to instruct automatic redeployment of the hipchatconnect container when an update of its image occurs in Docker Cloud registry, with the following line

> autoredeploy: true

After creating the stack file and hitting the deploy button, we see the result of our stack with its two containers running on our node on Microsoft Azure!

![Docker Cloud stack](https://c6.staticflickr.com/9/8737/27745039973_30f15757da_o.jpg)

Let's have a break a moment to see what we have achieved at the moment and what remains to accomplish our goal to have a proper CI/CD pipeline and be able to deploy to production from a simple commit/merge in source code repository.
We have at the moment all the infrastructure ready. We have a node which is a machine in Azure running our application which is composed of two Docker containers.
So the last point is to connect this infrastructure to our source code repository which is a git repository on Github.

To do that we will use the *Build* part offered by Docker Cloud and the repositories. A repository is a collection of tagged images. When you create a service, you choose an image to use to create containers. You can read more about it on the page "[Docker Cloud repositories](https://docs.docker.com/docker-cloud/builds/repos/)"
Then we will enable the [autobuild](https://docs.docker.com/docker-cloud/builds/automated-build/) by connecting it to our Github repository which will trigger a new build with every git push to our source code repository.

![Docker Cloud Github Repository Build](https://c6.staticflickr.com/9/8782/28360642885_2f32f1a280_o.jpg)

Pretty impressive results in such a little effort and short time! I have built some CI/CD in the past, but it wasn't that easy, we see clearly the potential of today's tooling that gives us so much power as software engineers!
