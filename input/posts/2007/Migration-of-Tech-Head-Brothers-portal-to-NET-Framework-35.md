---
title: "Migration of Tech Head Brothers portal to .NET Framework 3.5"
permalink: /2007/11/28/Migration-of-Tech-Head-Brothers-portal-to-NET-Framework-35/
date: 11/28/2007 9:01:39 PM
updated: 11/28/2007 9:01:39 PM
disqusIdentifier: 20071128090139
alias:
 - /post/Migration-of-Tech-Head-Brothers-portal-to-NET-Framework-35.aspx/index.html
---
I started yesterday the migration of [Tech Head Brothers](http://www.techheadbrothers.com/) portal to .NET Framework 3.5 and Visual Studio 2008.

The automatic migration of the solution went straight without any error. During the process I was asked if I want to now target .NET Framework 3.5, replying yes seems not to have changed all things needed, for example in the solution properties I had to do the change manually, but it seems that the web.config was updated correctly.
<!-- more -->

Being ready to see the application running I started a compilation and got my first issue, and finally the only one.

It seems that the compiler is making more check than the older one. 

On my data access layer I use [Euss](http://euss.evaluant.com/), "an extensible programming model and runtime components for building data aware solutions on the .Net platform" . So I rely on that framework on my data access layer and for sure I reference it.

Compiling the business layer I got the following compilation error:

![](http://farm3.static.flickr.com/2162/2070512203_85b8bbec0d_o.jpg) 

How can it be:

1.  It compiles on Visual Studio 2005 without any error  The business layer doesn't have a reference to Euss for sure, it is one of the goal of my DAL to encapsulate it 

The link to the error didn't really helped because the code was absolutely correct!

After some research I found the issue in the data access layer, in which I have a generic Repository class for all CRUD operations that I parameterize with a business entity of my domain. The code look like this: 

<span style="color: rgb(0,0,255)">namespace</span> TechHeadBrothers.Portal.DAL  
{  
    <span style="color: rgb(0,0,255)">public</span> <span style="color: rgb(0,0,255)">class</span> <span style="color: rgb(43,145,175)">Repository</span><T>  
        <span style="color: rgb(0,0,255)">where</span> T : <span style="color: rgb(0,0,255)">class  
</span>    {  

        <span style="color: rgb(128,128,128)">///</span><span style="color: rgb(0,128,0)"> </span><span style="color: rgb(128,128,128)"><summary>
</span>        <span style="color: rgb(128,128,128)">///</span><span style="color: rgb(0,128,0)"> Reads from the specified query.
</span>        <span style="color: rgb(128,128,128)">///</span><span style="color: rgb(0,128,0)"> </span><span style="color: rgb(128,128,128)"></summary>
</span>        <span style="color: rgb(128,128,128)">///</span><span style="color: rgb(0,128,0)"> </span><span style="color: rgb(128,128,128)"><param name="query"></span><span style="color: rgb(0,128,0)">The query.</span><span style="color: rgb(128,128,128)"></param>
</span>        <span style="color: rgb(128,128,128)">///</span><span style="color: rgb(0,128,0)"> </span><span style="color: rgb(128,128,128)"><returns></returns>
</span>        <span style="color: rgb(0,0,255)">protected</span> <span style="color: rgb(0,0,255)">static</span> <span style="color: rgb(43,145,175)">IList</span><T> Read(<span style="color: rgb(43,145,175)">Query</span> query)
        {
            <span style="color: rgb(0,0,255)">return</span> <span style="color: rgb(43,145,175)">PersistenceFactory</span>.Context.Load<T>(query);
        }
[](http://11011.net/software/vspaste)


The issue is that in one method, one of the Read operation, I use the Query class. This class is coming from Euss framework. As Query is a parameter of a method in a public class and this method is marked as protected, it might be inherited in another layer and override. Then the issue is that Query parameter in this other layer is not known because we want to have the Euss framework encapsulated on the data access layer. So we got the error.

<u>Update</u>: The fix is as following:

        <span style="color: rgb(0,0,255)">internal</span> <span style="color: rgb(0,0,255)">static</span> <span style="color: rgb(43,145,175)">IList</span><T> Read(<span style="color: rgb(43,145,175)">Query</span> query)
        {
            <span style="color: rgb(0,0,255)">return</span> <span style="color: rgb(43,145,175)">PersistenceFactory</span>.Context.Load<T>(query);
        }

Weird thing is that the same thing works without any issue on Visual Studio 2005.

So now I can't wait to test some ideas, like "[Business entity and C# extension methods](http://weblogs.asp.net/lkempe/archive/2007/10/31/business-entity-and-c-extension-methods.aspx)".

I am still currently missing VS Web Deployment Add-In, but it was announced by [Scott Guthrie](http://weblogs.asp.net/scottgu/default.aspx) on his blog :

> ##### <u>Silverlight Tools and VS Web Deployment Project Add-Ins</u>
> 
> Two popular add-ins to Visual Studio are not yet available to download for the final VS 2008 release.  These are the Silverlight 1.1 Tools Alpha for Visual Studio and the Web Deployment Project add-in for Visual Studio.  Our hope is to post updates to both of them to work with the final VS 2008 release in the next two weeks.  If you are doing Silverlight 1.1 development using VS 2008 Beta2 you'll want to stick with with VS 2008 Beta2 until this updated Silverlight Tools Add-In is available. 
