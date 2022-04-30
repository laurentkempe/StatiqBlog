---
title: "Webpart portal personalization"
permalink: /2004/08/04/Webpart-portal-personalization/
date: 8/4/2004 7:52:00 AM
updated: 8/4/2004 7:52:00 AM
disqusIdentifier: 20040804075200
tags: ["ASP.NET 2.0"]
---
In some portal scenarios you might want to have only webmaster(s) manage the layout of the site. So your goal is to avoid that a registered user change it.
With Webparts you might build portal that match this scenario like this:
Add to you web.config:

```xml
<system.web>
    ...
    <webParts>
        <personalization>
            <authorization>
                <allow roles="webmasters" verbs="enterSharedScope"/>
            </authorization>
        </personalization>
    </webParts>
    ...
</system.web>
```

This configuration set that only users with the role webmaster might change the layout.

Then you might place a WebPartPageMenu control on your site. This control is listing all the personalisation to the webpart system you might do as a logged in user. In the case of a user in the role of webmasters, the WebPartPageMenu will have one of it entries set to: Show Share view.
You can select this entry and then Design Page Layout to be able to design the layout of the page for the user the anonymous users.