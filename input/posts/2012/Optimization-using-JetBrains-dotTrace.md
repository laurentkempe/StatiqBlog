---
title: "Optimizing Skye Editor using JetBrains dotTrace"
permalink: /2012/12/07/Optimization-using-JetBrains-dotTrace/
date: 12/7/2012 1:55:00 PM
updated: 12/7/2012 11:07:27 PM
disqusIdentifier: 20121207015500
coverImage: https://farm9.staticflickr.com/8150/6969851740_a5dbe52669_c.jpg
coverSize: partial
thumbnailImage: https://farm9.staticflickr.com/8150/6969851740_a5dbe52669_q.jpg
coverCaption: "Vosges, France"
tags: ["dotTrace", "Jetbrains", "C#", "innoveo solutions"]
alias:
 - /post/Optimization-using-JetBrains-dotTrace.aspx/index.html
---
<!-- [![WP_000092](http://farm9.staticflickr.com/8150/6969851740_a5dbe52669_m.jpg)](http://www.flickr.com/photos/laurentkempe/6969851740/ "WP_000092 by Laurent Kempé, on Flickr") -->

This post is a transcript of an internal post I did on [Innoveo Solutions](http://www.innoveo.com/) internal blog. Thanks to Innoveo to let me share this here!

Skye Editor is our metal model editor which is written in C# 4, WPF uses [Model-View-ViewModel](http://msdn.microsoft.com/en-us/magazine/dd419663.aspx) design pattern and [MVVM Light](http://mvvmlight.codeplex.com/).
<!-- more -->

The post shows the usage of [JetBrains dotTace](http://www.jetbrains.com/profiler/) to optimize Skye Editor and the importance of profiling your code, here it is.

For the release 2.20 of our Skye Editor product we have done already some optimization like “FindProductValue of ModelProduct to use a dictionary”

Starting of the release 2.21 the goal was to go one step further with “Optimize Loading/Deleting of definition and update to MVVMLight 4 RTM”

The results are quite awesome!

Here I am comparing the last version of Skye Editor which we shipped, 2.18 to the version currently in development for the next release 2.21.

The performance measurement scenario is as following:

*   Starting the application 
*   Loading a big definition, BigDefinition.zip, 2743 Kb zip, 19928 Kb Xml 
*   Deleting a brick which as lots of sub bricks and attributes, value ranges, values...   

I used the profiler [dotTrace from JetBrains](http://www.jetbrains.com/profiler/) to measure the performance improvement.

Here is a first result for the method ActualizeFromNewArchive, which is used when we load, import or activate a definition. This method is responsible of building all the View Models used in the editor which we use to display the tree of root, brick, the attributes, value range, values but also the backendinfo.. and finally the texts. So on big definition there is a lot to create especially for the texts.

2.18

![](http://farm9.staticflickr.com/8203/8251402507_20b0511221_o.jpg)

2.21

<font style="background-color: #ffff00">![](http://farm9.staticflickr.com/8337/8251402531_a91f4332cc_o.jpg)</font>

So we went from **9083ms** to **944ms** which is around a 9.6 factor improvement as we can see on the following picture!

<font style="background-color: #ffff00">![](http://farm9.staticflickr.com/8482/8251410205_f0b9c20d9f_o.jpg)</font>

That's quite impressive. But where does it come from? Let dig deeper in the execution tree.

2.18 

<font style="background-color: #ffff00">![](http://farm9.staticflickr.com/8060/8251413607_5bc4a306cd_o.jpg)</font>

2.21 

<font style="background-color: #ffff00">![](http://farm9.staticflickr.com/8209/8251414039_f746ba2210_o.jpg)</font>

So the first improvement is due an improvement done on [MVVM Light 4 RTM](http://www.galasoft.ch/mvvm/) a library we are using from the beginning which lets us decouple our View Models / Views using some messaging mechanisms among other features. I helped it's author [Laurent Bugnion](http://www.galasoft.ch/intro_en.html) to test and to improve the toolkit, he even mention us on [MVVM Light 4 RTM](http://www.galasoft.ch/mvvm/).

We went from **2219ms** to **35ms** but across the whole scenario (all usage of the Register method) we went from **3072ms** to **130ms**, which we can see here:

<font style="background-color: #ffff00">![](http://farm9.staticflickr.com/8059/8251415365_ec74709abb_o.jpg)</font>

The improvement there is that CleanupList is not anymore done at that moment but only when the application is idle. Clever. And what is really cool is that I mentioned that performance issue and Laurent fixed it in the next release. Thanks Laurent!

But this is not all because we have won only 3072ms which doesn't bring us from 9083ms to 944ms.

The other big improvement is the optimization of the way we find value which as radically changed.

2.18 

<font style="background-color: #ffff00">![](http://farm9.staticflickr.com/8490/8251415203_89e2f8798d_o.jpg)</font>

2.21 

<font style="background-color: #ffff00">![](http://farm9.staticflickr.com/8347/8251415265_a21d2f2500_o.jpg)</font>

From **5103ms** to **13ms** !

**2.18**

> public ProductValue FindProductValue(string uuid)
        {
            return Values.AsBindingQueryable().FirstOrDefault(pv => pv.UUID == uuid);
        }

**2.21**

> public ProductValue FindProductValue(string uuid)
        {
            return Values.FindBindingByUuid(uuid);
        }

Look more in details

**2.18**

> private readonly List<TBinding> _bindingList;

        public IQueryable<TBinding> AsBindingQueryable()
        {
            return _bindingList.AsQueryable();
        }

**2.21**

> public TBinding FindBindingByUuid(string uuid)
        {
            Tuple<TBinding, TModel> value;
            _modelDictionary.Value.TryGetValue(uuid, out value);
            return value != null ? value.Item1 : default(TBinding);

The huge difference between those two methods is that the 2.18 is using a list and LINQ to find the first value which match the uuid we are searching. When the 2.21 is using a dictionary which index all values by uuid. 

Another improvement of the 2.21 was to go from the following version of the method to the previously shown one: 

> public TBinding FindBindingByUuid(string uuid)
        {
            return _modelDictionary.Value.ContainsKey(uuid) ?
> 
>                    _modelDictionary.Value[uuid].Item1 : default(TBinding);
        }

This one make two access to the dictionary and the other only one access, which improved also quite a bit. 

Another improvement is that we removed the usage of a ThreadSafeObservableCollection which was dispatching to the UI thread all operations. Basically you could operate the collection from a background thread while it was bound to the UI, which normally you cannot do due to thread affinity, except if you dispatch, which for sure as a cost. 

So that's it for the improvement when we load/import/activate a definition! 

Now about deleting. 

2.18

<font style="background-color: #ffff00">![](http://farm9.staticflickr.com/8066/8251415073_7435454566_o.jpg)</font>

2.21

<font style="background-color: #ffff00">![](http://farm9.staticflickr.com/8067/8252484362_4bd63cd9fc_o.jpg)</font>

It would be nice to have this gain but in fact we had to refactor the operation so that one part is executed on the UI thread on the other part on a background thread. So basically what touch to the View Model is executed into the UI thread and what touch the Model on the background thread. 

So we have also to count this 

2.18

<font style="background-color: #ffff00">![](http://farm9.staticflickr.com/8346/8251415035_5eb2e1fda1_o.jpg)</font>

2.21

<font style="background-color: #ffff00">![](http://farm9.staticflickr.com/8343/8252484168_cb07bb43e2_o.jpg)</font>

So we go from **30720ms** to **21666ms**. Which is again a good improvement 

This can be again improved a lot because currently we have to traverse the whole tree and count all the relations to the texts we want to delete which is accounting for **19468ms**. 

    
With a cache of relation it will much much faster. But that for next time! 

I hope you will enjoy the time saving of all those optimizations in Skye Editor! 
