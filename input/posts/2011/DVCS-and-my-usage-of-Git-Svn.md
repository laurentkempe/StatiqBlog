---
title: "DVCS and my usage of Git Svn"
permalink: /2011/11/07/DVCS-and-my-usage-of-Git-Svn/
date: 11/7/2011 11:14:00 PM
updated: 11/12/2011 10:44:36 PM
disqusIdentifier: 20111107111400
tags: ["Git", "DVCS"]
alias:
 - /post/DVCS-and-my-usage-of-Git-Svn.aspx/index.html
---
Sometime ago I moved “away” from subversion has my Version Control System (VCS) because I felt not free of my way of working with it. I found a great was to improve my work experience by using Git Svn in front of our central Subversion repository.

I started my experimentation with Mercurial and Hg Svn because I was already using Mercurial for my personal projects. I hit some much the wall with Hg Svn that I decided quite quickly to go and try Git Svn. I had no experience with Git at that time and I had the feeling that it was more complex to handle than Mercurial (which I still think).
<!-- more -->

So currently I use Git Svn to work on one of our product at [Innoveo Solutions](http://www.innoveo.com/). I also made a presentation to the team during our techno pizza lunch about DVCS, Git, Git Flow and Git Svn.

And here is the list of what I personally gained as a developer:

1.  **2 steps commit**       

    1.  Stage/commit, Push    
2.  **Local history / branching **      

    1.  No connection to central repository needed to branch/to look at history    
3.  **Experiment / Refactoring / Spikes **      

    1.  Commit changes on one path, if wrong rollback    
4.  **Smarter Merging **      

    1.  Git’s focus on content rather file location 
    2.  Better at resolving merge conflicts for you (e.g. renames) 
    3.  Branching/Merging is daily workflow not anymore an ‘exceptional case’    
5.  **Stash changes **
6.  **Rebase / Rewriting history **      

    1.  Until push you can use interactive rebase      

Due to my 4h daily commute (more than 2h in the train), I very much appreciate the offline capabilities of DVCS. It lets me branch, watch the history without having a connection to the central repository. So that a really convenience thing in my day to day job.

But what I even prefer is working in local branches for experiment, refactoring. With that capability I am able again to commit small steps of code change. I particularly appreciate the possibility to experiment some refactoring and to rollback wrong changes when I feel that I went the wrong way with the change. In the past this was not possible and often you would not commit to SVN because that code would be shared with the others or t would break the build. Ok, I know I could do that with Svn working in a feature branch, but we all know the pain it is with the merging back, especially when you refactor and rename files. During refactoring it is important to be able to save/commit small chunk of code change and even more important is to be able to rollback those changes. So in that case DVCS is a perfect match to that problem.

What I currently don’t like in my current way of working with Git in front of Svn is that I have local branches which I don’t sync back to the central server. I don’t like it because if I have an issue with my local machine then those changes might be lost. I will investigate in the coming weeks about possible solutions even if the best one would be a migration of the central repository to Git. But his is another story because it means a wider change in the team.
