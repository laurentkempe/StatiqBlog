---
title: "Running your TeamCity builds from PowerShell for any Git branch"
permalink: /2012/02/07/Running-your-TeamCity-builds-from-a-command-line/
date: 2/7/2012 1:09:02 AM
updated: 2/8/2012 4:50:22 PM
disqusIdentifier: 20120207010902
coverImage: https://farm6.staticflickr.com/5257/5561493976_859ff775f3_b.jpg
coverSize: partial
thumbnailImage: https://farm6.staticflickr.com/5257/5561493976_859ff775f3_q.jpg
coverCaption: "Plage le long de la Savane des Pétrifications, Martinique"
tags: ["Team City", "Productivity", "Agile", "Git", "PowerShell"]
alias:
 - /post/Running-your-TeamCity-builds-from-a-command-line.aspx/index.html
---
<!-- [![Plage le long de la Savane des Pétrifications](http://farm6.staticflickr.com/5257/5561493976_859ff775f3_m.jpg)](http://www.flickr.com/photos/laurentkempe/5561493976/ "Plage le long de la Savane des Pétrifications by Laurent Kempé, on Flickr") -->  

I love [TeamCity](http://www.jetbrains.com/teamcity/) and use it since a while to automate my build/release processes. As human we should never do the work a machine can do, we have certainly better and more interesting things to do.
<!-- more -->

The habit I saw in the different projects I worked for is to create new TeamCity builds for the branches you work on. It take quite some work to do, even with templates…

So I came with another way of doing it. It is leverage Git, PowerShell and the possibility to run TeamCity builds by “[Accessing Server by HTTP](http://confluence.jetbrains.net/display/TCD6/Accessing+Server+by+HTTP)”. Basically you just need to make a HTTP request to an Url like this:

> `http:``//testuser:testpassword@teamcity.jetbrains.com/httpAuth/action.html?add2Queue=bt10&system.name=<property name1>&system.value=<value1>&system.name=<property name2>&system.value=<value2>&env.name=<environment variable name1>&env.value=<environment variable value1>&env.name=<environment variable name2>&env.value=<environment variable value2>`

We will also use the possibility to pass custom parameters (system properties and environment variables) through the HTTP request.

The parameter we want to pass is the Ref name of the Git VCS Root, so we start by setting it using a configuration parameter called **BuildRefName**, as this:

 [![Git, PowerShell, TeamCity](http://farm8.staticflickr.com/7005/6830300407_f835d6002f_o.png)](http://www.flickr.com/photos/laurentkempe/6830300407/ "Git, PowerShell, TeamCity by Laurent Kempé, on Flickr")   

Then we need to add a configuration parameters for our TeamCity project with the name **BuildRefName** which we currently set to **master**, so that if the parameter is not defined it will default to this value:
 [![Git, PowerShell, TeamCity 2](http://farm8.staticflickr.com/7164/6830361585_d10f7c0f3a_o.png)](http://www.flickr.com/photos/laurentkempe/6830361585/ "Git, PowerShell, TeamCity 2 by Laurent Kempé, on Flickr")   

Now that we have configured this you can already start a build from your browser by calling the URL!

> `http:``//testuser:testpassword@teamcity.jetbrains.com/httpAuth/action.html?add2Queue=bt10`

Be sure to replace bt10 with your build id.

Nice ,but we want to get a step further and be able to start the build from PowerShell, which is quite easy!

To achieve this goal you add those two functions to your PowerShell profile:

{% codeblock Microsoft.PowerShell_profile.ps1 lang:powershell %}
function Get-Web($url, 
    [switch]$self,
    $credential, 
    $toFile,
    [switch]$bytes)
{
    #.Synopsis
    #    Downloads a file from the web
    #.Description
    #    Uses System.Net.Webclient (not the browser) to download data
    #    from the web.
    #.Parameter self
    #    Uses the default credentials when downloading that page (for downloading intranet pages)
    #.Parameter credential
    #    The credentials to use to download the web data
    #.Parameter url
    #    The page to download (e.g. www.msn.com)    
    #.Parameter toFile
    #    The file to save the web data to
    #.Parameter bytes
    #    Download the data as bytes   
    #.Example
    #    # Downloads www.live.com and outputs it as a string
    #    Get-Web http://www.live.com/
    #.Example
    #    # Downloads www.live.com and saves it to a file
    #    Get-Web http://wwww.msn.com/ -toFile www.msn.com.html
    $webclient = New-Object Net.Webclient
    if ($credential) {
        $webClient.Credentials = $credential
    }
    if ($self) {
        $webClient.UseDefaultCredentials = $true
    }
    if ($toFile) {
        if (-not "$toFile".Contains(":")) {
            $toFile = Join-Path $pwd $toFile
        }
        $webClient.DownloadFile($url, $toFile)
    } else {
        if ($bytes) {
            $webClient.DownloadData($url)
        } else {
            $webClient.DownloadString($url)
        }
    }
}

function tcBuild([string]$branch) {

    $url = "http://YourServerUrl/httpAuth/action.html?add2Queue=YourBuildId"

    if ($branch) {
        $url = $url + "&name=BuildRefName&value=" + $branch
    }
    
    $credentials = New-Object System.Net.NetworkCredential("Username", "Password")

    Get-Web $url -credential $credentials
}
{% endcodeblock %}

Please adapt the tcBuild script by replacing the YourServerUrl, YourBuildId, Username, Password by your personal values.

The first function Get-Web is from this blog post: “[Microcode: PowerShell Scripting Tricks: Scripting The Web (Part 1) (Get-Web)](http://blogs.msdn.com/b/mediaandmicrocode/archive/2008/12/01/microcode-powershell-scripting-tricks-scripting-the-web-part-1-get-web.aspx)”.

The second, **tcBuild**, is quite easy and is the one you will use to start a build on the TeamCity server.

My workflow now is the following one:

1.  I commit on my local Git repository then 
2.  when I want to run a build I push to the centralized Git origin repository in my branch then 
3.  I start my build on the Git Branch from PowerShell by typing : 

> tcBuild Jobping-10-NewFeature

where Jobping-10-NewFeature is the name of my Git branch.

This is quite cool and give me also the possibility to run kind of personal builds!
