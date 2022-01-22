---
title: "Writing a first Node.js application on Windows"
permalink: /2012/03/25/Writing-a-first-Nodejs-application-on-Windows/
date: 3/25/2012 2:25:39 AM
updated: 3/25/2012 2:53:32 AM
disqusIdentifier: 20120325022539
coverImage: https://farm8.staticflickr.com/7184/6972712717_3065764dc8_h.jpg
coverSize: partial
thumbnailImage: https://farm8.staticflickr.com/7184/6972712717_29c1c987ab_q.jpg
coverCaption: "Grande Anse, Le Diamant, Martinique"
tags: ["nodejs"]
alias:
 - /post/Writing-a-first-Nodejs-application-on-Windows.aspx/index.html
---
<!-- [![Plage de la Grande Anse du Diamant](http://farm8.staticflickr.com/7184/6972712717_29c1c987ab_m.jpg)](http://www.flickr.com/photos/laurentkempe/6972712717/ "Plage de la Grande Anse du Diamant by Laurent Kempé, on Flickr") -->   

I started some weeks ago to play with [Github Hubot](http://www.laurentkempe.com/post/Starting-TeamCity-builds-from-HipChat-using-Github-Hubot.aspx) and hosted one version on [Heroku](http://www.heroku.com/) to start learning about all those new technologies.
<!-- more -->

The next step was to go a bit onto [Node.js](http://nodejs.org/) and write a first small application.

First you need to [download Node.js](http://nodejs.org/#download) and install it on your machine. I downloaded the Windows version 0.6.14.

When Node.js is installed on your machine you should find it on the folder *C:\Program Files (x86)\nodejs* for 64 bits machines and on *C:\Program Files\nodejs* for the 32 bits.

Then start a PowerShell window and type “node -v” you should see v0.6.14.

Now start you preferred text editor and create a new file server.js and copy paste the code from the home page of Node.js

> <span class="kwrd">var</span> http = require(<span class="str">'http'</span>);
http.createServer(<span class="kwrd">function</span> (req, res) {
  res.writeHead(200, {<span class="str">'Content-Type'</span>: <span class="str">'text/plain'</span>});
  res.end(<span class="str">'Hello World\n'</span>);
}).listen(1337, <span class="str">'127.0.0.1'</span>);
console.log(<span class="str">'Server running at http://127.0.0.1:1337/'</span>);


<style type="text/css">










.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
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



Go back to your PowerShell window and type “node server.js” which should display “Server running at [http://127.0.0.1:1337/](http://127.0.0.1:1337/)”

Now run your preferred browser and enter the url [http://127.0.0.1:1337/](http://127.0.0.1:1337/)

[![nodejs](http://farm8.staticflickr.com/7055/7011307473_308a566b37_o.png)](http://www.flickr.com/photos/laurentkempe/7011307473/ "nodejs by Laurent Kempé, on Flickr")

I went a little further following the [Node Beginner](http://www.nodebeginner.org/) site and created a first module. 

    
To do so I used [JetBrains WebStorm 3](http://www.jetbrains.com/webstorm/) which you can easily configure to run and debug your Node.js application.

You need to configure it this way

[![nodejs2](http://farm8.staticflickr.com/7253/6865213052_0a80e9a449_o.png)](http://www.flickr.com/photos/laurentkempe/6865213052/ "nodejs2 by Laurent Kempé, on Flickr") 



Then you can add a Run/Debug configuration for Node.Js

[![nodejs3](http://farm8.staticflickr.com/7188/7011334891_f505c3d868_o.png)](http://www.flickr.com/photos/laurentkempe/7011334891/ "nodejs3 by Laurent Kempé, on Flickr") 



Finally you can run your Node.js application in Debug mode 

[![nodejs4](http://farm8.staticflickr.com/7226/7011339387_5c4e5b1472_o.png)](http://www.flickr.com/photos/laurentkempe/7011339387/ "nodejs4 by Laurent Kempé, on Flickr") 



and for sure hit the breakpoints and watch the values of your variables

[![nodejs5](http://farm8.staticflickr.com/7072/6865235082_e952e089c2_o.png)](http://www.flickr.com/photos/laurentkempe/6865235082/ "nodejs5 by Laurent Kempé, on Flickr") 



Which will help during my learning of this new technology!
