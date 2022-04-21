---
title: "Silverlight Streaming hosting and Expression Encoder"
permalink: /2007/09/12/Silverlight-Streaming-hosting-and-Expression-Encoder/
date: 9/12/2007 4:36:51 PM
updated: 9/12/2007 4:36:51 PM
disqusIdentifier: 20070912043651
tags: ["Silverlight", "Silverlight Streaming", "Expression Encoder"]
alias:
 - /post/Silverlight-Streaming-hosting-and-Expression-Encoder.aspx/index.html
---
***How do you host your video content created with ***[***Expression Encoder***](http://www.microsoft.com/expression/products/overview.aspx?key=encoder)*** on ***[***Silverlight Streaming***](http://silverlight.live.com/)*** ?***

The first answer is wait for [the plugin](http://www.microsoft.com/expression/products/overview.aspx?key=encoder) that will let you do that:
<!-- more -->

> *Coming Soon*
> Publish directly to Silverlight Streaming.

If you can't wait take a look at [Silverlight Streaming SDK](http://dev.live.com/silverlight/) that tell you how to do it !

***But how do you do if you want to have your video on Silverlight Streaming and your application on your site ?***

Read the [documentation](http://dev.live.com/silverlight/initparams.aspx). I have to say, it was quite obscure the first time I read it. So here is the way we did it with [Mathieu](http://www.techheadbrothers.com/Auteurs.aspx/mathieu-kempe).

The output of Expression Encoder is saved in the following folder:

C:\Users\*Username*\Documents\Expression\Expression Encoder\Output

In this folder you have everything to start with a very good looking player; the xaml, the JavaScript, your video, a html document, even a visual studio project. You might look at Tim Heuer video "[cheating at creating a Silverlight media player](http://www.timheuer.com/blog/archive/2007/08/31/cheating-creating-silverlight-media-player.aspx)".

To start you have to modify the default.html to use the Silverlight JavaScript script delivered by Silverlight Streaming:

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">script</span> <span style="color: rgb(255,0,0)">type</span><span style="color: rgb(0,0,255)">='text/javascript'</span> <span style="color: rgb(255,0,0)">src</span><span style="color: rgb(0,0,255)">="Silverlight.js"></</span><span style="color: rgb(163,21,21)">script</span><span style="color: rgb(0,0,255)">></span>

[](http://11011.net/software/vspaste)by

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">script</span> <span style="color: rgb(255,0,0)">type</span><span style="color: rgb(0,0,255)">="text/javascript"</span> <span style="color: rgb(255,0,0)">src</span><span style="color: rgb(0,0,255)">="http://agappdom.net/h/silverlight.js"></</span><span style="color: rgb(163,21,21)">script</span><span style="color: rgb(0,0,255)">>
</span>
[](http://11011.net/software/vspaste)


This is recommended by the documentation, but it is not mandatory:

> Instead of hosting Silverlight.js on your own web site and using it, you should use <tt>[http://agappdom.net/h/silverlight.js](http://agappdom.net/h/silverlight.js)</tt>

Then you have to modify the StartPlayer.js, replace:

<span style="color: rgb(0,0,255)">function</span> StartPlayer_0(parentId) {
    <span style="color: rgb(0,0,255)">this</span>._hostname = EePlayer.Player._getUniqueName(<span style="color: rgb(163,21,21)">"xamlHost"</span>);
    Silverlight.createObjectEx( {   source: <span style="color: rgb(163,21,21)">'player.xaml'</span>, 
                                    parentElement: $get(parentId ||<span style="color: rgb(163,21,21)">"divPlayer_0"</span>), 
                                    id:<span style="color: rgb(0,0,255)">this</span>._hostname, 
                                    properties:{ width:<span style="color: rgb(163,21,21)">'100%'</span>, 
                                                 height:<span style="color: rgb(163,21,21)">'100%'</span>, 
                                                 version:<span style="color: rgb(163,21,21)">'1.0'</span>, 
                                                 background:document.body.style.backgroundColor, 
                                                 isWindowless:<span style="color: rgb(163,21,21)">'false'</span> }, 
                                    events:{ onLoad:Function.createDelegate(<span style="color: rgb(0,0,255)">this</span>, <span style="color: rgb(0,0,255)">this</span>._handleLoad) } } );
    <span style="color: rgb(0,0,255)">this</span>._currentMediainfo = 0;      
}


with this, adding the [initParams](http://dev.live.com/silverlight/initparams.aspx):

<span style="color: rgb(0,0,255)">function</span> StartPlayer_0(parentId) {
    <span style="color: rgb(0,0,255)">this</span>._hostname = EePlayer.Player._getUniqueName(<span style="color: rgb(163,21,21)">"xamlHost"</span>);

    Silverlight.createHostedObjectEx( {   source: <span style="color: rgb(163,21,21)">'player.xaml'</span>, 
                                        parentElement: $get(parentId ||<span style="color: rgb(163,21,21)">"divPlayer_0"</span>), 
                                        id: <span style="color: rgb(0,0,255)">this</span>._hostname, 
                                        properties:{ width:<span style="color: rgb(163,21,21)">'100%'</span>, 
                                                     height:<span style="color: rgb(163,21,21)">'100%'</span>, 
                                                     version:<span style="color: rgb(163,21,21)">'1.0'</span>, 
                                                     background:document.body.style.backgroundColor, 
                                                     isWindowless:<span style="color: rgb(163,21,21)">'false'</span> }, 
                                        events:{ onLoad:Function.createDelegate(<span style="color: rgb(0,0,255)">this</span>, <span style="color: rgb(0,0,255)">this</span>._handleLoad) },
                                        **initParams:<span style="color: rgb(163,21,21)">"streaming:/4065/livewriterdemo/livewriterdemo.wmv"</span>** } );
    <span style="color: rgb(0,0,255)">this</span>._currentMediainfo = 0;      
}

[](http://11011.net/software/vspaste)


Now you need a way to get back this the real url of you video out of the streaming:/4065/livewriterdemo/livewriterdemo.wmv, this is done automatically by the script doing a post to Silverlight Streaming server that returns the following javascript:

SLStreaming._StartApp("bl2", null  
, {}  
, []  
, ["http://msbluelight-0.agappdom.net/e1/d/4065/8.w/63325188000/0.UZcUXMfJgIK0I0HcP-SQGzhvvVE/livewriterdemo.wmv"]);

Now we need to take car of the real url in the _handleLoad method we change the call to the _playNextVideo to a new method ChangeVideo:

StartPlayer_0.prototype= {
    _handleLoad: <span style="color: rgb(0,0,255)">function</span>() {
        <span style="color: rgb(0,0,255)">this</span>._player = $create(   ExtendedPlayer.Player, 
                                  { <span style="color: rgb(0,128,0)">// properties
</span>                                    autoPlay    : <span style="color: rgb(0,0,255)">true</span>, 
                                    volume      : 1.0,
                                    muted       : <span style="color: rgb(0,0,255)">false
</span>                                  }, 
                                  { <span style="color: rgb(0,128,0)">// event handlers
</span>                                    mediaEnded: Function.createDelegate(<span style="color: rgb(0,0,255)">this</span>, <span style="color: rgb(0,0,255)">this</span>._onMediaEnded),
                                    mediaFailed: Function.createDelegate(<span style="color: rgb(0,0,255)">this</span>, <span style="color: rgb(0,0,255)">this</span>._onMediaFailed)
                                  },
                                  <span style="color: rgb(0,0,255)">null</span>, $get(<span style="color: rgb(0,0,255)">this</span>._hostname)  ); 
        <span style="color: rgb(0,128,0)">//this._playNextVideo();     
</span>        <span style="color: rgb(0,0,255)">this</span>.ChangeVideo();
    },    
    ChangeVideo: <span style="color: rgb(0,0,255)">function</span>(){            

        <span style="color: rgb(0,0,255)">var</span> params = $get(<span style="color: rgb(0,0,255)">this</span>._hostname).InitParams;
        <span style="color: rgb(0,0,255)">this</span>._player.set_mediainfo(
                { <span style="color: rgb(163,21,21)">"mediaUrl"</span>: params, <span style="color: rgb(163,21,21)">"placeholderImage"</span>: <span style="color: rgb(163,21,21)">""</span>, <span style="color: rgb(163,21,21)">"chapters"</span>: [] }  
            );                                                                                                              
    },                  


We get access to the initParams converted to a real url using:

        <span style="color: rgb(0,0,255)">var</span> params = $get(<span style="color: rgb(0,0,255)">this</span>._hostname).InitParams;

Tanks to [Mathieu](http://www.techheadbrothers.com/Auteurs.aspx/mathieu-kempe) for the help in the javascript coding.

So this is all you need to do to have your video hosted and delivered by [Silverlight Streaming](http://silverlight.live.com/) and your application hosted on your own site.
