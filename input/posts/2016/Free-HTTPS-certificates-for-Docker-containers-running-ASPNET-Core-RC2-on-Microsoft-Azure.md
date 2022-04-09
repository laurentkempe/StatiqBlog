---
title: Free HTTPS certificates for Docker containers running ASP.NET Core RC2 on Microsoft Azure
permalink: /2016/06/20/Free-HTTPS-certificates-for-Docker-containers-running-ASPNET-Core-RC2-on-Microsoft-Azure/
date: 2016-06-20 19:51:30
tags: ["Microsoft Azure", "Docker", "ASP.NET Core RC2", "HipChat Connect"]
disqusIdentifier: 20160620195130
coverSize: partial
coverCaption: 'Anse Caffard, Martinique, France'
coverImage: 'https://c3.staticflickr.com/2/1522/25005836130_d09528cb16_h.jpg'
thumbnailImage: 'https://c3.staticflickr.com/2/1522/25005836130_8b9c330c8e_q.jpg'
---
Following my previous posts on [ASP.NET Core RC2, Docker and HipChat Connect add-on]() and [Deploying Docker containers running ASP.NET Core RC2 to Microsoft Azure Cloud]() I needed, as promised in the last post, to come with a solution to secure the whole with an HTTPS certificate!
<!-- more -->

So the overall goal is to have my Hipchat Connect add-on written in C# and ASP.NET Core RC2 hosted in a Linux Docker container running in Microsoft Azure and secured by an HTTPS certificate.

I am following for quite some time the [Let's Encrypt](https://letsencrypt.org/) project which is just perfect for what I want to do!

> Let’s Encrypt is a new Certificate Authority: It’s free, automated, and open.

Now that I know that I can get in an automated way and for free an HTTPS certificate, the question is how should I architecture the solution? The answer is quite simple. As I use Docker, I can plug another Docker container with a reverse proxy in front of my current container which runs the [Kestrel](https://github.com/aspnet/KestrelHttpServer) web server hosting our C# and ASP.NET Core RC2 code.

<?# image center clear group=blog https://c6.staticflickr.com/8/7494/27687906621_782bf08e81_o.jpg alt="Reverse proxy & Kestrel containers architecture" /?>

Ok which reverse proxy then? First, I had a look to [NGINX](https://www.nginx.com/resources/wiki/#) which I knew then found [Traefik](https://traefik.io/). I liked very much the [DEVOXX presentation, in French](https://www.youtube.com/watch?v=QvAz9mVx5TI), from its author [Emile Vauge](https://twitter.com/emilevauge) and it, supports Let's Encrypt out of the box.
<?# Plyr video=QvAz9mVx5TI /?>

I tried Traefik but till now could not make it work the way I wanted, so back to NGINX. But I do not give up.

I searched for a Docker image which runs NGINX and can be configured to get automatically and renew Let's Encrypt certificates! I finally found [HTTPS-PORTAL](https://github.com/SteveLTN/https-portal) which was looking very promising.

> A fully automated HTTPS server powered by Nginx, Let's Encrypt and Docker.

To achieve the architecture I described before I started to use [Docker compose](https://docs.docker.com/compose/) so that with one command I can start/stop both containers either locally or on Microsoft Azure!

Following the [documentation of HTTPS-PORTAL](https://github.com/SteveLTN/https-portal#quick-start) I finally got the whole working with the following docker-compose.yml file
```yaml {data-file=docker-compose.yml data-gist=d529fcdf54724a900533f26fa4a768c3}
https-portal:
  image: steveltn/https-portal
  ports:
    - '80:80'
    - '443:443'
  links:
    - hipchatconnect
  restart: always
  environment:
    DOMAINS: 'hipchat.laurentkempe.com -> http://hipchatconnect:5000'
    STAGE: 'production'
    # STAGE: local
    # FORCE_RENEW: 'true'

hipchatconnect:
  image: laurentkempe/hipchatconnect
  environment:
    BASE_URL: 'https://hipchat.laurentkempe.com'
```

I fell into a trap, so be aware of it! I got first NGINX 502 Bad gateway error. I thought that the docker-compose.yml part for hipchat had to declare an exposed port. In fact, it doesn't work like that. We are using links so that both containers are on the same internal network and that they can communicate. But that happens on their internal ports, so in our case for Kestrel and our Docker image, it is the port 5000. To realize that I had to connect to the container using

<?! alert info ?>
\> docker exec -i -t CONTAINER /bin/bash
<?!/ alert ?>

I installed wget, got the IP of the Kestrel container and tried to connect to it using wget also to get an error. That's the moment I realized about the port!

To have [Let's Encrypt set up our HTTPS server](https://letsencrypt.org/how-it-works/) and have it automatically obtain a browser-trusted certificate, without any human intervention, we need first to own a domain and to configure the DNS so that it resolves to the public IP of our virtual machine.

But first of all we need to configure our Azure IP address to be static

![Microsoft Azure static IP](https://c2.staticflickr.com/8/7340/27762904595_2258b2973e_o.png)

And don't forget to open the 443 HTTPS port on the Microsoft Azure firewall.

Now we are ready to start both containers using Docker compose
```shell {data-file=docker-compose.exe data-gist=d529fcdf54724a900533f26fa4a768c3}
> docker-compose.exe up -d
Starting hipchatconnect_hipchatconnect_1
Starting hipchatconnect_https-portal_1
```

To be able to see what's going on in the container we can use *docker ps* then *docker logs*
```shell {data-file=docker-ps data-gist=d529fcdf54724a900533f26fa4a768c3}
> docker ps
CONTAINER ID        IMAGE                         COMMAND             CREATED      
c7f789a5656b        steveltn/https-portal         "/init"             2 minutes ago
e7dc0907e7ce        laurentkempe/hipchatconnect   "dotnet run"        19 hours ago 
```
```shell {data-file=docker-logs data-gist=d529fcdf54724a900533f26fa4a768c3}
> docker logs -f c7f789a5656b
[fix-attrs.d] applying owners & permissions fixes...
[fix-attrs.d] 00-runscripts: applying...
[fix-attrs.d] 00-runscripts: exited 0.
[fix-attrs.d] done.
[cont-init.d] executing container initialization scripts...
[cont-init.d] 00-setup: executing...
2016/06/19 05:45:33 [notice] 118#118: signal process started
Generating RSA private key, 2048 bit long modulus
..+++
.........................................+++
e is 65537 (0x10001)
Signing certificates from https://acme-v01.api.letsencrypt.org ...
Parsing account key...
Parsing CSR...
Registering account...
Registered!
Verifying hipchat.laurentkempe.com...
hipchat.laurentkempe.com verified!
Signing certificate...
Certificate signed!
2016/06/19 05:45:41 [notice] 138#138: signal process started
Signed key for hipchat.laurentkempe.com
2016/06/19 05:45:41 [notice] 139#139: signal process started
[cont-init.d] 00-setup: exited 0.
[cont-init.d] 10-set-docker-gen-status: executing...
[cont-init.d] 10-set-docker-gen-status: exited 0.
[cont-init.d] done.
[services.d] starting services
[services.d] done.
Starting crond ...
```

Finally, we can use a web browser to access our public API now through HTTPS. And we can add the HipChat Connect add-on to one of our room.

![HipChat Connect add-on](https://c2.staticflickr.com/8/7640/27764957416_3d36d8123c_o.gif)

The next step will be to automate all of this process so that we can develop, build and deploy all of this quickly and automatically!
