---
title: "Using Resharper Search with Pattern to protect you from your code"
permalink: /2010/11/19/Using-Resharper-Search-with-Pattern-to-protect-you-from-your-code/
date: 11/19/2010 12:02:53 AM
updated: 11/19/2010 12:02:53 AM
disqusIdentifier: 20101119120253
tags: ["ReSharper", "Jetbrains"]
alias:
 - /post/Using-Resharper-Search-with-Pattern-to-protect-you-from-your-code.aspx/index.html
---
Today I faced a nasty bug, a null pointer exception in a property databound to our WPF application. This had some nasty side effects, and for sure this part of the code didn’t had any unit test, too bad! For sure now it has.

The line of code was really simple
<!-- more -->

Values.Where(model => model.IsSelected).FirstOrDefault().Refresh();

I guess you see the issue! If not here is the MSDN documentation:

> <dl><dt>public static TSource **FirstOrDefault**< TSource >(this [[NotNull](about:blank#)] [IEnumerable](about:blank#)<TSource> <var>source</var>) </dt><dd></dd><dt>*in class [Enumerable](about:blank#)*</dt></dl>
> 
> ## Summary:
> 
> Returns the first element of a sequence, or a default value if the sequence contains no elements.
> 
> ### Parameters:
> 
> <var>source</var>:
> The [IEnumerable<out T>](about:blank#) to return the first element of.
> 
> ## Type Parameters:
> 
> <var>TSource</var>:
> The type of the elements of <var>source</var>.
> 
> ## Returns:
> 
> default(<var>TSource</var>) if <var>source</var> is empty; otherwise, the first element in <var>source</var>.
> 
> ### Exceptions:
> 
> [ArgumentNullException](about:blank#):
> <var>source</var> is null.

Yeah, **FirstOrDefault** might returns null, so if you chain a call to another method it just crash!

For sure we know that! We use this method for that purpose, but an error can happen that fast!

So I decided that I wanted to be protected by my tooling. So I went back to read the post “[Introducing ReSharper 5.0: Structural Search and Replace](http://blogs.jetbrains.com/dotnet/2010/04/introducing-resharper-50-structural-search-and-replace/)”

After several trial and some help from [Ilya](http://resharper.blogspot.com/) (Thanks!) I finally found the correct way to express what I wanted. My goal was to find all code which uses **FirstOrDefault() **method followed by a call to another method. Exactly like my issue.

> $enumerable$.FirstOrDefault().$method$()
> 
> enumerable is an Expression of type System.Collections.IEnumerable or derived type
> 
> method is a identifier placeholder with an empty indentifier name regexp

![](http://farm2.static.flickr.com/1284/5186669137_d9f4f98471_o.png)

I then verified that this error was found. And also luckily that this was the only error of that kind in our application.

![](http://farm5.static.flickr.com/4151/5187275884_fd57929394_o.png)

Then I finally added it to the [Resharper Pattern Catalog](http://www.jetbrains.com/resharper/webhelp/Reference__Windows__Pattern_Catalogue.html), to show a warning.

![](http://farm2.static.flickr.com/1036/5187278260_3509a8c317_o.png)

Now when ever I will type this stupid thing, I will have my preferred tool [Resharper](http://www.jetbrains.com/resharper/), telling me how stupid I am to even try this!

![](http://farm5.static.flickr.com/4113/5187281640_fc10ec4169_o.png)

BY the way you might download a sample Pattern Catalog from this blog post “[Sample SSR Pattern Catalog Available for Download](http://blogs.jetbrains.com/dotnet/2010/06/sample-ssr-pattern-catalog-available-for-download/)”. There are some cool stuff in there and it might help writing your owns.
