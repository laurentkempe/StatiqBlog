---
title: "Playing with jQuery Templates API and Flickr"
permalink: /2010/10/05/Playing-with-jQuery-Templates-API-and-Flickr/
date: 10/5/2010 9:30:02 AM
updated: 10/5/2010 9:30:02 AM
disqusIdentifier: 20101005093002
tags: ["jQuery"]
---
Last week I spent some time playing with the today’s [announced jQuery Templates API](http://blog.jquery.com/2010/10/04/new-official-jquery-plugins-provide-templating-data-linking-and-globalization/)       
It was funny to see the different announcement tonight; [Scott](http://weblogs.asp.net/scottgu/archive/2010/10/04/jquery-templates-data-link-and-globalization-accepted-as-official-jquery-plugins.aspx), [JQuery Blog](http://blog.jquery.com/2010/10/04/new-official-jquery-plugins-provide-templating-data-linking-and-globalization/), [James](http://www.jamessenior.com/2010/09/30/jquery-templating-in-the-wild/)…

<!-- more -->
Tonight I have spent a bit more time with it and decided to adapt the sample I found here : “[Enabling JSONP calls on ASP.NET MVC](http://blogorama.nerdworks.in/entry-EnablingJSONPcallsonASPNETMVC.aspx)” to use JQuery Templates

I used [JetBrains WebStorm](http://www.jetbrains.com/webstorm/) to develop and here is the result

```html
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE html
        PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
        "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>flickr JQuery Template</title>
    <script type="text/javascript" src="js/code.jquery.com%20jquery-1.4.2.js"/>
    <script type="text/javascript" src="js/jquery-ui-1.8.5.custom.min.js"/>
    <script type="text/javascript" src="js/jquery.tmpl.js"/>
</head>
<body>
<script id="flickrTemplate" type="text/html">
    <div>
        <h2>$</h2>
        <div>
            <img src="http://farm5.static.flickr.com/\({server}/\)_$_t.jpg"
                  title="$"
                  alt="$" />
        </div>
    </div>
</script>
    <div id="interesting_photos"></div>
    <script type="text/javascript">
    //
    // Flickr REST url
    //
    var url = "http://api.flickr.com/services/rest/?";
    //
    // My Flickr API key
    //
    var api_key = "--your flickr api key here--";
    // get interesting photos
    //
    function getInterestingPhotos() {
        //
        // build the URL
        //
        var call = url + "method=flickr.interestingness.getList&amp;api_key="
                       + api_key
                       + "&amp;per_page=5&amp;page=1&amp;format=json&amp;jsoncallback=?";
        //
        // make the ajax call
        //
        $.getJSON(call, function(rsp) {
            if (rsp.stat != "ok") {
                //
                // something went wrong!
                //
                $( "#interesting_photos" ).append(
                    "&lt;label style="color:red"&gt;Whoops!  It didn't work!" +
                    "  This is embarrassing!  Here's what Flickr had to " +
                    " say about this - " + rsp.message + "&lt;/label&gt;");
            }
            else {
                //
                // build the html
                //
                $("#flickrTemplate")
                        .tmpl(rsp.photos.photo)
                        .appendTo( "#interesting_photos" );
            }
        });
    }
    </script>
    <script type="text/javascript">
        $(function() {
            $(getInterestingPhotos);
        })
    </script>
</body>
</html>
```

I could have done a template also of error message.
