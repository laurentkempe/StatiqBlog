---
title: "TeamCity and Gitlab working together with SSH Keys"
permalink: /2012/08/24/TeamCity-and-Gitlab-working-together-with-SSH-Keys/
date: 8/24/2012 11:44:59 PM
updated: 8/24/2012 11:59:24 PM
disqusIdentifier: 20120824114459
coverImage: https://farm9.staticflickr.com/8299/7764455174_207330f8d0_h.jpg
coverSize: partial
thumbnailImage: https://farm9.staticflickr.com/8299/7764455174_84625faf34_q.jpg
coverCaption: "Calzarellu, Corse, France"
tags: ["Git", "Gitlab", "Team City"]
alias:
 - /post/TeamCity-and-Gitlab-working-together-with-SSH-Keys.aspx/index.html
---
<!-- [![Calzarellu](http://farm9.staticflickr.com/8299/7764455174_84625faf34_m.jpg)](http://www.flickr.com/photos/laurentkempe/7764455174/ "Calzarellu by Laurent Kempé, on Flickr") -->
On February I posted about “[Running your TeamCity builds from PowerShell for any Git branch](http://www.laurentkempe.com/post/Running-your-TeamCity-builds-from-a-command-line.aspx)” and now I have to configure a new ssh key so that [TeamCity](http://www.jetbrains.com/teamcity/) can connect to [Gitlab](http://www.gitlabhq.com/).

I struggled a bit getting continuous error message from TeamCity that the connection failed because it was enable to load identity file. File rights were all ok on the key file and everything looked fine. But still the error message.
<!-- more -->

Then I remembered that I had the same issue in February while using [PuTTYgen](http://www.chiark.greenend.org.uk/~sgtatham/putty/download.html) to generate my SSH key pair. 

I forgot a minor details that I had to generate the key pair like this:

1.  Click Generate button and move your mouse 
2.  Click Save public key 
3.  Click Save private key (if you need it in this format) 
4.  Click on the menu Conversions / Export OpenSSH key and save that key   

![](http://farm9.staticflickr.com/8422/7850573396_a560b261e5_o.png)

Then upload the exported OpenSSH key on the TeamCity server. The one in the putty format isn’t working, and that was my problem!

Finally copy the public key generated to your clipboard

![](http://farm8.staticflickr.com/7253/7850602710_bc53602681_o.png)

and use it in Gitlab

![](http://farm9.staticflickr.com/8291/7850608050_0195906fa0_o.png)

Now when you test the Git connection from TeamCity you should see the following

![](http://farm9.staticflickr.com/8436/7850614266_912bc8d8b6_o.png)
