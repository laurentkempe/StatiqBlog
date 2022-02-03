---
title: "Using Github hubot and Appharbor service hook to get deployment status in Campfire/HipChat rooms"
permalink: /2012/04/30/Using-Appharbor-service-hook-to-get-build-status-in-HipChat-rooms/
date: 4/30/2012 12:44:00 PM
updated: 9/17/2012 6:52:06 AM
disqusIdentifier: 20120430124400
coverImage: https://farm8.staticflickr.com/7040/6829836492_f5146ee0c8_h.jpg
coverSize: partial
thumbnailImage: https://farm8.staticflickr.com/7040/6829836492_39e190a19d_q.jpg
coverCaption: "Grande Anse, Le Diamant, Martinique"
tags: ["GitHub", "hubot", "HipChat", "Campfire", "CoffeeScripts"]
alias:
 - /post/Using-Appharbor-service-hook-to-get-build-status-in-HipChat-rooms.aspx/index.html
---
<!-- [![STA_0178](http://farm8.staticflickr.com/7040/6829836492_39e190a19d_m.jpg)](http://www.flickr.com/photos/laurentkempe/6829836492/ "STA_0178 by Laurent Kempé, on Flickr") -->
[Appharbor](https://appharbor.com) provides a [service hook](http://support.appharbor.com/kb/3rd-party-integrations/developing-a-service-hook) which let’s you get informed when a build is finished sending both succeeded and failed builds.

We wanted to use this to get informed in our [Campfire](http://campfirenow.com/) / [HipChat](https://www.hipchat.com) room so that our distributed team can be informed about builds status without having to go to AppHarbor web site.
<!-- more -->

The idea was to extend our [GitHub Hubot](http://hubot.github.com/) hosted on [Heroku](http://www.heroku.com/).

From AppHarbor support here is the information we need about “[Developing a service hook](http://support.appharbor.com/kb/api/developing-a-service-hook)”

> We will send a POST request content-type "application/json" and the following body:
> 
>         {
  "application": {
    "name": "Foo"
  }, 
  "build": {
    "commit": {
      "id": "77d991fe61187d205f329ddf9387d118a09fadcd", 
      "message": "Implement foo"
    }, 
    "status": "succeeded"
  }
}

We need to build a Hubot script; which are CoffeeScript, to have an HTTP endpoint listening to this Post payload. Then it needs to read the payload and format it to be able to send it in readable message to the Campfire / HipChat room.

With the release 2.1.3 of Hubot there is a new easy way to have an HTTP Listener:

> #### HTTP Listener
> 
> Hubot has a HTTP listener which listens on the port specified by the `PORT` environment variable.
> 
> You can specify routes to listen on in your scripts by using the `router` property on `robot`.
> 
> module.exports = (robot) ->
  robot.router.get "/hubot/version", (req, res) ->
    res.end robot.version
> 
> There are functions for GET, POST, PUT and DELETE, which all take a route and callback function that accepts a request and a response.

We can use this easily in our Hubot script which is called **appharbor-listener.coffee**.

> module.exports = (robot) ->
>   robot.router.post "/hubot/appharbor", (req, res) ->
>     robot.logger.info "Message received for appharbor"

Now that we are able to listen to POST payload on the url …/hubot/appharbor we need to send a message to the Campfire / HipChat room, which is a bit different from the other scripts. The http listener scripts doesn’t get msg which is normally used to send the response from our bot to the room. Here we have to do it differently and use **robot.send **which I found on the post ‘[Hubot HTTP Daemon Support](http://blog.iweb-hosting.co.uk/blog/2012/01/21/hubot-http-daemon-support/)’

> user = robot.userForId 'broadcast'
> user.room = 'Your Room Id'
> user.type = 'groupchat'
> 
> message = "AppHarbor build '#{buildStatus}' for application: '#{builtApplicationName}'"
> 
> robot.logger.info "User: '#{user.room}','#{user.type}'"
> robot.logger.info "Message: '#{message}'"
> 
> robot.send user, "#{message}"

Currently this is working only with the Campfire adapter, the HipChat one is crashing as [described here](https://github.com/hipchat/hubot-hipchat/issues/24#issuecomment-5410146).

Here is the whole script

<script src="https://gist.github.com/2562466.js"> </script>

And finally here is the result of posting a sample payload using [fiddler](http://fiddler2.com/fiddler2/) 

[![github hubot appharbor integration](http://farm8.staticflickr.com/7270/6983395094_ea83422211_o.jpg)](http://www.flickr.com/photos/laurentkempe/6983395094/ "github hubot appharbor integration by Laurent Kempé, on Flickr")
