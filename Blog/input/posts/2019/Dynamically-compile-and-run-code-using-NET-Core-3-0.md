---
title: 'Dynamically compile and run code using .NET Core 3.0'
permalink: /2019/02/18/dynamically-compile-and-run-code-using-dotNET-Core-3.0/
disqusIdentifier: '20190218193012'
coverSize: partial
tags:
  - .NET Core
  - Roslyn
coverCaption: 'Levé du jour à Torre Di Calzarello, Corse, France'
coverImage: 'https://farm2.staticflickr.com/1947/43876018160_d11c5e61af_b.jpg'
thumbnailImage: 'https://farm2.staticflickr.com/1947/43876018160_d11c5e61af_q.jpg'
date: 2019-02-18 20:30:12
---

Let' see in this blog post the new possibility offered by **.NET Core 3.0 preview 2** to load and unload assemblies at run time using **[AssemblyLoadContext](https://github.com/dotnet/coreclr/blob/master/src/System.Private.CoreLib/src/System/Runtime/Loader/AssemblyLoadContext.cs#L14)**.

<!-- more -->

You can read more about it on the [dotnet / coreclr github repository](https://github.com/dotnet/coreclr/blob/master/Documentation/design-docs/assemblyloadcontext.md). You can also see the progress of the .NET Core team on that particular topic on the [Unloadability Github project page](https://github.com/dotnet/coreclr/projects/9).

> Here are some of the scenarios that motivated this work:
- Ability to load multiple versions of the same assembly within a given process (e.g. for plugin frameworks)
- Ability to load assemblies explicitly in a context isolated from that of the application.
- Ability to override assemblies being resolved from application context.
- Ability to have isolation of statics (as they are tied to the LoadContext)
- Expose LoadContext as a first class concept for developers to interface with and not be a magic.

In our case, the idea of this tiny project is the following:
- Watch a C# source code file for any modification using Rx and the FileWatcher
- On any change on that file, load it in memory as text
- Compile the file using Roslyn into an assembly which is kept in memory
- Execute the entry point of the assembly
- Unload the assembly

We will start with a simple hello world application, what else 😉 and we will allow the main application to pass some arguments to the dynamically compiled assembly.

First, we are using some Rx code to observe the file, I won't go into that detail because it is not the purpose of that post. The code is coming from [Wes Higbee](https://github.com/g0t4) from the repository [g0t4/Rx-FileSystemWatcher](https://github.com/g0t4/Rx-FileSystemWatcher).

We are using Rx-FileSystemWatcher to observe the _Sources_ folder and filter for _DynamicProgram.cs_. When this file is changed, we trigger the build, load the assembly generated, find the entry point and invoke it passing "France" as the first parameter.

<div style="clear:both;"></div>{% gist 1ea03376d0ef2a4da3358ab2629cccf2 Program.cs %}

The main program delegates the compilation to the _Compiler_ class, which is using Roslyn to compile the C# file _DynamicProgram.cs_. If there are some compilation errors, those are displayed on the console output. Otherwise, the compilation result is a _Hello.dll_ returned as a byte array.

<div style="clear:both;"></div>{% gist 1ea03376d0ef2a4da3358ab2629cccf2 Compiler.cs %}

Then, the main program delegate to the _Runner_ class which is in charge of loading and executing the entry point of the just compiled new assembly.
We are marking the method *LoadAndExecute* with **[MethodImpl(MethodImplOptions.NoInlining)]** so that the method cannot be inlined and to ensure that nothing would be kept alive.

<div style="clear:both;"></div>{% gist 1ea03376d0ef2a4da3358ab2629cccf2 Runner.cs %}

We are loading the assembly using our own simple implementation of **AssemblyLoadContext**, this is just to mark that the context is collectible. So that we can unload the assembly using the method **AssemblyLoadContext.Unload()**.

<div style="clear:both;"></div>{% gist 1ea03376d0ef2a4da3358ab2629cccf2 SimpleUnloadableAssemblyLoadContext.cs %}

In fact, the unloading does not happen immediately, it will wait that the GC collect the assembly. This is why we are calling **GC.Collect()** and **GC.WaitForPendingFinalizers()** in the *Execute* method. This is not mandatory but in our case, we want to be sure that the previous assembly is unloaded before compiling and loading the new one.

Let's run the application, change the file Program.cs in the folder Sources and see it working 😎

{% img https://raw.githubusercontent.com/laurentkempe/DynamicRun/master/doc/screenshot.png Running %}

This is opening some new capabilities which we might explore in some new posts!

You can access to the whole project on Github, [laurentkempe/DynamicRun](https://github.com/laurentkempe/DynamicRun).

Finally, you can read even more about it on "[Using and debugging unloadability in .NET Core](https://github.com/dotnet/coreclr/blob/a7cbc5c8d1bd48cafec48ac50900ff9e96c1485c/Documentation/project-docs/unloadability-howto.md)" and can also have a look at this interesting project  [natemcmaster/DotNetCorePlugins](https://github.com/natemcmaster/DotNetCorePlugins) which starts to talk about the same topic on "[Make plugins unloadable](https://github.com/natemcmaster/DotNetCorePlugins/issues/16)".
