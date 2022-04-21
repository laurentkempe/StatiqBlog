---
title: "Playing with jQuery Templates API and Flickr"
permalink: /2010/10/05/Playing-with-jQuery-Templates-API-and-Flickr/
date: 10/5/2010 9:30:02 AM
updated: 10/5/2010 9:30:02 AM
disqusIdentifier: 20101005093002
tags: ["jQuery"]
alias:
 - /post/Playing-with-jQuery-Templates-API-and-Flickr.aspx/index.html
---
Last week I spent some time playing with the today’s [announced jQuery Templates API](http://blog.jquery.com/2010/10/04/new-official-jquery-plugins-provide-templating-data-linking-and-globalization/)       
It was funny to see the different announcement tonight; [Scott](http://weblogs.asp.net/scottgu/archive/2010/10/04/jquery-templates-data-link-and-globalization-accepted-as-official-jquery-plugins.aspx), [JQuery Blog](http://blog.jquery.com/2010/10/04/new-official-jquery-plugins-provide-templating-data-linking-and-globalization/), [James](http://www.jamessenior.com/2010/09/30/jquery-templating-in-the-wild/)…

<!-- more -->
Tonight I have spent a bit more time with it and decided to adapt the sample I found here : “[Enabling JSONP calls on ASP.NET MVC](http://blogorama.nerdworks.in/entry-EnablingJSONPcallsonASPNETMVC.aspx)” to use JQuery Templates

I used [JetBrains WebStorm](http://www.jetbrains.com/webstorm/) to develop and here is the result

<pre style="line-height: 135%; border-bottom: #cecece 1px solid; border-left: #cecece 1px solid; padding-bottom: 5px; background-color: #fbfbfb; min-height: 40px; padding-left: 5px; width: 650px; padding-right: 5px; overflow: auto; border-top: #cecece 1px solid; border-right: #cecece 1px solid; padding-top: 5px"><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">&lt;?xml version="<span style="color: #8b0000">1.0</span>" encoding="<span style="color: #8b0000">UTF-8</span>"?&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">&lt;!DOCTYPE html
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        PUBLIC "<span style="color: #8b0000">-//W3C//DTD XHTML 1.0 Transitional//EN</span>"
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        "<span style="color: #8b0000">http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd</span>"&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">&lt;html xmlns="<span style="color: #8b0000">http://www.w3.org/1999/xhtml</span>" xml:lang="<span style="color: #8b0000">en</span>" lang="<span style="color: #8b0000">en</span>"&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">&lt;head&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    &lt;title&gt;flickr JQuery Template&lt;/title&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    &lt;script type="<span style="color: #8b0000">text/javascript</span>" src="<span style="color: #8b0000">js/code.jquery.com%20jquery-1.4.2.js</span>"/&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    &lt;script type="<span style="color: #8b0000">text/javascript</span>" src="<span style="color: #8b0000">js/jquery-ui-1.8.5.custom.min.js</span>"/&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    &lt;script type="<span style="color: #8b0000">text/javascript</span>" src="<span style="color: #8b0000">js/jquery.tmpl.js</span>"/&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">&lt;/head&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">&lt;body&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px"></pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">&lt;script id="<span style="color: #8b0000">flickrTemplate</span>" type="<span style="color: #8b0000">text/html</span>"&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    &lt;div&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        &lt;h2&gt;${title}&lt;/h2&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        &lt;div&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">            &lt;img src="<span style="color: #8b0000">http://farm5.static.flickr.com/${server}/${id}_${secret}_t.jpg</span>" 
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                  title="<span style="color: #8b0000">${title}</span>" 
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                  alt="<span style="color: #8b0000">${title}</span>" /&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        &lt;/div&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    &lt;/div&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">&lt;/script&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px"></pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    &lt;div id="<span style="color: #8b0000">interesting_photos</span>"&gt;&lt;/div&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px"></pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    &lt;script type="<span style="color: #8b0000">text/javascript</span>"&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    <span style="color: #008000">// Flickr REST url</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    <span style="color: #0000ff">var</span> url = "<span style="color: #8b0000">http://api.flickr.com/services/rest/?</span>";
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px"></pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    <span style="color: #008000">// My Flickr API key</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    <span style="color: #0000ff">var</span> api_key = "<span style="color: #8b0000">--your flickr api key here--</span>";
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px"></pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    <span style="color: #008000">// get interesting photos</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    <span style="color: #0000ff">function</span> getInterestingPhotos() {
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        <span style="color: #008000">// build the URL</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        <span style="color: #0000ff">var</span> call = url + "<span style="color: #8b0000">method=flickr.interestingness.getList&amp;amp;api_key=</span>"
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                       + api_key
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                       + "<span style="color: #8b0000">&amp;amp;per_page=5&amp;amp;page=1&amp;amp;format=json&amp;amp;jsoncallback=?</span>";
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px"></pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        <span style="color: #008000">// make the ajax call</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        $.getJSON(call, <span style="color: #0000ff">function</span>(rsp) {
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">            <span style="color: #0000ff">if</span> (rsp.stat != "<span style="color: #8b0000">ok</span>") {
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                <span style="color: #008000">// something went wrong!</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                $( "<span style="color: #8b0000">#interesting_photos</span>" ).append(
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                    "<span style="color: #8b0000">&amp;lt;label style=\"color:red\"&amp;gt;Whoops!  It didn't work!</span>" +
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                    "<span style="color: #8b0000">  This is embarrassing!  Here's what Flickr had to </span>" +
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                    "<span style="color: #8b0000"> say about this - </span>" + rsp.message + "<span style="color: #8b0000">&amp;lt;/label&amp;gt;</span>");
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">            }
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">            <span style="color: #0000ff">else</span> {
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                <span style="color: #008000">// build the html</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                <span style="color: #008000">//</span>
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                $("<span style="color: #8b0000">#flickrTemplate</span>")
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                        .tmpl(rsp.photos.photo)
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">                        .appendTo( "<span style="color: #8b0000">#interesting_photos</span>" );
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">            }
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        });
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    }
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    &lt;/script&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px"></pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    &lt;script type="<span style="color: #8b0000">text/javascript</span>"&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        $(<span style="color: #0000ff">function</span>() {
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">            $(getInterestingPhotos);
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">        })
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">    &lt;/script&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px"></pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">&lt;/body&gt;
</pre><pre style="background-color: #fbfbfb; margin: 0em; width: 100%; font-family: consolas,'Courier New',courier,monospace; font-size: 12px">&lt;/html&gt;</pre></pre>

I could have done a template also of error message.
