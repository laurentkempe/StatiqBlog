---
title: Hosting nugets in TeamCity
date: 2016-04-14 22:04:11
tags: [".NET Development", "continuous integration"]
permalink: /2016/04/14/Hosting-nugets-in-TeamCity/
disqusIdentifier: 20160414220411
coverSize: partial
coverCaption: 'Îlet Chevalier, Sainte-Anne, Martinique'
coverImage: 'https://farm2.staticflickr.com/1604/25521348035_047dad2235_h.jpg'
thumbnailImage: 'https://farm2.staticflickr.com/1604/25521348035_20fa6744ce_q.jpg'
---
In the past in my team, we were storing the Telerik assemblies into our SVN repository then in Git! I wasn't happy about that for a really long time. Each releases our repository was growing much more than it was needed for nothing!
<!-- more -->
One day Telerik shipped their great WPF controls as nugets which solved half of our problem! Now the next problem was where do we store those nugets so that we can get those back. Searching a bit we found some solutions like [myget](https://myget.org/) but we wanted one which we could host on our infrastructure. After some more research, we found ou that TeamCity, could work as a [Nuget server](https://confluence.jetbrains.com/display/TCD9/NuGet#NuGet-UsingTeamCityasNuGetServer).

That was just perfect because we have been using TeamCity from day 1 of the project. We finally go the  second half of our solution.

You first need to activate the Nuget server on TeamCity:

![tc-nuget-server-1](https://farm2.staticflickr.com/1674/26338964882_405d486347_o.png)

We have activated the guest account so that the Nuget feed URL is accessible without any credentials. We use it on our LAN so that's not really an issue for us. But it also works with credentials if you want.

The way TeamCity works with Nuget server is quite easy: any artifacts resolved after a build are then available through the Nuget server.

So next step was to define how to we bring a new version of the Telerik nugets when they ship a new version.

![tc-nuget-server-2](https://farm2.staticflickr.com/1441/26365113121_7f3047a0da_o.png)

Again easy, just configure a build which takes the .nupkg from one folder and then bundle those as a build artifact.

![tc-nuget-server-3](https://farm2.staticflickr.com/1671/25828490813_5a089e7001_o.png)

So when Telerik release a new version we just need to upload all the .nupkg files to the folder *C:\Telerik\* and run the build!

One more step in the good direction of automation!
