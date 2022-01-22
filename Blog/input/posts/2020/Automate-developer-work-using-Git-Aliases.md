---
title: 'Automate developer work using Git Aliases'
permalink: /2020/02/28/Automate-developer-work-using-Git-Aliases/
coverSize: partial
tags:
  - Git
  - PowerShell
  - Windows
coverCaption: 'Fleur de Tiaré, Moorea, Polynésie française'
coverImage: 'https://live.staticflickr.com/4359/35813310943_479293da80_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/4359/35813310943_375167de07_q.jpg'
date: 2020-02-28 16:26:27
disqusIdentifier: 20200228162627
---
I am using Git for many, many years. I am a big fan of it. How could that be different when you had to use versioning systems like CVS, SVN... I even took influence so that the whole company I work for migrates to it.

Over the years, I have used different Git clients, tried to work only from my IDE without much liking it.

<!-- more -->

Then, I started to use more the Console through a tool that I love [cmder](https://cmder.net/) ❤. It is bundling other awesome tools like [ConEmu](https://conemu.github.io/)...

On top of those two, I added [posh-git](https://github.com/dahlbyk/posh-git), a PowerShell module integrating Git and PowerShell by providing Git status summary information that can be displayed in the PowerShell prompt. To which, I added, [oh-my-posh](https://github.com/JanDeDobbeleer/oh-my-posh) a prompt theming engine for PowersShell. 

![cmder ConEmu posh-git oh-my-posh](https://live.staticflickr.com/65535/49566323082_e1817988c2_c.jpg)

That's a working environment that suites me. I have even cmder pinned on the first position of the Windows taskbar so that I can press **Win + 1 shortcut** and either run it or get back to it as fast as possible.

I felt great about it, but as a developer, I want more! Why? Cause **I want to automate repetitive tasks, so that my life is easier**.

The natural next step is to identify daily repetitive tasks. As we are talking about Git, the thing that comes to mind is creating a branch, checking out branches, review code in a branch. Then committing changes, rebasing your work...

# Conventions

**To be able to automate you need conventions**. In our case, the naming of a branch is quite important, maybe also the commit message format.

It is interesting to be able to navigate from your code, through the branch name to the specifications on the work you are doing. Using the ticket number in the branch name makes sense to help with that. Having the ticker number as the first part of any commit message will help also.

For example, naming a branch "feature/PRJ-123-my-feature-branch" will follow a convention that would let us see that it is a feature branch linked to the requirement ticket PRJ-123. The rest is just something that will help you to remember what the ticket is about.

With such a simple convention, you can start to automate things! 👍

To check out the branch and be able to work on it, from the command line we need to type the Git command:

> git checkout feature/PRJ-123-my-feature-branch

# Simple Git Alias

With the following simple Git alias, located in your [.gitconfig](https://git-scm.com/docs/git-config)

{% codeblock .gitconfig %}
[alias]
co = checkout
{% endcodeblock %}

you now type

> git co feature/PRJ-123-my-feature-branch

That's six chars less to type. But. that's still too much to type. You will tell me, you can use autocompletion provided by the tool you installed and you will be right. Nevertheless, we want more!

We want to type

> git cfb 123

cfb for check out feature branch.

# Composing Git Aliases

The great folk who created Git thought of that problem and they provide us a very nice and powerful way to extend on simple Git aliases by being able to compose them.

From getting the feature branch name, we want to extract the ticket number and use those new capabilities to have a very nice new git feature.

To get the current branch name, we use the following alias:

{% codeblock .gitconfig %}
# Get current branch name
currentbranch = rev-parse --abbrev-ref HEAD
{% endcodeblock %}

To get the ticket number, we compose the previous alias we the following one

{% codeblock .gitconfig %}
# Get Jira ticket number from current branch name
jiran = "!f() { git currentbranch |
                sed "s/feature.//" |
                grep -o -E "PRJ+-[0-9]+";
              }; f"
{% endcodeblock %}

This one looks a bit more complex and we will get back to it to explain it in more detail. Finally, we can create our interesting and time saver new alias.

{% codeblock .gitconfig %}
# Open Browser on Jira ticket
j = "!explorer https://company.atlassian.net/browse/$(git jiran)"
{% endcodeblock %}

Now, using simple branch naming convention and little script code we are able to open the web browser and navigate to the web page describing the ticket requirements. And this by only typing "git j" ❤.

We are using Jira in that example but you could use a Github issue or whatever you prefer.

# Bash function Git Aliases

The alias which gets the ticket number out of the branch name seems quite complex at first sight, but it is really easy when you know about bash functions. !f() { } defines a bash function “f“.

It gives you access to command line variables like:

* **$1** is the 1st parameter passed to the command.
* **$2** is the 2nd parameter passed to the command...
* **$@** means all command line parameters passed.

It also allows to chain git commands with && and uses the entire Unix toolkit.

Now, back to our goal of typing a minimum to check out a feature branch: "git cfb 123".
We can leverage lots of power of Unix combined to Git aliases to achieve our goal.

First, we need to find the feature branch from the ticket number

{% codeblock .gitconfig %}
# Find feature branch from ticket number
fb = "!f() { git branch -a |
             grep "feature/.*$1.*" |
             sed "s/remotes.//" |
             sed "s/origin.//" |
             sort -u;
           }; f"
{% endcodeblock %}


Then we combine this new alias in the final cfb alias, which checks out the branch and pull so that you are up to date and can continue to work

{% codeblock .gitconfig %}
# Checkout feature branch from ticket number
cfb = "!f() { featureBranch=$(git fb $1);
              git checkout $featureBranch;
              git pull;
            }; f"
{% endcodeblock %}

# Presentation

{% reveal https://laurentkempe.com/presentations/Automate%20Dev%20work%20using%20Git%20aliases/index.html#/ 800 600 %}

Press f key to see the presentation full screen, or [navigate to it](https://laurentkempe.com/presentations/Automate%20Dev%20work%20using%20Git%20aliases/index.html#/).

# More

You can access all the aliases I use and created and the others which I borrowed from different people like [Phil Haack](https://haacked.com/archive/2014/07/28/github-flow-aliases/) and [Nicola Paolucci](https://www.atlassian.com/blog/git/advanced-git-aliases), on my Github repository [laurentkempe/dotfiles](https://github.com/laurentkempe/dotfiles/blob/master/git/.gitconfig.aliases#L13).

<p></p>
{% githubCard user:laurentkempe repo:dotfiles align:left %}
