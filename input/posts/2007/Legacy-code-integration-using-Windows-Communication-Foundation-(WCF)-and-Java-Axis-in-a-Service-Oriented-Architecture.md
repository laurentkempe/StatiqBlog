---
title: "Legacy code integration using Windows Communication Foundation (WCF) and Java Axis in a Service Oriented Architecture"
permalink: /2007/06/22/Legacy-code-integration-using-Windows-Communication-Foundation-(WCF)-and-Java-Axis-in-a-Service-Oriented-Architecture/
date: 6/22/2007 4:17:11 AM
updated: 6/22/2007 4:17:11 AM
disqusIdentifier: 20070622041711
tags: ["Web Services", "WCF", "Interoperability", "Java", "Axis", ".NET Framework 3.0", "SOA", "SOAP"]
alias:
 - /post/Legacy-code-integration-using-Windows-Communication-Foundation-(WCF)-and-Java-Axis-in-a-Service-Oriented-Architecture.aspx/index.html
---
What are the options when you need to integrate Windows legacy code in a heterogeneous [Service Oriented Architecture](http://en.wikipedia.org/wiki/Service-oriented_architecture) (SOA)?  

The proposed problem was to expose a set of Windows C++ DLLs to a global SOA platform written in Java. Those DLLs would be then exposed as backend computation services.  
<!-- more -->

One of the options used in some past projects was to use the [Microsoft SOAP Toolkit](http://msdn2.microsoft.com/en-us/webservices/aa740662.aspx). But your C++ DLLs needs to be COM Objects for that. I experienced it and still have production code running with it. It works quite well even with surprising complex data structures. But as today it is definitely not the way to go.  

The service oriented world has evolved rapidly over the last years and using such an old deprecated technology ([Microsoft SOAP Toolkit](http://msdn2.microsoft.com/en-us/webservices/aa740662.aspx)) is not really efficient for a project. 

So at last it was time to check [.NET](http://msdn2.microsoft.com/en-us/netframework/default.aspx) in this entire [Java](http://java.sun.com/) world ;-)  

The general idea was to define a layered solution. From bottom up I first defined an interoperability layer using [.NET Interop](http://msdn2.microsoft.com/en-us/library/sd10k43k(VS.80).aspx) to be able to call the C++ DLLs from .NET. Then on top of this first layer I added another layer exposing the whole as a web service.  

![](http://www.techheadbrothers.com/images/blog/legacylayer.jpg)  

Now that I had the backend web service working I had to call it from Java. So are all those promises of web service interoperability just working out of the box?  

Yes and No. You need to first have a real look at the different frameworks stack you want to use. For example, when you are working with Java Axis you are tied to [SOAP 1.1](http://www.w3.org/TR/2000/NOTE-SOAP-20000508/), so you have to take care that the other side can understand this version of the [SOAP standard](http://www.w3.org/TR/soap/).  

So what do I have? First I have a client that must be written in Java using [Axis](http://ws.apache.org/axis/java/index.html) 1.2-1.4 using SOAP 1.1, then a C++ Windows DLL backend that must be exposed in an interoperable way. By luck I was free to choose the technology used on that backend. Both [ASP.NET](http://msdn2.microsoft.com/en-us/asp.net/default.aspx) web services (ASMX) and [WCF](http://msdn2.microsoft.com/en-us/library/ms735119.aspx) can interoperate with the SOAP 1.1 standard, so I decided to go on with [C#](http://msdn2.microsoft.com/en-us/vcsharp/aa336809.aspx) and [WCF](http://msdn2.microsoft.com/en-us/library/ms735119.aspx).  

The way I approached the problem was to go on iteratively, making a first proof of concept to integrate the C++ layer with the .NET layer, then to integrate this upper layer with WCF and a .NET client to finally finish with a Java client.  

Due to the ease of the interface exposed by the web service I didn’t wrote it using data contract design first. In more complex scenarios I would definitely go on with it because it leads to better success with interoperability.  

So I finally adopted the following final solution.  

### Adopted solution

On the backend a [Windows Communication Foundation](http://msdn2.microsoft.com/en-us/library/ms735119.aspx) web service written in C# using the [.NET Framework 3.0](http://www.netfx3.com/). The web service layer uses .NET interop to make a call to the legacy C++ Dlls.  

On the client side; a Java 1.5 proxy/stub class generated using [WSDL2Java](http://ws.apache.org/axis/java/user-guide.html#WSDL2JavaBuildingStubsSkeletonsAndDataTypesFromWSDL) out of the [WSDL](http://www.w3.org/TR/wsdl) exposed by the service.  

### Hosting

The last question concerning the web service was to define the way I wanted to host it?  

Two possibilities in my case: [Windows Service](http://en.wikipedia.org/wiki/Windows_service) and [Internet Information Services](http://www.microsoft.com/windowsserver2003/iis/default.mspx) (IIS).  

After making some [load test](http://en.wikipedia.org/wiki/Load_testing) on the web service I realized that it was [leaking memory](http://en.wikipedia.org/wiki/Memory_leak) so I finally went to the IIS solution because it provides you all the services for [recycling](http://www.microsoft.com/technet/prodtechnol/windowsserver2003/library/iis/0e570911-b88e-46be-96eb-a82f737dde5a.mspx) a buggy process. It was also easier because some part of the deployment process for web services hosted in IIS were already implemented and tested.  

### Faced problems

The first minor thing I went through was to check which versions of WCF and Axis are compatible. 

Then the C++ interop was the main work because it is not such an easy task to marshal between the different worlds in a correct way. 

And finally the last one was that the legacy C++ Dlls where using [ifstream](http://www.cplusplus.com/reference/iostream/ifstream/) to load configuration [ini files](http://en.wikipedia.org/wiki/INI_file), without a possibility to specify the full path of those files. As the web application was running in the [Application Pool process (w3wp.exe)](http://www.microsoft.com/technet/technetmag/issues/2006/01/ServingTheWeb/), loading the ini file was using the base path of w3wp.exe; C:\WINDWS\System32\inetsrv\. That would force me to deploy the ini files in that folder. What an ugly solution. Adding the bin folder of the application to the PATH was not making the trick too. And as we couldn’t change the C++ Dlls I had to find something else. 

The solution I came to, thanks to [David Wang](http://blogs.msdn.com/david.wang/) (yes you also [Richard](http://blogs.codes-sources.com/richardc/default.aspx) ;-), was to use this little method before calling into the legacy Dlls, changing the current directory:

        <span style="color: rgb(128,128,128)">///</span><span style="color: rgb(0,128,0)"> </span><span style="color: rgb(128,128,128)"><summary>
</span>        <span style="color: rgb(128,128,128)">///</span><span style="color: rgb(0,128,0)"> Sets the current directory.
</span>        <span style="color: rgb(128,128,128)">///</span><span style="color: rgb(0,128,0)"> </span><span style="color: rgb(128,128,128)"></summary>
</span>        <span style="color: rgb(0,0,255)">private</span> <span style="color: rgb(0,0,255)">static</span> <span style="color: rgb(0,0,255)">void</span> SetCurrentDirectory()
        {
            <span style="color: rgb(0,0,255)">string</span> binpath = <span style="color: rgb(43,145,175)">Path</span>.Combine(<span style="color: rgb(43,145,175)">HostingEnvironment</span>.ApplicationPhysicalPath, <span style="color: rgb(163,21,21)">"bin"</span>);
            <span style="color: rgb(43,145,175)">Directory</span>.SetCurrentDirectory(binpath);
        }
[](http://11011.net/software/vspaste)


### Conclusion

I really like the way WCF is handling the separation between the service description, the implementation and the configuration that will define how you expose the service to the world. 


The other thing I still enjoy to see is complex systems using so different technologies talking to each other. Here we had a caller written in Java running on a Linux server calling a web service written in C# calling multiple C++ Dlls running on a Windows server.


I am still amazed about those little things!!!
