---
title: "Fixing ReSharper inspection Results used from XAML"
permalink: /2011/11/12/Fixing-ReSharper-inspection-Results-used-from-XAML/
date: 11/12/2011 1:45:52 AM
updated: 11/12/2011 2:52:24 AM
disqusIdentifier: 20111112014552
coverImage: https://farm6.staticflickr.com/5297/5515390031_ee3ab01e4a_b.jpg
coverSize: partial
thumbnailImage: https://farm6.staticflickr.com/5297/5515390031_ee3ab01e4a_q.jpg
coverCaption: "Rocher du diamant depuis la grande anse du diamant, Le Diamant, Martinique"
tags: ["ReSharper", "XAML", "WPF", "Silverlight", "WP7"]
alias:
 - /post/Fixing-ReSharper-inspection-Results-used-from-XAML.aspx/index.html
---
<!--[![Rocher du diamant depuis la grande anse du diamant](http://farm6.static.flickr.com/5297/5515390031_ee3ab01e4a_m.jpg)](http://www.flickr.com/photos/laurentkempe/5515390031/ "Rocher du diamant depuis la grande anse du diamant by Laurent Kempé, on Flickr")-->
Finishing this week sprint I decided to inspect some code using [ReSharper 6.1 EAP](http://confluence.jetbrains.net/display/ReSharper/ReSharper+Early+Access+Program) and I started to give ReSharper a chance to help me find some of broken code.

When I started I had some of the following inspection results. It is clearly showing that some properties wasn’t identified as used in a WPF binding.
<!-- more -->

![](http://farm7.static.flickr.com/6096/6334117277_7e0d090b13_o.png)

So ReSharper proposed to make the property private which for sure was not ok for me has I knew it was used.

Some time ago I started to assign some design DataContext to be able to navigate from View to my ViewModel just by pressing **Go to Declaration** (CTRL-B in IDEA scheme) in ReSharper.

<style type="text/css">
.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }
.code { font-size: 12px; color: #000; font-family: Consolas, "Courier New", Courier, Monospace; background-color: #F1F1F1; line-height: normal; }
.code p		{ padding: 5px; }
.code .rem	{ color: #008000; }
.code .kwrd	{ color: #0000ff; }
.code .str	{ color: #006080; }
.code .op	{ color: #0000c0; }
.code .preproc { color: #0000ff; }
.code .asp	{ background-color: #ffff00; }
.code .html { color: #800000; }
.code .attr { color: #ff0000; }
.code .alt	{ background-color: #f4f4f4; }
.code .lnum	{ color: #606060; }
</style>

<pre class="code"><span style="color: blue">&lt;</span><span style="color: #a31515">UserControl </span><span style="color: red">x</span><span style="color: blue">:</span><span style="color: red">Class</span><span style="color: blue">="skyeEditor.View.StatusBarView"
             </span><span style="color: red">xmlns</span><span style="color: blue">="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             </span><span style="color: red">xmlns</span><span style="color: blue">:</span><span style="color: red">x</span><span style="color: blue">="http://schemas.microsoft.com/winfx/2006/xaml"
             </span><span style="color: red">xmlns</span><span style="color: blue">:</span><span style="color: red">mc</span><span style="color: blue">="http://schemas.openxmlformats.org/markup-compatibility/2006"
             </span><span style="color: red">xmlns</span><span style="color: blue">:</span><span style="color: red">d</span><span style="color: blue">="http://schemas.microsoft.com/expression/blend/2008"
             </span><span style="color: red">xmlns</span><span style="color: blue">:</span><span style="color: red">ViewModel</span><span style="color: blue">="clr-namespace:skyeEditor.ViewModel"
             </span><span style="color: red">mc</span><span style="color: blue">:</span><span style="color: red">Ignorable</span><span style="color: blue">="d"
             </span><strong><span style="color: red">d</span><span style="color: blue">:</span><span style="color: red">DataContext</span><span style="color: blue">="{</span><span style="color: #a31515">d</span><span style="color: blue">:</span><span style="color: #a31515">DesignInstance </span><span style="color: red">Type</span></strong><span style="color: blue"><strong>=ViewModel:StatusBarViewModel}"</strong>&gt;
</span></pre>

This for sure helps ReSharper code inspection as you can see on the following screenshot

![](http://farm7.static.flickr.com/6056/6334138827_993d3fea14_o.png)

Now I can navigate from View to ViewModel, can avoid to have developer deleting properties that are sued in XAML bindings, but I can also get an idea of where a ViewModel property is used by using ReSharper **Find Usage** (Alt-F7). I see right away that the StatusBarView.xaml is using the property ShowActivityProgress.

![](http://farm7.static.flickr.com/6230/6334152313_b13b7a1394_o.png)

So don’t forget to add the d:DataContext into your XAML !
