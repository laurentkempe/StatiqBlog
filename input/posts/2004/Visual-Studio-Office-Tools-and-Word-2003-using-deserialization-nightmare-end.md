---
title: "Visual Studio Office Tools and Word 2003 using deserialization, nightmare end"
permalink: /2004/08/28/Visual-Studio-Office-Tools-and-Word-2003-using-deserialization-nightmare-end/
date: 8/28/2004 5:32:00 PM
updated: 8/28/2004 5:32:00 PM
disqusIdentifier: 20040828053200
tags: ["Tech Head Brothers", ".NET Development"]
alias:
 - /post/Visual-Studio-Office-Tools-and-Word-2003-using-deserialization-nightmare-end.aspx/index.html
---
I finally fixed the issue I reported in the post "[New milestone reached in the development of the authoring tool for Tech Head Brothers French portal](http://weblogs.asp.net/lkempe/archive/2004/08/23/219122.aspx) ". I am still waiting some feedback from Peter (he might be in holidays).

I am getting an exception when I try to deserialize an object. This exception is due to a security problem.
<!-- more -->

What I did is to use SGen tool from [Daniel Cazzulino](http://weblogs.asp.net/cazzu/) described in this post: "[Strongly-typed, event-rising, design-time generated custom XmlSerializers (even more than Whidbey sgen!)](http://weblogs.asp.net/cazzu/archive/2004/08/02/SGen.aspx) ". I also used Mike Woodring code described in the post: "[The Last Configuration Section Handler I'll Ever Need](http://www.pluralsight.com/craig/articleview.aspx/CLR%20Workings/The%20Last%20Configuration%20Section%20Handler%20I.xml)". I mixed all and modified XmlSerializerSectionHandler from Mike so that it doesn't return a fixed IDictionary but an object. With reflection I can determine which Handler to instantiate, then from this handler I call it method Create and I get something like that to deserialize an object from the configuration file:
<font size="2">


SchemaSettings schemaSettings = (SchemaSettings)AssemblySettings.GetConfig("SchemaSettings");

With the configuration:
<font color="#0000ff" size="2">


<</font><font color="#800000" size="2">configuration</font><font color="#0000ff" size="2">>

</font><font size="2">


</font><font color="#0000ff" size="2"><</font><font color="#800000" size="2">configSections</font><font color="#0000ff" size="2">>

</font><font size="2">


</font><font color="#0000ff" size="2"><</font><font color="#800000" size="2">section</font><font color="#ff00ff" size="2"> </font><font color="#ff0000" size="2">name</font><font color="#0000ff" size="2">="SchemaSettings"

</font><font color="#ff00ff" size="2">


</font><font color="#ff0000" size="2">type</font><font color="#0000ff" size="2">="TechHeadBrothers.Configuration.XmlDynamicSerializerSectionHandler, THBPublisher"/>

</font><font size="2">


</font><font color="#0000ff" size="2"></</font><font color="#800000" size="2">configSections</font><font color="#0000ff" size="2">></font>

<font color="#0000ff" size="2"><font size="2">


</font><font color="#0000ff" size="2"><</font><font color="#800000" size="2">SchemaSettings</font><font color="#ff00ff" size="2"> </font><font color="#ff0000" size="2">type</font><font color="#0000ff" size="2">="THBPublisher.SchemaSettings, THBPublisher"</font><font color="#ff00ff" size="2"> </font><font color="#ff0000" size="2">serializer</font><font color="#0000ff" size="2">="THBPublisher.SchemaSettingsSerializer, THBPublisher"</font><font color="#ff00ff" size="2"> </font><font color="#ff0000" size="2">reader</font><font color="#0000ff" size="2">="THBPublisher.SchemaSettingsReader, THBPublisher">

</font><font size="2">


</font><font color="#0000ff" size="2"><</font><font color="#800000" size="2">Name</font><font color="#0000ff" size="2">></font><font size="2">Laurent</font><font color="#0000ff" size="2"></</font><font color="#800000" size="2">Name</font><font color="#0000ff" size="2">>

</font><font size="2">


</font><font color="#0000ff" size="2"></</font><font color="#800000" size="2">SchemaSettings</font><font color="#0000ff" size="2">>

</font><font size="2">


</font><font color="#0000ff" size="2"></</font><font color="#800000" size="2">configuration</font><font color="#0000ff" size="2">></font>

</font></font>So the good point at the end is that I have a configuration file looking like a normal one, that I can get configuration settings from even deserialize object from it. Nice.
