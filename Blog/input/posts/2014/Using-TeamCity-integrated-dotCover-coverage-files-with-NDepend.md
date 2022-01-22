---
title: "Using TeamCity integrated dotCover coverage files with NDepend"
permalink: /2013/11/29/Using-TeamCity-integrated-dotCover-coverage-files-with-NDepend/
date: 11/29/2013 6:55:49 PM
updated: 11/29/2013 10:21:02 PM
disqusIdentifier: 20131129065549
coverImage: https://farm3.staticflickr.com/2869/11098125944_ed0e781a91_h.jpg
coverSize: partial
thumbnailImage: https://farm3.staticflickr.com/2869/11098125944_08a562bd51_q.jpg
coverCaption: "Byron Bay, Australia"
tags: ["NDepend", "Team City", "dotCover"]
alias:
 - /post/Using-TeamCity-integrated-dotCover-coverage-files-with-NDepend.aspx/index.html
---
<!-- [![Byron Bay 2013-08-26 038_DxO](http://farm3.staticflickr.com/2869/11098125944_08a562bd51_m.jpg)](http://www.flickr.com/photos/laurentkempe/11098125944/ "Byron Bay 2013-08-26 038_DxO by Laurent Kempé, on Flickr")-->

For a long time I wanted to integrated [NDepend](http://www.ndepend.com) on our build server so that this week I invested some time here and there to achieve that goal. I did that already a long time ago, I even wrote the documentation which you can read on [NDepend website](http://www.ndepend.com/Doc_CI_TeamCity.aspx).
<!-- more -->

This time I wanted to go one step further. 

We use a first build which is building each feature branch we are developing. One of the responsibility of that build is to run unit tests, integrations tests, specifications and gather code coverage. To achieve that we are using the [TeamCity integrated dotCover](http://confluence.jetbrains.com/display/TCD8/JetBrains+dotCover) in each build steps running our different tests. This is collecting artifacts which aren’t directly shown on the Artifacts tab:

![](http://farm3.staticflickr.com/2836/11114221833_738e29571e_o.png)

Clicking show reveal the file we are interested about, dotCover.snapshot:

![](http://farm3.staticflickr.com/2833/11114229113_b46ec7804e_o.png)

Something to note is that to be able to use code coverage NDepend needs the pdb files, this is why we have another artifact named PDBs.zip. And finally the third is our software with the exe and dlls.

Now that we have a build which generate the coverage file which we want to pass to NDepend, let’s create another TeamCity build which will define Snapshot Dependency and Artifact Dependency to the previous build:

![](http://farm8.staticflickr.com/7363/11114582156_0605499626_o.png)

We are extracting the exe to a NDepend folder and all dlls out of the archive Libs folder to the same NDepend folder.     
We do the same for the pdb files so that NDepend can use the code coverage data.      
Finally we extract the dotCover.snapshot to a dotCover folder.

Then the issue we had was that the dotCover.snapshot file is not of the format that NDepend is expecting.

So as a first build step of our NDepend build we need to convert dotCover.snapshot file, this is done using a Command Line build step and dotCover integrated in TeamCity, using the [report command](http://www.jetbrains.com/dotcover/webhelp/dotCover__Console_Runner_Commands.html) and the ReportType equal to NDependXML:

![](http://farm4.staticflickr.com/3679/11115318034_55d5175a9a_o.png)

So after that build first step we have a new converted file; dotCoverNDepend.xml which can be consumed by NDepend.

Then in the second build step we are using dotCoverNDepend.xml with the new NDepend 5.1 CoverageFiles command parameter:

![](http://farm8.staticflickr.com/7364/11115499383_e14d7b48f7_o.png)

Here is the full command

> C:\NDepend\NDepend.Console.exe "%teamcity.build.checkoutDir%\skyeEditor.ndproj" /CoverageFiles "%teamcity.build.checkoutDir%\dotCover\dotCoverNDepend.xml" /InDirs "%teamcity.build.checkoutDir%\NDepend" "C:\Windows\Microsoft.NET\Framework\v4.0.30319" "C:\Windows\Microsoft.NET\Framework\v4.0.30319\WPF" /OutDir "C:\NDependOutput"

This will create the NDepend report which we will archive as an artifact, on the General Settings of the build

![](http://farm6.staticflickr.com/5491/11115423854_92c437b8d4_o.png)

Then you will need to define that you want to see the NDepend report as a TeamCity Report Tab, which you define by navigating to Administration > Report Tabs, clicking Create new report tab and specifying

![](http://farm6.staticflickr.com/5533/11115432286_aa8423bdc1_o.png)

Finally you will have the following NDepend report with code coverage shown for your builds

![](http://farm3.staticflickr.com/2849/11115450035_9bf0cefdcb_o.png)

One last thing I struggled about is that the NDepend builds were not started, because I thought it was enough to configure the Dependencies, but you need to define also a Build Trigger with Trigger on changes in snapshot dependencies ticked:

![](http://farm6.staticflickr.com/5493/11115554176_63214bac84_o.png)

Thanks to **Yegor **for the discussion which greatly helped as always! And also to **Ruslan** which also helped through [the post on the jetbrains forum](http://devnet.jetbrains.com/message/5504378#5504378). And finally thanks to [**Patrick**](http://codebetter.com/patricksmacchia/) who introduced really fast the [/CoverageFiles in NDepend](http://ndepend.uservoice.com/forums/226344-ndepend-user-voice/suggestions/4897199-add-a-command-line-argument-to-depend-console-exe-), so do not hesitate to give feedback using the new [NDepend user voice](http://ndepend.uservoice.com/forums/226344-ndepend-user-voice).
