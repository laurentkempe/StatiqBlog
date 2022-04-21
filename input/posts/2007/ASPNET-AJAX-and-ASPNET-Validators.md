---
title: "ASP.NET AJAX and ASP.NET Validators"
permalink: /2007/03/27/ASPNET-AJAX-and-ASPNET-Validators/
date: 3/27/2007 6:30:01 AM
updated: 3/27/2007 6:30:01 AM
disqusIdentifier: 20070327063001
tags: ["ASP.NET 2.0", "ASP.NET AJAX", "ASP.NET"]
alias:
 - /post/ASPNET-AJAX-and-ASPNET-Validators.aspx/index.html
---
If you are using ASP.NET Validators in UpdatePanel consider the post of [Matt Gibbs](http://blogs.msdn.com/mattgi/) : "[ASP.NET AJAX Validators](http://blogs.msdn.com/mattgi/archive/2007/01/23/asp-net-ajax-validators.aspx)".

> ASP.NET AJAX provides new APIs for registering script with the ScriptManager.  Using these APIs allows controls to work well with partial rendering.  Without them, controls placed inside an UpdatePanel won't work as expected. In previous CTP releases of ASP.NET AJAX, we had a set of validator controls that derived from the v2.0 controls and used the new APIs. This made them work well with ASP.NET AJAX. WindowsUpdate will soon include a version of System.Web that can take advantage of the new APIs.  So the new controls which would have been redundant have been removed.  However, the update isn't available yet and ASP.NET AJAX has been released.  So, in the short-term, the source code for a set of custom validator controls that work with partial rendering is available [here](http://blogs.msdn.com/mattgi/attachment/1516974.ashx).
<!-- more -->
> 
> The .zip file includes a solution and .csproj file as well as the compiled DLL.  Just put the DLL in the /bin directory of your application and include the following <tagMapping section in the pages section of the web.config.
> 
>       <tagMapping>
>         <add tagType="System.Web.UI.WebControls.CompareValidator"           mappedTagType="Sample.Web.UI.Compatibility.CompareValidator, Validators, Version=1.0.0.0"/>
>         <add tagType="System.Web.UI.WebControls.CustomValidator"            mappedTagType="Sample.Web.UI.Compatibility.CustomValidator, Validators, Version=1.0.0.0"/>
>         <add tagType="System.Web.UI.WebControls.RangeValidator"             mappedTagType="Sample.Web.UI.Compatibility.RangeValidator, Validators, Version=1.0.0.0"/>
>         <add tagType="System.Web.UI.WebControls.RegularExpressionValidator" mappedTagType="Sample.Web.UI.Compatibility.RegularExpressionValidator, Validators, Version=1.0.0.0"/>
>         <add tagType="System.Web.UI.WebControls.RequiredFieldValidator"     mappedTagType="Sample.Web.UI.Compatibility.RequiredFieldValidator, Validators, Version=1.0.0.0"/>
>         <add tagType="System.Web.UI.WebControls.ValidationSummary"          mappedTagType="Sample.Web.UI.Compatibility.ValidationSummary, Validators, Version=1.0.0.0"/>
>       </tagMapping>
