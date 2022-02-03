---
title: 'Moving to Cake (C# Make)'
permalink: /2016/04/05/Moving-to-Cake-CSharp-Make/
date: 2016-04-05 17:46:28
disqusIdentifier: 20160405174628
coverImage: https://farm2.staticflickr.com/1527/25703873226_8f2eea0426_h.jpg
coverSize: partial
thumbnailImage: https://farm2.staticflickr.com/1527/25703873226_512850f0c5_q.jpg
coverCaption: "Le rocher du Diamant, Martinique"
tags: [".NET Development", "continuous integration"]
---
I finally invested time to migrate our build from a heteroclite mixture of  MSBuild, TeamCity build steps and whatever to something that really delight me: [Cake (C# Make)](http://cakebuild.net/).
<!-- more -->

I tried in the past several other systems like PSake, Fake... Never grasped those and gave up quite quickly.

This time, two triggers pushed me to look further:<p></p>
1. We had to build a patch and someone deactivated [TeamCity project settings versioning in Git](http://laurentkempe.com/2014/12/13/TeamCity-9-project-settings-versioning-in-Git/) so we could not get back the settings and could not build on TeamCity, too bad!
2. We were working on improving the performance of our build by going to NUnit 3 and trying to run our specifications, integrations and unit tests in parallel.

For long I was the advocate of versioning our build script with the source code but never got the time to finally do it. I guess you need a bit of pain to trigger some changes.

So last week I prepared a small presentation to my great team which I presented this last Monday. You can have a look that presentation [here](https://sway.com/G8xS5gVqbwOA9euI).

Most of the slides are coming from the Cake documentation provided by the team behind this great project!

I would like to put some emphasis on one part which is not coming from Cake documentation which is: **Why Cake?**<p></p>  

* **Unify** our build process
* **Versioning** the build script **with the source code** of the application
* Being able to **run** our build **where we want**
 * Any machine with .NET framework, good for our escrow process
 * Our TeamCity build server
 * New cloud build services like AppVeyor, Visual Studio Team Services...
* **It is C#**. In the past we looked at PSake, Fake.. but never really grasped those
* Coming with almost all [tools](http://cakebuild.net/dsl), [add-ins](http://cakebuild.net/addins?path=contributing%252Fguidelines) we need for our build process. Missing NDepend which stays as a TeamCity build at the moment.
* **Open source** and **great responsive community** on [Gitter chat](https://gitter.im/cake-build/cake)
* No need to version binaries
* Syntax Highlighting in Visual Studio Code

One point to take away from that list is **great responsive community** and nothing proves it more than [this discussion](https://gitter.im/cake-build/cake?at=57024151d39de41b49604f5e) and that [pull request](https://github.com/cake-build/cake/issues/805) which fixes an issue we just found and reported. The fix came the same day and will be available in the next patch release, great job. 

So our current situation is much better now!
