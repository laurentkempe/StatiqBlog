---
title: "TeamCity 9 project settings versioning in Git"
permalink: /2014/12/13/TeamCity-9-project-settings-versioning-in-Git/
date: 12/13/2014 3:16:58 AM
updated: 12/13/2014 3:39:12 AM
disqusIdentifier: 20141213031658
coverImage: https://farm8.staticflickr.com/7480/16005747085_d88c4e6dc1_h.jpg
coverSize: partial
thumbnailImage: https://farm8.staticflickr.com/7480/16005747085_1d45cdda13_q.jpg
coverCaption: "Anse Cafard, Martinique"
tags: ["Team City", "Git"]
alias:
 - /post/TeamCity-9-project-settings-versioning-in-Git.aspx/index.html
---
<!-- [![Anse Cafard](https://farm8.staticflickr.com/7480/16005747085_1d45cdda13_m.jpg)](https://www.flickr.com/photos/laurentkempe/16005747085 "Anse Cafard by Laurent Kempé, on Flickr") -->

One of the great new feature of [TeamCity 9](https://confluence.jetbrains.com/display/TCD9/What%27s+New+in+TeamCity+9.0) is the possibility of [Storing project settings in Git and Mercurial](https://confluence.jetbrains.com/display/TCD9/What%27s+New+in+TeamCity+9.0#What%27sNewinTeamCity9.0-StoringprojectsettingsinGitandMercurial).

When you develop software it is primordial to be able to reproduce successfully builds. To achieve that goal you need for sure first to version the source code. But too often the build scripts are forgotten! Especially when the build scripts are created with such a great tool that is [TeamCity](https://www.jetbrains.com/teamcity/).
<!-- more -->

So we want to keep the source code and the configuration of the build server quite near. So that we are sure we can always rebuild a previous version of the software.

What we don't want is to have a mixture of source code and build configurations. To achieve that goal we can use the Git possibility to [create orphan branch](http://git-scm.com/docs/git-checkout/)

> git checkout --orphan teamcity/settings

Then we remove all content from the old working tree, normally your current source code. No worries, the other files are kept in the other branches!

> git rm -rf .

We add a ReadMe.md explaining that this branch is about storing the build server settings and we make a first commit

> git add ReadMe.md
> git commit -m "Initial TeamCity build settings commit"

And finally we push that to the origin git repository

> git push origin teamcity/settings

Now on your TeamCity server you can follow the instruction in [Storing Project Settings in Version Control](https://confluence.jetbrains.com/display/TCD9/Storing+Project+Settings+in+Version+Control) to define that TeamCity must version all changes which are done to your project.

We do it on the top most project so that we get all stored in our Git repository.

To achieve that we define a new TeamCity VCS Root pointing to our newly created orphaned branch; teamcity/settings and finally click the Apply button.

After some seconds you will get in your Git repository a second commit done by TeamCity containing all the configurations files!

Nice new feature!
