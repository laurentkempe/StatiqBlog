---
title: "ProjectReference with Condition in your MSBuild project files"
permalink: /2009/12/03/ProjectReference-with-Condition-in-your-MSBuild-project-files/
date: 12/3/2009 7:24:42 AM
updated: 12/3/2009 7:24:42 AM
disqusIdentifier: 20091203072442
tags: ["Visual Studio", "ReSharper", "continuous integration", "Team City", "MSBuild"]
alias:
 - /post/ProjectReference-with-Condition-in-your-MSBuild-project-files.aspx/index.html
---
Since some time I have the current scenario where I need to have conditional reference in a project. Basically the application must reference an assembly in one case in other it should reference another one. This was working correctly from an MSBuild point of view as the first implemented solution let me compile and run the application on my development machine and it was also working for our [TeamCity](http://www.jetbrains.com/teamcity/index.html) build server. So everything was fine in this perfect word expect one thing!

The issue was the following; Visual Studio was showing two references of the ‘same assembly’ with different path. Not really an issue you would say because the correct one was used at compile time and at run time in all configurations. So the issue was that this had an impact of [ReSharper](http://www.jetbrains.com/resharper/index.html). And this is I cannot accept because it affect my productivity.
<!-- more -->

So the other day I had a discussion with [Ilya](http://resharper.blogspot.com/) of [JetBrains](http://www.jetbrains.com/) which gave me some idea but also told me that ReSharper reads project structure out of Visual Studio and that it doesn't provide lots of info, e.g. conditions on references. So this is why seeing two reference of the ‘same assembly’ was not a problem on Visual Studio itself and on the build server but was an issue to ReSharper because it was seeing two same reference, same namespace, same classes…

Today I had some time free and decided to see where I can come with this issue. And I found a solution.

My first solution, which had the explained issue was the same as the one on this post “[Don't be afraid of your csproj-Files (III): We have a condition](http://www.realfiction.net/?q=node/164)”, so having a Condition on the ItemGroup.

The solution I came to is to bring the Condition to one upper level than the ItemGroup, so I used **Choose** like this:

```xml
<Choose>
  <When Condition=" '$(Configuration)' == 'client1DeployClickOnce' ">
    <ItemGroup>
        <ProjectReferenceInclude="..\client1\app.Controls\app.Controls.csproj">
        <Project>{A7714633-66D7-4099-A255-5A911DB7BED8}</Project>
        <Name>app.Controls %28Sources\client1\app.Controls%29</Name>
      </ProjectReference>
    </ItemGroup>
  </When>
  <Otherwise>
    <ItemGroup>
      <ProjectReference Include="..\app.Controls\app.Controls.csproj">
        <Project>{2E6D4065-E042-44B9-A569-FA1C36F1BDCE}</Project>
        <Name>app.Controls %28Sources\app.Controls%29</Name>
      </ProjectReference>
    </ItemGroup>
  </Otherwise>
</Choose>
```

Reloading the project I had the surprise to see only one reference and that ReSharper was working again correctly!

For sure the build on TeamCity is also working perfectly.
