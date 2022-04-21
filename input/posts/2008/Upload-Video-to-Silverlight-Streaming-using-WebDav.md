---
title: "Upload Video to Silverlight Streaming using WebDav"
permalink: /2008/06/21/Upload-Video-to-Silverlight-Streaming-using-WebDav/
date: 6/21/2008 11:50:02 PM
updated: 6/21/2008 11:50:02 PM
disqusIdentifier: 20080621115002
tags: ["Silverlight Streaming"]
alias:
 - /post/Upload-Video-to-Silverlight-Streaming-using-WebDav.aspx/index.html
---
Follow up on the post from [Laurent](http://weblogs.asp.net/lduveau/) “[Expression Encoder 2 publish to Silverlight Streaming plugin](http://weblogs.asp.net/lduveau/archive/2008/06/20/expression-encoder-2-publish-to-silverlight-streaming-plugin.aspx)”.

My way to upload is to use [WebDav](http://en.wikipedia.org/wiki/WebDav) by mapping a network drive using my Silverlight Streaming Account ID as the user name and my Account Key as the password. You will find those information on the Administration [Home page of Silverlight Streaming](http://silverlight.live.com/).
<!-- more -->

Then you have a direct access to your folders on Silverlight Streaming. You can then create a folder and upload your video with the name: video.vmw. And they will appear on your [Manage Videos](https://silverlight.live.com/Videos.aspx) page.

<u>Important</u>: It is a must that the video are encoded in a format recognized by silverlight and that the name is video.wmv.

It rocks!
