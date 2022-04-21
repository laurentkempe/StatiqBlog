---
title: "Webpart portal personalization"
permalink: /2004/08/04/Webpart-portal-personalization/
date: 8/4/2004 7:52:00 AM
updated: 8/4/2004 7:52:00 AM
disqusIdentifier: 20040804075200
tags: ["ASP.NET 2.0"]
alias:
 - /post/Webpart-portal-personalization.aspx/index.html
---
<P>In some portal scenarios you might want to have only webmaster(s) manage the layout of the site. So your goal is  to avoid that a registered user change it.</P>
<P>With Webparts you might build portal that match this scenario like this:</P>
<P>Add to you web.config:</P>
<P><system.web><BR>    ...</P>
<P>    <webParts><BR>        <personalization><BR>            <authorization><BR>                <allow roles="webmasters" verbs="enterSharedScope"/><BR>            </authorization><BR>        </personalization><BR>    </webParts><BR>    ...</P>
<P></system.web></P>
<P>This configuration set that only users with the role webmaster might change the layout.</P>
<P></P>
<P>Then you might place a WebPartPageMenu control on your site. This control is listing all the personalisation to the webpart system you might do as a logged in user. In the case of a user in the role of webmasters, the WebPartPageMenu will have one of it entries set to: Show Share view.<BR>You can select this entry and then Design Page Layout to be able to design the layout of the page for the user the anonymous users.</P>
