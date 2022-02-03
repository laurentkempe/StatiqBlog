---
title: "Azure DevOps for Visual Studio Extensions"
permalink: /2019/03/05/Azure-DevOps-for-Visual-Studio-Extensions/
disqusIdentifier: 20190305221442
coverSize: partial
tags:
  - Azure DevOps
  - Visual Studio
  - Chocolatey
  - Github
coverCaption: 'Moorea, Polynésie française, France'
coverImage: 'https://farm5.staticflickr.com/4350/36640551660_cb7e3db2b0_b.jpg'
thumbnailImage: 'https://farm5.staticflickr.com/4350/36640551660_cb7e3db2b0_q.jpg'
date: 2019-03-05 22:14:42
---
As you might have guessed reading some of my older posts, I like to automate things which I believe a computer should do in place of humans.

<!-- more -->

It is true for "[Automating my development machine installation](https://laurentkempe.com/2018/06/01/Automating-development-machine-installation/)" or deploying software in "[Build, ship and run ASP.NET Core on Microsoft Azure using Docker Cloud](https://laurentkempe.com/2016/07/18/Build-ship-and-run-ASP-NET-Core-on-Microsoft-Azure-using-Docker-Cloud/)"... Today I will show you how to publish Visual Studio extensions using [Azure DevOps](https://azure.microsoft.com/en-us/services/devops/).

I am the author of **[Git Diff Margin](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin)**, a Visual Studio extension displaying live Git changes of the currently edited file on Visual Studio margin and scroll bar. It supports Visual Studio 2012 through Visual Studio 2019 Preview. You can [watch a short video](#Git-Diff-Margin-feature-demo) about some of its features at the end of this blog post.

# Previously 
My way of releasing [Git Diff Margin](https://github.com/laurentkempe/GitDiffMargin) was tedious with lots of manual steps<p></p>

1. Update versions in all AssemblyInfo.cs, in source.extension.vsixmanifest
1. Build on my local machine
1. Get the vsix, install it in Visual Studio and test it
1. Tag with Git and push to Github
1. Login to the Visual Studio marketplace, upload by hand the vsix, adapt the description and publish
1. Create a release on Github, upload by hand the vsix, adapt the description and publish
1. Login to Chocolatey.org, upload by hand the nupkg, adapt the description and publish

There is clearly lots of place for some automation.

# First improvement

For some time, I have used [AppVeyor](https://www.appveyor.com/) to at least have a Continuous Integration build. This was helping already to be sure that a Pull Request would build correctly and to get the vsix artifact built somewhere else than on my developer machine.

A good step for sure, but not enough to please me.

# Today
I have greatly reduced the burden using [Azure DevOps](https://azure.microsoft.com/en-us/services/devops/) 💕 and this **at no cost**!
Microsoft is supporting the open source community with [Azure Pipelines](https://azure.microsoft.com/en-us/services/devops/pipelines/) with 10 parallel jobs with unlimited minutes for CI/CD! Thanks for that.

You can watch a great quick intro video from [Abel Wang](https://twitter.com/AbelSquidHead) to get a better idea
{% youtube NuYDAs3kNV8 %}

## Build pipeline

So, creating a build pipeline on Azure DevOps is the first step and it is super easy! It comes in two flavors; one with a [visual editor](https://docs.microsoft.com/en-us/azure/devops/pipelines/get-started-designer?view=azure-devops&tabs=new-nav) and one as a [YAML file](https://docs.microsoft.com/en-us/azure/devops/pipelines/yaml-schema?view=azure-devops&tabs=schema).

The overall goal of the build pipeline is to<p></p>

1. Start a build when something changes on the master branch of [Git Diff Margin Github repository](https://github.com/laurentkempe/GitDiffMargin)
1. Produce artifacts resulting from a successful build

To achieve this here are the steps executed by the build pipeline<p></p>

1. [Installing Nuget Tool](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/tool/nuget?view=azure-devops)
1. Updating all AssemblyInfo.cs automatically using [Assembly Info extension](https://marketplace.visualstudio.com/items?itemName=bleddynrichards.Assembly-Info-Task)
1. Update Git Diff Margin Vsix Version using [Vsix Tools extension](https://marketplace.visualstudio.com/items?itemName=SamirBoulema.Vsix-Tools)
1. Building Git Diff Margin Visual Studio solution using [Visual Studio Build task](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/build/visual-studio-build?view=azure-devops)
1. Running the tests. ❗ Currently, I still need to have this working
1. [Copy](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/utility/copy-files?view=azure-devops&tabs=yaml) build artifacts to a staging folder and to the Chocolatey folder
1. Run [Chocolatey pack](https://marketplace.visualstudio.com/items?itemName=gep13.chocolatey-azuredevops)
1. Finally, [publish the build artifacts](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/utility/publish-build-artifacts?view=azure-devops) so that they can be downloaded later on

You can see that we already automated a good part of the manual tasks that we had to do previously. In fact, now we still need to update the version manually but only in one place which is the Azure DevOps YAML file: *azure-pipelines.yml*.

The remaining manual tasks are

1. Get the vsix, install it in Visual Studio and test it
1. Update README-Marketplace.md file which will be uploaded to the Visual Studio Marketplace as a description file 
1. Tag with Git and push to Github

This is the *azure-pipelines.yml* file which automates the whole build step!

{% gist dcc375c8bd03fb0367b5b6835464b45c azure-pipelines.yml %}

By the way, you can get nice [clickable build status badge](https://docs.microsoft.com/en-us/rest/api/azure/devops/build/badge/get%20build%20badge%20data?view=azure-devops-rest-5.0) like the following one

[![Build Status](https://dev.azure.com/techheadbrothers/GitDiffMargin/_apis/build/status/laurentkempe.GitDiffMargin)](https://dev.azure.com/techheadbrothers/GitDiffMargin/_build/latest?definitionId=7) 

## Release pipeline

Now that we have access to the build artifacts; [GitDiffMargin.vsix for the Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin) and [GitDiffMargin.nupkg for Chocolatey](https://www.chocolatey.org/packages/GitDiffMargin) we can automate the next step through a [Azure DevOps release pipeline](https://docs.microsoft.com/en-us/azure/devops/pipelines/release/?view=azure-devops).

In this step, we would like to automate the following

5. Login to the Visual Studio marketplace, upload by hand the vsix, adapt the description and publish
6. Create a draft release on Github, upload by hand the vsix, create a description based on changes
7. Login to Chocolatey.org, upload by hand the nupkg, adapt the description and publish

For that, we created a release pipeline job named "Marketplace - Github - Choco" with 3 tasks connected to our previously defined build pipeline artifacts
{% image center clear https://farm8.staticflickr.com/7807/32349146347_f88d9b1fce_o.png 653 423 Azure DevOps Release pipeline %}

In the details
{% image center clear https://farm8.staticflickr.com/7840/47290958411_251b163117_o.png 642 296 Azure DevOps Release pipeline tasks %}

The first task publish Git Diff Margin to Visual Studio Extension using [Azure DevOps Extension Tasks](https://marketplace.visualstudio.com/items?itemName=ms-devlabs.vsts-developer-tools-build-tasks)

{% gist dcc375c8bd03fb0367b5b6835464b45c azure-vs-release-pipelines.yml %}

The second task creates a GitHub release draft using [GitHub Release task](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/utility/github-release?view=azure-devops)

{% gist dcc375c8bd03fb0367b5b6835464b45c azure-github-release-pipelines.yml %}

The third task publish to Chocolatey using [Chocolatey](https://marketplace.visualstudio.com/items?itemName=gep13.chocolatey-azuredevops)

{% gist dcc375c8bd03fb0367b5b6835464b45c azure-chocolatey-release-pipelines.yml %}

The release pipeline is triggered manually when I am ready to publish a new version

{% image center clear https://farm8.staticflickr.com/7820/46376429945_e3fde68d19_o.png 661 142 Azure DevOps Releasing %}

And if everything worked correctly you see the following result
{% image center clear https://farm8.staticflickr.com/7883/40326503893_58116c2328_o.png 687 497 Azure DevOps Release result %}

And now people have access to the latest version of Git Diff Margin on [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin), on [Github release](https://github.com/laurentkempe/GitDiffMargin/releases/tag/v3.9.3) and on [Chocolatey](https://www.chocolatey.org/packages/GitDiffMargin/).

Finally, we achieved our goal and let the machines do the work, which saves us time and avoid some mistakes.

# Thanks
* [Gary Ewan Park](https://www.gep13.co.uk/) aka [@gep13](https://twitter.com/gep13) for the great help he provided me on the [Chocolatey](https://www.chocolatey.org/) part of this journey!
* [Utkarsh Shigihalli](https://github.com/onlyutkarsh) aka [@onlyutkarsh](https://twitter.com/onlyutkarsh) for [extending of Azure DevOps Extension Tasks for the Visual Studio marketplace](https://github.com/Microsoft/vsts-extension-build-release-tasks/issues/103)

# Git Diff Margin feature demo
<a data-flickr-embed="true" href="https://www.flickr.com/photos/laurentkempe/14879945429/" title="Git Diff Margin features demo"><img src="https://farm6.staticflickr.com/5562/14879945429_cc40e1db81_o.jpg" width="480" height="360" alt="Git Diff Margin features demo"></a><script async src="//embedr.flickr.com/assets/client-code.js" charset="utf-8"></script>
