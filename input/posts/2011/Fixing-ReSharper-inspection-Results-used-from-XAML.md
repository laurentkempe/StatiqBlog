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

```xml
<UserControl x:Class="skyeEditor.View.StatusBarView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
             xmlns:ViewModel="clr-namespace:skyeEditor.ViewModel"
             mc:Ignorable="d"
             d:DataContext="{d:DesignInstance Type=ViewModel:StatusBarViewModel}">
```

This for sure helps ReSharper code inspection as you can see on the following screenshot

![](http://farm7.static.flickr.com/6056/6334138827_993d3fea14_o.png)

Now I can navigate from View to ViewModel, can avoid to have developer deleting properties that are sued in XAML bindings, but I can also get an idea of where a ViewModel property is used by using ReSharper **Find Usage** (Alt-F7). I see right away that the StatusBarView.xaml is using the property ShowActivityProgress.

![](http://farm7.static.flickr.com/6230/6334152313_b13b7a1394_o.png)

So don’t forget to add the d:DataContext into your XAML !
