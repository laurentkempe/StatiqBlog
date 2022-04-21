---
title: "VisualSvn, TortoiseSvn and relocating a working copy"
permalink: /2010/01/28/VisualSvn-TortoiseSvn-and-relocating-a-working-copy/
date: 1/28/2010 8:04:19 PM
updated: 5/7/2010 7:53:27 AM
disqusIdentifier: 20100128080419
tags: ["VisualSVN"]
alias:
 - /post/VisualSvn-TortoiseSvn-and-relocating-a-working-copy.aspx/index.html
---
Whenever you have to relocate your svn working copy because the svn server url or protocol as changed, you need to use [TortoiseSvn](http://tortoisesvn.net/) [relocate](http://tortoisesvn.net/docs/release/TortoiseSVN_en/tsvn-dug-relocate.html). If, like me, you use [VisualSvn](http://www.visualsvn.com/visualsvn/) plugin then you would need to quit Visual Studio and come back to Windows Explorer and [TortoiseSvn](http://tortoisesvn.net/), right click your project folder then find Relocate command:

<!-- more -->
![](/images/4311387612_f21c311484_o1_431CA1D4.png)

Type in the dialog which open the new url of the svn repository, then click Ok.

TortoiseSvn will then do it works and the next time you open Visual Studio you will have your working copy pointing to the new server.

Read the [documentation on the following page](http://tortoisesvn.net/docs/release/TortoiseSVN_en/tsvn-dug-relocate.html):

> If your repository has for some reason changed it's location (IP/URL). Maybe you're even stuck and can't commit and you don't want to checkout your working copy again from the new location and to move all your changed data back into the new working copy, TortoiseSVN → Relocate is the command you are looking for. It basically does very little: it scans all `entries` files in the `.svn` folder and changes the URL of the entries to the new value.
> 
> You may be surprised to find that TortoiseSVN contacts the repository as part of this operation. All it is doing is performing some simple checks to make sure that the new URL really does refer to the same repository as the existing working copy.
> 
> ##### Warning
> 
> *This is a very infrequently used operation*. The relocate command is*only* used if the URL of the repository root has changed. Possible reasons are:
> 
> * The IP address of the server has changed.
> * The protocol has changed (e.g. http:// to https://).
> * The repository root path in the server setup has changed.
> 
> Put another way, you need to relocate when your working copy is referring to the same location in the same repository, but the repository itself has moved.
> 
> It does not apply if:
> 
> * You want to move to a different Subversion repository. In that case you should perform a clean checkout from the new repository location.
> * You want to switch to a different branch or directory within the same repository. To do that you should use TortoiseSVN →Switch.... Read [the section called “To Checkout or to Switch...”](http://tortoisesvn.net/tsvn-dug-branchtag.html#tsvn-dug-switch-1) for more information.
> 
> If you use relocate in either of the cases above, it *will corrupt your working copy* and you will get many unexplainable error messages while updating, committing, etc. Once that has happened, the only fix is a fresh checkout.
