---
title: "C# implementation of newMediaObject for the MetaWeblog API"
permalink: /2006/08/28/C-implementation-of-newMediaObject-for-the-MetaWeblog-API/
date: 8/28/2006 4:46:34 AM
updated: 8/28/2006 4:46:34 AM
disqusIdentifier: 20060828044634
tags: ["Tech Head Brothers", "ASP.NET"]
alias:
 - /post/C-implementation-of-newMediaObject-for-the-MetaWeblog-API.aspx/index.html
---
If you have a blog you might know about the [MetaWeblog API](http://www.xmlrpc.com/metaWeblogApi). I implemented it for [Tech Head Brothers](http://www.techheadbrothers.com/) portal to be able to post news from a client. Today I am using Live Writer to post on my blog and I also wanted to have the possibility to post news on the new version of [Tech Head Brothers](http://www.techheadbrothers.com/) portal but with pictures and without the usage of a ftp server.

Checking the API I found a new method that I had not implemented: **newMediaObject**.
<!-- more -->

> **metaWeblog.newMediaObject (blogid, username, password, struct) returns struct
> **
> The *blogid*, *username* and *password* params are as in the Blogger API.
> 
> The struct must contain at least three elements, *name*, *type *and *bits*.
> 
> *name* is a string, it may be used to determine the name of the file that stores the object, or to display it in a list of objects. It determines how the weblog refers to the object. If the name is the same as an existing object stored in the weblog, it may replace the existing object.
> 
> *type *is a string, it indicates the type of the object, it's a standard MIME type, like audio/mpeg or image/jpeg or video/quicktime.
> 
> *bits *is a base64-encoded binary value containing the content of the object.
> 
> The struct may contain other elements, which may or may not be stored by the content management system.
> 
> If newMediaObject fails, it throws an error. If it succeeds, it returns a struct, which must contain at least one element, url, which is the url through which the object can be accessed. It must be either an FTP or HTTP url.

I defined in the interface two struct as following:
 <div style="border-right: 1px solid; padding-right: 10px; border-top: 1px solid; padding-left: 10px; font-size: 11px; background: #fafafa; padding-bottom: 10px; border-left: 1px solid; color: #333333; line-height: 15px; padding-top: 10px; border-bottom: 1px solid; font-family: verdana, helvetica, arial, sans-serif">

<span style="color: blue">public</span> <span style="color: blue">struct</span> <span style="color: teal">MediaObjectUrl</span>

{

    <span style="color: blue">public</span> <span style="color: blue">string</span> url;

}
</div>  

<div style="border-right: 1px solid; padding-right: 10px; border-top: 1px solid; padding-left: 10px; font-size: 11px; background: #fafafa; padding-bottom: 10px; border-left: 1px solid; color: #333333; line-height: 15px; padding-top: 10px; border-bottom: 1px solid; font-family: verdana, helvetica, arial, sans-serif">

<span style="color: blue">public</span> <span style="color: blue">struct</span> <span style="color: teal">MediaObject</span>

{

    <span style="color: blue">public</span> <span style="color: blue">string</span> name;

    <span style="color: blue">public</span> <span style="color: blue">string</span> type;

    <span style="color: blue">public</span> <span style="color: blue">byte</span>[] bits;

}
</div>


Added the method in the IMetaWeblog interface:

<div style="border-right: 1px solid; padding-right: 10px; border-top: 1px solid; padding-left: 10px; font-size: 11px; background: #fafafa; padding-bottom: 10px; border-left: 1px solid; color: #333333; line-height: 15px; padding-top: 10px; border-bottom: 1px solid; font-family: verdana, helvetica, arial, sans-serif">

[<span style="color: teal">XmlRpcMethod</span>(<span style="color: maroon">"metaWeblog.newMediaObject"</span>,

    Description=<span style="color: maroon">"Add a media object to a post using the "</span>

                + <span style="color: maroon">"metaWeblog API. Returns media url as a string."</span>)]

<span style="color: teal">MediaObjectUrl</span> newMediaObject(

    <span style="color: blue">string</span> blogid,

    <span style="color: blue">string</span> username,

    <span style="color: blue">string</span> password,

    <span style="color: teal">MediaObject</span> mediaObject);
</div>


And finally the following implementation:

<div style="border-right: 1px solid; padding-right: 10px; border-top: 1px solid; padding-left: 10px; font-size: 11px; background: #fafafa; padding-bottom: 10px; border-left: 1px solid; color: #333333; line-height: 15px; padding-top: 10px; border-bottom: 1px solid; font-family: verdana, helvetica, arial, sans-serif">

<span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"><summary></span>

<span style="color: gray">///</span><span style="color: green"> Post a media object.</span>

<span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"></summary></span>

<span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"><param name="blogid"></span><span style="color: green">The blogid.</span><span style="color: gray"></param></span>

<span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"><param name="username"></span><span style="color: green">The username.</span><span style="color: gray"></param></span>

<span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"><param name="password"></span><span style="color: green">The password.</span><span style="color: gray"></param></span>

<span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"><param name="mediaObject"></span><span style="color: green">The media object.</span><span style="color: gray"></param></span>

<span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"><returns></span><span style="color: green">MediaObjectUrl  defining the url of the media</span><span style="color: gray"></returns></span>

<span style="color: blue">public</span> <span style="color: teal">MediaObjectUrl</span> newMediaObject(<span style="color: blue">string</span> blogid, 

                                     <span style="color: blue">string</span> username, 

                                     <span style="color: blue">string</span> password, 

                                     <span style="color: teal">MediaObject</span> mediaObject)

{

    <span style="color: blue">if</span> (!ValidUser(username, password))

        <span style="color: blue">throw</span> <span style="color: blue">new</span> <span style="color: teal">XmlRpcFaultException</span>(0, <span style="color: maroon">"You have no right to do that."</span>);

    <span style="color: blue">string</span> filename = <span style="color: teal">Path</span>.Combine(<span style="color: teal">HttpContext</span>.Current.Request.PhysicalApplicationPath, 

                                   <span style="color: maroon">"images/"</span> + mediaObject.name);

    <span style="color: blue">if</span> (!<span style="color: teal">Directory</span>.Exists(<span style="color: teal">Path</span>.GetDirectoryName(filename)))

        <span style="color: teal">Directory</span>.CreateDirectory(<span style="color: teal">Path</span>.GetDirectoryName(filename));

    <span style="color: teal">File</span>.WriteAllBytes(filename, mediaObject.bits);

    <span style="color: teal">MediaObjectUrl</span> mediaObjectUrl = <span style="color: blue">new</span> <span style="color: teal">MediaObjectUrl</span>();

    mediaObjectUrl.url = <span style="color: teal">ConfigurationManager</span>.AppSettings[<span style="color: maroon">"BlogUrl"</span>] + 

                         <span style="color: maroon">"/images/"</span> + 

                         mediaObject.name;

    <span style="color: blue">return</span> mediaObjectUrl;

}
</div>


The good point now is that I am able to let the authors of the site post news with embeded pictures without managing a ftp server. 
