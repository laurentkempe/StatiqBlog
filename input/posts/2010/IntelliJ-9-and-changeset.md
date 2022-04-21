---
title: "IntelliJ 9 and changeset"
permalink: /2010/05/07/IntelliJ-9-and-changeset/
date: 5/7/2010 3:58:30 AM
updated: 5/7/2010 3:58:30 AM
disqusIdentifier: 20100507035830
tags: ["innoveo solutions", "Jetbrains", "IntelliJ", "Productivity"]
alias:
 - /post/IntelliJ-9-and-changeset.aspx/index.html
---
Day after day I find little gems in [IntelliJ 9](http://www.jetbrains.com/idea/index.html) that just make me more productive and give me more time to deal with the real interesting things.

Today for example I had to change a web.xml file which I was said that I should take care not to commit because if this file would go to our customer than we would have a problem. 
<!-- more -->

We are currently using [JIRA](http://www.atlassian.com/software/jira/) and [Greenhopper](http://www.atlassian.com/software/greenhopper) and I use the excellent plugin [Atlassian Connector for IntelliJ IDEA](http://confluence.atlassian.com/display/IDEPLUGIN/Atlassian+IntelliJ+Connector+2.0+Release+Notes)

My working process, which is not rocket science but needs a bit of discipline, is the following:

1.  Before starting a change I check that there is a task in our JIRA
2.  Then I get this task and create a changeset in IntelliJ
3.  Starting from now, everything I change is logged into that changeset
4.  If I need to work shortly on another task I get another task from JIRA and create another changeset and start to log my work on that new changeset
5.  When it is time to commit I just commit the changeset. Done  

Back to my web.xml issue, I had to take care not to commit it. I knew it from the start that I will have an issue if I commit that file. 

So immediately, having the information,  I created a new changeset named “**Do not commit**” and added the web.xml change to that changeset.

![4583794252_6d251ef393_o[1]](/images/4583794252_6d251ef393_o%5B1%5D.png "4583794252_6d251ef393_o[1]")

I was then on the safe side! Why? Because after working almost the whole morning and changing hundred of files I didn’t had to remember about that possible issue because the tool will remind me that. What a mind refresher!

If I had not done that then I would have to first remind that I don’t have to commit that file and then I would have to browse the hundreds of file searching for the one I have to commit and the other one.

So help yourself work on your toolset and become a more productive developer!
