---
title: "Refactoring the set of Predicate with an Interpreter"
permalink: /2005/03/12/Refactoring-the-set-of-Predicate-with-an-Interpreter/
date: 3/12/2005 9:09:00 AM
updated: 3/12/2005 9:09:00 AM
disqusIdentifier: 20050312090900
tags: ["Tech Head Brothers", "Whidbey", "ASP.NET 2.0", ".NET Framework 2.0"]
alias:
 - /post/Refactoring-the-set-of-Predicate-with-an-Interpreter.aspx/index.html
---



This evening I continued my journey with the [C# Generics](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnvs05/html/csharp_generics.asp). 
I refactored what I described in my post "[DataAccessLayer.FindAll(PublishedBy(Author))](http://weblogs.asp.net/lkempe/archive/2005/03/09/391247.aspx) 
" to be able to use the [Design 
Pattern Interpreter](http://www.dofactory.com/Patterns/PatternInterpreter.aspx).
<!-- more -->

The result in the ASPX code behind file is the following:

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> AndSpec spec = <font color="blue">new</font> AndSpec(
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span>                         <font color="blue">new</font> AndSpec(
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span>                             <font color="blue">new</font> PublishedSpec(),
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span>                             <font color="blue">new</font> BeforeDateSpec(DateTime.Parse(<font color="maroon">"01/01/2005"</font>))
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span>                         ),
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span>                         <font color="blue">new</font> OrSpec(
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span>                             <font color="blue">new</font> AuthorSpec(<font color="maroon">"Mathieu Kempé"</font>),
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span>                             <font color="blue">new</font> AuthorSpec(<font color="maroon">"Laurent Kempé"</font>)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  9</span>                         )
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 10</span>                     );
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 11</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 12</span> DataAccessor<Article> articles = <font color="blue">new</font> DataAccessor<Article>(<font color="maroon">"GetArticles"</font>);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 13</span> GridView3.DataSource = articles.FindAll(Matching(spec));
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 14</span> GridView3.DataBind();

With the unique Matching Predicate, replacing all the other Predicate:

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">protected</font> Predicate<Article> Matching(Spec spec)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span>     <font color="blue">return</font> <font color="blue">delegate</font>(Article a)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span>         <font color="blue">return</font> spec.isSatisfiedBy(a);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span>     };
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span> }</pre>

