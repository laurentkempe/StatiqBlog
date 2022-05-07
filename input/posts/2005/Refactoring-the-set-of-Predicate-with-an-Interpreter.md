---
title: "Refactoring the set of Predicate with an Interpreter"
permalink: /2005/03/12/Refactoring-the-set-of-Predicate-with-an-Interpreter/
date: 3/12/2005 9:09:00 AM
updated: 3/12/2005 9:09:00 AM
disqusIdentifier: 20050312090900
tags: ["Tech Head Brothers", "Whidbey", "ASP.NET 2.0", ".NET Framework 2.0"]
---

This evening I continued my journey with the [C# Generics](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnvs05/html/csharp_generics.asp). 
I refactored what I described in my post "[DataAccessLayer.FindAll(PublishedBy(Author))](http://weblogs.asp.net/lkempe/archive/2005/03/09/391247.aspx)" to be able to use the [Design Pattern Interpreter](http://www.dofactory.com/Patterns/PatternInterpreter.aspx).
<!-- more -->

The result in the ASPX code behind file is the following:

```csharp
AndSpec spec = new AndSpec(
                        new AndSpec(
                            new PublishedSpec(),
                            new BeforeDateSpec(DateTime.Parse("01/01/2005"))
                        ),
                        new OrSpec(
                            new AuthorSpec("Mathieu Kempé"),
                            new AuthorSpec("Laurent Kempé")
                        )
                    );

DataAccessor<Article> articles = new DataAccessor<Article>("GetArticles");
GridView3.DataSource = articles.FindAll(Matching(spec));
GridView3.DataBind();
```

With the unique Matching Predicate, replacing all the other Predicate:

```csharp
protected Predicate<Article> Matching(Spec spec)
{
    return delegate(Article a)
    {
        return spec.isSatisfiedBy(a);
    };
}
```

To achieve this I first added two more properties to my Entity, Article. I 
did not added new constructors because those two properties are not 
mandatory:

```csharp
private DateTime datePublished;

public DateTime DatePublished
{
    get { return datePublished; }
    set { datePublished = value; }
}

private DateTime dateModified;

public DateTime DateModified
{
    get { return dateModified; }
    set { dateModified = value; }
}
```
Then I modified my Data Access Layer to have it a bit more generic, starting with the interface :

```csharp
interface IDataAccess<T>
{
    List<T> GetAll();

    List<T> FindAll(Predicate<T> match);
}
```

I removed the method `T Get(Guid uuid)` and `T Get(string uuid)` because that can be easily expressed with a Predicate.

I made a Generic implementation of my Data Access through a DataAccessor class:

```csharp
namespace TechHeadBrothers.Portal.DAL
{
    public class DataAccessor<T> : IDataAccess<T> where T : new()
    {
        List<T> list = null;

        public DataAccessor(string sp)
        {
            readFromDatabase(sp);
        }

        #region Protected Methods

        protected void readFromDatabase(string sp)
        {
            <font color="green">// Create Instance of Connection and Command Object
            SqlConnection myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["TechHeadBrothers"].ConnectionString);

            SqlDataAdapter myCommand = new SqlDataAdapter(sp, myConnection);

            <font color="green">// Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            <font color="green">// Create and Fill the DataSet
            DataSet ds = new DataSet();
            myCommand.Fill(ds);

            myConnection.Close();

            this.list = DataAdapterFactory.createAdapter<T>().Adapt(ds.Tables[0]);
        }

        #endregion

        #region IDataAccess<T> Members

        public List<T> GetAll()
        {
            return list;
        }

        public List<T> FindAll(Predicate<T> match)
        {
            return list.FindAll(match);
        }

        #endregion
    }
}
```

I had to implement an Adapter to convert the data from the Database representation to the Entity :


```csharp
abstract class DataAdapter<T> 
{
    public List<T> Adapt(DataTable table)
    {
        List<T> list = new List<T>(table.Rows.Count);

        foreach (DataRow row in table.Rows)
        {
            list.Add(this.Adapt(row));
        }

        return list;
    }

    public abstract T Adapt(DataRow row);
}
```

And a Factory :

```csharp
class DataAdapterFactory
{
    public static DataAdapter<T> createAdapter<T>() where T : new()
    {
        string name = new T().GetType().Name.ToLower();

        if ( name == "article")
            return new ArticleAdapter() as DataAdapter<T>;
        else if ( name == "author")
            return new AuthorAdapter() as DataAdapter<T>;

        return null;
    }
}
```

Here is the concrete implementation for the Article Entity Adapter:

```csharp
class ArticleAdapter : DataAdapter<Article>
{
    public override Article Adapt(DataRow row)
    {
        Article article = new Article((string)row["Title"],
                                      (string)row["Description"],
                                      (string)row["Author"],
                                        (bool)row["isPublished"],
                                        (Guid)row["uuid"]);

        if (row["DatePublished"] != DBNull.Value)
        {
            article.DatePublished = (DateTime)row["DatePublished"];
        }

        if (row["DateModified"] != DBNull.Value)
        {
            article.DateModified = (DateTime)row["DateModified"];
        }

        return article;
    }
}
```

I made also a Business Layer generic class, but has it is just for the moment a wrapper around the DataAccessor, I will not show it.

And finally the [Interpreter Design Pattern](http://www.dofactory.com/Patterns/PatternInterpreter.aspx):

```csharp
public abstract class Spec
{
    public abstract bool isSatisfiedBy(Article article);
}
```

With the different concrete Specifications:

```csharp
public class PublishedSpec : Spec
{
    public PublishedSpec()
    {
    }

    public override bool isSatisfiedBy(Article article)
    {
         return (article.isPublished == true);
    }
}

public class BeforeDateSpec : Spec
{
    private DateTime date;

    public DateTime Date
    {
        get { return date; }
        set { date = value; }
    }

    public BeforeDateSpec(DateTime date)
    {
        this.Date = date;
    }

    public override bool isSatisfiedBy(Article article)
    {
        return (article.DatePublished < this.Date);
    }
}

public class AuthorSpec : Spec
{
    private string author;

    public string Author
    {
      get { return author;}
      set { author = value; }
    }

    public AuthorSpec (string author)
    {
        this.Author = author;
    }

    public override bool isSatisfiedBy(Article article)
    {
         return (article.Author == this.Author);
    }
}

public class NotSpec : Spec
{
    private Spec specToNegate;

    public NotSpec(Spec specToNegate)
    {
        this.specToNegate = specToNegate;
    }

    public override bool isSatisfiedBy(Article article)
    {
        return !specToNegate.isSatisfiedBy(article);
    }
}

public class AndSpec : Spec
{
    private Spec augend;
    public Spec Augend
    {
      get { return augend;}
    }

    private Spec addend;
    public Spec Addend
    {
      get { return addend;}
    }
    
    public AndSpec (Spec augend, Spec addend)
    {
        this.augend = augend;
        this.addend = addend;
    }

    public override bool  isSatisfiedBy(Article article)
    {
         return Augend.isSatisfiedBy(article) && Addend.isSatisfiedBy(article);
    }
}

public class OrSpec : Spec
{
    private Spec augend;
    public Spec Augend
    {
        get { return augend; }
    }

    private Spec addend;
    public Spec Addend
    {
        get { return addend; }
    }

    public OrSpec(Spec augend, Spec addend)
    {
        this.augend = augend;
        this.addend = addend;
    }

    public override bool isSatisfiedBy(Article article)
    {
        return Augend.isSatisfiedBy(article) || Addend.isSatisfiedBy(article);
    }
}
```