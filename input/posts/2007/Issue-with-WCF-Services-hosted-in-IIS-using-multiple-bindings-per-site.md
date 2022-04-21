---
title: "Issue with WCF Services hosted in IIS using multiple bindings per site"
permalink: /2007/08/02/Issue-with-WCF-Services-hosted-in-IIS-using-multiple-bindings-per-site/
date: 8/2/2007 2:26:24 AM
updated: 8/2/2007 2:26:24 AM
disqusIdentifier: 20070802022624
tags: ["WCF"]
alias:
 - /post/Issue-with-WCF-Services-hosted-in-IIS-using-multiple-bindings-per-site.aspx/index.html
---
I am currently working on the new Tech Head Brothers authoring tool [THBAuthoring](http://www.codeplex.com/THBAuthoring) as you can read in this post '[Migration from WSE 3 to WCF](http://weblogs.asp.net/lkempe/archive/2007/06/27/migration-from-wse-3-to-wcf.aspx)'. Two days ago I went to production with the migration from WSE to WCF. Today I had to post an article about Linq written by [Kader Yildirim](http://www.techheadbrothers.com/Auteurs.aspx?Id=491e538c-0a65-46fc-8c7f-ebf7ee0e56ab) and was quite optimistic that it will work because I followed as always the process of developing on my notebook, then deploying to staging, testing of staging and finally going to production. This time it didn't work. I had a the following error: 

"This collection already contains an address with scheme http. there can be at most one address per scheme in this collection."
<!-- more -->

Weird

I did a comparison using WinMerge of the web.config of staging and production web site, and nothing special in there.

After some research on the configuration of the web site in IIS I realized that the production one was using multiple bindings and the one from staging was not. Removing all bindings except the one defined in the WCF service made it, now my service is working.

Even if it is not a really good solution I can leave with it for the moment. And with some research I found this post from [Ram Poornalingam](http://blogs.msdn.com/rampo/default.aspx) '[Supporting Multiple IIS Bindings Per Site](http://blogs.msdn.com/rampo/archive/2007/06/15/supporting-multiple-iis-bindings-per-site.aspx)' that I have not tried right now but will asap.
