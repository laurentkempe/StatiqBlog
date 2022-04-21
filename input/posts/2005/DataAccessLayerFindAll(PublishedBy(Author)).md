---
title: "DataAccessLayer.FindAll(PublishedBy(Author))"
permalink: /2005/03/10/DataAccessLayerFindAll(PublishedBy(Author))/
date: 3/10/2005 7:43:00 AM
updated: 3/10/2005 7:43:00 AM
disqusIdentifier: 20050310074300
tags: ["Whidbey", "ASP.NET 2.0", ".NET Framework 2.0"]
alias:
 - /post/DataAccessLayerFindAll(PublishedBy(Author)).aspx/index.html
---



One of the first development rule is to **write human readable 
code**. Why ? To avoid to have to write comments ;-) Because it 
***will be read as a spoken language*** and ***it 
separate important code from distracting one***.
<!-- more -->

What do you think about that line of code:

<span style="COLOR: blue">return</span> DAL.FindAll(PublishedBy(Author));

Someone that has no clue about development might even read that code. Ok 
except the DAL word ;-)

With C# 2.0 you are able to write readable code like this using Generics, and 
the power of [closures](http://martinfowler.com/bliki/Closures.html). 
In C# 2.0 closure are supported through the form of anonymous delegates.

Check out the different methods returning collection accordign to predefined 
rules: **getAllUnpublished**(), **getAllPublished**(), 
**getAllPublishedBy**()... There are all using Predicate, so to say 
anonymous delegate, that gives you human readable code.

Client code:

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">protected</font> <font color="blue">void</font> Page_Load(<font color="blue">object</font> sender, EventArgs e)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span>     GridView2.DataSource = TechHeadBrothers.Portal.BLL.ArticleBLL.getAllPublishedBy(<font color="maroon">"Laurent Kempé"</font>);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span>     GridView2.DataBind();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span>     GridView3.DataSource = TechHeadBrothers.Portal.BLL.ArticleBLL.getAllWaitPublishingBy(<font color="maroon">"Laurent Kempé"</font>);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span>     GridView3.DataBind();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span> }

The Business Layer:

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">#region</font> Using
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span> <font color="blue">using</font> System;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span> <font color="blue">using</font> System.Collections.Generic;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span> <font color="blue">using</font> System.Text;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span> <font color="blue">using</font> TechHeadBrothers.Portal.Entities;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span> <font color="blue">using</font> TechHeadBrothers.Portal.DAL; 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  9</span> <font color="blue">#endregion</font>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 10</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 11</span> <font color="blue">namespace</font> TechHeadBrothers.Portal.BLL
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 12</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 13</span>     <font color="blue">public</font> <font color="blue">static</font> <font color="blue">class</font> ArticleBLL
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 14</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 15</span>         <font color="blue">static</font> ArticleDAL DAL = <font color="blue">new</font> ArticleDAL();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 16</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 17</span>         <font color="blue">public</font> <font color="blue">static</font> List<Article> getAll()
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 18</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 19</span>             <font color="blue">return</font> DAL.GetAll();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 20</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 21</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 22</span>         <font color="blue">public</font> <font color="blue">static</font> List<Article> getAllUnpublished()
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 23</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 24</span>             <font color="blue">return</font> DAL.FindAll(NotPublished());
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 25</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 26</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 27</span>         <font color="blue">public</font> <font color="blue">static</font> List<Article> getAllPublished()
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 28</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 29</span>             <font color="blue">return</font> DAL.FindAll(Published());
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 30</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 31</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 32</span>         <font color="blue">public</font> <font color="blue">static</font> List<Article> getAllPublishedBy(<font color="blue">string</font> Author)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 33</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 34</span>             <font color="blue">return</font> DAL.FindAll(PublishedBy(Author));
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 35</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 36</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 37</span>         <font color="blue">public</font> <font color="blue">static</font> List<Article> getAllWaitPublishingBy(<font color="blue">string</font> Author)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 38</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 39</span>             <font color="blue">return</font> DAL.FindAll(WaitPublishingBy(Author));
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 40</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 41</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 42</span>         <font color="blue">protected</font> <font color="blue">static</font> Predicate<Article> WaitPublishingBy(<font color="blue">string</font> author)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 43</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 44</span>             <font color="blue">return</font> <font color="blue">delegate</font>(Article a)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 45</span>             {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 46</span>                 <font color="blue">return</font> !a.isPublished && (a.Author.ToLower() == author.ToLower());
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 47</span>             };
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 48</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 49</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 50</span>         <font color="blue">protected</font> <font color="blue">static</font> Predicate<Article> PublishedBy(<font color="blue">string</font> author)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 51</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 52</span>             <font color="blue">return</font> <font color="blue">delegate</font>(Article a)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 53</span>             {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 54</span>                 <font color="blue">return</font> a.isPublished && (a.Author.ToLower() == author.ToLower());
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 55</span>             };
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 56</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 57</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 58</span>         <font color="blue">protected</font> <font color="blue">static</font> Predicate<Article> Published()
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 59</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 60</span>             <font color="blue">return</font> <font color="blue">delegate</font>(Article a)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 61</span>             {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 62</span>                 <font color="blue">return</font> a.isPublished;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 63</span>             };
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 64</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 65</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 66</span>         <font color="blue">protected</font> <font color="blue">static</font> Predicate<Article> NotPublished()
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 67</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 68</span>             <font color="blue">return</font> <font color="blue">delegate</font>(Article a)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 69</span>             {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 70</span>                 <font color="blue">return</font> (!a.isPublished);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 71</span>             };
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 72</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 73</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 74</span> }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 75</span> 

