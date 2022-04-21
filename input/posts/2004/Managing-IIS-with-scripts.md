---
title: "Managing IIS with scripts"
permalink: /2004/07/23/Managing-IIS-with-scripts/
date: 7/23/2004 12:14:00 AM
updated: 7/23/2004 12:14:00 AM
disqusIdentifier: 20040723121400
tags: ["Work", "Infrastructure"]
alias:
 - /post/Managing-IIS-with-scripts.aspx/index.html
---
I needed to create a virtual directory in IIS 6 during the deployment of one of our backend application on a Windows 2003 server. This application is a COM component written in C++ that I developed wrapping a very old VB6 COM component. The whole exposed as a Web Service using the [SOAP Toolkit 3](http://www.microsoft.com/downloads/details.aspx?FamilyId=C943C0DD-CEEC-4088-9753-86F052EC8450&displaylang=en). I already discussed about it [here](http://weblogs.asp.net/lkempe/archive/2003/11/06/36233.aspx).

So I created a script that will register both COM component, by the way regsvr32 is really bad cause it doesn't return different value if it fails. Right now I have no verification in the script that let me know if the registration went well. I plan to add it in a second step by reading the content of the registry using the [reg command](http://www.microsoft.com/resources/documentation/windows/xp/all/proddocs/en-us/reg.mspx). The script is using the <em>SOAPVDIR.CMD</em> packaged with the [SOAP Toolkit 3](http://www.microsoft.com/downloads/details.aspx?FamilyId=C943C0DD-CEEC-4088-9753-86F052EC8450&displaylang=en) to create the Virtual Directory with the soap ISAPI of the [SOAP Toolkit 3](http://www.microsoft.com/downloads/details.aspx?FamilyId=C943C0DD-CEEC-4088-9753-86F052EC8450&displaylang=en):
<!-- more -->

>"c:\Program Files\MSSOAP\Binaries\<strong>SOAPVDIR.CMD</strong>" <strong>CREATE</strong> $VDIR_NAME <em><strong>path</strong></em>

Then I needed to change the user name used for the anonymous access:

>cscript c:\Inetpub\AdminScripts\adsutil.vbs <strong>SET</strong> /W3SVC/1/ROOT/$VDIR_NAME/<strong>AnonymousUserName</strong> <em><strong>myusername</strong></em>

and his password:

>cscript c:\Inetpub\AdminScripts\adsutil.vbs <strong>SET</strong> /W3SVC/1/ROOT/$VDIR_NAME/<strong>AnonymousUserPass</strong> <em><strong>mypassword</strong></em>

At this point I am not that happy about this method cause I have to specify in clear text a password in a script. I have two options. Either the user has to pass the password when running the script, but as it is a script calling this new script and I don't want to change it, I find that I could implement my own command using .NET and the namespace: [System.DirectoryServices](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/iissdk/iis/using_system_directoryservices_to_configure_iis.asp) with such code:

using System;
using System.DirectoryServices;
using System.Reflection;

namespace ADSI1
{
  class ConfigIIS
  {
    [STAThread]
    static void Main(string[] args)
    {
      string serverName = "localhost";
      string password = "<administrative_password>";
      string serverID = "1234";

      CreateNewWebSite(serverName, password, serverID);
      CreateVDir(serverName, password, serverID);
    }

    static void CreateNewWebSite(string serverName, string password, string serverID)
    {
      DirectoryEntry w3svc = new DirectoryEntry ("IIS://" + serverName + "/w3svc",serverName + "\\administrator", password,AuthenticationTypes.Secure);
      DirectoryEntries sites = w3svc.Children;
      DirectoryEntry newSite = sites.Add(serverID,"IIsWebServer"); //create a new site
      newSite.CommitChanges();
    }

    static DirectoryEntry CreateVDir (string vdirname, string serverID)
    {
      DirectoryEntry newvdir;
      DirectoryEntry root=new DirectoryEntry("IIS://localhost/W3SVC/" + serverID + "/Root");
      newvdir=root.Children.Add(vdirname, "IIsWebVirtualDir");
      newvdir.Properties["Path"][0]= "c:\\inetpub\\wwwroot";
      newvdir.Properties["AccessScript"][0] = true;
      newvdir.CommitChanges();
      return newvdir;
    }
  }
}
</administrative_password>

And then I could save the encrypted password in the config file of the tool.

<u>Update</u>: I found some articles about the namespace [System.DirectoryServices](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/iissdk/iis/using_system_directoryservices_to_configure_iis.asp) here:

<ul>
<li>[Introduction to System.DirectoryServices, Part 1](http://www.ondotnet.com/lpt/a/4026)</li>
<li>[Introduction to System.DirectoryServices, Part 2](http://www.ondotnet.com/lpt/a/4027)</li></ul>
