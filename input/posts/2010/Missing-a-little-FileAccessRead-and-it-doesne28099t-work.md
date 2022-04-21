---
title: "Missing a little FileAccess.Read and it doesn’t work"
permalink: /2010/05/22/Missing-a-little-FileAccessRead-and-it-doesne28099t-work/
date: 5/22/2010 1:19:21 AM
updated: 5/22/2010 1:19:21 AM
disqusIdentifier: 20100522011921
tags: [".NET Framework 3.5"]
alias:
 - /post/Missing-a-little-FileAccessRead-and-it-doesne28099t-work.aspx/index.html
---
What is the difference between this code

<div style="line-height:135%; padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:4f938988-5f57-401f-aa50-3fabd8f1c6ed" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background-color: #ffffff; overflow: auto; padding: 2px 5px; white-space: nowrap"><span style="color:#0000ff">using</span> (<span style="color:#0000ff">var</span> fileStream = <span style="color:#0000ff">new</span> <span style="color:#2b91af">FileStream</span>(settingsFilename,<br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">FileMode</span>.Open))<br> {<br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">return</span> ReadSettings(fileStream);<br> }</div> </div> </div>
And this code

<div style="line-height:135%; padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:d23edba0-0809-4467-80de-7e73647d4a04" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background-color: #ffffff; overflow: auto; padding: 2px 5px; white-space: nowrap"><span style="color:#0000ff">using</span> (<span style="color:#0000ff">var</span> fileStream = <span style="color:#0000ff">new</span> <span style="color:#2b91af">FileStream</span>(settingsFilename,<br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">FileMode</span>.Open,<br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">FileAccess</span>.Read))<br> {<br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">return</span> ReadSettings(fileStream);<br> }</div> </div> </div>  

Almost nothing, just a little **FileAccess.Read** !

But this little thing makes a big difference when you run your software in a secured environment.

The application with this piece of code was deployed to a customer reporting that the application was crashing at a the point of reading the settings. Weird, really weird. After getting back the log and I finally discovered that using juste FileMode.Open needs modify rights and that’s was the issue because the customer deploy the settings file on a folder in which the user doesn’t have the modify rights.
