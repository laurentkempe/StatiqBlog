---
title: "CSS Adapters issues with Login controls"
permalink: /2007/05/05/CSS-Adapters-issues-with-Login-controls/
date: 5/5/2007 6:45:12 AM
updated: 5/5/2007 6:45:12 AM
disqusIdentifier: 20070505064512
tags: ["ASP.NET 2.0", "ASP.NET"]
alias:
 - /post/CSS-Adapters-issues-with-Login-controls.aspx/index.html
---
If you are using [ASP.NET 2.0 CSS Friendly Control Adapters 1.0](http://www.asp.net/cssadapters/) with the Login controls of ASP.NET 2.0 you might have experienced some issue like multiple postback when using Internet Explorer. You might get a [GREAT fix and explanation of the issue](http://forums.asp.net/ShowPost.aspx?PostID=1676119) from [Tana Isaac](http://forums.asp.net/members/ticanaer.aspx) of Wellington, New Zealand.

> **Double Postback Problem - Cause** (skip this if you just want the fix!):
<!-- more -->
> 
> Buttons that reside within the controls that are *adapted *by the CSS Control Adapters, for example the CreateUserWizard.CreateUserButton, are rendered out differently depending on the button type (which is set for example via CreateUserWizard.CreateUserButtonType = ButtonType.Link). The default button type used by the membership controls is Button. The following html controls are rendered out for the different System.Web.UI.WebControls.ButtonType enum values:
> 
> ButtonType.Button: input, type=submit
> ButtonType.Image: input, type=image
> ButtonType.Link: anchor
> 
> Both of the input controls will automatically cause the form that they reside within to be posted back to the server when they are clicked, whereas the anchor will not - instead it needs some javascript to cause a postback. This is where the problem is - all three html controls are rendered out with javascript attached to post the form back to the server on a click event, which allows buttons of type 'Link' to work correctly but causes buttons of type 'Button' and 'Image' to postback twice - the first time due to the javascript and the second because of the native postback.
> 
> The javascript method used to cause the postback is as follows:
> 
> WebForm_DoPostBackWithOptions(WebForm_PostBackOptions(eventTarget, eventArgument, validation, validationGroup, actionUrl, trackFocus, clientSubmit))
> 
> In order to stop 'Button' buttons and 'Image' buttons firing twice we just need to set the clientSubmit parameter to false when these types of buttons are rendered out.
> 
> **Other problems (specific to the CreateUserWizardAdapter control)**
> 
> Once the double postback problem was fixed two other problems popped up. The first was that users still weren't being created. This was because the id and name (which is derived from the id) being used for the create user button was missing an underscore.
> 
> The other problem was that the cancel button didn't work. It was also missing an underscore from its name and also wasn't registered for Event Validation.
> 
> [Read more...](http://forums.asp.net/ShowPost.aspx?PostID=1676119)

**Great work Tana**, **Thanks** for sharing you saved me some time.

<u>Update</u>:** **The CSS Adapters project is now hosted on Codeplex, [http://www.codeplex.com/cssfriendly](http://www.codeplex.com/cssfriendly "http://www.codeplex.com/cssfriendly").
