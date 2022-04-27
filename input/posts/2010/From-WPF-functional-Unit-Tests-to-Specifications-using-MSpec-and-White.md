---
title: "From WPF functional Unit Tests to Specifications using MSpec and White"
permalink: /2010/06/11/From-WPF-functional-Unit-Tests-to-Specifications-using-MSpec-and-White/
date: 6/11/2010 3:21:22 AM
updated: 6/13/2010 8:09:10 PM
disqusIdentifier: 20100611032122
tags: ["white", "WPF", "unit test", "MSpec"]
---
I am in the train back home and wanted to try out quickly to migrate our WPF functional tests written has Unit Tests to BDD Specifications. 

Here is the code I started from, pure Unit Test using NUnit and White
<!-- more -->

```csharp
[Test]
public void Opening_Valid_VersionZip()
{
    OpenAndWait("Product.zip");

    Assert.That(MainWindow.Title.Equals("Product.zip - Innoveo Skye® Editor"));
    Assert.That(Status.Text.Equals("product"));
    Assert.That(ProductTree.Nodes.Count >= 1);
    Assert.IsFalse(SplashScreen.Visible);
    Assert.IsTrue(SaveButton.Enabled);
    Assert.IsTrue(ActivateButton.Enabled);
}
```

Now the same functional test written as a BDD specification using MSpec

```csharp
[Subject("OpenVersionZip")]
public class when_user_open_valid_versionzip : MainWindowViewSpecs
{
    Establish context = () => {};

    Because of = () => OpenAndWait("Product.zip");

    It should_display_mainwindow_title_correctly = () =>
        MainWindow.Title.ShouldEqual("Product.zip - Innoveo Skye® Editor");

    It should_display_status_correctly = () =>
        Status.Text.ShouldEqual("product");

    It should_display_the_product_tree = () =>
        ProductTree.Nodes.ShouldNotBeNull();

    It should_hide_the_splashscreen = () =>
        SplashScreen.Visible.ShouldBeFalse();

    It should_enable_save_button = () =>
        SaveButton.Enabled.ShouldBeTrue();

    It should_enable_activate_button = () =>
        ActivateButton.Enabled.ShouldBeTrue();
}
```

And the output in ReSharper MSpec plugin

![From WPF functional Unit Tests to Specifications using MSpec and White](https://farm2.staticflickr.com/1586/24553233906_c38b6e933a_o.png) 

Which one do you prefer? I personally have made my choice.
