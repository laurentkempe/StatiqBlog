---
title: "White’s tip for your automated WPF functional tests"
permalink: /2010/01/28/Whitee28099s-tip-for-your-automated-WPF-functional-tests/
date: 1/28/2010 5:24:03 AM
updated: 1/28/2010 5:24:03 AM
disqusIdentifier: 20100128052403
tags: ["WPF", "unit test", "white"]
---
When you build automated WPF functional test using [White](http://white.codeplex.com/) in which you need to open a file through a Windows open file dialog, you will be confronted with the following issue. Windows open file dialog remember the last path with which you opened a file.

So you might have some unit tests that are green for a while which starts to be red for no apparent reasons. 
<!-- more -->

The solution I came to is as this. 

First I use Visual Studio, Copy to Output Directory, to copy the needed file to the output directoy in which your software will be started by the unit tests, e.g. for notValidVersionZip.zip

![](/images/4309956698_b62daf51f5_o1_50F26E1E.png) 

So now I am sure that the needed file is in the same path than the application. I then also need to be sure that when the application start the Windows open file dialog it points to this path. In the past implementation I was just using a filename and was lucky enough the path used by the Windows open file dialog was the correct one.

To get to the correct path is easy. We just navigate to the correct path using the Windows open file dialog in an automated way. The correct path is the path in which the application as been started, so you can get it like that:

```csharp
/// <summary>
/// Gets the current path.
/// </summary>
/// <returns></returns>
private static string GetCurrentPath()
{
    return Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
}
```

We have the correct path and we still need to automate the Windows open file dialog to navigate to that path. We can do this like that:

```csharp
protected void Open(string filename)
{
    OpenButton.Click();

    var openModalWindow =
        MainWindow.ModalWindow("Please choose a Zip file", InitializeOption.NoCache);
    Assert.IsNotNull(openModalWindow);
 
    var splittedPath = GetCurrentPath().Split(new[] { '\\' });
 
    foreach (var pathPart in splittedPath)
    {
        openModalWindow.Enter(pathPart);
        openModalWindow.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
        openModalWindow.WaitWhileBusy();
    }
 
    openModalWindow.Enter(filename);
    openModalWindow.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
}
```
Basically we split the path into it different path parts that White will enter into the dialog followed by a enter. Don’t forget to use the method **WaitWhileBusy()** after each enter, otherwise it will be too fast and sometime your test will not go to the correct path and then will not find the file.

Finally White enter the filename followed by enter and the file is opened.

Nice!

If you are using like me [ReSharper](http://www.jetbrains.com/resharper/index.html) to run your unit tests don’t forget to set it up to run tests from Project output folder.
![](/images/4309993844_8d9e828f8c_o1_46056709.png)
