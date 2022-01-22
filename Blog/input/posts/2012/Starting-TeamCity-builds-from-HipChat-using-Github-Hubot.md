---
title: "Starting TeamCity builds from HipChat using Github Hubot"
permalink: /2012/02/07/Starting-TeamCity-builds-from-HipChat-using-Github-Hubot/
date: 2/7/2012 5:10:52 PM
updated: 2/8/2012 5:44:04 AM
disqusIdentifier: 20120207051052
coverImage: https://farm6.staticflickr.com/5184/5561154518_845f00ec72_b.jpg
coverSize: partial
thumbnailImage: https://farm6.staticflickr.com/5184/5561154518_845f00ec72_q.jpg
coverCaption: "La table du diable depuis la Savane des Pétrifications, Martinique"
tags: ["Team City", "Agile", "Git", "Productivity"]
alias:
 - /post/Starting-TeamCity-builds-from-HipChat-using-Github-Hubot.aspx/index.html
---
<!-- [![La table du diable depuis la Savane des Pétrifications](http://farm6.staticflickr.com/5184/5561154518_845f00ec72_m.jpg)](http://www.flickr.com/photos/laurentkempe/5561154518/ "La table du diable depuis la Savane des Pétrifications by Laurent Kempé, on Flickr") -->   

After writing about “[Running your TeamCity builds from PowerShell for any Git branch](http://www.laurentkempe.com/post/Running-your-TeamCity-builds-from-a-command-line.aspx)” I’d like to talk about another integration which is using [Github Hubot](http://hubot.github.com/) so that the build could be started directly form a chat room.
<!-- more -->

So using the same idea, which is to extend our development environment, we implemented a way to start TeamCity builds directly from our [HipChat](https://www.hipchat.com/) room. You could do the same from [Campfire](http://campfirenow.com/) for sure. 

The main idea of this is to have our daily tools right at our disposal where we spend a good part of our days, chat rooms.

First we had to install [Github Hubot](http://hubot.github.com/) and we have chosen to use [Heroku](http://www.heroku.com/) which offer free hosting [for one of their web dyno](http://www.heroku.com/pricing#1-0). They even offer what they call [Hubot Factory](https://hubot-factory.herokuapp.com/about) which is a way to allow people to easily and quickly deploy new instances of Hubot to Heroku. In fact we went with a manual installation described on this page “[Hubot for HipChat on Heroku](https://github.com/hipchat/hubot-hipchat#readme)”. 

The idea is still the same we need to run TeamCity builds by “[Accessing the Server by HTTP](http://confluence.jetbrains.net/display/TCD6/Accessing+Server+by+HTTP)”.

This time we want to have Hubot calling the TeamCity server so we need to add a new CoffeeScript to the scripts of Hubot which will launch the HTTP request needed.

Our script is heavily inspired from the TeamCity.coffee script, and here is a first version:

{% codeblock TeamCity.coffee lang:coffeescript http://underscorejs.org/#compact TeamCity.coffee %}
# Starts a build on TeamCity.
#
# You need to set the following variables:
# HUBOT_TEAMCITY_USERNAME = <user name="">
# HUBOT_TEAMCITY_PASSWORD = 
# HUBOT_TEAMCITY_HOSTNAME = <host port="" :="">
#
# start build buildId -- Starts TeamCiyt build using buildId
module.exports = (robot) ->
  robot.respond /start build (.*)/i, (msg) ->
    username = process.env.HUBOT_TEAMCITY_USERNAME
    password = process.env.HUBOT_TEAMCITY_PASSWORD
    hostname = process.env.HUBOT_TEAMCITY_HOSTNAME
    buildName = msg.match[1]

    msg.http("http://#{hostname}/httpAuth/action.html?add2Queue=#{buildName}")
      .headers(Authorization: "Basic #{new Buffer("#{username}:#{password}").toString("base64")}",
               Accept: "application/json")
      .get() (err, res, body) ->
        if err
          msg.send "Team city says: #{err}"
          return</host></user>
{% endcodeblock %}

By the way I started to learn CoffeeScript so this is more hacking then something really productive.

And here is the result in our HipChat room asking hubot to start a build with the command:

> hubot start build bt21

[![TeamCity, GitHub Hubot and HipChat](http://farm8.staticflickr.com/7024/6834441105_d42fa87111_o.jpg)](http://www.flickr.com/photos/laurentkempe/6834441105/ "TeamCity, GitHub Hubot and HipChat by Laurent Kempé, on Flickr")

I will talk in another post about the last part which shows the status of the build in the chat room.
