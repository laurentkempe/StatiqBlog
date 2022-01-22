---
title: 'Automating my development machine installation'
permalink: /2018/06/01/Automating-development-machine-installation/
disqusIdentifier: 20180601181512
coverSize: partial
tags:
  - Windows
  - PowerShell
coverCaption: 'Porto, Portugal'
coverImage: 'https://farm2.staticflickr.com/1734/41774816134_efbd8a4834_h.jpg'
thumbnailImage: 'https://farm2.staticflickr.com/1734/41774816134_261d684149_q.jpg'
date: 2018-06-01 18:15:12
---
Since Microsoft went to [Windows as a service](https://docs.microsoft.com/en-us/windows/deployment/update/waas-quick-start), so since Windows 10, I fully re-install my development machine, a Surface Book, with each main release of Windows.

As I hate to lose time, I searched a way to automate it, so that it is fast, repeatable and does the work, not me.
<!-- more -->

**TL; DR;** (Spoiler) Install your development environment tools with one click on a hyperlink!

Since long, I am using **[Chocolatey](https://chocolatey.org/)**, **the package manager for Windows**, to install some of my tools. I even created an installation of [Git Diff Margin](https://marketplace.visualstudio.com/items?itemName=LaurentKempe.GitDiffMargin), my Visual Studio extension displaying live changes of the currently edited file on Visual Studio 2012+ margin and scrollbar. Note to me, update the package to the latest version of Git Diff Margin.

That's already a fantastic step forward, you type one command line like this

{% blockquote  %}
choco install git -y
{% endblockquote %}

and you get Git installed without doing anything, except typing the command! No need to search on which website you need to download the software, download it, maybe extract it to finally be able to run it and click next-next-next (eh Cédric). Just one command on the command line and you are done. Great, no?

But after re-installing my machine this way a first time and a second time I wanted more automation. So I decided to dig deeper in a tool I found also a long time ago but never took the time to use; **[Boxstarter](https://boxstarter.org/): repeatable, reboot resilient Windows environment installations made easy using Chocolatey packages**. Seems to exactly match what I wanted to achieve for the next level of automation.

So I searched some examples on Github and cooked my own script which I open sourced and called it **[Cacao](https://github.com/laurentkempe/Cacao/blob/master/PrepareMyCacao.ps1)**. A nice name, no :D?

{% image fancybox center group:martinique https://farm5.staticflickr.com/4616/40521281581_57a9b1d503_o.jpg https://farm5.staticflickr.com/4616/40521281581_3d775063cc.jpg 375 500 Cabosse de cacaoyer, Martinique  %}

So now, when I have installed my Windows 10 machine from scratch I just need to install Boxstarter, download my script and launch it! A very nice step forward indeed.

Wait can't we do better, there is still quite some things to do by hand, and you need to remember those things!
What do you think of being able to click on a hyperlink, click on OK then everything would be installed for you by little pixies while you do something else? That would be just fantastic. The best is that it is possible!

You can see such a link on [Cacao README.md file](https://github.com/laurentkempe/Cacao/blob/master/README.md) search for the text 'Install my tools!'.

This link is built as following

{% codeblock Cacao install hyperlink lang:html  %}
<a href='http://boxstarter.org/package/nr/url?https://raw.githubusercontent.com/laurentkempe/Cacao/master/PrepareMyCacao.ps1'>
  Install my tools!
</a>
{% endcodeblock %}

So easy and so powerful. You append the hyperlink to the raw URL of your script to an URL which will install Boxstarter, and that does the trick!

So now we are ready to install development environment tools by one click on a hyperlink! What do you want more?

During the Build2018 Microsoft got interested in that topic, which is great, and you can read about it in their blog post "[Join us for a hot cup o Chocolatey!](https://blogs.msdn.microsoft.com/commandline/2018/05/08/join-us-for-a-hot-cup-o-chocolatey/)" and on their Github repo [windows-dev-box-setup-scripts](https://github.com/Microsoft/windows-dev-box-setup-scripts).

One fantastic side effect is that now I can also update all my tools by running one command

{% blockquote %}
choco upgrade all -y
{% endblockquote %}

Like they say, there’s never been a better time to be a developer!
