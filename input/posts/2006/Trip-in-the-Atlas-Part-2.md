---
title: "Trip in the Atlas - Part 2"
permalink: /2006/06/13/Trip-in-the-Atlas-Part-2/
date: 6/13/2006 6:38:00 AM
updated: 6/13/2006 6:38:00 AM
disqusIdentifier: 20060613063800
tags: ["Tech Head Brothers", "ASP.NET 2.0", "ASP.NET AJAX"]
---
This time the targeted scenario was to implement a login the way I saw it on [on10.net](http://www.on10.net/).

Using the [Atlas Control Toolkit](http://atlas.asp.net/default.aspx?tabid=47&subtabid=477), I started to implement the scenario with a [ModalPopup](http://atlas.asp.net/atlastoolkit/ModalPopup/ModalPopup.aspx) atlas control surrounding an ASP.NET Login.
<!-- more -->

After some drag and drop on the designer I hit the F5, and I was in front of my Modal login. Nice, but no postback, so no login. Tehn I came with the idea to add the property UseSubmitBehavoir to false for OK/Cancel button like so:

```html
<asp:Button ID=”OkButton”
            runat=”server”
            Text=”OK”
            OnClick=”OkButton_Click”
            UseSubmitBehavior=false>
</asp:Button>
```

I had then my postback, but all controls in my login template were empty. After spending some time on that without success I gave up.

But yesterday evening I came back to this and downloaded the latest bits of the [Atlas Control Toolkit from CodePlex](http://www.codeplex.com/Wiki/View.aspx?ProjectName=AtlasControlToolkit). By the way [Korby](http://blogs.msdn.com/korbyp/), I can't wait getting my projects there ;-) And restarted the implementation using the demo web application they deliver with it. And there it worked fine. 

```html
<asp:Panel ID=”LoginPanel” runat=”server” CssClass=”modalPopup” Style=”display: none”>
   <asp:Login ID=”Login1” runat=”server” LoginButtonText=”S’identifier”
              PasswordLabelText=”Mot de passe:”
              PasswordRequiredErrorMessage=”Le mot de passe est requis.”
              RememberMeText=”Se souvenir de moi.”
              TitleText=”Identifiez vous “
              UserNameLabelText=”Nom d’utilisateur:”
              UserNameRequiredErrorMessage=”Le nom d’utilisateur est requis.”>
   </asp:Login>
   <asp:Button ID=”CancelButton” runat=”server” Text=”Cancel”></asp:Button></asp:Panel>

   <span class=”login”>

      <asp:LinkButton ID=”LinkButton1” runat=”server” Text=”S’identifier”></asp:LinkButton>   |
      <asp:HyperLink ID=”HyperLink1” runat=”server” NavigateUrl=”~/Register.aspx”>
          S’enregistrer
      </asp:HyperLink>
   </span>

<atlasToolkit:ModalPopupExtender ID=”ModalPopupExtender1” runat=”server”>
   <atlasToolkit:ModalPopupProperties TargetControlID=”LinkButton1”
                                      PopupControlID=”LoginPanel”
                                      BackgroundCssClass=”modalBackground”
                                      CancelControlID=”CancelButton” />
</atlasToolkit:ModalPopupExtender>
```

And now with some slight modification to my implementation it is also working fine as you can see:

![atlas login](/images/2006/atlaslogin.jpg)
