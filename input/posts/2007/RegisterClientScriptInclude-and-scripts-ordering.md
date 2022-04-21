---
title: "RegisterClientScriptInclude and scripts ordering"
permalink: /2007/05/25/RegisterClientScriptInclude-and-scripts-ordering/
date: 5/25/2007 8:04:38 AM
updated: 5/25/2007 8:04:38 AM
disqusIdentifier: 20070525080438
tags: ["ASP.NET AJAX"]
alias:
 - /post/RegisterClientScriptInclude-and-scripts-ordering.aspx/index.html
---
I spend a part of this evening fighting with *ScriptManager.RegisterClientScriptInclude *and had no chance to use it in a way that my javascripts would appear after MicrosoftAjax.js and in an order that I defined.

I finally found a solution, using ScriptManagerProxy and declaratively listing the javascripts in the order I wanted.<scripts>
