---
title: "The beauty of C# and Linq"
permalink: /2010/01/26/The-beauty-of-C-and-Linq/
date: 1/26/2010 5:57:12 AM
updated: 1/26/2010 5:57:12 AM
disqusIdentifier: 20100126055712
tags: ["C#", "Linq"]
alias:
 - /post/The-beauty-of-C-and-Linq.aspx/index.html
---
Today I faced the following challenge to solve: return all possible combinations of three source collections.

We are using C# and with Linq it was just so easy.
<!-- more -->

<div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:4d864d53-59b3-4fa4-abf7-37489ccb240f" class="wlWriterEditableSmartContent"> <div style="border: #000080 1px solid; color: #000; font-family: 'Courier New', Courier, Monospace; font-size: 10pt"> <div style="background: #fff; max-height: 300px; overflow: auto"> <ol style="background: #ffffff; margin: 0; padding: 0 0 0 5px;"> <li><span style="color:#0000ff">public</span> <span style="color:#2b91af">List</span>&lt;<span style="color:#0000ff">string</span>&gt; Contexts</li> <li style="background: #f3f3f3">{</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">get</span></li> <li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;{</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">var</span> result = <span style="color:#0000ff">from</span> u <span style="color:#0000ff">in</span> SelectedUseCases</li> <li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">from</span> c <span style="color:#0000ff">in</span> SelectedChannels</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">from</span> up <span style="color:#0000ff">in</span> SelectedUserProfiles</li> <li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">select</span> <span style="color:#0000ff">string</span>.Format(<span style="color:#a31515">"{0}-{1}-{2}"</span>, u.Value, c.Value, up.Value);</li> <li>&nbsp;</li> <li style="background: #f3f3f3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#0000ff">return</span> result.ToList();</li> <li>&nbsp;&nbsp;&nbsp;&nbsp;}</li> <li style="background: #f3f3f3">}</li> </ol> </div> </div> </div>

Simple and beautiful!
