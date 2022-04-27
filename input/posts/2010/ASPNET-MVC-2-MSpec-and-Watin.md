---
title: "ASP.NET MVC 2, MSpec and Watin"
permalink: /2010/05/19/ASPNET-MVC-2-MSpec-and-Watin/
date: 5/19/2010 5:07:53 PM
updated: 10/15/2010 11:20:48 AM
disqusIdentifier: 20100519050753
tags: ["Watin", "MSpec", "Jobping"]
---
The other day I posted about “[Automated functional tests using Watin and MSpec](http://www.laurentkempe.com/post/Automated-functional-tests-using-Watin-and-MSpec.aspx)” which we do at [Jobping](http://www.jobping.com) as a spike to automate our functional tests on our ASP.NET MVC 2 site.

Yesterday evening I was facing an issue in my base class **WebBaseSpec** which led to really strange side effects. Basically when I was running one unit test alone it was Green, running all or more than one unit test will fail miserably with the well known STA issue of [Watin](http://watin.sourceforge.net/).
<!-- more -->

So I thought that I had an issue with the ReSharper MSpec plugin but after some discussion with [Alexander Groß](http://therightstuff.de/)I realized that the second failing test was showing another issue than the STA issue.

Going further I realized that when I was checking the following


```csharp
It should_direct_user_to_aboutus_page = () =>
    Browser.Uri.Route().ShouldMapTo<HomeController>(x => x.About());
```

First I needed to call the ASP.NET MVC RegisterRoutes

```csharp
MvcApplication.RegisterRoutes(RouteTable.Routes);
```

which was done in the constructor of my **WebBaseSpec** class.

```csharp
protected WebBaseSpec()
{
    MvcApplication.RegisterRoutes(RouteTable.Routes);
    InitBrowser();
}
```

That’s was the problem, I was registering the routes several time, one time per test. So first one was ok, second one was failing…

So I modified it to the following, ensuring that the routes were registered only one time!

```csharp
private static bool registered;

/// <summary>
/// Initializes a new instance of the <see cref="WebBaseSpec"/> class.
/// </summary>
protected WebBaseSpec()
{
    if (!registered)
    {
        MvcApplication.RegisterRoutes(RouteTable.Routes);
        registered = true;
    }
    InitBrowser();
}
```

Now I can run all my functional tests again

![ASP.NET MVC 2, MSpec and Watin](https://farm2.staticflickr.com/1647/24523990132_7413049f9a_o.png)
