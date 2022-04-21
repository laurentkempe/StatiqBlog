---
title: "File dialog automation using White framework"
permalink: /2010/07/16/File-dialog-automation-using-White-framework/
date: 7/16/2010 3:22:07 AM
updated: 5/2/2012 12:53:45 AM
disqusIdentifier: 20100716032207
tags: ["acceptance test", "white"]
alias:
 - /post/File-dialog-automation-using-White-framework.aspx/index.html
---
Today [TeamCity](http://www.jetbrains.com/teamcity/index.html) was showing me one functional test failure on my WPF application.

I already discussed about this problem here: [White’s tip for your automated WPF functional tests](http://www.laurentkempe.com/post/Whitee28099s-tip-for-your-automated-WPF-functional-tests.aspx)       
<!-- more -->
The solution I presented at that time was working on my local development environment but not on my Continuous Integration system; aka TeamCity. So I went for another solution which was searching for the filename ComboBox and was setting the value. 

This was working for some time but today not anymore. The issue I discovered is that using the ComboBox needed that the path was already used in the past otherwise setting the value was failing. So I was stuck and had to find another solution.

I fired spy++ and searched for a solution and after some debugging I came to the following one:

<div style="line-height:135%; padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:9f29d62e-a07a-4c37-8831-5484ee322783" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background-color: #000000; max-height: 500px; overflow: auto; padding: 2px 5px; white-space: nowrap"><span style="color:#cc7832">var</span><span style="color:#ffffff"> openModalWindow = </span><br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ffffff">MainWindow.ModalWindow(</span><span style="color:#a5c25c">"Please choose a Zip file"</span><span style="color:#ffffff">, </span><span style="color:#ffc66d">InitializeOption</span><span style="color:#ffffff">.NoCache);</span><br> <br> <span style="color:#ffffff">MainWindow.WaitWhileBusy();</span><br> <span style="color:#ffffff"></span><span style="color:#ffc66d">Assert</span><span style="color:#ffffff">.IsNotNull(openModalWindow);</span><br> <br> <span style="color:#ffffff"></span><span style="color:#cc7832">var</span><span style="color:#ffffff"> filePath = </span><span style="color:#ffc66d">Path</span><span style="color:#ffffff">.Combine(GetCurrentPath(), filename);</span><br> <br> <span style="color:#ffffff"></span><span style="color:#cc7832">var</span><span style="color:#ffffff"> filenameTextBox = </span><br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ffffff">openModalWindow.Get&lt;</span><span style="color:#ffc66d">TextBox</span><span style="color:#ffffff">&gt;(</span><span style="color:#ffc66d">SearchCriteria</span><span style="color:#ffffff">.ByAutomationId(</span><span style="color:#a5c25c">"1148"</span><span style="color:#ffffff">));</span><br> <span style="color:#ffffff">filenameTextBox.SetValue(filePath);</span><br> <br> <span style="color:#ffffff">openModalWindow.Keyboard.PressSpecialKey(</span><span style="color:#ffc66d">KeyboardInput</span><span style="color:#ffffff">.</span><span style="color:#ffc66d">SpecialKeys</span><span style="color:#ffffff">.RETURN);</span></div> </div> </div>

This is working on my local environment but also on TeamCity!

The key point was to find the TextBox with the AutomationId of 1148.
