---
title: "TeamCity migration tip"
permalink: /2009/11/03/TeamCity-migration-tip/
date: 11/3/2009 12:39:27 AM
updated: 11/3/2009 12:39:27 AM
disqusIdentifier: 20091103123927
alias:
 - /post/TeamCity-migration-tip.aspx/index.html
---
Today I am migrating a [TeamCity](http://www.jetbrains.com/teamcity/index.html) installation from an old server to a new server and discovered that it is really easy to do that!

First I fully re-installed TeamCity using the Windows Setup that JetBrains delivered on the new server.
<!-- more -->

Then I stopped the old and new instances and transferred the folder **[.BuildServer](http://www.jetbrains.net/confluence/display/TCD4/TeamCity+Data+Directory)** from the old server to the new server.

Restarted the new instance and got all my settings from the old instance on the new instance (projects, users…)

This saved my day! and lots of work! Thank you JetBrains for this great tool.

**<u>Update</u>**

If you set on the original server the [Server URL](http://www.jetbrains.net/confluence/display/TCD4/Configuring+Server+URL), don’t forget to update it to the new Server URL. This can be done through the Administration > Server Configuration page.
