---
title: "NFOP new way of creating the Pdf"
permalink: /2005/03/18/NFOP-new-way-of-creating-the-Pdf/
date: 3/18/2005 3:26:00 AM
updated: 3/18/2005 3:26:00 AM
disqusIdentifier: 20050318032600
tags: ["Tech Head Brothers", ".NET Development"]
alias:
 - /post/NFOP-new-way-of-creating-the-Pdf.aspx/index.html
---
I was asked about the new way to work with [NFOP](http://nfop.sourceforge.net/) to generate the pdf without 
the old Engine class. So here is what I am doing:

<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  1</span> <font color="blue">#region</font> Using
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  2</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  3</span> <font color="blue">using</font> System.Net;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  4</span> <font color="blue">using</font> System.Reflection;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  5</span> <font color="blue">using</font> System.Xml;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  6</span> <font color="blue">using</font> System.Xml.Xsl;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  7</span> <font color="blue">using</font> java.io;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  8</span> <font color="blue">using</font> log4net;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey">  9</span> <font color="blue">using</font> org.apache.fop.apps;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 10</span> <font color="blue">using</font> org.xml.sax;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 11</span> <font color="blue">using</font> TechHeadBrothers.Portal.UI;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 12</span> <font color="blue">using</font> StringWriter = System.IO.StringWriter;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 13</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 14</span> <font color="blue">#endregion</font>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 15</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 16</span> <font color="blue">namespace</font> TechHeadBrothers.TechHeadBrothers.Portal.UI
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 17</span> {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 18</span>     <font color="gray">/// <summary>
</font><span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 19</span>     <font color="gray">/// Strategy to print PDF using FOP
</font><span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 20</span>     <font color="gray">/// </summary>
</font><span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 21</span>     <font color="blue">public</font> <font color="blue">class</font> FOPPrintStrategy : PrintStrategy
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 22</span>     {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 23</span>         <font color="blue">#region</font> Logging Definition
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 24</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 25</span>         <font color="blue">private</font> <font color="blue">static</font> <font color="blue">readonly</font> ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 26</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 27</span>         <font color="blue">#endregion</font>    
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 28</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 29</span>         <font color="blue">protected</font> <font color="blue">override</font> <font color="blue">void</font> Print(PrintArticle page, <font color="blue">string</font> xmlpath, <font color="blue">string</font> xslpah, XsltArgumentList xsltargs)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 30</span>         {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 31</span>             <font color="blue">string</font> fullFoDoc = XmlContentToFoContent(page, xmlpath, xslpah);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 32</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 33</span>             InputSource source = <font color="blue">new</font> InputSource(fullFoDoc);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 34</span>             ByteArrayOutputStream output = <font color="blue">new</font> ByteArrayOutputStream();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 35</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 36</span>             <font color="blue">try</font>
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 37</span>             {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 38</span>                 Driver driver = <font color="blue">new</font> Driver(source, output);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 39</span>                 driver.setRenderer(Driver.RENDER_PDF);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 40</span>                 driver.run();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 41</span>                 output.close();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 42</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 43</span>                 <font color="blue">int</font> sz = output.buf.Length;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 44</span>                 <font color="blue">byte</font>[] pdf = <font color="blue">new</font> <font color="blue">byte</font>[sz];
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 45</span>                 <font color="blue">for</font> (<font color="blue">int</font> i = <font color="maroon">0</font>; i < sz; i++)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 46</span>                     pdf[i] = (<font color="blue">byte</font>) output.buf[i];
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 47</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 48</span>                 page.Response.ClearHeaders();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 49</span>                 page.Response.Clear();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 50</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 51</span>                 page.Response.AddHeader(<font color="maroon">"Content-Disposition"</font>, <font color="maroon">"attachment; filename=TechHeadBrothers.pdf"</font>);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 52</span>                 page.Response.ContentType = <font color="maroon">"application/octet-stream"</font>;
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 53</span> 
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 54</span>                 page.Response.Flush();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 55</span>                 page.Response.BinaryWrite(pdf);
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 56</span>                 page.Response.End();
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 57</span>             }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 58</span>             <font color="blue">catch</font> (FOPException fope)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 59</span>             {
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 60</span>                 <font color="blue">if</font> (log.IsDebugEnabled)
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 61</span>                     log.Debug(fope.getMessage());
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 62</span>             }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 63</span>         }
<span style="COLOR: teal; BACKGROUND-COLOR: lightgrey"> 64</span> }
