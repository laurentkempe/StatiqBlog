---
title: "First experience with CSS friendly control adapters beta"
permalink: /2006/04/29/First-experience-with-CSS-friendly-control-adapters-beta/
date: 4/29/2006 8:41:00 AM
updated: 4/29/2006 8:41:00 AM
disqusIdentifier: 20060429084100
alias:
 - /post/First-experience-with-CSS-friendly-control-adapters-beta.aspx/index.html
---
I had the chance to realize that CSS friendly control adapters beta from the 
ASP.NET Team was online for a short time and downloaded it, so I had the 
pleasure to experience it a bit this evening.

By the way I recommend the reading of the White Paper, that is well 
written.
<!-- more -->

So my first idea was to integrate the Menu on [Tech Head Brothers](http://www.techheadbrothers.com/). IMHO this new 
menu is far better than the one delivered with ASP.NET because it uses a pure 
CSS approach.

After copying the set of needed files to my project, I was able to start 
changing all the CSS to meet my needs. I did not faced any real issue to change 
the whole thing, but then I realized that I missed the possibility to know the 
selected item of my menu. I started to look at the code and in 2 minutes it was 
changed. Nice.

So I render this:

<div class="THBMenu">  
 <div 
class="AspNet-Menu-Horizontal">  
  <ul 
class="AspNet-Menu">  
   <li 
class="AspNet-Menu-Leaf">  
    <a 
href="/Website/Default.aspx" 
class="AspNet-Menu-Link">  
     Accueil  
    </a>  
   </li>  
   <li 
class="AspNet-Menu-Leaf">  
    <a 
href="/Website/Articles.aspx" 
class="AspNet-Menu-Link">  
     Articles  
    </a>  
   </li>  
   <li 
class="AspNet-Menu-Leaf">  
    <a 
href="/Website/Astuces.aspx" 
class="**AspNet-Menu-Link-Selected**">  
     Astuces  
    </a>  
   </li>

To achieve this I modified the method BuildItem in the file MenuAdapter.cs 
like this:

<font color="blue">if</font> (item != Control.SelectedItem)
    writer.WriteAttribute(<font color="maroon">"class"</font>, <font color="maroon">"AspNet-Menu-Link"</font>);
<font color="blue">else</font>
    writer.WriteAttribute(<font color="maroon">"class"</font>, <font color="maroon">"AspNet-Menu-Link-Selected"</font>);

And then I added the new thing in the CSS, MenuExample.css: 

.THBMenu ul.AspNet-Menu li.AspNet-Menu-Leaf a.AspNet-Menu-Link-Selected
{
    color: #1A2633;
    background: url(../../PersistantImage.ashx?theme=Default&file=Rounded.gif) no-repeat bottom center;
}

For such a result:

![](http://www.techheadbrothers.com/images/blog/CSS friendly control adapters 01.jpg)

[ Currently Playing : Because I Want You - Placebo - Meds (03:22) 
]