To achieve this I first added two more properties to my Entity, Article. I 
did not added new constructors because those two properties are not 
mandatory:

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">private</font> DateTime datePublished;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span> <font color="blue">public</font> DateTime DatePublished
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span>     <font color="blue">get</font> { <font color="blue">return</font> datePublished; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span>     <font color="blue">set</font> { datePublished = value; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span> }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  9</span> <font color="blue">private</font> DateTime dateModified;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 10</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 11</span> <font color="blue">public</font> DateTime DateModified
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 12</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 13</span>     <font color="blue">get</font> { <font color="blue">return</font> dateModified; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 14</span>     <font color="blue">set</font> { dateModified = value; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 15</span> }

Then I modified my Data Access Layer to have it a bit more generic, starting with the interface :

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">interface</font> IDataAccess<T>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span>     List<T> GetAll();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span>     List<T> FindAll(Predicate<T> match);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span> }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span> </pre>

I removed the method "T Get(Guid uuid)" and "T Get(string uuid)" because that can be easily expressed with a Predicate.

I made a Generic implementation of my Data Access through a DataAccessor class:

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">namespace</font> TechHeadBrothers.Portal.DAL
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span>     <font color="blue">public</font> <font color="blue">class</font> DataAccessor<T> : IDataAccess<T> where T : <font color="blue">new</font>()
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span>         List<T> list = <font color="blue">null</font>;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span>         <font color="blue">public</font> DataAccessor(<font color="blue">string</font> sp)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  9</span>             readFromDatabase(sp);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 10</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 11</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 12</span>         <font color="blue">#region</font> Protected Methods
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 13</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 14</span>         <font color="blue">protected</font> <font color="blue">void</font> readFromDatabase(<font color="blue">string</font> sp)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 15</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 16</span>             <font color="green">// Create Instance of Connection and Command Object
</font><span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 17</span>             SqlConnection myConnection =
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 18</span>                 <font color="blue">new</font> SqlConnection(ConfigurationManager.ConnectionStrings[<font color="maroon">"TechHeadBrothers"</font>].ConnectionString);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 19</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 20</span>             SqlDataAdapter myCommand = <font color="blue">new</font> SqlDataAdapter(sp, myConnection);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 21</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 22</span>             <font color="green">// Mark the Command as a SPROC
</font><span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 23</span>             myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 24</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 25</span>             <font color="green">// Create and Fill the DataSet
</font><span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 26</span>             DataSet ds = <font color="blue">new</font> DataSet();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 27</span>             myCommand.Fill(ds);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 28</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 29</span>             myConnection.Close();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 30</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 31</span>             <font color="blue">this</font>.list = DataAdapterFactory.createAdapter<T>().Adapt(ds.Tables[<font color="maroon">0</font>]);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 32</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 33</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 34</span>         <font color="blue">#endregion</font>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 35</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 36</span>         <font color="blue">#region</font> IDataAccess<T> Members
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 37</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 38</span>         <font color="blue">public</font> List<T> GetAll()
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 39</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 40</span>             <font color="blue">return</font> list;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 41</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 42</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 43</span>         <font color="blue">public</font> List<T> FindAll(Predicate<T> match)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 44</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 45</span>             <font color="blue">return</font> list.FindAll(match);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 46</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 47</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 48</span>         <font color="blue">#endregion</font>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 49</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 50</span> }
</pre>

 </pre>

I had to implement an Adapter to convert the data from the Database representation to the Entity :</pre>

 </pre>

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">abstract</font> <font color="blue">class</font> DataAdapter<T> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span>     <font color="blue">public</font> List<T> Adapt(DataTable table)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span>         List<T> list = <font color="blue">new</font> List<T>(table.Rows.Count);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span>         <font color="blue">foreach</font> (DataRow row <font color="blue">in</font> table.Rows)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  9</span>             list.Add(<font color="blue">this</font>.Adapt(row));
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 10</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 11</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 12</span>         <font color="blue">return</font> list;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 13</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 14</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 15</span>     <font color="blue">public</font> <font color="blue">abstract</font> T Adapt(DataRow row);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 16</span> }</pre></pre>

 </pre>

And a Factory :</pre>

 </pre>

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">class</font> DataAdapterFactory
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span>     <font color="blue">public</font> <font color="blue">static</font> DataAdapter<T> createAdapter<T>() where T : <font color="blue">new</font>()
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span>         <font color="blue">string</font> name = <font color="blue">new</font> T().GetType().Name.ToLower();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span>         <font color="blue">if</font> ( name == <font color="maroon">"article"</font>)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span>             <font color="blue">return</font> <font color="blue">new</font> ArticleAdapter() <font color="blue">as</font> DataAdapter<T>;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  9</span>         <font color="blue">else</font> <font color="blue">if</font> ( name == <font color="maroon">"author"</font>)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 10</span>             <font color="blue">return</font> <font color="blue">new</font> AuthorAdapter() <font color="blue">as</font> DataAdapter<T>;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 11</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 12</span>         <font color="blue">return</font> <font color="blue">null</font>;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 13</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 14</span> }</pre>

 </pre>

Here is the concrete implementation for the Article Entity Adapter:</pre></pre></pre>

 </pre>

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">class</font> ArticleAdapter : DataAdapter<Article>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span>     <font color="blue">public</font> <font color="blue">override</font> Article Adapt(DataRow row)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span>         Article article = <font color="blue">new</font> Article((<font color="blue">string</font>)row[<font color="maroon">"Title"</font>],
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span>                                       (<font color="blue">string</font>)row[<font color="maroon">"Description"</font>],
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span>                                       (<font color="blue">string</font>)row[<font color="maroon">"Author"</font>],
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span>                                         (<font color="blue">bool</font>)row[<font color="maroon">"isPublished"</font>],
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  9</span>                                         (Guid)row[<font color="maroon">"uuid"</font>]);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 10</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 11</span>         <font color="blue">if</font> (row[<font color="maroon">"DatePublished"</font>] != DBNull.Value)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 12</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 13</span>             article.DatePublished = (DateTime)row[<font color="maroon">"DatePublished"</font>];
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 14</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 15</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 16</span>         <font color="blue">if</font> (row[<font color="maroon">"DateModified"</font>] != DBNull.Value)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 17</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 18</span>             article.DateModified = (DateTime)row[<font color="maroon">"DateModified"</font>];
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 19</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 20</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 21</span>         <font color="blue">return</font> article;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 22</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 23</span> }</pre></pre></pre>

 </pre>

I made also a Business Layer generic class, but has it is just for the moment a wrapper around the DataAccessor, I will not show it.</pre>

 </pre>

And finally the [Interpreter Design Pattern](http://www.dofactory.com/Patterns/PatternInterpreter.aspx):</pre>

 </pre>

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">public</font> <font color="blue">abstract</font> <font color="blue">class</font> Spec
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span>     <font color="blue">public</font> <font color="blue">abstract</font> <font color="blue">bool</font> isSatisfiedBy(Article article);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span> }</pre></pre>

