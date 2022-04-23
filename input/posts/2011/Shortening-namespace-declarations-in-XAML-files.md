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


```xaml
<Window x:Class="skyeEditor.View.Dialogs.ProductSettingsDialog"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:RadInput="clr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls.Input"
        xmlns:Rad="clr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls"
```

Searching for a solution I thought of ASP.NET in which you can declare in the web.config things like that:

```xml
<pages pageBaseType=”System.Web.Mvc.WebViewPage”>
    <namespaces>
        <add namespace=”MyNamespace” />
```

My goal was to have one namespace declaration for example **telerik** and be able to use it in all my XAML files without having to declare too much. 

So I started an online discussion with [Laurent Bugnion](http://www.galasoft.ch/intro_en.html) who has always shown me the good direction, which in this case is the 

> ### [XmlnsDefinitionAttribute](http://msdn.microsoft.com/en-us/library/system.windows.markup.xmlnsdefinitionattribute.aspx) Class

Followed by an article from [Sandino Di Mattia](http://blog.sandrinodimattia.net/) about “[A Guide to Cleaner XAML with Custom Namespaces and Prefixes (WPF/Silverlight)](http://www.codeproject.com/KB/silverlight/xaml_custom_namespaces.aspx?adcid=2499&azid=85&PageFlow=FixedWidth)”.

Using the idea I was able to add the following to the **AssemblyInfo.cs** of my own project:

```csharp
[assembly: XmlnsDefinition("telerik",
                           "Telerik.Windows.Controls",
                           AssemblyName = "Telerik.Windows.Controls")]

[assembly: XmlnsDefinition("telerik",
                           "Telerik.Windows.Controls",
                           AssemblyName = "Telerik.Windows.Controls.Input")]

[assembly: XmlnsDefinition("telerik",
                           "Telerik.Windows.Controls",
                           AssemblyName = "Telerik.Windows.Controls.Navigation")]

[assembly: XmlnsDefinition("telerik",
                           "Telerik.Windows.Controls",
                           AssemblyName = "Telerik.Windows.Controls.RibbonView")]
```
And then my XAML file declaration looks like that:


```xml
<Window x:Class="skyeEditor.View.Dialogs.ProductSettingsDialog"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:telerik="http://schemas.telerik.com/2008/xaml/presentation"
```

And I can use everywhere for all Telerik RadControls for WPF with the prefix **telerik** 

```xml
<telerik:RadComboBox ItemsSource="{Binding ProductNodeLabels}"
                     SelectedItem="{Binding SelectedProductNodeLabel}"
                     Margin="0,0,0,5" />
```

A little step but a nice improvement!
