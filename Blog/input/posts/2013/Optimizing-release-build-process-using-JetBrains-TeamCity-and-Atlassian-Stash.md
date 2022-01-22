---
title: "Optimizing release build process using JetBrains TeamCity and Atlassian Stash"
permalink: /2013/10/17/Optimizing-release-build-process-using-JetBrains-TeamCity-and-Atlassian-Stash/
date: 10/17/2013 7:34:14 PM
updated: 10/17/2013 9:55:36 PM
disqusIdentifier: 20131017073414
coverImage: https://farm8.staticflickr.com/7421/9599787416_9e67712879_h.jpg
coverSize: partial
thumbnailImage: https://farm8.staticflickr.com/7421/9599787416_582f4ab142_q.jpg
coverCaption: "Byron Bay Light House, Australia"
tags: ["Team City", "Git", "Stash"]
alias:
 - /post/Optimizing-release-build-process-using-JetBrains-TeamCity-and-Atlassian-Stash.aspx/index.html
---
<!-- [![Byron Bay Light House](http://farm8.staticflickr.com/7421/9599787416_582f4ab142_m.jpg)](http://www.flickr.com/photos/laurentkempe/9599787416/ "Byron Bay Light House by Laurent Kempé, on Flickr") -->   

Automating and optimizing the processes I use everyday to work is something important so that I get more productive and spend less time in things that a computer is better at.
<!-- more -->

Previously I had 3 builds defined in [TeamCity](http://www.jetbrains.com/teamcity/) one for all [feature branches](http://confluence.jetbrains.com/display/TCD8/Working+with+Feature+Branches), one for release and one for patch. For feature branch and patch branch I needed to go to TeamCity to define two Build Parameters: the branch name and the release number.

My goal was to avoid to go to TeamCity when we have a release and have to set those [Build Parameters](http://confluence.jetbrains.com/display/TCD8/Configuring+Build+Parameters).

I wanted one TeamCity build which would

1.  Determine automatically the version number 
2.  Determine the branch to use to build that release   

The second point was easy! You just need to follow the same principles defined for [feature branches](http://confluence.jetbrains.com/display/TCD8/Working+with+Feature+Branches). I defined the following branch naming convention: any release should have a branch name like this release/skye-editor-2.26.0 for a release of Skye Editor 2.26.0. Then I defined in my VCS Root of my TeamCity Build the branch specification:

> +:refs/heads/release/skye-editor-*

and the same for the Build Triggers / VCS Trigger / Branch filter.

Now the first point is a bit more complex!

As we want to determine automatically the version number we quickly realize that the release number is defined in the branch name itself, e.g. release/skye-editor-**2.26.0**. 

So why not use it? Yeah great idea but how? 

First idea that came was to pass that value as a parameter to the build script and deal with splitting the branch name to the release number into the build script. As [Yegor](http://www.jetbrains.com/company/people/Yarko_Yegor.html) was confirming that currently TeamCity has no way to parse values. I didn’t really like that idea of passing that parameter! So I continued to think about alternatives and finally came to ask Yegor:

> I had another idea, in teamcity when the active builds are displayed for a feature branch build only the second part is displayed, e.g. if I specify as branch specification SKYE-* and I have a branch with SKYE-1077-blabla then it wil show 1077-blabla. Is there a build parameter which would map this 1077-blabla?

And the answer was

> If you have something like refs/heads/SKYE-(*), then only the part in the brackets is regarded as logic branch name. Logic branch name is available as %teamcity.build.branch%

That’s it! Thanks Yegor. We have the way to get our version without having to change our build script and without writing any code!

So I just used the logical branch name %teamcity.build.branch% as my configuration parameter CurrentRelease which was already existing and that I had to manually update before:

![](http://farm4.staticflickr.com/3672/10324541816_1c57beaa6c_o.png)

Replacing the previous two manual configurations

![](http://farm8.staticflickr.com/7453/10324571806_5a56729f3e_o.png)

So now I create easily a Git release branch; release/skye-editor-2.26.0, then use pull requests of [Atlassian Stash](https://www.atlassian.com/software/stash/overview) to review and merge my feature branches to that release branch which is automatically built using TeamCity.

This is a great improvement and shorten my release check list, all good!
