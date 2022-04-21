---
title: "ClickOnce ISignedCode::Sign returned error: 0x80880253"
permalink: /2010/01/12/ClickOnce-ISignedCodeSign-returned-error-0x80880253/
date: 1/12/2010 6:08:53 AM
updated: 1/12/2010 6:08:53 AM
disqusIdentifier: 20100112060853
tags: ["continuous integration", "Team City", "MSBuild", "ClickOnce"]
alias:
 - /post/ClickOnce-ISignedCodeSign-returned-error-0x80880253.aspx/index.html
---
Tonight we got the following issue on our [TeamCity](http://www.jetbrains.com/teamcity/index.html) build server which produce different ClickOnce setups :

> c:\WINDOWS\Microsoft.NET\Framework\v3.5\Microsoft.Common.targets(3652, 9): **error MSB3482: An error occurred while signing: Failed to sign ..\..\Tests\Output\bin\DeployClickOnce\app.publish\setup.exe**. 
<!-- more -->
> **SignTool Error: ISignedCode::Sign returned error: 0x80880253
> **    **The signer's certificate is not valid for signing.**
> SignTool Error: An error occurred while attempting to sign: ..\..\Tests\Output\bin\DeployClickOnce\app.publish\setup.exe

Checking the certificate used by our project I found that the expiration date was yesterday:

![](/images/4267117780_617c4d5071_o1_2F2A08B4.png) 

So I had first to create a new test certificate.

Then I had to re-install the certificate on the server, as described in “[ClickOnce certificate and TeamCity](http://weblogs.asp.net/lkempe/archive/2009/11/02/clickonce-certificate-and-teamcity.aspx)”. Before installing I had to remove the old one using:

> Psexec.exe -i -s cmd.exe

then running

> mmc

and removing by hand the old certifcate.
