---
title: "ReSharper and Source Analysis Tools for C#"
permalink: /2008/06/25/ReSharper-and-Source-Analysis-Tools-for-C/
date: 6/25/2008 6:05:33 AM
updated: 6/25/2008 6:05:33 AM
disqusIdentifier: 20080625060533
tags: ["ReSharper", "unit test"]
alias:
 - /post/ReSharper-and-Source-Analysis-Tools-for-C.aspx/index.html
---
If you do have [Microsoft Source Analysis Tools for C#](http://blogs.msdn.com/sourceanalysis/archive/2008/05/23/announcing-the-release-of-microsoft-source-analysis.aspx) and [JetBrains ReSharper 4.0](http://www.jetbrains.com/resharper/) installed in you development environment than you might get issue when you run your unit tests through ReSharper [Unit Test Explorer](http://www.jetbrains.com/resharper/features/unit_testing.html#Unit_Test_Explorer) and [Sessions](http://www.jetbrains.com/resharper/features/unit_testing.html#Unit_Test_Sessions). As you can [read on my bug report feedback](http://www.jetbrains.net/jira/browse/RSRP-73126):

> There is known problem in StyleCop, which prevents build system of VS to report any progress. Unit Testing waits for project to be built. You can switch building to "Never" on Session toolbar and build manually, or uninstall StyleCop
