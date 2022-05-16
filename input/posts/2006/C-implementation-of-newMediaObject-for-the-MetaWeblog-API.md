---
title: "C# implementation of newMediaObject for the MetaWeblog API"
permalink: /2006/08/28/C-implementation-of-newMediaObject-for-the-MetaWeblog-API/
date: 8/28/2006 4:46:34 AM
updated: 8/28/2006 4:46:34 AM
disqusIdentifier: 20060828044634
tags: ["Tech Head Brothers", "ASP.NET"]
---
If you have a blog you might know about the [MetaWeblog API](http://www.xmlrpc.com/metaWeblogApi). I implemented it for [Tech Head Brothers](http://www.techheadbrothers.com/) portal to be able to post news from a client. Today I am using Live Writer to post on my blog and I also wanted to have the possibility to post news on the new version of [Tech Head Brothers](http://www.techheadbrothers.com/) portal but with pictures and without the usage of a ftp server.

Checking the API I found a new method that I had not implemented: **newMediaObject**.
<!-- more -->

> **metaWeblog.newMediaObject (blogid, username, password, struct) returns struct**
>
> The *blogid*, *username* and *password* params are as in the Blogger API.
> 
> The struct must contain at least three elements, *name*, *type* and *bits*.
> 
> *name* is a string, it may be used to determine the name of the file that stores the object, or to display it in a list of objects. It determines how the weblog refers to the object. If the name is the same as an existing object stored in the weblog, it may replace the existing object.
> 
> *type* is a string, it indicates the type of the object, it's a standard MIME type, like audio/mpeg or image/jpeg or video/quicktime.
> 
> *bits* is a base64-encoded binary value containing the content of the object.
> 
> The struct may contain other elements, which may or may not be stored by the content management system.
> 
> If newMediaObject fails, it throws an error. If it succeeds, it returns a struct, which must contain at least one element, url, which is the url through which the object can be accessed. It must be either an FTP or HTTP url.

I defined in the interface two struct as following:

```csharp
public struct MediaObjectUrl
{
    public string url;
}

public struct MediaObject
{
    public string name;

    public string type;

    public byte[] bits;
}
```

Added the method in the IMetaWeblog interface:

```csharp
[XmlRpcMethod("metaWeblog.newMediaObject",
    Description="Add a media object to a post using the "
                + "metaWeblog API. Returns media url as a string.")]
MediaObjectUrl newMediaObject(
    string blogid,
    string username,
    string password,
    MediaObject mediaObject);
```

And finally the following implementation:

```csharp
///
/// Post a media object.
///
/// The blogid.
/// The username.
/// The password.
/// The media object.
/// MediaObjectUrl  defining the url of the media

public MediaObjectUrl newMediaObject(string blogid,
                                    string username,
                                    string password,
                                    MediaObject mediaObject)
{
    if (!ValidUser(username, password))
        throw new XmlRpcFaultException(0, "You have no right to do that.");

    string filename = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath,
                                   "images/" + mediaObject.name);

    if (!Directory.Exists(Path.GetDirectoryName(filename)))
        Directory.CreateDirectory(Path.GetDirectoryName(filename));

    File.WriteAllBytes(filename, mediaObject.bits);

    MediaObjectUrl mediaObjectUrl = new MediaObjectUrl();

    mediaObjectUrl.url = ConfigurationManager.AppSettings["BlogUrl"] +
                        "/images/" +
                        mediaObject.name;

    return mediaObjectUrl;
}
```

The good point now is that I am able to let the authors of the site post news with embeded pictures without managing a ftp server.
