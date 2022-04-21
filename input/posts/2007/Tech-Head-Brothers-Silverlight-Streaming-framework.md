---
title: "Tech Head Brothers Silverlight Streaming framework"
permalink: /2007/10/12/Tech-Head-Brothers-Silverlight-Streaming-framework/
date: 10/12/2007 4:24:05 AM
updated: 10/12/2007 4:24:05 AM
disqusIdentifier: 20071012042405
tags: ["Silverlight", "Silverlight Streaming"]
alias:
 - /post/Tech-Head-Brothers-Silverlight-Streaming-framework.aspx/index.html
---
[Tech Head Brothers Silverlight Streaming framework](http://www.codeplex.com/THBSLSFramework) is an implementation of [Silverlight Streaming REST API](http://msdn2.microsoft.com/en-us/library/bb851621.aspx) as a class library and a set of [Workflow Foundation](http://msdn.microsoft.com/workflow/) activities focused on the [Hosting Content on Silverlight Streaming](http://msdn2.microsoft.com/en-us/library/bb802532.aspx).  

I have implemented this light framework (some functionality are still missing) as a class library and [Patrice Lamarche](http://www.techheadbrothers.com/Auteurs.aspx/patrice-lamarche) ([http://patricelamarche.net](http://patricelamarche.net)) implemented the Workflow Foundation part of it.  
<!-- more -->

The following picture shows the classes used in the framework:  

![](http://farm3.static.flickr.com/2404/1544834652_bb88cafa0d_o.jpg) 

It is basic with a Manager class (*SilverlightStreamingManager*) using a set of Command classes (*AddFileCommand*, *DeleteFileCommand*, *GetFilesetFilesCommand*).

Uploading a Video is as simple as those few lines:

            <span style="color: rgb(43,145,175)">SilverlightStreamingManager</span> mgr =
                <span style="color: rgb(0,0,255)">new</span> <span style="color: rgb(43,145,175)">SilverlightStreamingManager</span>(<span style="color: rgb(163,21,21)">"TOBEDEFINED"</span>, <span style="color: rgb(163,21,21)">"TOBEDEFINED"</span>);

            <span style="color: rgb(0,0,255)">string</span> fileset = <span style="color: rgb(163,21,21)">"testvideo"</span>;

            <span style="color: rgb(0,0,255)">if</span> (mgr.UploadVideo(<span style="color: rgb(163,21,21)">@"T:\_Projects\Silverlight\SilverlightStreaming\media\fiona.wmv"</span>, fileset))
                <span style="color: rgb(43,145,175)">Console</span>.WriteLine(<span style="color: rgb(163,21,21)">"Uploaded sucessfully!! "</span>);
            <span style="color: rgb(0,0,255)">else
</span>                <span style="color: rgb(43,145,175)">Console</span>.WriteLine(<span style="color: rgb(163,21,21)">"Something went wrong !!"</span>);

You also might prefer to use the corresponding workflow activities delivered or even the sample workflow:

![](http://farm3.static.flickr.com/2166/1544053443_2beb3e2919_o.jpg) 

Enjoy!!
