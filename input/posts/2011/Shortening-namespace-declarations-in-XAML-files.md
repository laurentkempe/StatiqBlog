---
title: "Shortening namespace declarations in XAML files"
permalink: /2011/12/15/Shortening-namespace-declarations-in-XAML-files/
date: 12/15/2011 2:49:43 AM
updated: 12/15/2011 7:30:59 AM
disqusIdentifier: 20111215024943
coverImage: https://farm6.staticflickr.com/5190/5563773074_38938ee129_b.jpg
coverSize: partial
thumbnailImage: https://farm6.staticflickr.com/5190/5563773074_38938ee129_q.jpg
coverCaption: "Vue sur la tête de la femme couchée de la terrasse de la villa cannelle, Le Diamant, Martinique"
tags: ["WPF", "Silverlight"]
alias:
 - /post/Shortening-namespace-declarations-in-XAML-files.aspx/index.html
---
<!--[![Vue sur la tête de la femme couchée de la terrasse de la villa cannelle](http://farm6.staticflickr.com/5190/5563773074_38938ee129_m.jpg)](http://www.flickr.com/photos/laurentkempe/5563773074/ "Vue sur la tête de la femme couchée de la terrasse de la villa cannelle by Laurent Kempé, on Flickr")-->
This afternoon I was working on [Innoveo](http://www.innoveo.com/) [Skye Editor](http://www.innoveo.com/SoftwareSolution.aspx) which is a WPF application written in C#. 

The application is using [Telerik RadControls for WPF](http://www.telerik.com/products/wpf.aspx). 
<!-- more -->

I was facing the issue of having more and more namespace declarations like the following for **RadInput** and **Rad**:

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

<pre class="code"><span style="color: blue">&lt;</span><span style="color: #a31515">Window </span><span style="color: red">x</span><span style="color: blue">:</span><span style="color: red">Class</span><span style="color: blue">="skyeEditor.View.Dialogs.ProductSettingsDialog"
        </span><span style="color: red">xmlns</span><span style="color: blue">="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        </span><span style="color: red">xmlns</span><span style="color: blue">:</span><span style="color: red">x</span><span style="color: blue">="http://schemas.microsoft.com/winfx/2006/xaml"
        </span><span style="color: red">xmlns</span><span style="color: blue">:</span><span style="color: red">RadInput</span><span style="color: blue">="clr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls.Input"
        </span><span style="color: red">xmlns</span><span style="color: blue">:</span><span style="color: red">Rad</span><span style="color: blue">="clr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls"
</span></pre>

Searching for a solution I thought of ASP.NET in which you can declare in the web.config things like that:

> &lt;pages pageBaseType="System.Web.Mvc.WebViewPage"&gt;
>   &lt;namespaces&gt;
>     &lt;add namespace="MyNamespace" /&gt;

My goal was to have one namespace declaration for example **telerik** and be able to use it in all my XAML files without having to declare too much. 

So I started an online discussion with [Laurent Bugnion](http://www.galasoft.ch/intro_en.html) who has always shown me the good direction, which in this case is the 

> ### [XmlnsDefinitionAttribute](http://msdn.microsoft.com/en-us/library/system.windows.markup.xmlnsdefinitionattribute.aspx) Class

Followed by an article from [Sandino Di Mattia](http://blog.sandrinodimattia.net/) about “[A Guide to Cleaner XAML with Custom Namespaces and Prefixes (WPF/Silverlight)](http://www.codeproject.com/KB/silverlight/xaml_custom_namespaces.aspx?adcid=2499&azid=85&PageFlow=FixedWidth)”.

Using the idea I was able to add the following to the **AssemblyInfo.cs** of my own project:

<pre class="code">[<span style="color: blue">assembly</span>: <span style="color: #2b91af">XmlnsDefinition</span>(<span style="color: #a31515">"telerik"</span>,
                           <span style="color: #a31515">"Telerik.Windows.Controls"</span>,
                           AssemblyName = <span style="color: #a31515">"Telerik.Windows.Controls"</span>)]

[<span style="color: blue">assembly</span>: <span style="color: #2b91af">XmlnsDefinition</span>(<span style="color: #a31515">"telerik"</span>,
                           <span style="color: #a31515">"Telerik.Windows.Controls"</span>,
                           AssemblyName = <span style="color: #a31515">"Telerik.Windows.Controls.Input"</span>)]

[<span style="color: blue">assembly</span>: <span style="color: #2b91af">XmlnsDefinition</span>(<span style="color: #a31515">"telerik"</span>,
                           <span style="color: #a31515">"Telerik.Windows.Controls"</span>,
                           AssemblyName = <span style="color: #a31515">"Telerik.Windows.Controls.Navigation"</span>)]

[<span style="color: blue">assembly</span>: <span style="color: #2b91af">XmlnsDefinition</span>(<span style="color: #a31515">"telerik"</span>,
                           <span style="color: #a31515">"Telerik.Windows.Controls"</span>,
                           AssemblyName = <span style="color: #a31515">"Telerik.Windows.Controls.RibbonView"</span>)]</pre>
                           
And then my XAML file declaration looks like that:

<pre class="code"><span style="color: blue">&lt;</span><span style="color: #a31515">Window </span><span style="color: red">x</span><span style="color: blue">:</span><span style="color: red">Class</span><span style="color: blue">="skyeEditor.View.Dialogs.ProductSettingsDialog"
        </span><span style="color: red">xmlns</span><span style="color: blue">="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        </span><span style="color: red">xmlns</span><span style="color: blue">:</span><span style="color: red">x</span><span style="color: blue">="http://schemas.microsoft.com/winfx/2006/xaml"
        </span><span style="color: red">xmlns</span><span style="color: blue">:</span><span style="color: red">telerik</span><span style="color: blue">="http://schemas.telerik.com/2008/xaml/presentation"
</span></pre>

And I can use everywhere for all Telerik RadControls for WPF with the prefix **telerik** 

<pre class="code"><span style="color: blue">&lt;</span><span style="color: #a31515">telerik</span><span style="color: blue">:</span><span style="color: #a31515">RadComboBox </span><span style="color: red">ItemsSource</span><span style="color: blue">="{</span><span style="color: #a31515">Binding </span><span style="color: red">ProductNodeLabels</span><span style="color: blue">}"
                        </span><span style="color: red">SelectedItem</span><span style="color: blue">="{</span><span style="color: #a31515">Binding </span><span style="color: red">SelectedProductNodeLabel</span><span style="color: blue">}"
                        </span><span style="color: red">Margin</span><span style="color: blue">="0,0,0,5" /&gt;
</span></pre>

A little step but a nice improvement!
