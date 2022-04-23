---
title: "Cleaning BlogEngine.NET spam"
permalink: /2011/10/30/Cleaning-BlogEngineNET-spam/
date: 10/30/2011 8:45:39 PM
updated: 11/6/2011 12:28:19 AM
disqusIdentifier: 20111030084539
coverImage: https://farm6.staticflickr.com/5266/5561291406_3371746e56_b.jpg
coverSize: partial
thumbnailImage: https://farm6.staticflickr.com/5266/5561291406_3371746e56_q.jpg
coverCaption: "La savane des pétrification, Martinique"
alias:
 - /post/Cleaning-BlogEngineNET-spam.aspx/index.html
---
<!--[![Martinique 2011](http://farm6.static.flickr.com/5136/5561350588_1d52c313f9_m.jpg)](http://www.flickr.com/photos/laurentkempe/5561350588/ "Martinique 2011 by Laurent Kempé, on Flickr")-->

I just migrated [my blog](http://http://www.laurentkempe.com/) to the latest version of [BlogEngine.NET](http://www.dotnetblogengine.net/) 2.5.0.6.
<!-- more -->

I had a shock when I saw the number of spam that I had on the blog! 

![](http://farm7.static.flickr.com/6225/6294439936_9e93d8ce72_o.png)

**447883** Spam! Wow. So I started the cleaning by using BlogEngine tools but it was damn slow, and no way to stop it when you started the delete all.

So I stopped the web site which was a bad idea because then one XML file was damaged. As I always do a backup before doing something like that I was on the safe side, and just reverted the files.

Then I used 7zip to zip the posts folder which is located in the App_Data which was 338 MB, again wow.

![](http://farm7.static.flickr.com/6112/6293933845_b413086a55_o.png)

Downloaded the zip file on my local machine, installed BlogEngine and imported the post.

I thought it would be faster on my machine because it is a recent one. But still to slow to treat 447833 spam messages.

So as a developer I went on and wrote a little application to do it. And after cleanup the spam which took less than 10 seconds I went to this folder size of the posts

![](http://farm7.static.flickr.com/6211/6294464822_9a3587d1da_o.png)

Quite a difference ! And BlogEngine showing me the results

![](http://farm7.static.flickr.com/6104/6294472166_5fcb56ab90_o.png)

And here is the code, it is using .NET Framework 4 and the parallelization of queries to treat files:

```csharp
#region using

using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

#endregion

namespace BlogEngineSpamDelete
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var files = Directory.GetFiles(@"C:\Temp\blogengine\posts", "*.xml");
            foreach (var file in files.AsParallel())
            {
                FixPost(file);
            }
        }

        private static void FixPost(string file)
        {
            XDocument doc;
            using (var stream = File.OpenRead(file))
            {
                doc = XDocument.Load(stream);
            }

            var comments = from comment in doc.Descendants(XName.Get("comment", String.Empty))
                           select comment;

            var spamComments = from comment in comments.ToArray()
                               let data = new CommentState(comment.Attribute("spam").Value,
                                                           comment.Attribute("approved").Value,
                                                           comment.Attribute("deleted").Value) 
                               where ShouldDeleteSpamAndUnApproved(data)
                               select comment;

            foreach (var spamComment in spamComments)
            {
                spamComment.Remove();
            }

            using (var writer = XmlWriter.Create(file, new XmlWriterSettings {Indent = true}))
            {
                doc.WriteTo(writer);
            }
        }

        private static bool ShouldDeleteSpam(CommentState commentState)
        {
            return !commentState.Approved && 
                   (commentState.Spam || commentState.Deleted);
        }
        
        private static bool ShouldDeleteSpamAndUnApproved(CommentState commentState)
        {
            return !commentState.Approved || 
                   commentState.Spam ||
                   commentState.Deleted;
        }

        private class CommentState
        {
            public CommentState(String spam, String approved, String deleted)
            {
                Approved = bool.Parse(approved);
                Spam = bool.Parse(spam);
                Deleted = bool.Parse(deleted);
            }

            public bool Approved { get; private set; }
            public bool Spam { get; private set; }
            public bool Deleted { get; private set; }
        }
    }
}
```

**Update**: I also posted the code on bitbucket: [https://bitbucket.org/lkempe/blogenginespamdelete](https://bitbucket.org/lkempe/blogenginespamdelete)
