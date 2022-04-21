---
title: "Automated functional tests using Watin and MSpec"
permalink: /2010/05/13/Automated-functional-tests-using-Watin-and-MSpec/
date: 5/13/2010 9:54:12 AM
updated: 5/14/2010 10:02:06 AM
disqusIdentifier: 20100513095412
alias:
 - /post/Automated-functional-tests-using-Watin-and-MSpec.aspx/index.html
---
I am conducting a [spike](http://searchsoftwarequality.techtarget.com/sDefinition/0,,sid92_gci1306773,00.html) for a couple of evening on the way we might automate our functional tests at [Jobping](http://www.jobping.com).  

I started with [Watin](http://watin.sourceforge.net/) and [MSpec](http://github.com/machine/machine.specifications) and the MSpec excellent plugin for [ReSharper 5](http://www.jetbrains.com/resharper/index.html) which gives the following great outputs directly from Visual Studio
<!-- more -->

 ![4602612162_a3a0e50945_o[1]](/images/4602612162_a3a0e50945_o%5B1%5D.png "4602612162_a3a0e50945_o[1]") 

After some discussion with [Alexander Groß](http://therightstuff.de/) (Thanks for your help ;) to gain some knowhow about MSpec I finally managed to have some automated functional tests running like this:
  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:f10ae5ad-86e6-4131-91ed-763bab0f53d4" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background-color: #ffffff; overflow: auto; padding: 2px 5px; white-space: nowrap">[<span style="color:#2b91af">Subject</span>(<span style="color:#a31515">"Search"</span>)]  
 <span style="color:#0000ff">public</span> <span style="color:#0000ff">class</span> <span style="color:#2b91af">when_user_search_using_keywords</span> : <span style="color:#2b91af">WebBaseSpec</span>  
 {  
     <span style="color:#0000ff">const</span> <span style="color:#0000ff">string</span> Keywords = <span style="color:#a31515">"C#"</span>;  
     <span style="color:#0000ff">static</span> <span style="color:#2b91af">SearchScreenObject</span> searchScreenObject;  
     <span style="color:#0000ff">static</span> <span style="color:#2b91af">ResultScreenObject</span> resultScreenObject;  

     <span style="color:#2b91af">Establish</span> context = () =>  
         {  
             searchScreenObject = <span style="color:#0000ff">new</span> <span style="color:#2b91af">SearchScreenObject</span>(Browser);  
             resultScreenObject = <span style="color:#0000ff">new</span> <span style="color:#2b91af">ResultScreenObject</span>(Browser);  
         };  

     <span style="color:#2b91af">Because</span> of = () => searchScreenObject.Search(Keywords);  

     <span style="color:#2b91af">It</span> should_direct_user_to_results_page = () =>   
         Browser.Uri.Route().ShouldMapTo<<span style="color:#2b91af">HomeController</span>>(x =>   
             x.Search(<span style="color:#a31515">"AU"</span>, <span style="color:#0000ff">new</span> <span style="color:#2b91af">SearchRequest</span> { Keywords = Keywords}));  

     <span style="color:#2b91af">It</span> should_fill_search_textbox_with_keywords_entered_by_user = () =>   
         resultScreenObject.SearchText.Text.ShouldEqual(Keywords);  
 }</div> </div> </div>  

I think it talks for itself!

Remarks to note:

1.  SearchScreenObject and ResultScreenObject represents an isolation layer between my tests and objects that are present on the web pages. This helps in the case you decide to change an id of an element
2.  I use [MvcContrib ShouldMap](http://mvccontrib.codeplex.com/wikipage?title=TestHelper) to ensure that the browser navigates to the correct destination page which add another isolation layer and let me change my URL without impacting my tests  

It is really funny to see the browser opening and clicking automatically, typing texts… 

Hopefully, at the end this will replace our smoke test document and quite some time of manual testing.