The Entity class is represented as:

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">#region</font> Using
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span> <font color="blue">using</font> System;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span> <font color="blue">using</font> System.Collections.Generic;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span> <font color="blue">using</font> System.Text; 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span> <font color="blue">#endregion</font>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  9</span> <font color="blue">namespace</font> TechHeadBrothers.Portal.Entities
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 10</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 11</span>     <font color="blue">public</font> <font color="blue">class</font> Article
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 12</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 13</span>         <font color="blue">private</font> Guid uuid;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 14</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 15</span>         <font color="blue">public</font> Guid Uuid
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 16</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 17</span>             <font color="blue">get</font> { <font color="blue">return</font> uuid; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 18</span>             <font color="blue">set</font> { uuid = value; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 19</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 20</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 21</span>         <font color="blue">private</font> <font color="blue">string</font> title;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 22</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 23</span>         <font color="blue">public</font> <font color="blue">string</font> Title
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 24</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 25</span>             <font color="blue">get</font> { <font color="blue">return</font> title; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 26</span>             <font color="blue">set</font> { title = value; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 27</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 28</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 29</span>         <font color="blue">private</font> <font color="blue">string</font> description;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 30</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 31</span>         <font color="blue">public</font> <font color="blue">string</font> Description
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 32</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 33</span>             <font color="blue">get</font> { <font color="blue">return</font> description; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 34</span>             <font color="blue">set</font> { description = value; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 35</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 36</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 37</span>         <font color="blue">private</font> <font color="blue">bool</font> ispublished;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 38</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 39</span>         <font color="blue">public</font> <font color="blue">bool</font> isPublished
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 40</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 41</span>             <font color="blue">get</font> { <font color="blue">return</font> ispublished; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 42</span>             <font color="blue">set</font> { ispublished = value; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 43</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 44</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 45</span>         <font color="blue">private</font> <font color="blue">string</font> author;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 46</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 47</span>         <font color="blue">public</font> <font color="blue">string</font> Author
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 48</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 49</span>             <font color="blue">get</font> { <font color="blue">return</font> author; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 50</span>             <font color="blue">set</font> { author = value; }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 51</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 52</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 53</span>         <font color="blue">public</font> Article(Guid uuid, <font color="blue">string</font> title, <font color="blue">string</font> description, <font color="blue">string</font> author)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 54</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 55</span>             <font color="blue">this</font>.Uuid = uuid;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 56</span>             <font color="blue">this</font>.Title = title;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 57</span>             <font color="blue">this</font>.Description = description;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 58</span>             <font color="blue">this</font>.isPublished = <font color="maroon">false</font>;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 59</span>             <font color="blue">this</font>.Author = author;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 60</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 61</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 62</span>         <font color="blue">public</font> Article(<font color="blue">string</font> uuid, <font color="blue">string</font> title, <font color="blue">string</font> description, <font color="blue">string</font> author)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 63</span>             : <font color="blue">this</font>(<font color="blue">new</font> Guid (uuid), title, description, author)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 64</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 65</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 66</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 67</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 68</span>         <font color="blue">public</font> Article(<font color="blue">string</font> title, <font color="blue">string</font> description, <font color="blue">string</font> author)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 69</span>             : <font color="blue">this</font>(Guid.NewGuid(), title, description, author)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 70</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 71</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 72</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 73</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 74</span> } 

And the Data Access Layer Article class:

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">#region</font> Using
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span> <font color="blue">using</font> System;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span> <font color="blue">using</font> System.Collections.Generic;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span> <font color="blue">using</font> System.Text;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span> <font color="blue">#endregion</font>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  9</span> <font color="blue">namespace</font> TechHeadBrothers.Portal.DAL
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 10</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 11</span>     <font color="blue">interface</font> IDataAccess<T>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 12</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 13</span>         T Get(Guid uuid);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 14</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 15</span>         T Get(<font color="blue">string</font> uuid);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 16</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 17</span>         List<T> GetAll();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 18</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 19</span>         List<T> FindAll(Predicate<T> match);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 20</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 21</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 22</span>     <font color="blue">public</font> <font color="blue">class</font> ArticleDAL : IDataAccess<TechHeadBrothers.Portal.Entities.Article>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 23</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 24</span>         List<TechHeadBrothers.Portal.Entities.Article> articles;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 25</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 26</span>         <font color="blue">public</font> ArticleDAL(List<TechHeadBrothers.Portal.Entities.Article> articles)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 27</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 28</span>             <font color="blue">this</font>.articles = articles;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 29</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 30</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 31</span>         <font color="blue">public</font> ArticleDAL()
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 32</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 33</span>             <font color="blue">this</font>.articles = <font color="blue">new</font> List<TechHeadBrothers.Portal.Entities.Article>();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 34</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 35</span>             <font color="green">//TODO: Remove this is just for test
</font><span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 36</span>             <font color="blue">this</font>.articles.Add(
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 37</span>                 <font color="blue">new</font> TechHeadBrothers.Portal.Entities.Article(<font color="maroon">"Les generics dans C#"</font>,
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 38</span>                 <font color="maroon">"Démonstration de l'utilisation des générics dans C# 2.0"</font>,
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 39</span>                 <font color="maroon">"Laurent Kempé"</font>));
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 40</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 41</span>             TechHeadBrothers.Portal.Entities.Article article =
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 42</span>                 <font color="blue">new</font> TechHeadBrothers.Portal.Entities.Article(<font color="maroon">"8BF7FEBE-9FEB-4db6-86B7-70A6C44B1CAA"</font>,
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 43</span>                 <font color="maroon">"Les iterators dans C#"</font>,
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 44</span>                 <font color="maroon">"Démonstration de l'utilisation des iterators dans C# 2.0"</font>,
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 45</span>                 <font color="maroon">"Laurent Kempé"</font>);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 46</span>             article.isPublished = <font color="maroon">true</font>;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 47</span>             <font color="blue">this</font>.articles.Add(article);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 48</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 49</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 50</span>         <font color="blue">#region</font> IDataAccess<Article> Members
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 51</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 52</span>         <font color="blue">public</font> TechHeadBrothers.Portal.Entities.Article Get(Guid uuid)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 53</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 54</span>             <font color="blue">for</font> (<font color="blue">int</font> i = <font color="maroon">0</font>; i < articles.Count; i++)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 55</span>             {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 56</span>                 <font color="blue">if</font> (articles[i].Uuid == uuid)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 57</span>                 {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 58</span>                     <font color="blue">return</font> articles[i];
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 59</span>                 }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 60</span>             }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 61</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 62</span>             <font color="blue">return</font> <font color="blue">null</font>;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 63</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 64</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 65</span>         <font color="blue">public</font> TechHeadBrothers.Portal.Entities.Article Get(<font color="blue">string</font> uuid)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 66</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 67</span>             <font color="blue">return</font> <font color="blue">this</font>.Get(<font color="blue">new</font> Guid(uuid));
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 68</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 69</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 70</span>         <font color="blue">public</font> List<TechHeadBrothers.Portal.Entities.Article> GetAll()
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 71</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 72</span>             <font color="blue">return</font> articles;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 73</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 74</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 75</span>         <font color="blue">public</font> List<TechHeadBrothers.Portal.Entities.Article> FindAll(Predicate<TechHeadBrothers.Portal.Entities.Article> match)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 76</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 77</span>             <font color="blue">return</font> articles.FindAll(match);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 78</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 79</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 80</span>         <font color="blue">#endregion</font>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 81</span>     }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 82</span> }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 83</span> </pre>

[ Currently Playing : Sound Enforcer / Impact - Carl Cox (04:10) 
]
