---
title: "Missing a little FileAccess.Read and it doesn’t work"
permalink: /2010/05/22/Missing-a-little-FileAccessRead-and-it-doesne28099t-work/
date: 5/22/2010 1:19:21 AM
updated: 5/22/2010 1:19:21 AM
disqusIdentifier: 20100522011921
tags: [".NET Framework 3.5"]
---
What is the difference between this code

```csharp
using (var fileStream = new FileStream(settingsFilename, FileMode.Open))
{
    return ReadSettings(fileStream);
}
```

And this code

```csharp
using (var fileStream = new FileStream(settingsFilename, FileMode.Open, FileAccess.Read))
{
    return ReadSettings(fileStream);
}
```

Almost nothing, just a little **FileAccess.Read** !

But this little thing makes a big difference when you run your software in a secured environment.

The application with this piece of code was deployed to a customer reporting that the application was crashing at a the point of reading the settings. Weird, really weird. After getting back the log and I finally discovered that using juste FileMode.Open needs modify rights and that’s was the issue because the customer deploy the settings file on a folder in which the user doesn’t have the modify rights.
