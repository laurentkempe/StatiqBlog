---
title: "Jetbrains Resharper 2.0 Early Access Program (EAP) started !!!"
permalink: /2005/07/20/Jetbrains-Resharper-20-Early-Access-Program-(EAP)-started-!!!/
date: 7/20/2005 6:55:00 AM
updated: 7/20/2005 6:55:00 AM
disqusIdentifier: 20050720065500
tags: ["Tools", ".NET Framework 2.0"]
---
**Great news** from [Jetbrains](http://www.jetbrains.com/) tonight with the launch of the ReSharper 2.0 EAP started. You can download the version for Visual Studio 2003 and Visual Studio 2005 Beta 2 and VS 2005 June CTP are supported [on this page](http://www.jetbrains.net/confluence/display/ReSharper/Download). I can't work anymore without that tool . There is for sure some refactoring capabilities in Visual Studio 2005 Beta 2, but it is far from being enough.

[Build 201 (Changes from ReSharper 1.5)](http://www.jetbrains.net/confluence/display/ReSharper/Changes)
<!-- more -->

##### [General]()

*   Support of Visual Studio 2005 (Beta 2 and June CTP are currently 
  supported) 
  Support of C# 2.0 constructs (for Visual Studio 2005) - not all constructs 
  are properly supported yet 
  Support of ASP.NET - in progress, not all features work properly 
  New preprocessor directives handling (no errors caused by use of 
  preprocessor directives anymore!) 


##### [On-the-fly Code Highlighting]()

*   New warning highlightings: redundant 'as' type cast; redundant empty 
  constructor; sealed member in sealed class; new virtual member in ....; 'new' 
  modifier is missing; redundant 'new' modifier; redundant boolean comparison; 
  redundant qualifiers; local or parameter hides member; empty control statement 
  body 
  More syntax errors highlighted 
  Lots of new quickfixes 
  Quickly enable/disable highlighting for particular file (Ctrl+8**) 


##### [Live Templates]()

*   Support for templates sharing, import/export, etc "Create template from selection" feature 


##### [Code Formatter]()

*   Line wrapping Code style settings: support for sharing, import/export 


##### [Other]()

*   Unit tests support (NUnit, csUnit, TeamTest tests are supported) 
  "Go to Symbol" feature - **Ctrl+Shift+Alt+N** (the same as 
  **Ctrl+N** and **Ctrl+Shift+N** but navigates 
  to all types and members) 
  Quick-documentation feature (Ctrl+Q**) shows popup with 
  documentation for symbol under caret (also works within doc-comment to show 
  presentable form of it) 
  File structure view with reqions, drag&drop etc 
  Mouse-click on implements/overrides/hides gutter icons to navigate 
  Rename refactoring suggests to rename overloaded methods 
  Rename refactoring suggests to rename the corresponding property when 
  renaming field and vise versa 
  Code completion after dot shows indexers 
  Code completion in namespace declarations


You can check the whole plan for ReSharper 2.0 [on this page](http://www.jetbrains.net/confluence/display/ReSharper/ReSharper+2.0+Plan).