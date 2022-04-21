---
title: "White’s tip for your automated WPF functional tests"
permalink: /2010/01/28/Whitee28099s-tip-for-your-automated-WPF-functional-tests/
date: 1/28/2010 5:24:03 AM
updated: 1/28/2010 5:24:03 AM
disqusIdentifier: 20100128052403
tags: ["WPF", "unit test", "white"]
alias:
 - /post/Whitee28099s-tip-for-your-automated-WPF-functional-tests.aspx/index.html
---
When you build automated WPF functional test using [White](http://white.codeplex.com/) in which you need to open a file through a Windows open file dialog, you will be confronted with the following issue. Windows open file dialog remember the last path with which you opened a file.

So you might have some unit tests that are green for a while which starts to be red for no apparent reasons. 
<!-- more -->

The solution I came to is as this. 

First I use Visual Studio, Copy to Output Directory, to copy the needed file to the output directoy in which your software will be started by the unit tests, e.g. for notValidVersionZip.zip

![](/images/4309956698_b62daf51f5_o1_50F26E1E.png) 

So now I am sure that the needed file is in the same path than the application. I then also need to be sure that when the application start the Windows open file dialog it points to this path. In the past implementation I was just using a filename and was lucky enough the path used by the Windows open file dialog was the correct one.

To get to the correct path is easy. We just navigate to the correct path using the Windows open file dialog in an automated way. The correct path is the path in which the application as been started, so you can get it like that:

<div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:55e8cf34-3def-431e-90fb-9299e1c955a7" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; overflow: auto"> <ol style="background: #ffffff; margin: 0; padding: 0 0 0 5px;"> <li><span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;summary&gt;</span></li> <li style="background: #f3f3f3"><span style="color:#808080">///</span><span style="color:#008000"> Gets the current path.</span></li> <li><span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;/summary&gt;</span></li> <li style="background: #f3f3f3"><span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;returns&gt;&lt;/returns&gt;</span></li> <li><span style="color:#0000ff">private</span> <span style="color:#0000ff">static</span> <span style="color:#0000ff">string</span> GetCurrentPath()</li> <li style="background: #f3f3f3">{</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">return</span> <span style="color:#2b91af">Path</span>.GetDirectoryName(<span style="color:#2b91af">Assembly</span>.GetExecutingAssembly().CodeBase);</li> <li style="background: #f3f3f3">}</li> </ol> </div> </div> </div>

We have the correct path and we still need to automate the Windows open file dialog to navigate to that path. We can do this like that:

<div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:13db65e7-54b9-402d-9138-c33cb2c3791e" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; overflow: auto"> <ol style="background: #ffffff; margin: 0; padding: 0 0 0 5px;"> <li><span style="color:#0000ff">protected</span> <span style="color:#0000ff">void</span> Open(<span style="color:#0000ff">string</span> filename)</li> <li style="background: #f3f3f3">{</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;OpenButton.Click();</li> <li style="background: #f3f3f3">&nbsp;</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">var</span> openModalWindow = </li> <li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MainWindow.ModalWindow(<span style="color:#a31515">"Please choose a Zip file"</span>, <span style="color:#2b91af">InitializeOption</span>.NoCache);</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">Assert</span>.IsNotNull(openModalWindow);</li> <li style="background: #f3f3f3">&nbsp;</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">var</span> splittedPath = GetCurrentPath().Split(<span style="color:#0000ff">new</span>[] { <span style="color:#a31515">'\\'</span> });</li> <li style="background: #f3f3f3">&nbsp;</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">foreach</span> (<span style="color:#0000ff">var</span> pathPart <span style="color:#0000ff">in</span> splittedPath)</li> <li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;{</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;openModalWindow.Enter(pathPart);</li> <li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;openModalWindow.Keyboard.PressSpecialKey(<span style="color:#2b91af">KeyboardInput</span>.<span style="color:#2b91af">SpecialKeys</span>.RETURN);</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;openModalWindow.WaitWhileBusy();</li> <li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;}</li> <li>&nbsp;</li> <li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;openModalWindow.Enter(filename);</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;openModalWindow.Keyboard.PressSpecialKey(<span style="color:#2b91af">KeyboardInput</span>.<span style="color:#2b91af">SpecialKeys</span>.RETURN);</li> <li style="background: #f3f3f3">}</li> </ol> </div> </div> </div>

Basically we split the path into it different path parts that White will enter into the dialog followed by a enter. Don’t forget to use the method **WaitWhileBusy()** after each enter, otherwise it will be too fast and sometime your test will not go to the correct path and then will not find the file.

Finally White enter the filename followed by enter and the file is opened.

Nice!

If you are using like me [ReSharper](http://www.jetbrains.com/resharper/index.html) to run your unit tests don’t forget to set it up to run tests from Project output folder.
![](/images/4309993844_8d9e828f8c_o1_46056709.png)
