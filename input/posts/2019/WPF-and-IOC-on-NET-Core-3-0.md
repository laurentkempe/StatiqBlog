---
title: 'WPF and IOC on .NET Core 3.0'
permalink: /2019/04/18/WPF-and-IOC-on-NET-Core-3-0/
disqusIdentifier: 20190418105756
coverSize: partial
tags:
  - WPF
  - .NET Core
coverCaption: 'Atimaono, Tahiti, France'
coverImage: 'https://live.staticflickr.com/4410/36576108926_d296e0fa6b_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/4410/36576108926_04e831cc92_q.jpg'
date: 2019-04-18 10:57:56
---
At work, we are planning to migrate our WPF application from .NET Framework 4.7 to [.NET Core 3.0](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-0). The main reason for doing so is that it was always a big pain to organize the updates of the .NET Framework on our customer machines. So being able to bundle .NET Core with our application is a big plus for us. Then, for sure, we are looking for the performance improvements brought by .NET Core and finally the new capabilities brought by the fast pace of innovation of .NET Core.
<!-- more -->
This is a great opportunity for us to see and renew a part of the libraries we depend on.  

In the current version of our application, we are using [StructureMap](https://github.com/structuremap/structuremap) as our [IOC container](https://en.wikipedia.org/wiki/Inversion_of_control). As you might have read [StructureMap has been sunsetted](https://jeremydmiller.com/2018/01/29/sunsetting-structuremap/) and replaced by [Lamar](https://jasperfx.github.io/lamar/).

We could have gone to Lamar as it maintains API compatibility with SturtureMap. I just did not want to bring the dependency to Roslyn, but it seems that it was changed in [Lamar 3.0](https://jeremydmiller.com/2019/03/29/lamar-v3-is-released-faster-smaller-quicker-cold-starts-internal-type-friendly/). So, we surely will consider it.

As a first try, I would like to have a look at the IOC container brought by the ASP.NET team; [Microsoft Extensions DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/3.0.0-preview4.19216.2). Even if it was brought by the ASP.NET team it doesn't mean at all that it cannot be used outside of ASP.NET!
It is marked as Fast by [Daniel Palme](https://twitter.com/danielpalme) in his post '[IoC Container Benchmark - Performance comparison](http://www.palmmedia.de/blog/2011/8/30/ioc-container-benchmark-performance-comparison)', so it is a good candidate.

The goal of this post is to see how we could use Microsoft Extensions DependencyInjection in a .NET Core 3.0 with WPF. We will start really easily by creating a new WPF application with the .NET Core CLI.

# Project creation

We create our project from the command line.

{% codeblock lang:shell %}
❯ mkdir WpfIoc
❯ cd WpfIoc
❯ dotnet.exe --version
3.0.100-preview4-011223

❯ dotnet new wpf
The template "WPF Application" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on C:\Users\laure\projects\WpfIoc\WpfIoc.csproj...
  Restore completed in 90.03 ms for C:\Users\laure\projects\WpfIoc\WpfIoc.csproj.

Restore succeeded.

❯ dotnet build
Microsoft (R) Build Engine version 16.1.54-preview+gd004974104 for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 19.92 ms for C:\Users\laure\projects\WpfIoc\WpfIoc.csproj.
C:\Program Files\dotnet\sdk\3.0.100-preview4-011223\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(151,5): message NETSDK1057: You are using a preview version of .NET Core. See: https://aka.ms/dotnet-core-preview [C:\Users\laure\projects\WpfIoc\WpfIoc.csproj]
  WpfIoc -> C:\Users\laure\projects\WpfIoc\bin\Debug\netcoreapp3.0\WpfIoc.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:01.63
{% endcodeblock %}

What we want to achieve is to bootstrap the application and inject in the constructor of our MainWindow a service which will be called to display some text on the main window of the application.

# Code

First, we need to add the reference to the *Microsoft Extensions DependencyInjection*.

{% codeblock lang:xml WpfIoc.csproj %}
<Project Sdk="Microsoft.NET.Sdk.WindowsDesktop">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>netcoreapp3.0</TargetFramework>
    <UseWPF>true</UseWPF>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="3.0.0-preview4.19216.2" />  
  </ItemGroup>

</Project>
{% endcodeblock %}

Then we create an interface *ITextService* which will be injected by the container into the MainWindow class.

{% codeblock lang:csharp ITextService.cs %}
public interface ITextService
{
    string GetText();
}
{% endcodeblock %}

 In fact, it is a concrete implementation of that interface which will be injected *TextService*.

{% codeblock lang:csharp TextService.cs %}
class TextService : ITextService
{
    private string _text;

    public TextService(string text)
    {
        _text = text;
    }
    
    public string GetText()
    {
        return _text;
    }
}
{% endcodeblock %}

Then we need to configure our IOC container.

{% codeblock lang:csharp App.xaml.cs %}
services.AddSingleton<ITextService>(provider => new TextService("Hi WPF .NET Core 3.0!"));
{% endcodeblock %}

So this also means that we need to have our IOC container creating our WPF MainWindow, no problem it is just another normal C# class.

{% codeblock lang:csharp App.xaml.cs %}
services.AddSingleton<MainWindow>();
{% endcodeblock %}

Next piece which we need to put in place is the one linking all the other pieces together; the IOC container! That's quite easy we just need to extend the App class to create *ServiceCollection* add the dependencies we want the IOC container to manage and then to call *BuildServiceProvider*.  

{% codeblock lang:csharp App.xaml.cs %}
public App()
{
    var serviceCollection = new ServiceCollection();
    ConfigureServices(serviceCollection);

    _serviceProvider = serviceCollection.BuildServiceProvider();
}

private void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<ITextService>(provider => new TextService("Hi WPF .NET Core 3.0!"));
    services.AddSingleton<MainWindow>();
}
{% endcodeblock %}

Then, on the *App_OnStartup* we are using the ServiceProvider to get an instance of *MainWindow* which would get the *ITextService* injected in its constructor.  

{% codeblock lang:csharp App.xaml.cs %}
private void App_OnStartup(object sender, StartupEventArgs e)
{
    var mainWindow = _serviceProvider.GetService<MainWindow>();
    mainWindow.Show();
}
{% endcodeblock %}

We also modified *App.xaml* to call *App_OnStartup*.

{% codeblock lang:xml App.xaml %}
<Application x:Class="wpfioc.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:wpfioc"
             Startup="App_OnStartup">
    <Application.Resources>
    </Application.Resources>
</Application>
{% endcodeblock %}

Finally, we modify the XAML of the MainWindow to display some text in a Label.

{% codeblock lang:xml MainWindow.xaml %}
<Window x:Class="WpfIoc.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfIoc"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="9*" />
            <RowDefinition Height="1*" />
        </Grid.RowDefinitions>
        <Label Name="Label" Content="Hello .NET Core!" HorizontalAlignment="Center" VerticalAlignment="Center" FontSize="40" />
    </Grid>
</Window>
{% endcodeblock %}

And we inject through the constructor, the *ITextService* interface which is used to set the Label text.

{% codeblock lang:csharp MainWindow.xaml.cs %}
    public partial class MainWindow : Window
    {
        public MainWindow(ITextService textService)
        {
            InitializeComponent();

            Label.Content = textService.GetText();
        }
    }
{% endcodeblock %}

# Result

We can run the application and see that *TextService* is called and the WPF application correctly displays the text.

![WPF IOC](https://live.staticflickr.com/65535/40675205763_ab0cd3c28b_o.png")

You can get all the code on GitHub 
{% githubCard user:laurentkempe repo:WpfIoc align:left %}
