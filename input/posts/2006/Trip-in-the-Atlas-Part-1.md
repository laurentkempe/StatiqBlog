---
title: "Trip in the Atlas - Part 1"
permalink: /2006/06/13/Trip-in-the-Atlas-Part-1/
date: 6/13/2006 6:11:54 AM
updated: 6/13/2006 6:11:54 AM
disqusIdentifier: 20060613061154
tags: ["Tech Head Brothers", "ASP.NET 2.0", "ASP.NET AJAX"]
---
After a smooth integration of Atlas in the new version of [Tech Head Brothers](http://www.techheadbrothers.com/) website, I 
decided to go a step further with a project a bit more complex then the scenario 
developed [here](http://weblogs.asp.net/lkempe/archive/2006/04/15/443019.aspx).
<!-- more -->

The new version of the site is totally rewritten in C# and ASP.NET 2 and uses 
the Webparts.

So the new scenario I wanted to implement was a system of connected Webparts, 
one showing an article, a how-to or news and the other the 
comments associated with that. The connection of the webparts was done in 
about 10 minutes, really cool techno.

Then I learned that an Atlas UpdatePanel can't be part of a Webpart because 
then you have the UpdatePanel in a Template of the WebpartZone and that doesn't 
work, so you have to move the UpdatePanel to have it around the Webpartzone. As 
I am working with nested Master Pages I decided to have the UpdatePanel in 
an asp Content control around the WebPartZone like this:


```html
<%@ Page Language="C#" MasterPageFile="~/OneColumn.master" AutoEventWireup="true"
    CodeFile="Articles.aspx.cs" Inherits="Articles" Title="Tech Head Brothers - Articles" %>

<%@ MasterType VirtualPath="~/OneColumn.master" %>

<%@ Register Assembly="Nsquared2.Web"
    Namespace="Nsquared2.Web.UI.WebControls.WebParts"

    TagPrefix="nsquared2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <atlas:UpdatePanel ID="panelarticles" Mode="Conditional" runat="server">
        <ContentTemplate>
            <nsquared2:TemplatedWebPartZone
                    ID="ContentZone"
                    runat="server"
                    ChromeTemplateFile="~/Templates/Chrome/THBOriginalTemplate.chrome"
                    CssClass="webpartzone"
                    Padding="0">
                <ZoneTemplate>
                </ZoneTemplate>
            </nsquared2:TemplatedWebPartZone>
        </ContentTemplate>
    </atlas:UpdatePanel>

</asp:Content>
```

Then in my comments WebPart I implemented a button doing a 
response redirect to another page that will handle the writing of a new comment, 
and this in the UpdatePanel postback not the whole Page postback. To be able to 
access some information from the calling page I had added some information in 
the Session on the first page that I wanted to read on the second one. Boom!! 
Session is empty on the second page. I then removed the UpdatePanel and session 
was back again. hum!!! But that was not the scenario I wanted for my comments, 
so I added again my UpdatePanel and made several tests withtout any success. 


Then searching on the [forum](http://forums.asp.net/default.aspx?GroupID=34) I found why, [here](http://forums.asp.net/thread/1241741.aspx) : "*The 
specific reason it happens is that when ScriptManager detects a Redirect during 
an async postback, it clears the response, which means all cookies are lost. One 
of those cookies is the session ID cookie. This doesn't necessarily affect all 
scenarios, but it does affect some.*" A bug in the CTP april of Atlas.

Then it was clear if the Response is cleared, no chance to get my 
Session values.

As it was not critical data I decided to use QueryString to pass 
on data and that's for sure worked. I will revert to Session when it will be 
fixed.

Now I have a system with connected WebPart, one displaying 
comments using Atlas UpdatePanel to reload only the comments that the suer wnats 
to see. Great stuff!!

There is one thing I don't liked in the WebPart connection is 
that if you have a link button in your page and the OnCommand is set, then when 
the Command is called the WebParts connection is not already done so you have to 
remmeber parameters given to your method and reuse them in OnPreRender.

[ Currently Playing : With My Two Hands - Ben Harper - Diamonds 
On The Inside (04:35) ]
