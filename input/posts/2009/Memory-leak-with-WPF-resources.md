---
title: "Memory leak with WPF resources"
permalink: /2009/04/17/Memory-leak-with-WPF-resources/
date: 4/17/2009 10:36:49 PM
updated: 4/17/2009 10:36:49 PM
disqusIdentifier: 20090417103649
tags: ["Tools", "WPF"]
alias:
 - /post/Memory-leak-with-WPF-resources.aspx/index.html
---
I am working for a couple of months now with WPF and MVVM on an a business application using .NET Framework 3.5 SP1. Lately I faced a memory leak. Not the easy kind of memory leak with events handlers which keeps objects and its element tree alive, [as explained here](http://blogs.msdn.com/jgoldb/archive/2008/02/04/finding-memory-leaks-in-wpf-based-applications.aspx).

No it was something else ! I searched in our code for quite some time without finding anything.
<!-- more -->

My internet research brought me to a blog post from [Ramon de Klein](http://blog.ramondeklein.nl/?page_id=2), “[Memory leak with WPF resources (in rare cases)](http://blog.ramondeklein.nl/?p=58)” which stated :

> **When does the problem occur?**
> The problem occurs in the following situation:
> 
> 1.  A style is defined in the application’s ResourceDictionary.
> 2.  The style uses a control template that uses media effects (i.e. DropShadowEffect).
> 3.  The media effect should be referenced using a StaticResource.

We were matching the two first points and I tried the proposed fix:

> You can force the effect to be frozen by specifying “[PresentationOptions:Freeze=True](http://msdn.microsoft.com/en-us/library/aa970057.aspx)”, but this is not common behavior.
> 
> …
> 
> The workaround is simple… Just add the Freeze attribute to all the effects that you don’t plan to modify at runtime.

But that didn’t made it. 

I decided then to move the style definition from the application ResourceDictionary to the MainWindowView ResourceDictionary, which was for sure a better place for it. This to avoid the first point.

This worked! And we do not have this memory leak anymore. But as always when you fix one, some other popped up! But that’s another story.

How did I came to find the blog post from [Ramon de Klein](http://blog.ramondeklein.nl/?page_id=2) ?

With a perfect timing I got an offer from [Red Gate](http://www.red-gate.com/) to test their latest tool still in Early Access Program: ANTS Memory Profiler 5

You might watch a two parts video [here](http://www.youtube.com/watch?v=KwCZ_nlL3Z4) (part1) and [here](http://www.youtube.com/watch?v=WQFc735EXUg&feature=related) (part2) and read about it [here](http://www.simple-talk.com/community/blogs/bart/archive/2009/02/16/71980.aspx). You can even [download a version](http://www.red-gate.com/messageboard/viewforum.php?f=92) from their forum, check it out.

![](http://farm4.static.flickr.com/3329/3450207374_a0fe74063b_o.png)First I used ANTS Memory Profiler 5 Timeline to see that the memory wasn’t released at certain points in which it should. The red line shows the Bytes in all Heaps and should go down after each vertical line (gray, blue and red)

Same result with Process Explorer, Memory usage going up without going down.

![](http://farm4.static.flickr.com/3311/3450216006_7bf3410a31_o.png) 

So we got on the screen the memory leak. Next step was to identify it.

Using Memory Profiler Class List, I browsed to the class that I new should be released, and proved it wasn’t: Live Instances is 3 and Instance Diff is +2. So at that time I knew that this class was maintained by something in memory.

![](http://farm4.static.flickr.com/3340/3449407599_db94a25df2_o.png) 

Watching the Memory Profiler Instance list, I could identity the different instances still in Memory and one wasn’t new, so a good candidate to look at:

![](http://farm4.static.flickr.com/3313/3449416263_f1af7940e1_o.png) 

Finally switching to the Memory Profiler Object Retention Graph, I could navigate up the graph to see that DropShadowEffect was maintaining a reference:

![](http://farm4.static.flickr.com/3370/3449432719_8582e06387_o.png)

Up to my application RessourceDictionnary, as exaplained in point 1 from [Ramon de Klein](http://blog.ramondeklein.nl/?page_id=2) blog post, as you can see.

![](http://farm4.static.flickr.com/3388/3450251632_93039a9260_o.png) 

All those information gave me the opportunity to find the blog post from Ramon and led me to the solution.

This is the result of the fix I implemented, as you can see I have now only one Live Instance of the ProductViewModel class and an Instance Diff of –1. This shows that the object wasn’t retained in memory and cleaned correctly.

 ![](http://farm4.static.flickr.com/3583/3450276240_c347c4d280_o.png) 

ANTS Memory Profiler 5 and [Ramon de Klein](http://blog.ramondeklein.nl/?page_id=2) “[Memory leak with WPF resources (in rare cases)](http://blog.ramondeklein.nl/?p=58)” blog post was of great help to fix this memory leak. Thanks!

Thanks flies also to Stephen Chambers for the support with my questions on ANTS Memory Profiler 5.

Finally I would like to warmly THANKS **[Laurent Bugnion](http://www.galasoft.ch/) **for his kind chats that helped a lot, as always!
