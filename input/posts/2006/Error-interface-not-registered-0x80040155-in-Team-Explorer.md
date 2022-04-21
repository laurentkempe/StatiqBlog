---
title: "Error interface not registered 0x80040155 in Team Explorer"
permalink: /2006/10/11/Error-interface-not-registered-0x80040155-in-Team-Explorer/
date: 10/11/2006 3:14:50 AM
updated: 10/11/2006 3:14:50 AM
disqusIdentifier: 20061011031450
tags: ["Team System", "Visual Studio"]
alias:
 - /post/Error-interface-not-registered-0x80040155-in-Team-Explorer.aspx/index.html
---
Tonight I had the following issue; right clicking on a Team Query in Team Explorer and choosing '*Open in Microsoft Excel*' gave the following error:

> interface not registered 0x80040155
<!-- more -->

The solution I found was to run: **C:\Program Files\Microsoft Office\OFFICE11>EXCEL.EXE /regserver**

Now I can again open my tasks in Excel.
