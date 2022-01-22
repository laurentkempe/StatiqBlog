---
title: "Using Thread.Sleep() in Unit Test! A good idea?"
permalink: /2012/11/20/Using-ThreadSleep-in-unit-test-A-good-idea/
date: 11/20/2012 7:57:00 PM
updated: 11/20/2012 8:25:46 PM
disqusIdentifier: 20121120075700
coverImage: https://farm8.staticflickr.com/7040/6978718001_f2bdaca933_b.jpg
coverSize: partial
thumbnailImage: https://farm8.staticflickr.com/7040/6978718001_f2bdaca933_q.jpg
coverCaption: "Green sea turle, Anse Noire, Martinique"
tags: [".NET", "C#", "TDD"]
alias:
 - /post/Using-ThreadSleep-in-unit-test-A-good-idea.aspx/index.html
---
<!-- [![Turtle](http://farm8.staticflickr.com/7040/6978718001_f2bdaca933_m.jpg)](http://www.flickr.com/photos/laurentkempe/6978718001/ "Turtle by Laurent Kempé, on Flickr") -->
In my humble opinion it is definitely not a good idea! Why?

1.  It is brittle test because it depends to the CPU load of the machine running the test. Maybe it runs fine on your development machine, and will for sure from time to time fail on your build server because of the load on the server. 
<!-- more -->
2.  It is slower then needed. If you increase the sleep time so that you “ensure” that the test should pass in all situation then the test will always as long as this time.   

So what can we do about it?

One of the first approach is to using polling like [NUnit and it’s Delayed Contraint](http://www.nunit.org/index.php?p=delayedConstraint&r=2.6) does. But I am not a big fan of this because you have to remember the details of it, and you can fall in that trap easily:

> Use of a **DelayedConstraint** with a value argument makes no sense, since the value will be extracted at the point of call. It's intended use is with delegates and references. If a delegate is used with polling, it may be called multiple times so only methods without side effects should be used in this way.

My personal preferred approach is:

1.  to expose as a protected property the Task running the background operation 
2.  to create a SUT class in my test class which inherit from the class with the protected property 
3.  to make the Task a public property of the SUT class 
4.  to have my test use the Task by calling the Task.Wait() just before the assertion   

For example here is one of my test using that approach:

<script src="https://gist.github.com/4117094.js"> </script>
