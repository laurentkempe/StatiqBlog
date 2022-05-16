---
title: "Alpha vertical slice of Silverlight Streaming on Tech Head Brothers"
permalink: /2007/10/17/Alpha-vertical-slice-of-Silverlight-Streaming-on-Tech-Head-Brothers/
date: 10/17/2007 4:42:26 AM
updated: 10/17/2007 4:42:26 AM
disqusIdentifier: 20071017044226
tags: ["Tech Head Brothers", "Silverlight", "Silverlight Streaming"]
---
Tonight I reached a new milestone on my current development for my portal Tech Head Brothers.

You might know, or guessed, from one of my last post; "[Tech Head Brothers Silverlight Streaming framework](http://weblogs.asp.net/lkempe/archive/2007/10/11/tech-head-brothers-silverlight-streaming-framework.aspx)" that I am working on adding [Silverlight Streaming](http://silverlight.live.com/) to Tech Head Brothers. I first [released a little framework](http://www.codeplex.com/THBSLSFramework) to ease the development against the REST API of [Silverlight Streaming](http://silverlight.live.com/). Now I went further on with a first vertical slice of the whole solution.
<!-- more -->

The solution is composed of four parts:

1.  a client application to post the video
2.  a service to receive the posted video
3.  an admin web application to publish the video
4.  a web interface to display the video 

After quite some discussions with [Mathieu](http://www.techheadbrothers.com/Auteurs.aspx/mathieu-kempe) about the best posting user experience for the different authors we finally decided that using [Live Writer](http://get.live.com/betas/writer_betas) was the best solution! And now that I have the first vertical slice I am really happy about the choice we made, because it makes the solution easy for the authors but also for us implementing the solution.

The client application customize Live Writer with a [SmartContentSource](http://msdn2.microsoft.com/en-us/library/aa738935.aspx) plugin letting the author upload it's video to [Silverlight Streaming](http://silverlight.live.com/) but also posting all information to Tech Head Brothers as a blog post.

The cool point here is that during the development of [innoveo solutions](http://www.innoveo.com/) website; my new company, I wrote a generic blog engine that basically let you define a blog just by adding an httphandler and implementing a Converter class:

*Definition of a new blog, that will be used in the httpHandlers part of the web.config*

```csharp
public class VideoBlog : GenericBlog<Video>
{
    public VideoBlog() : base(new VideoBlogAssembler())
    {
    }
}
```

*Definition of the converter class, converting an business entity to/from a Post*

```csharp
public class VideoBlogAssembler : IBlogAssembler<Video>
{
```


```csharp
/// <summary>
/// Converts the specified video.
/// </summary>
/// <param name="video">The video.</param>
/// <returns></returns>
public Post Convert(Video video)
{
    Post post = new Post();

    post.dateCreated = video.PublishDate;
    post.description = video.Description;
    ...
```

The admin part was quick to develop just extending the page I already had for articles publication.

And finally [Mathieu](http://www.techheadbrothers.com/Auteurs.aspx/mathieu-kempe) did a great job on the XAML Silverlight Player and all the javascript part that I juste needed to integrate.

So now you know it, yeah we are adding Video to Tech Head Brothers and I hope really soon.
