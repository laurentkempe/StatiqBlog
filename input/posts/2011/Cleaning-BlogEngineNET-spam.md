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

<style type="text/css">
.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
    line-height: 135%;
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style>

<pre class="csharpcode"><span class="preproc">#region</span> <span class="kwrd">using</span>

<span class="kwrd">using</span> System;
<span class="kwrd">using</span> System.IO;
<span class="kwrd">using</span> System.Linq;
<span class="kwrd">using</span> System.Xml;
<span class="kwrd">using</span> System.Xml.Linq;

<span class="preproc">#endregion</span>

<span class="kwrd">namespace</span> BlogEngineSpamDelete
{
    <span class="kwrd">internal</span> <span class="kwrd">class</span> Program
    {
        <span class="kwrd">private</span> <span class="kwrd">static</span> <span class="kwrd">void</span> Main(<span class="kwrd">string</span>[] args)
        {
            var files = Directory.GetFiles(<span class="str">@"C:\Temp\blogengine\posts"</span>, <span class="str">"*.xml"</span>);
            <span class="kwrd">foreach</span> (var file <span class="kwrd">in</span> files.AsParallel())
            {
                FixPost(file);
            }
        }

        <span class="kwrd">private</span> <span class="kwrd">static</span> <span class="kwrd">void</span> FixPost(<span class="kwrd">string</span> file)
        {
            XDocument doc;
            <span class="kwrd">using</span> (var stream = File.OpenRead(file))
            {
                doc = XDocument.Load(stream);
            }

            var comments = from comment <span class="kwrd">in</span> doc.Descendants(XName.Get(<span class="str">"comment"</span>, String.Empty))
                           select comment;

            var spamComments = from comment <span class="kwrd">in</span> comments.ToArray()
                               let data = <span class="kwrd">new</span> CommentState(comment.Attribute(<span class="str">"spam"</span>).Value,
                                                           comment.Attribute(<span class="str">"approved"</span>).Value,
                                                           comment.Attribute(<span class="str">"deleted"</span>).Value) 
                               <span class="kwrd">where</span> ShouldDeleteSpamAndUnApproved(data)
                               select comment;

            <span class="kwrd">foreach</span> (var spamComment <span class="kwrd">in</span> spamComments)
            {
                spamComment.Remove();
            }

            <span class="kwrd">using</span> (var writer = XmlWriter.Create(file, <span class="kwrd">new</span> XmlWriterSettings {Indent = <span class="kwrd">true</span>}))
            {
                doc.WriteTo(writer);
            }
        }

        <span class="kwrd">private</span> <span class="kwrd">static</span> <span class="kwrd">bool</span> ShouldDeleteSpam(CommentState commentState)
        {
            <span class="kwrd">return</span> !commentState.Approved &amp;&amp; 
                   (commentState.Spam || commentState.Deleted);
        }
        
        <span class="kwrd">private</span> <span class="kwrd">static</span> <span class="kwrd">bool</span> ShouldDeleteSpamAndUnApproved(CommentState commentState)
        {
            <span class="kwrd">return</span> !commentState.Approved || 
                   commentState.Spam ||
                   commentState.Deleted;
        }

        <span class="kwrd">private</span> <span class="kwrd">class</span> CommentState
        {
            <span class="kwrd">public</span> CommentState(String spam, String approved, String deleted)
            {
                Approved = <span class="kwrd">bool</span>.Parse(approved);
                Spam = <span class="kwrd">bool</span>.Parse(spam);
                Deleted = <span class="kwrd">bool</span>.Parse(deleted);
            }

            <span class="kwrd">public</span> <span class="kwrd">bool</span> Approved { get; <span class="kwrd">private</span> set; }
            <span class="kwrd">public</span> <span class="kwrd">bool</span> Spam { get; <span class="kwrd">private</span> set; }
            <span class="kwrd">public</span> <span class="kwrd">bool</span> Deleted { get; <span class="kwrd">private</span> set; }
        }
    }
}</pre>

**Update**: I also posted the code on bitbucket: [https://bitbucket.org/lkempe/blogenginespamdelete](https://bitbucket.org/lkempe/blogenginespamdelete)
