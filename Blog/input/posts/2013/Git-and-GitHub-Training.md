---
title: "Git and GitHub Training"
permalink: /2013/05/28/Git-and-GitHub-Training/
date: 5/28/2013 6:20:00 AM
updated: 5/28/2013 4:16:36 PM
disqusIdentifier: 20130528062000
tags: ["Git", "GitHub"]
alias:
 - /post/Git-and-GitHub-Training.aspx/index.html
---
Today I had the pleasure to attend “Git and GitHub Training” held by [Tim Berglund](http://timberglund.com/) one of the GitHubber, so called because he is working at [Github](https://github.com/).

In the past I enjoyed very much the video Tim and [Matthew McCullough](http://matthewjmccullough.com/), another GitHubber, did for O’Reilly
<!-- more -->

*   [McCullough and Berglund on Mastering Git](http://shop.oreilly.com/product/0636920017462.do)
*   [McCullough and Berglund on Mastering Advanced Git](http://shop.oreilly.com/product/0636920024774.do)


So it was really nice to meet Tim in one of his course.

**What are the class objectives?**

*   Understand how Git works and how to apply that to day to day development.
*   Learn how GitHub makes distributed collaboration both effective and enjoyable.
*   Practice the use of Pull Requests to make contributions to any project.
*   Learn the basic 10 commands that will appear in your every-day use of Git.
*   Know how to “back out” mistakes using Git’s incredible history and ability to revert almost any change.
*   Leverage the features of GitHub for easier collaboration with colleagues.
*   Discover how the offline capabilities of Git work.


**Topics**

*   Introductions
*   Git and your initial setup
*   Git configuration and its inheritance
*   SSH Authentication and your first repository
*   Understanding and thinking in Git's three stages
*   Adding, committing, and diff-ing code changes
*   The Similarity Index; Moving, Renaming, and Removing files
*   Reviewing Version History in Git
*   Strategies for Efficiency (quick workflows, GitIgnores, etc.)
*   Managing and using Git Remotes
*   GitHub
*   Forking Repos
*   Pull Requests
*   Branching, Tagging, and Stashing
*   Merging, Rebasing, and managing conflicts
*   Undoing your work with Git


The course was taking place at [Canoo headquarters](http://www.canoo.com/service/contact/) in Basel, which have really very nice offices and were welcoming us very nicely!

We went through all the topics described and even a bit more as most of the people in the course were already having some experience with git.

Did I liked the course, the format and the way to present it?

**Definitely yes!** I enjoyed it and would really recommend it to people which are starting with git. I especially liked the way Tim presented the inner working of git which I think is important to get right, especially when you come from another vcs like svn. Also that the log is a graph and not a list…

Did I learned a lot?

Not really but that was planned. I spent almost two years working with git svn at [Innoveo Solutions](http://www.innoveo.com/) and git for my personal projects. Now I am also using git without the svn part at work and continue to take influence so that the whole company migrate to git. So I gained some experience, which the course would have give me much quicker, and I guess my colleagues, Cédric, Carlos and Christian gained some knowledge in one day which took me much more time to grasp. So the course if a great starter, as for quite some things, you need to practice to really get it!

For people with a good understanding of git I would take care to go to the [Advanced Git & GitHub Course](http://training.github.com/web/advanced-git/).

During the course we had some open discussions on various topics, but here are some which I would like to put some emphasis on.

My personal experience brought me from thinking that I want to have the tools integrated into my IDE to, after getting more knowledge about git, using git from the command line. This makes me as a developer much more efficient and let me automate some stuff, which I couldn’t do with the IDE. For sure I agree with Tim ![Winking smile](/images/wlEmoticon-winkingsmile.png) that if I would have to work with cmd it would be rude but today there is [PowerShell](http://technet.microsoft.com/en-us/scriptcenter/powershell.aspx) and combined with [ConEmu](https://code.google.com/p/conemu-maximus5/) and [PoshGit](https://github.com/dahlbyk/posh-git) it makes very effective, even on Windows which was still seen as a clicking environment!

I learned about

> git config --global credential.helper cache

But this wasn’t working on my Windows machine, so I went back to [https://github.com/anurse/git-credential-winstore](https://github.com/anurse/git-credential-winstore)

This was also new to me

> git push --delete origin mybranch

because I am used to do it with the following, which is for sure less expressive of what it does

> git push origin :mybranch

Finally things I heard during the course which I want to investigate further are

*   packfiles for binary files - [http://git-scm.com/book/en/Git-Internals-Packfiles](http://git-scm.com/book/en/Git-Internals-Packfiles)
*   git daemon start a git daemon on a shared wifi
*   git bundle if network is difficult
*   dig more on the git help config


I also had interesting discussions with some of the Canoo people especially about [Scratch](http://scratch.mit.edu/), one way to bring kids to programming and the [Raspberry Pi](http://www.raspberrypi.org/).

So that was a great day! And I hope to have some others like this one in the future.
