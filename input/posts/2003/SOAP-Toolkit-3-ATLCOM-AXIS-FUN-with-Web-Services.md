---
title: "SOAP Toolkit 3, ATL/COM, AXIS : FUN with Web Services"
permalink: /2003/11/07/SOAP-Toolkit-3-ATLCOM-AXIS-FUN-with-Web-Services/
date: 11/7/2003 7:42:00 AM
updated: 11/7/2003 7:42:00 AM
disqusIdentifier: 20031107074200
tags: ["Tools"]
alias:
 - /post/SOAP-Toolkit-3-ATLCOM-AXIS-FUN-with-Web-Services.aspx/index.html
---
<P>Today I was able to validate a first step in one of the change we are experiencing at work. The project is to wrap an original project (C++ dll and VB6 ActiveX) around a C++ ATL/COM dll to be able to expose it as a Web Service in IIS 6. I used SOAP Toolkit 3.0 to do that. The difficulties were that there were some complicated structures and array of structures to pass in and get back from the Web Service. First I was amazed to see that the STK 3 was able to handle that complexity. Then I created a stub using AXIS WSDL2Java and little client application. It is really cool to see those peaces of code communicating without any issues. I like the idea of having two totally different worlds able to talk together ;-) No we have to deal with another big step the integration of the different possible stubs in our Virtual Transport Framework. This will allow users of the framework to call a service without knowing the transport protocol, the location, the load balancing strategy, the error handling…</P>
