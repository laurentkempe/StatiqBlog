---
title: "Running Github Hubot on a Windows machine"
permalink: /2012/04/28/Running-Github-Hubot-on-a-Windows-machine/
date: 4/28/2012 9:15:24 AM
updated: 9/26/2012 2:48:36 PM
disqusIdentifier: 20120428091524
coverImage: https://farm8.staticflickr.com/7053/6975717721_1eaca89dcc_h.jpg
coverSize: partial
thumbnailImage: https://farm8.staticflickr.com/7053/6975717721_11b528a835_q.jpg
coverCaption: "Anse Cafard, Martinique"
tags: ["hubot", "GitHub", "Windows 7"]
alias:
 - /post/Running-Github-Hubot-on-a-Windows-machine.aspx/index.html
---
<!-- [![IMG_0063](http://farm8.staticflickr.com/7053/6975717721_11b528a835_m.jpg)](http://www.flickr.com/photos/laurentkempe/6975717721/ "IMG_0063 by Laurent Kempé, on Flickr") -->
I finally managed to get [Github Hubot](http://hubot.github.com/) running on my Windows 7 64 bits machine. Thanks to  [Sean Copenhaver](https://github.com/copenhas) & [Thomas Kahlow](https://github.com/kahlow) which pointed me to the correct direction on the ‘[Run hubot on windows](https://github.com/github/hubot/issues/166#issuecomment-5391161)’ discussion.

So here is how I did it.
<!-- more -->

First of all download and install [nodejs 0.6.15](http://nodejs.org/). This will also install [npm 1.1.16](http://npmjs.org/) which is the node package manager.

Then start Powershell and create a new folder e.g. HubotWorking

> mkdir HubotWorking
> 
> cd .\HubotWorking
> 
> npm install hubot

You should see the following output

![npminstallhubot](http://farm8.staticflickr.com/7179/7119734567_1d28afba2b_o.jpg)

Go on with the following

> cd .\node_modules\hubot
> 
> node .\node_modules\coffee-script\bin\coffee .\bin\hubot

And the magic!

[![hubotonwindows](http://farm8.staticflickr.com/7206/6973680654_0cb983b26a_o.jpg)](http://www.flickr.com/photos/laurentkempe/6973680654/ "hubotonwindows by Laurent Kempé, on Flickr")

You might want also to run it through [JetBrains WebStorm 4.0](http://www.jetbrains.com/webstorm/), so here is the configuration you will need
 [![webstorm hubot](http://farm8.staticflickr.com/7199/7119772727_438c6cd97b_o.jpg)](http://www.flickr.com/photos/laurentkempe/7119772727/ "webstorm hubot by Laurent Kempé, on Flickr")   

And here is the result in the WebStorm nodejs runner
 [![runhubotfromwebstorm](http://farm8.staticflickr.com/7203/7119778369_40d9f0a11a_o.jpg)](http://www.flickr.com/photos/laurentkempe/7119778369/ "runhubotfromwebstorm by Laurent Kempé, on Flickr")
