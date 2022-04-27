---
title: "The beauty of C# and Linq"
permalink: /2010/01/26/The-beauty-of-C-and-Linq/
date: 1/26/2010 5:57:12 AM
updated: 1/26/2010 5:57:12 AM
disqusIdentifier: 20100126055712
tags: ["C#", "Linq"]
---
Today I faced the following challenge to solve: return all possible combinations of three source collections.

We are using C# and with Linq it was just so easy.
<!-- more -->

```csharp
public List<string> Contexts
{
    get
    {
        var result = from u in SelectedUseCases
                     from c in SelectedChannels
                     from up in SelectedUserProfiles
                     select string.Format("{0}-{1}-{2}", u.Value, c.Value, up.Value);
 
        return result.ToList();
    }
}
```

Simple and beautiful!
