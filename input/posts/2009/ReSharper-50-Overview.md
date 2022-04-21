---
title: "ReSharper 5.0 Overview"
permalink: /2009/10/13/ReSharper-50-Overview/
date: 10/13/2009 3:29:45 AM
updated: 10/13/2009 3:29:45 AM
disqusIdentifier: 20091013032945
tags: ["ReSharper"]
alias:
 - /post/ReSharper-50-Overview.aspx/index.html
---
As part of the [Jetbrains Academy](http://weblogs.asp.net/lkempe/archive/2009/09/06/joining-jetbrains-academy.aspx) I am using/testing [ReSharper](http://www.jetbrains.com/resharper/index.html) 5 for around a month now. It is quite stable for a pre-release and I have lots of fun using it, and as always a productivity boost. Some of my preferred features are: External Sources, ASP.NET MVC integration, Solution-Wide Warnings and Suggestions, Upgrade-to-LINQ Actions. Till now I wasn’t able to test the Native NUnit Support, but this is definitely something I will test in the near future.

Jetbrains just posted on their blog [ReSharper 5.0 Overview](http://blogs.jetbrains.com/dotnet/2009/10/resharper-50-overview/), check it out:
<!-- more -->

> As promised, we’re publishing general ReSharper 5.0 overview, elaborating on its feature set in more detail.
> 
> Please keep in mind that this is a preliminary document. The general picture will stay unchanged but local amendments can not be ruled out at this point, and many user interface items will probably change.
> 
> ##### Features
> 
> *   **External Sources**
> A solution is not limited to sources included in your projects, but it also contains sources that were used to build your libraries. Some companies publish parts of their sources using the Source Server feature of debug information files (*PDB*). This is the same technology that Microsoft uses to provide access to source code for parts of .NET Framework. With ReSharper 5, you can now access it as if it were a part of your solution. When no sources are available, ReSharper does a decent job of reconstructing types’ structure from metadata for your browsing pleasure.
> [![](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/external_sources_crop-247x300.png "external_sources_crop")](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/external_sources_crop.png)
> *   **Structured Patterns**
> “I was assigned to new project, and the source code is full of [*your favorite code smell here*]. Please, make ReSharper analyze and fix it!”. Fortunately, ReSharper 5 can address this demand. You can set up your own code patterns, search for them, replace them, include them into code analysis, and even use quick fixes to replace! Building patterns and enforcing good practices has never been easier. Corporate and team policies, your own frameworks, favorite open source libraries and tools — you can cover them all.
> [![](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/structural_search-300x239.png "structural_search")](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/structural_search.png)
> *   **Project Refactorings and Dependencies View**
> Once you are used to smart, automated refactorings provided by ReSharper, you can’t think of executing them manually anymore. In this release, we extend ReSharper’s coverage to bring you several refactorings for project structure. With ReSharper 5, you can move files and folders between projects, synchronize namespaces to folder structure in any scope as large as solution, safely delete obsolete subsystems without going type by type, and split a file with lots of types created from usages into their own dedicated files in one go. We have also added special project dependencies view to help you track down excessive dependencies between projects and eliminate them. As an early ReSharper 5 user said, “I no longer feel fear to restructure my project. I just go and do it when I feel it is right to do so”.
> *   **Call Tracking**
> Find usages, find usages, find usages. Formerly, attempting to track call sequences in code could end up with lost context, lots of *Find Results* windows and frustration. With ReSharper 5, you can inspect an entire call sequence in a single window in a simple and straightforward manner. Stuck in unfamiliar code? ReSharper’s code inspecting tools for the rescue!
> [![](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/calltracking-300x177.png "calltracking")](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/calltracking.png)
> *   **Value Tracking**
> Value Tracking provides you with important information about data flow in your program. At any point in your source code, select a variable, parameter, field or property and ask ReSharper to inspect it. You will then see how its value flows through your program, back to its sources or straight to consumers. Wonder how null could be passed to a specific parameter? Track it!
> [![](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/valuetracking-300x205.png "valuetracking")](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/valuetracking.png)
> *   **Internationalization**
> Software localization and globalization has always been a tough and at times unwanted task for developers. ReSharper 5 greatly simplifies the process of working with resources by providing a full stack of features for *ResX* files and resource usages in C# and VB.NET code, in ASP.NET and XAML markup. *Move string to resource*, *Find usages of resource* and other navigation features, refactoring support, inspections and fixes — all ReSharper goodness for your localization pleasure.
> 
> ##### Technologies and Languages
> 
> *   **Visual Studio 2010**
> We will publish more information about Visual Studio 2010 support when VS Beta 2 is released. Currently, ReSharper 5 builds support Visual Studio 2005 and Visual Studio 2008.
> *   **C# 4 and VB10**
> New language versions appear at a great speed nowadays, and ReSharper team works hard to support them right when you need them. ReSharper 5 provides beta support for C# 4 and VB10, as Visual Studio 2010 does itself. Variance, dynamic types, named arguments and optional parameters, embedded COM assemblies — all of these features are supported in the new ReSharper. During VS 2010 Beta 2 phase we’re hoping to learn from your experience using these features and improve their support for Visual Studio 2010 release.
> *   **ASP.NET**
> With this new version, ReSharper support for ASP.NET is improved tenfold. In addition to performance and responsiveness improvements, lots of new features for ASP.NET markup files are introduced to make your life easier. Web-specific navigation, master page support, new inspections and syntax highlighting for web files, *File Structure* and *Go to File Member* for in-page navigation and overview, live templates for common markup and more!
> [![](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/asp_generate1-300x240.png "asp_generate1")](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/asp_generate1.png)
> *   **ASP.NET MVC**
> ASP.NET MVC deserved our special attention: special syntax highlighting, inspections, navigation to and from action or controller, and even actions to create new types and methods from usage in pages.
> [![](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/aspnetmvc-300x135.png "aspnetmvc")](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/aspnetmvc.png)
> 
> ##### Productivity
> 
> *   **IntelliSense**
> ReSharper continues to bring first-class IntelliSense experience, and the new version gives even more. We have added automatic completion for enum members and boolean values, made automatic triggering smarter, and greatly improved speed. Completion for unresolved symbols in local scope is a new ReSharper IntelliSense feature. Another improvement is completion for all-lower text with CamelHumps  — to make *cocopro* match *CodeCompletionProvider* — and that means you don’t need to press *Shift* too often.
> *   **Bookmarks**
> This is a simple yet powerful feature: drop a numbered marker with a single shortcut, jump back at any time with another keyboard key. Up to 10 numbered bookmarks, unlimited unnumbered bookmarks, full list of bookmarked positions in a single pop-up window — all to help you switch between several code spots instantly.
> [![](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/bookmarks-300x154.png "bookmarks")](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/bookmarks.png)
> 
> ##### Inspections
> 
> *   **Solution-Wide Warnings and Suggestions**
> We have received a lot of positive feedback from our users regarding solution-wide error analysis that allows you to immediately see compilation errors in whole solution. In ReSharper 5, we took this technology to a new level by adding warnings and suggestions to the list. Now you can browse code smells that ReSharper finds across your solution and quickly improve quality of your code.
> *   **Upgrade-to-LINQ Actions**
> With C# 3.0 and LINQ, developers are able to write data-intensive code more easily by directly describing their intent to the compiler. However, years of imperative programming left us with tons of *foreach*-style code waiting to be upgraded. ReSharper 5 detects parts of your code that can be rewritten using the new LINQ syntax and suggests to make the conversion automatically, to make the developer’s intent crystal clear.
> [![](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/foreach_new-300x232.png "foreach_new")](http://blogs.jetbrains.com/dotnet/wp-content/uploads/2009/10/foreach_new.png)
> *   **Use IEnumerable Where Possible**
> With the power of LINQ, *IEnumerable* is more than enough to pass a collection of values. So why restrict yourself with API requiring you to pass old-school arrays, *List*s and *ArrayList*s? ReSharper will scan your code base to detect methods that can safely return and accept *IEnumerable* instead of a more specific type. Of course, we will also take care of the conversion.
> *   **New and Improved Code Inspections**
> We have collected rich customer feedback and went through a list of common errors that developers make in code. Based on that, we have added a ton of highly intelligent inspections to immediately boost your .NET expertise. For example, if you take your API seriously and want it to be well documented, ReSharper will help you by highlighting errors in XML comments.
> 
> ##### Other improvements
> 
> *   **Native NUnit Support**
> ReSharper 5 introduces a completely new approach to running your NUnit tests. Our engine is now based on native NUnit code. What it means to you is 100% compatibility with the latest released version of NUnit and full support of its recent unit testing features.
> *   **XML Formatting**
> XML data is an important part of modern applications and you want it to be in order. The new version of ReSharper is supplied with superb configurable formatter for XML files.
