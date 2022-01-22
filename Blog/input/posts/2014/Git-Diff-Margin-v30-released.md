---
title: "Git Diff Margin v3.0 released"
permalink: /2014/09/24/Git-Diff-Margin-v30-released/
date: 9/24/2014 8:04:39 AM
updated: 9/24/2014 8:16:07 AM
disqusIdentifier: 20140924080439
tags: ["Visual Studio"]
alias:
 - /post/Git-Diff-Margin-v30-released.aspx/index.html
---
25 Days after the v2.0 I am pleased to announce the v3.0 of [Git Diff Margin](http://visualstudiogallery.msdn.microsoft.com/cf49cf30-2ca6-4ea0-b7cc-6a8e0dadc1a8)!

**[Git Diff Margin](http://visualstudiogallery.msdn.microsoft.com/cf49cf30-2ca6-4ea0-b7cc-6a8e0dadc1a8) displays live Git changes of the currently edited file on Visual Studio margin and scroll bar.**
<!-- more -->

Thanks to the great work of [Sam Harwell](https://github.com/sharwell) Git Diff Margin v3.0 now support Visual Studio 2010, 2012, 2013 and Visual Studio 14 "CTP".

Here are the release notes 

### New features

*   Support for Visual Studio 2010, 2012 and Visual Studio 14 "CTP" 
*   Show diff using Visual Studio Diff window except for Visual Studio 2010 which still use external diff tool 
*   Possibility to define shortcuts for next/previous change navigation 
*   Add options for highlighting untracked lines [#29](https://github.com/laurentkempe/GitDiffMargin/issues/29) 
*   Update icons   

### Improvements

*   Improve external diff configuration handling in .gitconfig [#32](https://github.com/laurentkempe/GitDiffMargin/issues/32) 
*   Improve "removed" glyph and editor diff positioning 
*   Improve support of Dark, Light and Blue theme 
*   Make sure the text editor is focused after a rollback 
*   Prevent ScrollDiffMargin from affecting the scroll bar behavior 
*   Play nice with other source control providers   

### Fixes

*   Fix Show Difference fails with DiffMerge for file names with spaces [#38](https://github.com/laurentkempe/GitDiffMargin/issues/38) 
*   Fix submodules issue [#40](https://github.com/laurentkempe/GitDiffMargin/issues/40)   

![](https://c4.staticflickr.com/4/3893/15335334635_a88dc1f271.jpg)
