---
title: '.NET Core Community online Hackathon'
permalink: /2018/06/06/dotnet-core-community-online-hackathon/
disqusIdentifier: 20180606082054
coverSize: partial
tags:
 - .NET Core
coverCaption: 'Tahiti, France'
coverImage: 'https://farm5.staticflickr.com/4396/36453581072_9c7848b74c_h.jpg'
thumbnailImage: 'https://farm5.staticflickr.com/4396/36453581072_a92ff68f56_q.jpg'
date: 2018-06-06 08:20:54
---
On Saturday, June 2, 2018, I had the chance to participate in the .NET Core Community online Hackathon.

As I got up really early this Saturday morning, I took the opportunity to participate. My goal was to see something new and learn a bit about how .NET Core is built and gain some new experience.
<!-- more -->

I heard about the online Hackathon on a tweet of Karel Zikmund {% twitter https://twitter.com/ziki_cz/status/999804438104625153 %}

Then reading about it on the [Github page](https://github.com/dotnet/corefx/wiki/Hackathon) linked to the tweet I decided to participate.

I wanted to start with a really small thing because I wasn't sure about the time I had, and I wanted to be sure to finish it!

I decided to go with [CoreFX](https://github.com/dotnet/corefx/blob/master/README.md) because it seemed easier than [CoreCLR](https://github.com/dotnet/coreclr/blob/master/README.md). I also had a look to [CoreFxLab](https://github.com/dotnet/corefxlab/blob/master/README.md). And I might implement something there later as there is some migration to [Benchmark.DotNet](https://benchmarkdotnet.org/) which I would also like to learn a bit about.

I joined the [gitter chat](https://gitter.im/dotnet/corefx-hackathon?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge) which proved to be really helpful.

Then I picked the [issue #19624, Remove conditional compilation in System.Diagnostics.Tracing](https://github.com/dotnet/corefx/issues/19624) from CoreFX.

I went and started to read [contribution guide](https://github.com/dotnet/corefx/wiki/New-contributor-Docs#contributing-guide) and started by installing the tooling, making a fork and clone of the CoreFX repo! All went quite well, even if the documentation could be improved in some places.

I tried to have the tests running inside Visual Studio, but after some pains and no success with [ReSharper](https://www.jetbrains.com/resharper/?fromMenu) and [Visual Studio]((https://visualstudio.microsoft.com/), I asked some help on the gitter which [Viktor Hofer](https://github.com/ViktorHofer) [provided kindly](https://gitter.im/dotnet/corefx-hackathon?at=5b1259f5f9f2e56cf234c485). You need to use the command 

{% codeblock Running tests from MSBuild lang:language  %}
msbuild /t:RebuildAndTest
{% endcodeblock %}

By running the tests I found an issue with one tests and quickly guessed that it was because my Windows 10 Pro installation is in en-US and I set my first day of the week to Monday! So I reported the [issue on Github](https://github.com/dotnet/corefx/issues/30074). The issue went through a discussion in the team and was finally fixed after being first rejected. I have to say that I agree that when someone clones a repository and run the tests, then everything should be working even in a weird setting like mine.

Back to my pull request, [Remove FEATURE_ETLEVENTS conditional compilation](https://github.com/dotnet/corefx/pull/30071) which was super easy, nothing really hard when you just need to remove conditional compilation. I just had to disable some Visual Studio plugin which format the code, especially removing extra spaces at the end of lines, which I was surprised to see on some files.

Finally after reviews and final approval from [Stephen Toub](https://github.com/stephentoub) the pull request was merged and will be part of .NET Core 3.0!

![Pull Request merged](https://farm2.staticflickr.com/1839/29090676488_e79f80d854_o.png)

So in the end, I was able to fix one issue due for the hackathon, created one issue which was fixed by the team and had some fun and some learning participating in this online Hackathon!

Thanks for organizing it and thanks to the people from the team which was present and helped us!
