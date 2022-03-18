---
title: 'Unit testing Async WPF ICommand'
permalink: /2022/02/10/unit-testing-async-wpf-icommand/
date: 02/10/2022 20:37:38
disqusIdentifier: 20220210083738
coverSize: partial
tags: [.NET, C#, WPF, unit test]
coverCaption: 'North Mona Vale, Australia'
coverImage: 'https://live.staticflickr.com/65535/51872954907_2104a32913_h.jpg'
thumbnailImage: 'https://live.staticflickr.com/65535/51872954907_ef93021c01_q.jpg'
---
In the past, within my team at [Innoveo](http://www.innoveo.com/), we had several discussions about the best way to unit test async WPF ICommand. We value quality, so testing is essential to us. We decided to make the methods called by the command `internal` so that our tests could call those.

What is the problem with unit testing an Async WPF ICommand? The problem is that the command is an `async void` method! So, you have no way to `await` the end of the execution of your command. So, your test might assert on things that are still executing.
<!-- more -->
Some weeks ago, a question about "Extract and Override call" taken from the book "Working Effectively with Legacy Code" for testing was raised on the [French DevApps community](https://devapps.ms/devenirmembre). I immediately thought of the discussions about testing async WPF ICommand and proposed that solution as a pragmatic solution that is not only easy to implement but also very simple to understand. Some did not like that solution because it was not perfect. I agree, but sometimes it is good to be pragmatic.

The discussion went further about `async void` testing and [Gérald Barré](https://twitter.com/meziantou) from [Meziantou's blog](https://meziantou.net/), which I highly recommend, answered it interestingly! He made a blog post about his solution "[Awaiting an async void method in .NET](https://www.meziantou.net/awaiting-an-async-void-method-in-dotnet.htm)" which leverage his own implementation of a [SynchronizationContext](https://docs.microsoft.com/en-us/dotnet/api/system.threading.synchronizationcontext?view=net-6.0) and `TaskCompletionSource`.

I decided to apply his solution to our original question; "How to test an async WPF ICommand"?

I quickly created a small .NET 6 WPF project and added to it the [Windows Community Toolkit](https://docs.microsoft.com/en-us/windows/communitytoolkit/) [MVVM](https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/introduction) nuget. I copy-pasted the code from Gérald to it and wrote the following tests.

```csharp
[TestFixture]
public class MainWindowViewModelTests
{
    [Test]
    public void Click_FailingToAwaitCommandExecution()
    {
        var mainWindowViewModel = new MainWindowViewModel();

        mainWindowViewModel.Click.Execute(null);
        
        Assert.That(mainWindowViewModel.Upper, Is.EqualTo("BEFORE"));
    }

    [Test]
    public async Task Click_ExpectUpperToBeUpperCase()
    {
        var mainWindowViewModel = new MainWindowViewModel();

        await AsyncVoidSynchronizationContext.Run(
            () => mainWindowViewModel.Click.Execute(null));
        
        Assert.That(mainWindowViewModel.Upper, Is.EqualTo("BEFORE"));
    }
}
```

The first one fails because nothing awaits the execution of the WPF command. The second one succeeds because the test is awaiting the command execution. Nice, very nice!

And here is the code of the `MainWindowViewModel` class.

```csharp {data-file=MainWindowViewModel.cs}
public class MainWindowViewModel : ObservableObject
{
    private ICommand? _click;
    private string _upper = "before";

    public ICommand Click => _click ??= new AsyncRelayCommand(Execute);

    public string Upper
    {
        get => _upper;
        set => SetProperty(ref _upper, value);
    }

    private async Task Execute()
    {
        await Task.Delay(2000);

        Upper = Upper.ToUpper();
    }
}
```

So, that is one way of solving testing an async WPF ICommand. But it is not the only way.

Windows Community Toolkit MVVM provides the `AsyncRelayCommand` which implements `IAsyncRelayCommand` providing an `ExecuteAsync` method returning a `Task` which can be awaited.

```csharp
[Test]
public async Task OtherClick_ExpectUpperToBeUpperCase()
{
    var mainWindowViewModel = new MainWindowViewModel();

    await mainWindowViewModel.OtherClick.ExecuteAsync(null);
    
    Assert.That(mainWindowViewModel.Upper, Is.EqualTo("BEFORE"));
}
```

And in this case we need to declare the command as an `IAsyncRelayCommand`

```csharp
    private IAsyncRelayCommand? _otherClick;

    public IAsyncRelayCommand OtherClick
        => _otherClick ??= new AsyncRelayCommand(Execute);
```

and use the `ExecuteAsync` method in our test

```csharp
[Test]
public async Task OtherClick_ExpectUpperToBeUpperCase()
{
    var mainWindowViewModel = new MainWindowViewModel();

    await mainWindowViewModel.OtherClick.ExecuteAsync(null);
    
    Assert.That(mainWindowViewModel.Upper, Is.EqualTo("BEFORE"));
}
```

# Conclusion

As a developer, I think it is always good to be pragmatic but this does not avoid to search for better solutions.

<?# githubCard user=laurentkempe repo=AsyncVoidCommandTesting align=left /?>
