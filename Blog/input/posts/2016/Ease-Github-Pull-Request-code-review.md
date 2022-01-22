---
title: Ease Github Pull Request code review
date: 2016-04-18 15:21:47
tags: ["Git", "GitHub", "Visual Studio"]
permalink: /2016/04/18/Ease-Github-Pull-Request-code-review/
disqusIdentifier: 20160418152147
coverSize: partial
coverCaption: 'Cascade trou du diable, Corse'
coverImage: 'https://farm1.staticflickr.com/713/20404617220_dea22d6f2c_h.jpg'
thumbnailImage: 'https://farm1.staticflickr.com/713/20404617220_63ef10f285_q.jpg'
---
Even if online [Github Pull Request](https://help.github.com/articles/using-pull-requests/) is a nice and effective tool, in some situation you need to open your solution in Visual Studio to verify something.
<!-- more -->
One evening, I created a project called [PReview](https://github.com/laurentkempe/PReview) which after you feed it with a diff file lets you filter Visual Studio Solution Explorer with all files changed. It is really alpha and a quick one evening hack!

The other day my colleague [Gianluigi](https://twitter.com/gianluigiconti) proposed to my team an alternative with a PowerShell script which you would run specifying the Pull Request id and the branch name:

> review 272 feature/1270-show-hide-panels

Nice! The script is good but as I am spending a bit more than a day of work working on a running train with an unstable internet connection I needed something that would work offline too. So I thought there should be a way that would fulfill that requirement!

I ended up with something really easy which also leverage the nice possibility of [GitDiffMargin](https://github.com/laurentkempe/GitDiffMargin) to see changes in Visual Studio margin.

Basically, you create a new branch at the beginning of the branch that you want to review. Then you merge the original branch into the review branch with the squash option.

Let's see a concrete example with my project [Nubot](https://github.com/laurentkempe/nubot). Our goal is to review the branch called feature/hipchat-connect.
First, we need to find the SHA1 of the previous commit of the start of the branch feature/hipchat-connect. You might use a tool like [SourceTree](https://www.sourcetreeapp.com/) 

![Ease Github Pull Request Code Review 1](https://farm2.staticflickr.com/1570/25900395853_1312aa8129_o.png)

or with a nice git alias 
> lg = log --color --graph --pretty=format:'%Cred%h%Creset -%C(yellow)%d%Creset %s %Cgreen(%cr) %C(bold blue)<%an>%Creset' --abbrev-commit --

![Ease Github Pull Request Code Review 2](https://farm2.staticflickr.com/1533/25900395843_2bc52142b8_o.png)

On both screenshots, we see that the SHA1 of the commit is **4d6a5d1**

So now we create our new review branch

> git checkout -b review/hipchat-connect 4d6a5d1

Then we squash the original branch into the review one

>  git merge --squash feature/hipchat-connect

Now opening Visual Studio you can go to the Solution Explorer and click on Pending Changes Filter and you will get the list of file modified

![Ease Github Pull Request Code Review 3](https://farm2.staticflickr.com/1445/26410906852_75b2c64267_o.png)

And on the source code, you will see the changes thanks to [GitDiffMargin](https://visualstudiogallery.msdn.microsoft.com/cf49cf30-2ca6-4ea0-b7cc-6a8e0dadc1a8)

![Ease Github Pull Request Code Review 4](https://farm2.staticflickr.com/1535/25900395813_bc5efee43e_o.png)

You have now all your current tools to navigate your code and do even easier code review!
