---
title: "Login control in an ASP.NET AJAX toolkit PopupControlExtender with a close button"
permalink: /2007/01/28/Login-control-in-an-ASPNET-AJAX-toolkit-PopupControlExtender-with-a-close-button/
date: 1/28/2007 8:27:58 AM
updated: 5/7/2010 7:51:53 AM
disqusIdentifier: 20070128082758
tags: ["Tech Head Brothers", "ASP.NET AJAX", "ASP.NET"]
alias:
 - /post/Login-control-in-an-ASPNET-AJAX-toolkit-PopupControlExtender-with-a-close-button.aspx/index.html
---
I remember when I tried to [implement a Login control in a ModalPopup](http://weblogs.asp.net/lkempe/archive/2006/06/12/Trip-in-the-Atlas---Part-2.aspx) with one of the early release of what was called at that time Atlas and is now called ASP.NET AJAX. I had lots of difficulties and now it works like a charm.

This time I would like to have a Login control in an ASP.NET AJAX [PopupControlExtender](http://ajax.asp.net/ajaxtoolkit/PopupControl/PopupControl.aspx) from the [ASP.NET AJAX Control Toolkit](http://ajax.asp.net/ajaxtoolkit).
<!-- more -->

<?xml:namespace prefix="asp"?><asp:panel id="loginPanel" style="display: none" runat="server"><asp:login id="LoginCtrl" runat="server" cssselectorclass="THBLogin" failuretext="Identifiant incorrect ! Essayez à nouveau..." loginbuttontext="S'identifier" passwordlabeltext="Mot de Passe" passwordrequirederrormessage="Le mot de passe est requis." remembermetext="Se souvenir de moi la prochaine fois." titletext="S'identifier" usernamelabeltext="Email" usernamerequirederrormessage="L'email est requis." createusertext="S'enregistrer" createuserurl="/Register.aspx" passwordrecoverytext="Mot de passe oublié ?" passwordrecoveryurl="/PasswordRecovery.aspx"></asp:login></asp:panel><?xml:namespace prefix="ajaxToolkit"?><ajaxtoolkit:popupcontrolextender id="PopEx" runat="server" targetcontrolid="loginHyperLink" popupcontrolid="loginPanel" position="Left"></ajaxtoolkit:popupcontrolextender>The implementation is really straight, a panel with a asp:Login in it:

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">asp</span><span style="color: rgb(0,0,255)">:</span><span style="color: rgb(163,21,21)">Panel</span> <span style="color: rgb(255,0,0)">ID</span><span style="color: rgb(0,0,255)">="loginPanel"</span> <span style="color: rgb(255,0,0)">runat</span><span style="color: rgb(0,0,255)">="server"</span> <span style="color: rgb(255,0,0)">Style</span><span style="color: rgb(0,0,255)">="display: none">
</span>    <span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">asp</span><span style="color: rgb(0,0,255)">:</span><span style="color: rgb(163,21,21)">Login</span> <span style="color: rgb(255,0,0)">ID</span><span style="color: rgb(0,0,255)">="LoginCtrl"</span> <span style="color: rgb(255,0,0)">runat</span><span style="color: rgb(0,0,255)">="server"</span> 
        <span style="color: rgb(255,0,0)">CssSelectorClass</span><span style="color: rgb(0,0,255)">="THBLogin"
</span>        <span style="color: rgb(255,0,0)">FailureText</span><span style="color: rgb(0,0,255)">="Identifiant incorrect ! Essayez ? nouveau..."
</span>        <span style="color: rgb(255,0,0)">LoginButtonText</span><span style="color: rgb(0,0,255)">="S'identifier"</span> 
        
        <span style="color: rgb(255,0,0)">PasswordLabelText</span><span style="color: rgb(0,0,255)">="Mot de Passe"</span> 
        <span style="color: rgb(255,0,0)">PasswordRequiredErrorMessage</span><span style="color: rgb(0,0,255)">="Le mot de passe est requis."
</span>        <span style="color: rgb(255,0,0)">RememberMeText</span><span style="color: rgb(0,0,255)">="Se souvenir de moi la prochaine fois."</span> 
        <span style="color: rgb(255,0,0)">TitleText</span><span style="color: rgb(0,0,255)">="S'identifier"
</span>        <span style="color: rgb(255,0,0)">UserNameLabelText</span><span style="color: rgb(0,0,255)">="Email"</span> 
        <span style="color: rgb(255,0,0)">UserNameRequiredErrorMessage</span><span style="color: rgb(0,0,255)">="L'email est requis."</span> 
        <span style="color: rgb(255,0,0)">CreateUserText</span><span style="color: rgb(0,0,255)">="S'enregistrer"</span> 
        <span style="color: rgb(255,0,0)">CreateUserUrl</span><span style="color: rgb(0,0,255)">="/Register.aspx"</span> 
        <span style="color: rgb(255,0,0)">PasswordRecoveryText</span><span style="color: rgb(0,0,255)">="Mot de passe oubli? ?"</span> 
        <span style="color: rgb(255,0,0)">PasswordRecoveryUrl</span><span style="color: rgb(0,0,255)">="/PasswordRecovery.aspx"</span> <span style="color: rgb(0,0,255)">/>
</</span><span style="color: rgb(163,21,21)">asp</span><span style="color: rgb(0,0,255)">:</span><span style="color: rgb(163,21,21)">Panel</span><span style="color: rgb(0,0,255)">></span>

Then you had the PopupControlExtender:

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">ajaxToolkit</span><span style="color: rgb(0,0,255)">:</span><span style="color: rgb(163,21,21)">PopupControlExtender</span> <span style="color: rgb(255,0,0)">ID</span><span style="color: rgb(0,0,255)">="PopEx"</span> <span style="color: rgb(255,0,0)">runat</span><span style="color: rgb(0,0,255)">="server"  
</span>    <span style="color: rgb(255,0,0)">TargetControlID</span><span style="color: rgb(0,0,255)">="loginHyperLink"  
</span>    <span style="color: rgb(255,0,0)">PopupControlID</span><span style="color: rgb(0,0,255)">="loginPanel"</span>              
    <span style="color: rgb(255,0,0)">Position</span><span style="color: rgb(0,0,255)">="Left"</span> <span style="color: rgb(0,0,255)">/></span>
[](http://11011.net/software/vspaste)


I also need a target control that will initiate the Popup display:

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">asp</span><span style="color: rgb(0,0,255)">:</span><span style="color: rgb(163,21,21)">HyperLink</span> <span style="color: rgb(255,0,0)">ID</span><span style="color: rgb(0,0,255)">="loginHyperLink"</span> <span style="color: rgb(255,0,0)">runat</span><span style="color: rgb(0,0,255)">="server"></span>S'identifier<span style="color: rgb(0,0,255)"></</span><span style="color: rgb(163,21,21)">asp</span><span style="color: rgb(0,0,255)">:</span><span style="color: rgb(163,21,21)">HyperLink</span><span style="color: rgb(0,0,255)">></span>

Till now nothing really special.

Then I wanted to add a close button to this Popup, so I a added a div closeLoginPanel with an embedded link:

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">asp</span><span style="color: rgb(0,0,255)">:</span><span style="color: rgb(163,21,21)">Panel</span> <span style="color: rgb(255,0,0)">ID</span><span style="color: rgb(0,0,255)">="loginPanel"</span> <span style="color: rgb(255,0,0)">runat</span><span style="color: rgb(0,0,255)">="server"</span> <span style="color: rgb(255,0,0)">Style</span><span style="color: rgb(0,0,255)">="display: none">
</span>    <span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">div</span> <span style="color: rgb(255,0,0)">class</span><span style="color: rgb(0,0,255)">="closeLoginPanel">
</span>        <span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">a</span> <span style="color: rgb(255,0,0)">title</span><span style="color: rgb(0,0,255)">="Fermer"></span>X<span style="color: rgb(0,0,255)"></</span><span style="color: rgb(163,21,21)">a</span><span style="color: rgb(0,0,255)">>
</span>    <span style="color: rgb(0,0,255)"></</span><span style="color: rgb(163,21,21)">div</span><span style="color: rgb(0,0,255)">>
</span>    <span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">asp</span><span style="color: rgb(0,0,255)">:</span><span style="color: rgb(163,21,21)">Login</span> <span style="color: rgb(255,0,0)">ID</span><span style="color: rgb(0,0,255)">="LoginCtrl"</span> <span style="color: rgb(255,0,0)">runat</span><span style="color: rgb(0,0,255)">="server"</span> 
[](http://11011.net/software/vspaste)


This is not enough because I need to have the close action started when a user click on the link (X). I first looked at the javascript of the PopupControlExtender and saw that it handles the onclik of the body element so I added 

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">a</span> <span style="color: rgb(255,0,0)">onclick</span><span style="color: rgb(0,0,255)">="document.body.click(); return false;"</span> <span style="color: rgb(255,0,0)">title</span><span style="color: rgb(0,0,255)">="Fermer"></span>X<span style="color: rgb(0,0,255)"></</span><span style="color: rgb(163,21,21)">a</span><span style="color: rgb(0,0,255)">></span>
[](http://11011.net/software/vspaste)


This was working fine on Internet Explorer 7 but was raising an error on FireFox 2. Looking in more detail in the javascript I finally changed my code to:

<span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">a</span> <span style="color: rgb(255,0,0)">onclick</span><span style="color: rgb(0,0,255)">="AjaxControlToolkit.PopupControlBehavior.__VisiblePopup.hidePopup(); return false;"</span> <span style="color: rgb(255,0,0)">title</span><span style="color: rgb(0,0,255)">="Fermer"></span>X<span style="color: rgb(0,0,255)"></</span><span style="color: rgb(163,21,21)">a</span><span style="color: rgb(0,0,255)">></span>
[](http://11011.net/software/vspaste)


This is working on both Internet Explorer and FireFox 2.

Here is the css I used:

<span style="color: rgb(163,21,21)">.closeLoginPanel  
</span>{  
    <span style="color: rgb(255,0,0)">font-family</span>: <span style="color: rgb(0,0,255)">Verdana,</span> <span style="color: rgb(0,0,255)">Helvetica,</span> <span style="color: rgb(0,0,255)">Arial,</span> <span style="color: rgb(0,0,255)">sans-serif</span>;  
    <span style="color: rgb(255,0,0)">line-height</span>: <span style="color: rgb(0,0,255)">17px</span>;  
    <span style="color: rgb(255,0,0)">font-size</span>: <span style="color: rgb(0,0,255)">11px</span>;  
    <span style="color: rgb(255,0,0)">font-weight</span>: <span style="color: rgb(0,0,255)">bold</span>;  

    <span style="color: rgb(255,0,0)">position</span>: <span style="color: rgb(0,0,255)">absolute</span>;  
    <span style="color: rgb(255,0,0)">top</span>:<span style="color: rgb(0,0,255)">8px</span>;  
    <span style="color: rgb(255,0,0)">right</span>: <span style="color: rgb(0,0,255)">10px</span>;  
}  

<span style="color: rgb(163,21,21)">.closeLoginPanel</span> <span style="color: rgb(163,21,21)">a  
</span>{  
    <span style="color: rgb(255,0,0)">background-color</span>: <span style="color: rgb(0,0,255)">#6699CC</span>;   
    <span style="color: rgb(255,0,0)">cursor</span>: <span style="color: rgb(0,0,255)">pointer</span>;  
    <span style="color: rgb(255,0,0)">color</span>: <span style="color: rgb(0,0,255)">#FFFFFF</span>;   
    <span style="color: rgb(255,0,0)">text-align</span>: <span style="color: rgb(0,0,255)">center</span>;   
    <span style="color: rgb(255,0,0)">text-decoration</span>: <span style="color: rgb(0,0,255)">none</span>;   
    <span style="color: rgb(255,0,0)">padding</span>: <span style="color: rgb(0,0,255)">5px</span>;  
}
[](http://11011.net/software/vspaste)


Here is the result:

![](http://www.techheadbrothers.com/images/blog/ajax_login_popup.gif)
