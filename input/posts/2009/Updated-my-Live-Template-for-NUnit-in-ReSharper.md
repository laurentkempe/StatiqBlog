---
title: "Updated my Live Template for NUnit in ReSharper"
permalink: /2009/05/09/Updated-my-Live-Template-for-NUnit-in-ReSharper/
date: 5/9/2009 4:59:33 PM
updated: 5/9/2009 4:59:33 PM
disqusIdentifier: 20090509045933
tags: ["Visual Studio", "ReSharper", "unit test"]
alias:
 - /post/Updated-my-Live-Template-for-NUnit-in-ReSharper.aspx/index.html
---
I tend to use a lot the [Live Template of ReSharper](http://www.jetbrains.com/resharper/features/code_templates.html), for example working for some time now with the WPF MVVM pattern I created a ViewModel template of such a class and use it extensively.

Today I updated the [File Template](http://www.jetbrains.com/resharper/features/code_templates.html#File_Templates) I use to write my [NUnit](http://nunit.com/index.php) tests like this:
<!-- more -->

using NUnit.Framework; 

namespace $NAMESPACE$     
{      
    // ReSharper disable InconsistentNaming 

    [TestFixture]     
    public class $CLASSNAME$      
    {          
        /*      
         * ... hold ...      
         *       
         * Arrange - Act - Assert      
         */ 

        [Test]     
        public void $FIRST_TEST_NAME$()      
        {      
            $END$      
        }      
    } 

   // ReSharper restore InconsistentNaming     
}

where I have the variable names defined as this:

![](http://farm4.static.flickr.com/3326/3515113386_55481b1a06_o.png)
