---
title: "Automated functional tests using Watin and MSpec"
permalink: /2010/05/13/Automated-functional-tests-using-Watin-and-MSpec/
date: 5/13/2010 9:54:12 AM
updated: 5/14/2010 10:02:06 AM
disqusIdentifier: 20100513095412
---
I am conducting a [spike](http://searchsoftwarequality.techtarget.com/sDefinition/0,,sid92_gci1306773,00.html) for a couple of evening on the way we might automate our functional tests at [Jobping](http://www.jobping.com). 

I started with [Watin](http://watin.sourceforge.net/) and [MSpec](http://github.com/machine/machine.specifications) and the MSpec excellent plugin for [ReSharper 5](http://www.jetbrains.com/resharper/index.html) which gives the following great outputs directly from Visual Studio
<!-- more -->

![4602612162_a3a0e50945_o[1]](/images/4602612162_a3a0e50945_o%5B1%5D.png "4602612162_a3a0e50945_o[1]") 

After some discussion with [Alexander Groß](http://therightstuff.de/) (Thanks for your help ;) to gain some knowhow about MSpec I finally managed to have some automated functional tests running like this:

```csharp
[Subject("Search")]
public class when_user_search_using_keywords : WebBaseSpec
{     
    const string Keywords = "C#";
    static SearchScreenObject searchScreenObject;
    static ResultScreenObject resultScreenObject;

    Establish context = () =>
        {
            searchScreenObject = new SearchScreenObject(Browser);
            resultScreenObject = new ResultScreenObject(Browser);
        };

    Because of = () => searchScreenObject.Search(Keywords);

    It should_direct_user_to_results_page = () =>
        Browser.Uri.Route().ShouldMapTo<HomeController>(x =>
            x.Search("AU", new SearchRequest ));

    It should_fill_search_textbox_with_keywords_entered_by_user = () =>
        resultScreenObject.SearchText.Text.ShouldEqual(Keywords);
}
```
I think it talks for itself!

Remarks to note:

1.  SearchScreenObject and ResultScreenObject represents an isolation layer between my tests and objects that are present on the web pages. This helps in the case you decide to change an id of an element
2.  I use [MvcContrib ShouldMap](http://mvccontrib.codeplex.com/wikipage?title=TestHelper) to ensure that the browser navigates to the correct destination page which add another isolation layer and let me change my URL without impacting my tests  

It is really funny to see the browser opening and clicking automatically, typing texts… 

Hopefully, at the end this will replace our smoke test document and quite some time of manual testing.
