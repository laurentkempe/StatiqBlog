---
title: "CopySourceAsHtml - Visual Studio 2003 plugin"
permalink: /2004/11/09/CopySourceAsHtml-Visual-Studio-2003-plugin/
date: 11/9/2004 6:36:00 AM
updated: 11/9/2004 6:36:00 AM
disqusIdentifier: 20041109063600
tags: ["Tools", ".NET Development"]
alias:
 - /post/CopySourceAsHtml-Visual-Studio-2003-plugin.aspx/index.html
---
Ever wanted to copy paste some code from Visual Studio .NET 2003 to your blog tool (e.g. [Sauce Reader](http://www.synop.com/Products/SauceReader/)) and keep the colorization ?<br>Here is the solution, [CopySourceAsHtml](http://www.jtleigh.com/people/colin/blog/archives/2004/11/copysourceashtm_3.html), an awesome plugin from [Colin Coller](http://www.jtleigh.com/people/colin/blog/). The cool point is that if "<em>VS.NET can highlight it, CSAH can copy it, and your code should look the same in your browser as it does in your editor</em>". I was a bit disappointed not finding the context menu in the editor for other source then C# but you might add a keyboard shortcups as described on this [page](http://www.jtleigh.com/people/colin/blog/archives/2004/10/copysourceashtm_1.html).

Even better, Colin provides the source. I guess I will soon integrate his colorization way to [Tech Head Brothers](http://www.techheadbrothers.com "Tech Head Brothers") Word 2003 [publishing tool](http://weblogs.asp.net/lkempe/archive/2004/08/23/219122.aspx).
<!-- more -->

Here a sample output of a C# code:

<div style="BORDER-RIGHT: windowtext 1pt solid; PADDING-RIGHT: 0pt; BORDER-TOP: windowtext 1pt solid; PADDING-LEFT: 0pt; FONT-SIZE: 10pt; BACKGROUND: #ffffff; PADDING-BOTTOM: 0pt; BORDER-LEFT: windowtext 1pt solid; COLOR: #000000; PADDING-TOP: 0pt; BORDER-BOTTOM: windowtext 1pt solid; FONT-FAMILY: Courier New">


<span style="BACKGROUND: #ffffff; COLOR: #008080">   39</span> <span style="COLOR: #0000ff">        #region</span><span> GuiBuilder</span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">   40</span>  

<span style="BACKGROUND: #ffffff; COLOR: #008080">   41</span> <span>        </span><span style="COLOR: #008000">//http://support.microsoft.com/default.aspx?scid=kb;EN-US;303018</span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">   42</span> <span>        </span><span style="COLOR: #0000ff">private</span><span style="COLOR: #0000ff"> bool</span><span> setupCommandBar()</span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">   43</span> <span>        {</span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">   44</span> <span>            ThisApplication.CustomizationContext = ThisDocument;</span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">   45</span>  

<span style="BACKGROUND: #ffffff; COLOR: #008080">   46</span> <span>            </span><span style="COLOR: #008000">// Add a button to the command bar.</span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">   47</span>  

<span style="BACKGROUND: #ffffff; COLOR: #008080">   48</span> <span>            oCommandBar = ThisApplication.CommandBars.Add("[Tech Head Brothers](http://www.techheadbrothers.com "Tech Head Brothers")", oMissing, oMissing, </span><span style="COLOR: #0000ff">true</span><span>);</span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">   49</span>  

<span style="BACKGROUND: #ffffff; COLOR: #008080">   50</span> <span>            AddButton(</span><span style="COLOR: #0000ff">ref</span><span> oCommandBar,</span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">   51</span> <span>                      </span><span style="COLOR: #0000ff">ref</span><span> oButtonNew,</span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">   52</span> <span>                      </span><span style="COLOR: #0000ff">new</span><span> _CommandBarButtonEvents_ClickEventHandler(oButtonNew_Click),</span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">   53</span> <span>                      "New Document",</span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">   54</span> <span>                      12);</span>
</div>


XSLT:

<div style="BORDER-RIGHT: windowtext 1pt solid; PADDING-RIGHT: 0pt; BORDER-TOP: windowtext 1pt solid; PADDING-LEFT: 0pt; FONT-SIZE: 10pt; BACKGROUND: #ffffff; PADDING-BOTTOM: 0pt; BORDER-LEFT: windowtext 1pt solid; COLOR: #000000; PADDING-TOP: 0pt; BORDER-BOTTOM: windowtext 1pt solid; FONT-FAMILY: Courier New">


<span style="BACKGROUND: #ffffff; COLOR: #008080">    1</span> <span style="COLOR: #0000ff"><?</span><span style="COLOR: #800000">xml</span><span style="COLOR: #ff0000"> version</span><span style="COLOR: #0000ff">="1.0"</span><span style="COLOR: #ff0000"> encoding</span><span style="COLOR: #0000ff">="ISO-8859-1"</span><span style="COLOR: #0000ff"> ?></span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">    2</span> <span style="COLOR: #0000ff"><</span><span style="COLOR: #800000">xsl:stylesheet</span><span style="COLOR: #800000"> xmlns</span><span style="COLOR: #ff00ff">:</span><span style="COLOR: #ff0000">thb</span><span style="COLOR: #0000ff">="http://www.techheadbrothers.com/WordFormat30.xsd"</span><span style="COLOR: #800000"> xmlns</span><span style="COLOR: #ff00ff">:</span><span style="COLOR: #ff0000">xsl</span><span style="COLOR: #0000ff">="http://www.w3.org/1999/XSL/Transform"</span><span style="COLOR: #ff0000"> version</span><span style="COLOR: #0000ff">="3.0"></span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">    3</span> <span style="COLOR: #0000ff">   <</span><span style="COLOR: #800000">xsl:output</span><span style="COLOR: #ff0000"> method</span><span style="COLOR: #0000ff">="html"</span><span style="COLOR: #ff0000"> version</span><span style="COLOR: #0000ff">="4.0"</span><span style="COLOR: #ff0000"> encoding</span><span style="COLOR: #0000ff">="iso-8859-1"</span><span style="COLOR: #ff0000"> indent</span><span style="COLOR: #0000ff">="yes"</span><span style="COLOR: #0000ff"> /></span>

<span style="BACKGROUND: #ffffff; COLOR: #008080">    4</span>  

<span style="BACKGROUND: #ffffff; COLOR: #008080">    5</span> <span style="COLOR: #0000ff">   <</span><span style="COLOR: #800000">xsl:param</span><span style="COLOR: #ff0000"> name</span><span style="COLOR: #0000ff">="readcounter"</span><span style="COLOR: #0000ff"> /></span>
</div>
