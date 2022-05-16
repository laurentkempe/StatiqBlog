---
title: "Tech Head Brothers Silverlight Streaming framework"
permalink: /2007/10/12/Tech-Head-Brothers-Silverlight-Streaming-framework/
date: 10/12/2007 4:24:05 AM
updated: 10/12/2007 4:24:05 AM
disqusIdentifier: 20071012042405
tags: ["Silverlight", "Silverlight Streaming"]
---
[Tech Head Brothers Silverlight Streaming framework](http://www.codeplex.com/THBSLSFramework) is an implementation of [Silverlight Streaming REST API](http://msdn2.microsoft.com/en-us/library/bb851621.aspx) as a class library and a set of [Workflow Foundation](http://msdn.microsoft.com/workflow/) activities focused on the [Hosting Content on Silverlight Streaming](http://msdn2.microsoft.com/en-us/library/bb802532.aspx).  

I have implemented this light framework (some functionality are still missing) as a class library and [Patrice Lamarche](http://www.techheadbrothers.com/Auteurs.aspx/patrice-lamarche) ([http://patricelamarche.net](http://patricelamarche.net)) implemented the Workflow Foundation part of it.  
<!-- more -->

The following picture shows the classes used in the framework:  

![](http://farm3.static.flickr.com/2404/1544834652_bb88cafa0d_o.jpg) 

It is basic with a Manager class (*SilverlightStreamingManager*) using a set of Command classes (*AddFileCommand*, *DeleteFileCommand*, *GetFilesetFilesCommand*).

Uploading a Video is as simple as those few lines:

```csharp
SilverlightStreamingManager mgr =
    new SilverlightStreamingManager("TOBEDEFINED", "TOBEDEFINED");

string fileset = "testvideo";

if (mgr.UploadVideo(@"T:\_Projects\Silverlight\SilverlightStreaming\media\fiona.wmv", fileset))
    Console.WriteLine("Uploaded sucessfully!! ");
else
    Console.WriteLine("Something went wrong !!");
```

You also might prefer to use the corresponding workflow activities delivered or even the sample workflow:

![](http://farm3.static.flickr.com/2166/1544053443_2beb3e2919_o.jpg) 

Enjoy!!
