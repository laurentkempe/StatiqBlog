---
title: "From WPF functional Unit Tests to Specifications using MSpec and White"
permalink: /2010/06/11/From-WPF-functional-Unit-Tests-to-Specifications-using-MSpec-and-White/
date: 6/11/2010 3:21:22 AM
updated: 6/13/2010 8:09:10 PM
disqusIdentifier: 20100611032122
tags: ["white", "WPF", "unit test", "MSpec"]
alias:
 - /post/From-WPF-functional-Unit-Tests-to-Specifications-using-MSpec-and-White.aspx/index.html
---
I am in the train back home and wanted to try out quickly to migrate our WPF functional tests written has Unit Tests to BDD Specifications. 

Here is the code I started from, pure Unit Test using NUnit and White
<!-- more -->
<div style="line-height:135%; padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:4955d68a-7a42-4fa8-bd02-2ff18f2492fe" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background-color: #ffffff; max-height: 500px; overflow: auto; padding: 2px 5px; white-space: nowrap">[<span style="color:#2b91af">Test</span>]<br> <span style="color:#0000ff">public</span> <span style="color:#0000ff">void</span> Opening_Valid_VersionZip()<br> {<br> &nbsp;&nbsp;&nbsp;&nbsp;OpenAndWait(<span style="color:#a31515">"Product.zip"</span>);<br> <br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">Assert</span>.That(MainWindow.Title.Equals(<span style="color:#a31515">"Product.zip - Innoveo Skye® Editor"</span>));<br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">Assert</span>.That(Status.Text.Equals(<span style="color:#a31515">"product"</span>));<br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">Assert</span>.That(ProductTree.Nodes.Count &gt;= 1);<br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">Assert</span>.IsFalse(SplashScreen.Visible);<br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">Assert</span>.IsTrue(SaveButton.Enabled);<br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">Assert</span>.IsTrue(ActivateButton.Enabled);<br> }</div> </div> </div>

Now the same functional test written as a BDD specification using MSpec

<div style="line-height:135%; padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:434f1161-ac6a-49fc-8c62-127913130898" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background-color: #ffffff; max-height: 500px; overflow: auto; padding: 2px 5px;">[<span style="color:#2b91af">Subject</span>(<span style="color:#a31515">"OpenVersionZip"</span>)]<br> <span style="color:#0000ff">public</span> <span style="color:#0000ff">class</span> <span style="color:#2b91af">when_user_open_valid_versionzip</span> : <span style="color:#2b91af">MainWindowViewSpecs</span><br> {<br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">Establish</span> context = () =&gt; {};<br> <br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">Because</span> of = () =&gt; OpenAndWait(<span style="color:#a31515">"Product.zip"</span>);<br> <br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">It</span> should_display_mainwindow_title_correctly = () =&gt; <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MainWindow.Title.ShouldEqual(<span style="color:#a31515">"Product.zip - Innoveo Skye® Editor"</span>);<br> <br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">It</span> should_display_status_correctly = () =&gt; <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Status.Text.ShouldEqual(<span style="color:#a31515">"product"</span>);<br> <br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">It</span> should_display_the_product_tree = () =&gt; <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProductTree.Nodes.ShouldNotBeNull();<br> <br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">It</span> should_hide_the_splashscreen = () =&gt; <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SplashScreen.Visible.ShouldBeFalse();<br> <br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">It</span> should_enable_save_button = () =&gt; <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SaveButton.Enabled.ShouldBeTrue();<br> <br> &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">It</span> should_enable_activate_button = () =&gt; <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActivateButton.Enabled.ShouldBeTrue();<br> }</div> </div> </div>

And the output in ReSharper MSpec plugin

![From WPF functional Unit Tests to Specifications using MSpec and White](https://farm2.staticflickr.com/1586/24553233906_c38b6e933a_o.png) 

Which one do you prefer? I personally have made my choice.
