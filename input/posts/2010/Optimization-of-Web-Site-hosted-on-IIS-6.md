---
title: "Optimization of Web Site hosted on IIS 6"
permalink: /2010/01/03/Optimization-of-Web-Site-hosted-on-IIS-6/
date: 1/3/2010 7:33:51 PM
updated: 1/3/2010 7:33:51 PM
disqusIdentifier: 20100103073351
tags: ["IIS"]
alias:
 - /post/Optimization-of-Web-Site-hosted-on-IIS-6.aspx/index.html
---
The other day I had to re-install my whole server which is hosting [Tech Head Brothers](http://www.techheadbrothers.com/). I had also to redo the configuration to have IIS deliver compressed content. I was quite sure to have a post on this but the only thing I could find was “[Optimization of a Web Site - Using Content Expiration (IIS 6.0)](http://weblogs.asp.net/lkempe/archive/2007/07/25/optimization-of-a-web-site-using-content-expiration-iis-6-0.aspx)”

So I started from scratch and after some issues I finally managed to have it running. I started uisng the following post from Scott Forsyth, “[IIS Compression in IIS6.0](http://weblogs.asp.net/owscott/archive/2004/01/12/57916.aspx)” and I took over those steps from his post:
<!-- more -->

> ***First, before anything else, backup the metabase.  ***This is done by right-clicking on the server in the IIS snap-in and selecting All Tasks -> Backup/Restore Configuration.  The rest is straight forward.
> 
> **Create Compression Folder (optional)**
> 
> The first thing I do is create a folder on the D drive where the static file compression will be cached. You can call it anything you want or leave the default of “%windir%\IIS Temporary Compressed Files” if that works for you. The IUSR_{machinename} will need write permission to the folder. If you use custom anonymous users, make sure to assign the proper user. IIS will still work even if the permissions are wrong but the compression won't work properly. Once running, it's worth double checking Event Viewer to see if any errors are occurring that keep IIS Compression from working.
> 
> **Enable Compression in IIS**
> 
> - From the IIS snap-in, right-click on the Web Sites node and click on Properties
> - Select the Service tab - Enable *Compress application files*
> - Enable *Compress static files*
> - Change Temporary Directory to the folder that you created above, or leave it at it's default
> - Set the max size of the temp folder to something that the hard drive can handle. i.e. 1000. 
> - Save and close the Web Site Properties dialog
> 
> Note: The temporary compress directory is only used for static pages.  Dynamic pages aren't saved to disk and are recreated every time so there is some CPU overhead used on every page request for dynamic content.

And from the last part I did the following:

*   Open the metabase located at C:\Windows\system32\inetsrv\metabase.xml in Notepad
*   Search for <IIsCompressionScheme   

And changed it like this, take care of the carriage return for the HcFileExtensions and HcScriptFileExtensions, it is important:

<IIsCompressionScheme    Location ="/LM/W3SVC/Filters/Compression/deflate"     
        HcCompressionDll="%windir%\system32\inetsrv\gzip.dll"      
        HcCreateFlags="0"      
        HcDoDynamicCompression="TRUE"      
        HcDoOnDemandCompression="TRUE"      
        HcDoStaticCompression="TRUE"      
        HcDynamicCompressionLevel="**9**"      
       ** HcFileExtensions="css       
            htm        
            html        
            js        
            txt"        
**        HcOnDemandCompLevel="**9**"      
        HcPriority="1"      
        **HcScriptFileExtensions="asp       
            aspx        
            axd"        
**    >      
</IIsCompressionScheme>      
<IIsCompressionScheme    Location ="/LM/W3SVC/Filters/Compression/gzip"      
        HcCompressionDll="%windir%\system32\inetsrv\gzip.dll"      
        HcCreateFlags="1"      
        HcDoDynamicCompression="TRUE"      
        HcDoOnDemandCompression="TRUE"      
        HcDoStaticCompression="TRUE"      
        HcDynamicCompressionLevel="**9**"      
       ** HcFileExtensions="css       
            htm        
            html        
            js"        
**        HcOnDemandCompLevel="**9**"      
        HcPriority="2"      
       ** HcScriptFileExtensions="asp       
            aspx        
            axd"        
**    >

And then as I explained in the post “[Optimization of a Web Site - Using Content Expiration (IIS 6.0)](http://weblogs.asp.net/lkempe/archive/2007/07/25/optimization-of-a-web-site-using-content-expiration-iis-6-0.aspx)” I then reconfigured the content expiration "[Using Content Expiration (IIS 6.0)](http://www.microsoft.com/technet/prodtechnol/WindowsServer2003/Library/IIS/0fc16fe7-be45-4033-a5aa-d7fda3c993ff.mspx?mfr=true)".