With the different concrete Specifications:

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">public</font> <font color="blue">class</font> PublishedSpec : Spec
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span>     <font color="blue">public</font> PublishedSpec()
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span>     <font color="blue">public</font> <font color="blue">override</font> <font color="blue">bool</font> isSatisfiedBy(Article article)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  9</span>          <font color="blue">return</font> (article.isPublished == <font color="maroon">true</font>);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 10</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 11</span> }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 12</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 13</span> <font color="blue">public</font> <font color="blue">class</font> BeforeDateSpec : Spec
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 14</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 15</span>     <font color="blue">private</font> DateTime date;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 16</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 17</span>     <font color="blue">public</font> DateTime Date
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 18</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 19</span>         <font color="blue">get</font> { <font color="blue">return</font> date; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 20</span>         <font color="blue">set</font> { date = value; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 21</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 22</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 23</span>     <font color="blue">public</font> BeforeDateSpec(DateTime date)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 24</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 25</span>         <font color="blue">this</font>.Date = date;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 26</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 27</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 28</span>     <font color="blue">public</font> <font color="blue">override</font> <font color="blue">bool</font> isSatisfiedBy(Article article)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 29</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 30</span>         <font color="blue">return</font> (article.DatePublished < <font color="blue">this</font>.Date);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 31</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 32</span> }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 33</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 34</span> <font color="blue">public</font> <font color="blue">class</font> AuthorSpec : Spec
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 35</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 36</span>     <font color="blue">private</font> <font color="blue">string</font> author;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 37</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 38</span>     <font color="blue">public</font> <font color="blue">string</font> Author
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 39</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 40</span>       <font color="blue">get</font> { <font color="blue">return</font> author;}
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 41</span>       <font color="blue">set</font> { author = value; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 42</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 43</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 44</span>     <font color="blue">public</font> AuthorSpec (<font color="blue">string</font> author)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 45</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 46</span>         <font color="blue">this</font>.Author = author;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 47</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 48</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 49</span>     <font color="blue">public</font> <font color="blue">override</font> <font color="blue">bool</font> isSatisfiedBy(Article article)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 50</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 51</span>          <font color="blue">return</font> (article.Author == <font color="blue">this</font>.Author);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 52</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 53</span> }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 54</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 55</span> <font color="blue">public</font> <font color="blue">class</font> NotSpec : Spec
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 56</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 57</span>     <font color="blue">private</font> Spec specToNegate;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 58</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 59</span>     <font color="blue">public</font> NotSpec(Spec specToNegate)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 60</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 61</span>         <font color="blue">this</font>.specToNegate = specToNegate;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 62</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 63</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 64</span>     <font color="blue">public</font> <font color="blue">override</font> <font color="blue">bool</font> isSatisfiedBy(Article article)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 65</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 66</span>         <font color="blue">return</font> !specToNegate.isSatisfiedBy(article);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 67</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 68</span> }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 69</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 70</span> <font color="blue">public</font> <font color="blue">class</font> AndSpec : Spec
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 71</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 72</span>     <font color="blue">private</font> Spec augend;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 73</span>     <font color="blue">public</font> Spec Augend
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 74</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 75</span>       <font color="blue">get</font> { <font color="blue">return</font> augend;}
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 76</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 77</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 78</span>     <font color="blue">private</font> Spec addend;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 79</span>     <font color="blue">public</font> Spec Addend
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 80</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 81</span>       <font color="blue">get</font> { <font color="blue">return</font> addend;}
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 82</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 83</span>     
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 84</span>     <font color="blue">public</font> AndSpec (Spec augend, Spec addend)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 85</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 86</span>         <font color="blue">this</font>.augend = augend;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 87</span>         <font color="blue">this</font>.addend = addend;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 88</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 89</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 90</span>     <font color="blue">public</font> <font color="blue">override</font> <font color="blue">bool</font>  isSatisfiedBy(Article article)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 91</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 92</span>          <font color="blue">return</font> Augend.isSatisfiedBy(article) && Addend.isSatisfiedBy(article);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 93</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 94</span> }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 95</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 96</span> <font color="blue">public</font> <font color="blue">class</font> OrSpec : Spec
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 97</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 98</span>     <font color="blue">private</font> Spec augend;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 99</span>     <font color="blue">public</font> Spec Augend
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">100</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">101</span>         <font color="blue">get</font> { <font color="blue">return</font> augend; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">102</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">103</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">104</span>     <font color="blue">private</font> Spec addend;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">105</span>     <font color="blue">public</font> Spec Addend
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">106</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">107</span>         <font color="blue">get</font> { <font color="blue">return</font> addend; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">108</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">109</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">110</span>     <font color="blue">public</font> OrSpec(Spec augend, Spec addend)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">111</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">112</span>         <font color="blue">this</font>.augend = augend;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">113</span>         <font color="blue">this</font>.addend = addend;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">114</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">115</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">116</span>     <font color="blue">public</font> <font color="blue">override</font> <font color="blue">bool</font> isSatisfiedBy(Article article)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">117</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">118</span>         <font color="blue">return</font> Augend.isSatisfiedBy(article) || Addend.isSatisfiedBy(article);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">119</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">120</span> }</pre>
