---
title: "TeamCity 5.0 'Failed to collect changes error' issue"
permalink: /2009/12/11/TeamCity-50-e2808bFailed-to-collect-changes-error-issue/
date: 12/11/2009 5:49:21 AM
updated: 12/11/2009 5:49:21 AM
disqusIdentifier: 20091211054921
tags: ["continuous integration", "Team City"]
---
Tonight I was facing an issue with [TeamCity](http://www.jetbrains.com/teamcity/index.html) 5.0 plugin in Visual Studio 2008.

I was getting the error message "Failed to collect changes error" in the new Local changes window.
<!-- more -->

Searching on the bug tracking tool of JetBrains for TeamCity I found this issue "[TW-10474 I can not make VS addin recollect changes after receiving "Failed to collect changes error"](http://youtrack.jetbrains.net/issue/TW-10474)”

Has this issue breaks my way to work with pre-tested build I decided to search for a solution because I cannot work without this now!

So following the instruction "Logging in TeamCity Visual Studio plugin" : [Reporting Issues](http://www.jetbrains.net/confluence/display/TCD3/Reporting+Issues)

Looking at the produced logs I found this:

```text
9:28:24 PM.987: Thread:31: svn.exe info "xml" "non-interactive" "p:\@@projects\_handsup\portal"      
9:28:24 PM.987: Thread:31: Failed to execute svn. code 1, error svn: Try "svn help" for more info svn: Syntax error parsing revision "projects\_handsup\portal" , output <?xml version="1.0"?> <info>      
9:28:24 PM.991: Thread:31: EXCEPTION: svn: Try "svn help" for more info      
svn: Syntax error parsing revision "projects\_handsup\portal".      
Svn has exited with code "1".      
SvnInfoUuidCommand failed
```

I tried then the svn command from the command prompt and got the same error!

I finally renamed the path to my project from "p:\@@projects\_handsup\portal" to "p:\projects\_handsup\portal"

And now it works again! So forget strange characters in your path!
