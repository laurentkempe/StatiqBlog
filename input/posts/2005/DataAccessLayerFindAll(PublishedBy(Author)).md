---
title: "DataAccessLayer.FindAll(PublishedBy(Author))"
permalink: /2005/03/10/DataAccessLayerFindAll(PublishedBy(Author))/
date: 3/10/2005 7:43:00 AM
updated: 3/10/2005 7:43:00 AM
disqusIdentifier: 20050310074300
tags: ["Whidbey", "ASP.NET 2.0", ".NET Framework 2.0"]
---

One of the first development rule is to **write human readable code**. Why ? To avoid to have to write comments ;-) Because it ***will be read as a spoken language*** and ***it separate important code from distracting one***.
<!-- more -->

What do you think about that line of code:

```csharp
return DAL.FindAll(PublishedBy(Author));
```

Someone that has no clue about development might even read that code. Ok except the DAL word ;-)

With C# 2.0 you are able to write readable code like this using Generics, and 
the power of [closures](http://martinfowler.com/bliki/Closures.html). 
In C# 2.0 closure are supported through the form of anonymous delegates.

Check out the different methods returning collection accordign to predefined 
rules: **getAllUnpublished**(), **getAllPublished**(), 
**getAllPublishedBy**()... There are all using Predicate, so to say 
anonymous delegate, that gives you human readable code.

Client code:

```csharp
protected void Page_Load(object sender, EventArgs e)
{
    GridView2.DataSource = TechHeadBrothers.Portal.BLL.ArticleBLL.getAllPublishedBy(<font color="maroon">"Laurent Kempé");
    GridView2.DataBind();

    GridView3.DataSource = TechHeadBrothers.Portal.BLL.ArticleBLL.getAllWaitPublishingBy(<font color="maroon">"Laurent Kempé");
    GridView3.DataBind();
}
```

The Business Layer:


```csharp
#region Using

using System;
using System.Collections.Generic;
using System.Text;
using TechHeadBrothers.Portal.Entities;
using TechHeadBrothers.Portal.DAL; 

#endregion
 
namespace TechHeadBrothers.Portal.BLL
{
    public static class ArticleBLL
    {
        static ArticleDAL DAL = new ArticleDAL();

        public static List<Article> getAll()
        {
            return DAL.GetAll();
        }

        public static List<Article> getAllUnpublished()
        {
            return DAL.FindAll(NotPublished());
        }

        public static List<Article> getAllPublished()
        {
            return DAL.FindAll(Published());
        }

        public static List<Article> getAllPublishedBy(string Author)
        {
            return DAL.FindAll(PublishedBy(Author));
        }

        public static List<Article> getAllWaitPublishingBy(string Author)
        {
            return DAL.FindAll(WaitPublishingBy(Author));
        }

        protected static Predicate<Article> WaitPublishingBy(string author)
        {
            return delegate(Article a)
            {
                return !a.isPublished && (a.Author.ToLower() == author.ToLower());
            };
        }

        protected static Predicate<Article> PublishedBy(string author)
        {
            return delegate(Article a)
            {
                return a.isPublished && (a.Author.ToLower() == author.ToLower());
            };
        }

        protected static Predicate<Article> Published()
        {
            return delegate(Article a)
            {
                return a.isPublished;
            };
        }

        protected static Predicate<Article> NotPublished()
        {
            return delegate(Article a)
            {
                return (!a.isPublished);
            };
        }
    }
}
 
```

The Entity class is represented as:

```csharp
#region Using

using System;
using System.Collections.Generic;
using System.Text; 

#endregion

namespace TechHeadBrothers.Portal.Entities
{
    public class Article
    {
        private Guid uuid;

        public Guid Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private bool ispublished;

        public bool isPublished
        {
            get { return ispublished; }
            set { ispublished = value; }
        }

        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public Article(Guid uuid, string title, string description, string author)
        {
            this.Uuid = uuid;
            this.Title = title;
            this.Description = description;
            this.isPublished = <font color="maroon">false;
            this.Author = author;
        }

        public Article(string uuid, string title, string description, string author)
            : this(new Guid (uuid), title, description, author)
        {

        }

        public Article(string title, string description, string author)
            : this(Guid.NewGuid(), title, description, author)
        {

        }
    }
} 
```

And the Data Access Layer Article class:

```csharp
#region Using

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace TechHeadBrothers.Portal.DAL
{
    interface IDataAccess<T>
    {
        T Get(Guid uuid);

        T Get(string uuid);

        List<T> GetAll();

        List<T> FindAll(Predicate<T> match);
    }

    public class ArticleDAL : IDataAccess<TechHeadBrothers.Portal.Entities.Article>
    {
        List<TechHeadBrothers.Portal.Entities.Article> articles;

        public ArticleDAL(List<TechHeadBrothers.Portal.Entities.Article> articles)
        {
            this.articles = articles;
        }

        public ArticleDAL()
        {
            this.articles = new List<TechHeadBrothers.Portal.Entities.Article>();

            <font color="green">//TODO: Remove this is just for test
            this.articles.Add(
                new TechHeadBrothers.Portal.Entities.Article(<font color="maroon">"Les generics dans C#",
                <font color="maroon">"Démonstration de l'utilisation des générics dans C# 2.0",
                <font color="maroon">"Laurent Kempé"));

            TechHeadBrothers.Portal.Entities.Article article =
                new TechHeadBrothers.Portal.Entities.Article(<font color="maroon">"8BF7FEBE-9FEB-4db6-86B7-70A6C44B1CAA",
                <font color="maroon">"Les iterators dans C#",
                <font color="maroon">"Démonstration de l'utilisation des iterators dans C# 2.0",
                <font color="maroon">"Laurent Kempé");
            article.isPublished = <font color="maroon">true;
            this.articles.Add(article);
        }

        #region IDataAccess<Article> Members

        public TechHeadBrothers.Portal.Entities.Article Get(Guid uuid)
        {
            for (int i = <font color="maroon">0; i < articles.Count; i++)
            {
                if (articles[i].Uuid == uuid)
                {
                    return articles[i];
                }
            }

            return null;
        }

        public TechHeadBrothers.Portal.Entities.Article Get(string uuid)
        {
            return this.Get(new Guid(uuid));
        }

        public List<TechHeadBrothers.Portal.Entities.Article> GetAll()
        {
            return articles;
        }

        public List<TechHeadBrothers.Portal.Entities.Article> FindAll(Predicate<TechHeadBrothers.Portal.Entities.Article> match)
        {
            return articles.FindAll(match);
        }

        #endregion
    }
}
```

[ Currently Playing : Sound Enforcer / Impact - Carl Cox (04:10) ]
