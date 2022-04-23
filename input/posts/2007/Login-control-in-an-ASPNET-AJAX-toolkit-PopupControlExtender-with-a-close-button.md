---
title: "Login control in an ASP.NET AJAX toolkit PopupControlExtender with a close button"
permalink: /2007/01/28/Login-control-in-an-ASPNET-AJAX-toolkit-PopupControlExtender-with-a-close-button/
date: 1/28/2007 8:27:58 AM
updated: 5/7/2010 7:51:53 AM
disqusIdentifier: 20070128082758
tags: ["Tech Head Brothers", "ASP.NET AJAX", "ASP.NET"]
---
I remember when I tried to [implement a Login control in a ModalPopup](http://weblogs.asp.net/lkempe/archive/2006/06/12/Trip-in-the-Atlas---Part-2.aspx) with one of the early release of what was called at that time Atlas and is now called ASP.NET AJAX. I had lots of difficulties and now it works like a charm.

This time I would like to have a Login control in an ASP.NET AJAX [PopupControlExtender](http://ajax.asp.net/ajaxtoolkit/PopupControl/PopupControl.aspx) from the [ASP.NET AJAX Control Toolkit](http://ajax.asp.net/ajaxtoolkit).
<!-- more -->

```html
<asp:panel id=”loginPanel” style=”display: none” runat=”server”>
  <asp:login id=”LoginCtrl” runat=”server”
             cssselectorclass=”THBLogin”
             failuretext=”Identifiant incorrect ! Essayez à nouveau…”
             loginbuttontext=”S’identifier”
             passwordlabeltext=”Mot de Passe”
             passwordrequirederrormessage=”Le mot de passe est requis.”
             remembermetext=”Se souvenir de moi la prochaine fois.”
             titletext=”S’identifier”
             usernamelabeltext=”Email”
             usernamerequirederrormessage=”L’email est requis.”
             createusertext=”S’enregistrer”
             createuserurl=”/Register.aspx”
             passwordrecoverytext=”Mot de passe oublié ?”
             passwordrecoveryurl=”/PasswordRecovery.aspx”>
          <ajaxtoolkit:popupcontrolextender id=”PopEx” runat=”server”
                                            targetcontrolid=”loginHyperLink”
                                            popupcontrolid=”loginPanel”
                                            position=”Left”>The implementation is really straight, a panel with a asp:Login in it:
                  <asp:Panel ID=”loginPanel” runat=”server” Style=”display: none”>
                          <asp:Login ID=”LoginCtrl” runat=”server”
                                     CssSelectorClass=”THBLogin”
                                     FailureText=”Identifiant incorrect ! Essayez ? nouveau…”
                                     LoginButtonText=”S’identifier”
                                     PasswordLabelText=”Mot de Passe”
                                     PasswordRequiredErrorMessage=”Le mot de passe est requis.”
                                     RememberMeText=”Se souvenir de moi la prochaine fois.”
                                     TitleText=”S’identifier”
                                     UserNameLabelText=”Email”
                                     UserNameRequiredErrorMessage=”L’email est requis.”
                                     CreateUserText=”S’enregistrer”
                                     CreateUserUrl=”/Register.aspx”
                                     PasswordRecoveryText=”Mot de passe oubli? ?”
                                     PasswordRecoveryUrl=”/PasswordRecovery.aspx” />
                  </asp:Panel>
```

Then you had the PopupControlExtender:

```html
<ajaxtoolkit:PopupControlExtender ID=”PopEx” runat=”server”
                                             targetcontrolid=”loginHyperLink”
                                             popupcontrolid=”loginPanel”
                                             position=”Left”>
```

I also need a target control that will initiate the Popup display:

```html
<asp:HyperLink ID=”loginHyperLink” runat=”server”>S’identifier</asp:HyperLink>
```

Till now nothing really special.

Then I wanted to add a close button to this Popup, so I a added a div closeLoginPanel with an embedded link:

```html
<asp:Panel ID=”loginPanel” runat=”server” Style=”display: none”>
  <div class=”closeLoginPanel”>
    <a title=”Fermer”>X</a>
  </div>
<asp:Login ID=”LoginCtrl” runat=”server”
```

This is not enough because I need to have the close action started when a user click on the link (X). I first looked at the javascript of the PopupControlExtender and saw that it handles the onclik of the body element so I added 

```html
<a onclick=”document.body.click(); return false;” title=”Fermer”>X</a>
```

This was working fine on Internet Explorer 7 but was raising an error on FireFox 2. Looking in more detail in the javascript I finally changed my code to:


```html
<a onclick=”AjaxControlToolkit.PopupControlBehavior.__VisiblePopup.hidePopup(); return false;” title=”Fermer”>X</a>
```

This is working on both Internet Explorer and FireFox 2.

Here is the css I used:


```css
.closeLoginPanel
{
    font-family: Verdana, Helvetica, Arial, sans-serif;
    line-height: 17px;
    font-size: 11px;
    font-weight: bold;

    position: absolute;
    top:8px;
    right: 10px;
}

.closeLoginPanel a
{
    background-color: #6699CC;
    cursor: pointer;
    color: #FFFFFF;
    text-align: center;
    text-decoration: none;
    padding: 5px;
}
```
Here is the result:

![](/images/2007/ajax_login_popup.gif)
