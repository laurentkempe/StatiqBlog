---
title: "Silverlight Streaming hosting and Expression Encoder"
permalink: /2007/09/12/Silverlight-Streaming-hosting-and-Expression-Encoder/
date: 9/12/2007 4:36:51 PM
updated: 9/12/2007 4:36:51 PM
disqusIdentifier: 20070912043651
tags: ["Silverlight", "Silverlight Streaming", "Expression Encoder"]
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

```html
<script type='text/javascript' src="Silverlight.js"></script>
```

by

```html
<script type="text/javascript" src="http://agappdom.net/h/silverlight.js"></script>
```

This is recommended by the documentation, but it is not mandatory:

> Instead of hosting Silverlight.js on your own web site and using it, you should use <tt>[http://agappdom.net/h/silverlight.js](http://agappdom.net/h/silverlight.js)</tt>

Then you have to modify the StartPlayer.js, replace:

```javascript
function StartPlayer_0(parentId) {
    this._hostname = EePlayer.Player._getUniqueName("xamlHost");
    Silverlight.createObjectEx( {   source: 'player.xaml', 
                                    parentElement: $get(parentId ||"divPlayer_0"), 
                                    id:this._hostname, 
                                    properties:{ width:'100%', 
                                                 height:'100%', 
                                                 version:'1.0', 
                                                 background:document.body.style.backgroundColor, 
                                                 isWindowless:'false' }, 
                                    events:{ onLoad:Function.createDelegate(this, this._handleLoad) } } );
    this._currentMediainfo = 0;      
}
```

with this, adding the [initParams](http://dev.live.com/silverlight/initparams.aspx):

```javascript
function StartPlayer_0(parentId) {
    this._hostname = EePlayer.Player._getUniqueName("xamlHost");

    Silverlight.createHostedObjectEx( {   source: 'player.xaml', 
                                        parentElement: $get(parentId ||"divPlayer_0"), 
                                        id: this._hostname, 
                                        properties:{ width:'100%', 
                                                     height:'100%', 
                                                     version:'1.0', 
                                                     background:document.body.style.backgroundColor, 
                                                     isWindowless:'false' }, 
                                        events:{ onLoad:Function.createDelegate(this, this._handleLoad) },
                                        **initParams:"streaming:/4065/livewriterdemo/livewriterdemo.wmv"** } );
    this._currentMediainfo = 0;      
}
```

Now you need a way to get back this the real url of you video out of the streaming:/4065/livewriterdemo/livewriterdemo.wmv, this is done automatically by the script doing a post to Silverlight Streaming server that returns the following javascript:

```javascript
SLStreaming._StartApp("bl2", null  
, {}  
, []  
, ["http://msbluelight-0.agappdom.net/e1/d/4065/8.w/63325188000/0.UZcUXMfJgIK0I0HcP-SQGzhvvVE/livewriterdemo.wmv"]);
```

Now we need to take car of the real url in the _handleLoad method we change the call to the _playNextVideo to a new method ChangeVideo:

```javascript
StartPlayer_0.prototype= {
    _handleLoad: function() {
        this._player = $create(   ExtendedPlayer.Player, 
                                  { <span style="color: rgb(0,128,0)">// properties
                                    autoPlay    : true, 
                                    volume      : 1.0,
                                    muted       : false
                                  }, 
                                  { <span style="color: rgb(0,128,0)">// event handlers
                                    mediaEnded: Function.createDelegate(this, this._onMediaEnded),
                                    mediaFailed: Function.createDelegate(this, this._onMediaFailed)
                                  },
                                  null, $get(this._hostname)  ); 
        <span style="color: rgb(0,128,0)">//this._playNextVideo();     
        this.ChangeVideo();
    },    
    ChangeVideo: function(){            

        var params = $get(this._hostname).InitParams;
        this._player.set_mediainfo(
                { "mediaUrl": params, "placeholderImage": "", "chapters": [] }  
            );                                                                                                              
    },                  
```

We get access to the initParams converted to a real url using:

```javascript
var params = $get(this._hostname).InitParams;
```

Thanks to [Mathieu](http://www.techheadbrothers.com/Auteurs.aspx/mathieu-kempe) for the help in the javascript coding.

So this is all you need to do to have your video hosted and delivered by [Silverlight Streaming](http://silverlight.live.com/) and your application hosted on your own site.
