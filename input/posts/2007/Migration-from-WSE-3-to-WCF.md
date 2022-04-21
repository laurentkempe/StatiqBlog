---
title: "Migration from WSE 3 to WCF"
permalink: /2007/06/27/Migration-from-WSE-3-to-WCF/
date: 6/27/2007 4:28:25 PM
updated: 6/27/2007 4:28:25 PM
disqusIdentifier: 20070627042825
tags: ["ASP.NET 2.0", "Web Services", "VSTO", "WCF", "SOAP"]
alias:
 - /post/Migration-from-WSE-3-to-WCF.aspx/index.html
---
I started to migrate the [Tech Head Brothers](http://www.techheadbrothers.com/) authoring tool and portal from Web Service Enhancement 3 (WSE 3) to Windows Communication Foundation (WCF). This is a next step in the integration of .NET Framework 3 in Tech Head Brothers portal.

Till today I was using WSE 3 from the Word VSTO solution to securely publish content to the portal directly out of Word 2003/2007.
<!-- more -->

The migration went straight due to my initial implementation that was already using interfaces and implementation classes. So basically I had to :

*   Remove the reference to WSE 3 and add one to System.ServiceModel
*   Change the attributes on the interface to ServiceContract and OperationContract
*   Update the web.config to parameterize the new WCF endpoint, binding...
*   Regenerate the client proxy and update a bit the client code 

This first step took me around 1h30 and was working very good but was still missing all the security of the old version.

Then to implement the security part of the web services:

*   I created a new certificate
*   Removed the Policy using the self developed aspnetUsernameTokenSecurity that is not needed anymore
*   Configured the web.config with a new wcf service behavior using userNameAuthentication and serviceAuthorization linked to ASP.NET providers
*   Replaced code checking the role of the user calling the service with an attribute PrincipalPermission
*   Regenerated the client proxy and reconfigured it 

So now I have security at the level of the message that is encrypted using the certificate and at the web service with role access check.

Currently the solution uses attributes to check the role of the user that access the web service. I don't find the solution flexible enough and the next step will be to have the configuration in a configuration file, that would let me change access rights without changing any line of code.

As always when this will work I will publish the source code of the VSTO client authoring tool (Tech Head Brothers Authoring) on the codeplex project: [THBAuthoring](http://www.codeplex.com/THBAuthoring).
