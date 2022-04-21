---
title: "Continuous integration and Nightly build with Team City"
permalink: /2008/04/23/Continuous-integration-and-Nightly-build-with-Team-City/
date: 4/23/2008 7:16:19 AM
updated: 4/23/2008 7:16:19 AM
disqusIdentifier: 20080423071619
tags: ["Tech Head Brothers", "continuous integration", "Team City"]
alias:
 - /post/Continuous-integration-and-Nightly-build-with-Team-City.aspx/index.html
---
I finally found some time this evening to re-configure Team City and adapt my MSBuild script to be able to achieve the following build configuration for [Tech Head Brothers](http://www.techheadbrothers.com/) portal:

![](http://farm3.static.flickr.com/2006/2434398929_d8bb394867_o.jpg) 
<!-- more -->

*   CI Trunk - Unit Tests, Coverage, Deploy Staging
*   *   Checkout source code from subversion Portal project trunk
    *   Compile
    *   Run [NUnit](http://nunit.com/index.php) tests
    *   Run [NCover](http://www.ncover.com/) with summary code coverage report
    *   Deploy to the Staging IIS    Nightly Trunk - Duplicate Finder
*   *   Find duplicates using Team City Build Runner    Nightly Trunk - Unit Tests, Coverage, NDepend
*   *   Checkout source code from subversion Portal project trunk
    *   Compile
    *   Run [NUnit](http://nunit.com/index.php) tests
    *   Run [NCover](http://www.ncover.com/) with summary code coverage report
    *   Run [NCover](http://www.ncover.com/) with full code coverage report
    *   Run [NDepend](http://www.ndepend.com/)   

Asap I will post about the way I achieve this configuration!
