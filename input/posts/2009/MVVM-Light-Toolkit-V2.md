---
title: "MVVM Light Toolkit V2"
permalink: /2009/10/14/MVVM-Light-Toolkit-V2/
date: 10/14/2009 6:12:53 AM
updated: 4/28/2010 8:02:22 PM
disqusIdentifier: 20091014061253
tags: ["WPF", "innoveo solutions", "MVVM"]
alias:
 - /post/MVVM-Light-Toolkit-V2.aspx/index.html
---
At [Innoveo Solutions](http://www.innoveo.com/) we are using .NET and WPF for our [Innoveo Skye](http://www.innoveo.com/Products.aspx)® Editor application. Skye® Editor is a distribution channel editor targeting business people letting them edit and configure their insurance products. 

From the beginning we have adopted the **Model-View-ViewModel** architecture. Having our solution growing we were facing the issue of having our ViewModels dependency growing too. Some ViewModel became too much dependent of others. This was obvious in our unit tests whose complexity to setup were growing too. It was time to find a solution to decouple the ViewModels.
<!-- more -->

The solution came out after a discussion with [Laurent Bugnion](http://www.galasoft.ch/intro_en.html), the famous author of [**MVVM Light Toolkit**](http://www.galasoft.ch/mvvm/getstarted/). At that time we used the V1 that already helped a lot in this decoupling.

Now with **[MVVM Light Toolkit V2](http://blog.galasoft.ch/archive/2009/10/03/mvvm-light-toolkit-v2-whatrsquos-new.aspx)** it is even better with the new Messenger API. What we also really appreciated in comparison to other frameworks is that it is really **light **and the ability to open and edit the user interface into Expression Blend.

So Thank you [Laurent](http://www.galasoft.ch/intro_en.html) for this GREAT framework and I looking forward for V3!

I also would like to thank my managers at Innoveo Solutions who understand Open Source and the need to have people contributing to Open Source projects, even during their professional working time. A Win-Win situation and not just a one way benefit as often.
