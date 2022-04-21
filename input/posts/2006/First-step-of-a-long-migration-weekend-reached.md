---
title: "First step of a long migration weekend reached"
permalink: /2006/06/03/First-step-of-a-long-migration-weekend-reached/
date: 6/3/2006 10:23:00 PM
updated: 6/3/2006 10:23:00 PM
disqusIdentifier: 20060603102300
tags: ["Work"]
alias:
 - /post/First-step-of-a-long-migration-weekend-reached.aspx/index.html
---
Yesterday, Friday 2 June, 2006 at 5:00 PM, we started our platform migration process at [ecenter solutions](http://www.ecenter-solutions.com/). 

This is the result of one year of hard work splitted in two phases:
<!-- more -->

<ol>
<li>first 6 months we made several proof of concepts</li>
<li>last 6 months we redesigned the whole architecture, made a new security concept, defined new processes and finally implemented it</li></ol>


So what did we currently have changed. We still have a three stages strategy with Integration, Pre-Production and Production, but we went :

<ul>
<li>from <strong>4</strong> HP L2000 (RISC Processor, 32 bits, 440MHz) to <strong>11</strong> HP DL385/DL585 (AMD Opteron processor, 64 bits, 2.6GHz)</li>
<li>from <strong>one</strong> DataCenter to <strong>two</strong> DataCenters with Business Recovery</li>
<li>from <strong>HP-UX</strong> 11.x to<strong> SLES</strong> 9 (64 bits)</li>
<li>from <strong>Virtual Vault</strong> to <strong>appArmor</strong></li>
<li>from <strong>Apache 1.x</strong> to <strong>Apache 2.x</strong></li>
<li>from <strong>Tomcat 3.x</strong> to <strong>Tomcat 5.x</strong></li>
<li>from <strong>Java 1.3</strong> to <strong>Java 1.5</strong> (64 bits)</li>
<li>from <strong>Oracle 9i</strong> to <strong>Oracle 10g</strong> (64 bits)</li>
<li>from a hoster in <strong>Francfort, Germany</strong> to one in <strong>Basle, Switzerland</strong></li></ul>


We also improved our deployment process, you will like it Thomas ;-), our maintenance setup, our production street switch...

The tough time of this weekend is the migration of the whole data set sized to more than 650 Gb. It worked :-) <strong>Great job Philippe</strong>.

After some slight last modifications our internal tests are all on green. We now are waiting the system acceptance test of the business for tomorrow. And the final Production activation on Tuesday. So as [Didier](http://www.didierbeck.com/) write it "[eCENTER: Please cross your fingers for us :-)](http://www.didierbeck.com/2006/06/ecenter-please-cross-your-fingers-for.php)".

After that we will need for sure some rest and "normal working hours".
