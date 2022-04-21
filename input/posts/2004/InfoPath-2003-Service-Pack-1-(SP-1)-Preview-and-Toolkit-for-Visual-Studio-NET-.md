---
title: "InfoPath 2003 Service Pack 1 (SP-1) Preview and Toolkit for Visual Studio .NET "
permalink: /2004/02/25/InfoPath-2003-Service-Pack-1-(SP-1)-Preview-and-Toolkit-for-Visual-Studio-NET-/
date: 2/25/2004 7:36:00 AM
updated: 2/25/2004 7:36:00 AM
disqusIdentifier: 20040225073600
tags: [".NET Development"]
alias:
 - /post/InfoPath-2003-Service-Pack-1-(SP-1)-Preview-and-Toolkit-for-Visual-Studio-NET-.aspx/index.html
---
I started to look at Infopath long time ago and was waiting for that solution. I was really surprised to see Infopath with something else then .NET to wrtie code for it. That's over. :-)

Since I implemented a tool to write the article on my web site [Tech Head Brothers](http://www.techheadbrothers.com/) based on Word 2003 and Visual Studio Toolkit I was waiting the toolkit for Infopath to be able to have a form to post News that will use WSE to handle secured connection to my site.<br>In this new version when you add a button you will get a dialog with a button "Edit from code" and when you click on it your get back to Visual Studio that already added a handler for you:
<!-- more -->
<font size="2">


</font><font color="#008000" size="2">// The following function handler is created by Microsoft Office InfoPath. Do not

</font><font size="2">


</font><font color="#008000" size="2">// modify the type or number of arguments.

</font><font size="2">


[InfoPathEventHandler(MatchPath="CTRL4_5", EventType=InfoPathEventType.OnClick)]

</font><font color="#0000ff" size="2">public</font><font size="2"> </font><font color="#0000ff" size="2">void</font><font size="2"> CTRL4_5_OnClick(DocActionEvent e)

{

</font><font color="#008000" size="2">// Write your code here.

</font><font size="2">


}

Thats really really cool.
</font>
