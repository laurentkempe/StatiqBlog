---
title: "Functional Programming Principles in Scala using JetBrains IntelliJ IDEA"
permalink: /2013/09/20/Functional-Programming-Principles-in-Scala-using-JetBrains-IntelliJ-IDEA/
date: 9/20/2013 8:43:00 PM
updated: 10/4/2013 4:53:57 AM
disqusIdentifier: 20130920084300
coverImage: https://farm4.staticflickr.com/3798/9553182109_8db2fd62d4_o.jpg
coverSize: partial
thumbnailImage: https://farm4.staticflickr.com/3798/9553182109_bcac5e189d_q.jpg
coverCaption: "Whale in Cape Bridgewater, Australia"
tags: ["Coursera", "IntelliJ", "Jetbrains", "Scala"]
alias:
 - /post/Functional-Programming-Principles-in-Scala-using-JetBrains-IntelliJ-IDEA.aspx/index.html
---
<!-- [![Another great shot of our friend the Whale of last week in Cape Bridgewater](http://farm4.staticflickr.com/3798/9553182109_bcac5e189d_m.jpg)](http://www.flickr.com/photos/laurentkempe/9553182109/ "Another great shot of our friend the Whale of last week in Cape Bridgewater by Laurent Kempé, on Flickr") -->

This week I started the [Coursera](https://www.coursera.org/) course [Functional Programming Principles in Scala](https://www.coursera.org/course/progfun) with Martin Odersky as instructor.

One of my first step was to have [JetBrains IntelliJ IDEA](http://www.jetbrains.com/idea/) working with the [Scala programming language](http://www.scala-lang.org/) so that I can work on the course assignments using my preferred IDE.
<!-- more -->

I have seen that The Guardian published a blog post [Functional Programming Principles in Scala: Setting up IntelliJ](http://www.theguardian.com/info/developer-blog/2012/sep/21/funtional-programming-principles-scala-setting-up-intellij) but it focus on the Unix-like operating systems (OSX and Linux). As I work on Windows I for sure installed it on my machine running Windows 8.1! I also use a different set of tools so here is the way I did it!

First of all I am assuming that you have [JetBrains IntelliJ IDEA 12](http://www.jetbrains.com/idea/) and JDK installed.

From IntelliJ you need to install the Scala plugin so go to File / Settings then on the dialog search for plugins, click on Browse Repositories button and search for Scala then select Scala v.0.19.299 from JetBrains

![](http://farm8.staticflickr.com/7341/9834166825_2867bcc1ba_o.png)

Then you also need to install the [SBT plugin also from JetBrains](http://blog.jetbrains.com/scala/2013/07/17/sbt-plugin-nightly-builds/) which is still in development and get nightly builds.

Add the following URL to the list of custom plugin repositories in Settings / Plugins / Browse Repositories / Manage Repositories:

> [http://download.jetbrains.com/scala/sbt-nightly-leda.xml](http://download.jetbrains.com/scala/sbt-nightly-leda.xml)

After that, you may install the plugin via Settings | Plugins | Browse Repositories. IDEA will check for the latest plugin builds automatically.

![](http://farm8.staticflickr.com/7363/9834729745_f23a4eed10_o.png)

Now extract the assignment zip that the coursera course is providing; e.g. for the example assignment, unpack the example.zip to a folder then in IntelliJ use File / Import Project. IntelliJ already recognize the folder as project:

![](http://farm3.staticflickr.com/2818/9834957283_3cff547c6e_o.png)

![](http://farm3.staticflickr.com/2893/9835093913_02feeb2bce_o.png)

Click Next

![](http://farm8.staticflickr.com/7366/9835020965_200917fb2a_o.png)

Tick Use auto-import and click Finish. IntelliJ will then import your project:

![](http://farm4.staticflickr.com/3818/9835074955_5eca26b99e_o.png)

Now you need to edit your project structure using File / Project Structure and choose Modules, then progfun-example

![](http://farm6.staticflickr.com/5450/9835157816_aa231ca57e_o.png)

You will see on the dialog for the Source and Test Source Folders some folder in red, just click on the X to delete all the one in red to get to the following state, then finally click OK

![](http://farm3.staticflickr.com/2832/9835244223_2f54a33cd8_o.png)

Now navigate to the test file ListsSuite and press ALT – SHIFT – F10 to start the tests

![](http://farm8.staticflickr.com/7419/9835291836_9c3aba2a3f_o.png)

And if you implemented the assignment you should see this results:

![](http://farm8.staticflickr.com/7409/9835313066_82e5e31067_o.png)

There is still two tests to fix in the first assignment, this is why there are red!

But as you can see now you have a full Scala development environment based on the great JetBrains IntelliJ IDEA IDE! Now you can code with pleasure!

I really enjoy the first week assignments and implemented the first two as [coding Kata](http://www.laurentkempe.com/post/Test-Driven-Development-Kata-String-Calculator.aspx)! And I even involved France-Anne for the second one; first time that she is coding something ![Smile](/images/wlEmoticon-smile.png) . Very nice!
