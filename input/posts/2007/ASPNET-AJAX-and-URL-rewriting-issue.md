---
title: "ASP.NET AJAX and URL rewriting issue"
permalink: /2007/08/04/ASPNET-AJAX-and-URL-rewriting-issue/
date: 8/4/2007 8:29:46 AM
updated: 8/4/2007 8:29:46 AM
disqusIdentifier: 20070804082946
tags: ["ASP.NET 2.0", "ASP.NET AJAX"]
alias:
 - /post/ASPNET-AJAX-and-URL-rewriting-issue.aspx/index.html
---
If you are using URL rewriting you might know that you have to take care about the way you reference resources has written in the Scott Guthrie post; [Tip/Trick: Url Rewriting with ASP.NET](http://weblogs.asp.net/scottgu/archive/2007/02/26/tip-trick-url-rewriting-with-asp-net.aspx):

> ###### **Handling CSS and Image Reference Correctly**
<!-- more -->
> 
> One gotcha that people sometime run into when using Url Rewriting for the very first time is that they find that their image and CSS stylesheet references sometimes seem to stop working.  This is because they have relative references to these files within their HTML pages - and when you start to re-write URLs within an application you need to be aware that the browser will often be requesting files in different logical hierarchy levels than what is really stored on the server.
> 
> For example, if our /products.aspx page above had a relative reference to "logo.jpg" in the .aspx page, but was requested via the /products/books.aspx url, then the browser will send a request for /products/logo.jpg instead of /logo.jpg when it renders the page.  To reference this file correctly, make sure you root qualify CSS and Image references ("/style.css" instead of "style.css").  For ASP.NET controls, you can also use the ~ syntax to reference files from the root of the application (for example: <asp:image imageurl="~/images/logo.jpg" runat="server"/>

This is for sure also the case for javascript. 

I am using the Request.PathInfo way described in Scott's post to rewrite one url on [Tech Head Brothers](http://www.techheadbrothers.com/). Everything works fine except that Sys.Services.AuthenticationService get confused about the rewriting of the URL and tries to post back on : 

http://localhost:8080/Auteurs.aspx/laurent-kempe/Authentication_JSON_AppService.axd/Login 

When I expect  

http://localhost:8080/Authentication_JSON_AppService.axd/Login 

Looking at the page rendered by ASP.NET I see that the following is rendered:

<script type="text/javascript">  
<!--  
Sys.Services._AuthenticationService.DefaultWebServicePath = 'Authentication_JSON_AppService.axd';  
// -->  
</script>

So I am clearly missing a / in the path and due to that the URL rewriting confuse the post to the server.

The first solution was found by [Cyril Durand](http://blogs.codes-sources.com/cyril/) (always of good help in this AJAX world ;) and is to add this line of code:

<span style="color: rgb(43,145,175)">ScriptManager</span>.GetCurrent(Page).AuthenticationService.Path = <span style="color: rgb(163,21,21)">"/Authentication_JSON_AppService.axd"</span>;
[](http://11011.net/software/vspaste)


But I did it a bit differently, directly in the javascript adding the following line:

Sys.Services.AuthenticationService.set_path(<span style="color: rgb(163,21,21)">'/Authentication_JSON_AppService.axd'</span>);
[](http://11011.net/software/vspaste)


Btw this javascript line would be generated at rendering time by the solution of Cyril.


Thanks Cyril for the always nice talks.
