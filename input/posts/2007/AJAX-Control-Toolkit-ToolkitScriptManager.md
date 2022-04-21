---
title: "AJAX Control Toolkit - ToolkitScriptManager"
permalink: /2007/06/09/AJAX-Control-Toolkit-ToolkitScriptManager/
date: 6/9/2007 6:24:41 AM
updated: 6/9/2007 6:24:41 AM
disqusIdentifier: 20070609062441
tags: ["ASP.NET AJAX"]
alias:
 - /post/AJAX-Control-Toolkit-ToolkitScriptManager.aspx/index.html
---
A new release (10606) is out, that you might [download on CodePlex](http://www.codeplex.com/AtlasControlToolkit).

I quickly tried out the script combining stuff and it works really great. I saved 7 round trip to the server and almost 2 seconds in the page load. You might read more about it on [Shawn Burke](http://blogs.msdn.com/sburke/archive/2007/06/07/updated-toolkit-release-now-available.aspx)
<!-- more -->

> **Script Combining**.  With the Toolkit, we made the decision to have components be responsible for their own scripts, as opposed to having one mondo-script file with all the script for the whole Toolkit.  This was the right call, as it allows adding to the Toolkit without having everyone pay a penalty for the added items.  However, for components with deep dependancies, this could add up to a lot of individual script files which caused multiple requests back to the server - a problem on slower connections.  We've not got functionality such that all of the scripts needed for all of the Toolkit components on the page will be dynamically combined into just one script file and sent down that way.

and [Scott Guthrie](http://weblogs.asp.net/scottgu/archive/2007/06/08/new-asp-net-ajax-control-toolkit-release.aspx) blogs.

> One of the biggest improvements with this toolkit release is support for a new "ToolkitScriptCombiner" control.  This control allows you to replace the default <asp:scriptmanager> control behavior, and supports the ability to dynamically merge multiple client-side Javascript scripts into a single file that is downloaded to the client at runtime.  Better yet, only the Javascript needed by the specific controls on the page are included within the combined download, to make it as small as possible.

And this just by doing this:

        <span style="background: rgb(255,238,98)"><%<span style="color: rgb(0,128,0)"></span>--<asp:ScriptManager ID="scriptManager" runat="server"/>--</span><span style="background: rgb(255,238,98)">%>
</span>        <span style="color: rgb(0,0,255)"><</span><span style="color: rgb(163,21,21)">ajaxToolkit</span><span style="color: rgb(0,0,255)">:</span><span style="color: rgb(163,21,21)">ToolkitScriptManager</span> <span style="color: rgb(255,0,0)">runat</span><span style="color: rgb(0,0,255)">="Server"</span> <span style="color: rgb(255,0,0)">ID</span><span style="color: rgb(0,0,255)">="scriptManager"</span> <span style="color: rgb(0,0,255)">/>
</span>
[](http://11011.net/software/vspaste)
