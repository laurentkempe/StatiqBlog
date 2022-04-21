---
title: "Mocking .NET framework SmtpClient class"
permalink: /2008/06/01/Mocking-NET-framework-SmtpClient-class/
date: 6/1/2008 7:41:20 AM
updated: 5/7/2010 7:52:14 AM
disqusIdentifier: 20080601074120
tags: ["unit test", "mock"]
alias:
 - /post/Mocking-NET-framework-SmtpClient-class.aspx/index.html
---
This Saturday like the last two I planned to work on my wooden terrace, but with the weather we have for this year's spring, it was almost impossible. So I replaced that with some development.

I am using [Rhino.Mocks](http://www.ayende.com/projects/rhino-mocks.aspx) as mock object framework and went to the following solution to mock [SmtpClient](http://msdn.microsoft.com/en-us/library/system.net.mail.smtpclient.aspx).
<!-- more -->

Capabilities of Rhino.Mock are to mock interfaces, delegates and virtual methods of classes!

My goal was to test my MailService class which use [SmtpClient](http://msdn.microsoft.com/en-us/library/system.net.mail.smtpclient.aspx) and in particular the method [SendAsync](http://msdn.microsoft.com/en-us/library/x5x13z6h.aspx), which is not a virtual method. SmtpClient inherit from System.Object so no way to use an interface for the unit test. 

Next step was then to make an interface, ISmtpClient, out of the SmtpClient of the .NET Framework using [Reflector for .Net](http://www.aisto.com/roeder/dotnet/). Then I modified the dependency of my MailService class from SmtpClient of the .NET framework to my interface ISmtpClient.
  <div style="font-weight: bold; font-size: 10pt; background: black; color: white; font-family: consolas">   

<span style="color: #cc7832">using</span> System.Net;

<span style="color: #cc7832">using</span> System.Net.Mail;

<span style="color: #cc7832">using</span> System.Security.Cryptography.X509Certificates;

<span style="color: #cc7832">using</span> System.Security.Permissions;

<span style="color: #cc7832">namespace</span> TechHeadBrothers.Portal.Services.Mail

{

    <span style="color: #cc7832">public</span> <span style="color: #cc7832">interface</span> <span style="color: #6897bb">ISmtpClient</span>

    {

        <span style="color: gray">// Events</span>

        <span style="color: #cc7832">event</span> <span style="color: #6897bb">SendCompletedEventHandler</span> SendCompleted;

        <span style="color: gray">// Properties</span>

        <span style="color: #ffc66d">X509CertificateCollection</span> ClientCertificates { <span style="color: #cc7832">get</span>; }

        <span style="color: #6897bb">ICredentialsByHost</span> Credentials { <span style="color: #cc7832">get</span>; <span style="color: #cc7832">set</span>; }

        <span style="color: #6897bb">SmtpDeliveryMethod</span> DeliveryMethod { <span style="color: #cc7832">get</span>; <span style="color: #cc7832">set</span>; }

        <span style="color: #cc7832">bool</span> EnableSsl { <span style="color: #cc7832">get</span>; <span style="color: #cc7832">set</span>; }

        <span style="color: #cc7832">string</span> Host { <span style="color: #cc7832">get</span>; <span style="color: #cc7832">set</span>; }

        <span style="color: #cc7832">string</span> PickupDirectoryLocation { <span style="color: #cc7832">get</span>; <span style="color: #cc7832">set</span>; }

        <span style="color: #cc7832">int</span> Port { <span style="color: #cc7832">get</span>; <span style="color: #cc7832">set</span>; }

        <span style="color: #ffc66d">ServicePoint</span> ServicePoint { <span style="color: #cc7832">get</span>; }

        <span style="color: #cc7832">int</span> Timeout { <span style="color: #cc7832">get</span>; <span style="color: #cc7832">set</span>; }

        <span style="color: #cc7832">bool</span> UseDefaultCredentials { <span style="color: #cc7832">get</span>; <span style="color: #cc7832">set</span>; }

        <span style="color: gray">// Methods</span>

        <span style="color: #cc7832">void</span> Send(<span style="color: #ffc66d">MailMessage</span> message);

        <span style="color: #cc7832">void</span> Send(<span style="color: #cc7832">string</span> from, <span style="color: #cc7832">string</span> recipients, <span style="color: #cc7832">string</span> subject, <span style="color: #cc7832">string</span> body);

        [<span style="color: #ffc66d">HostProtection</span>(<span style="color: #6897bb">SecurityAction</span><span style="font-weight: normal">.LinkDemand, ExternalThreading = </span><span style="color: #cc7832">true</span>)]

        <span style="color: #cc7832">void</span> SendAsync(<span style="color: #ffc66d">MailMessage</span> message, <span style="color: #cc7832">object</span> userToken);

        [<span style="color: #ffc66d">HostProtection</span>(<span style="color: #6897bb">SecurityAction</span><span style="font-weight: normal">.LinkDemand, ExternalThreading = </span><span style="color: #cc7832">true</span>)]

        <span style="color: #cc7832">void</span> SendAsync(<span style="color: #cc7832">string</span> from, <span style="color: #cc7832">string</span> recipients, <span style="color: #cc7832">string</span> subject, <span style="color: #cc7832">string</span> body, <span style="color: #cc7832">object</span> userToken);

        <span style="color: #cc7832">void</span> SendAsyncCancel();

    }

}
 </div>  

Then I wrote the class SmtpClientProxy. It is based on the [design pattern Proxy](http://en.wikipedia.org/wiki/Proxy_pattern). So it basically maintains a reference, and controls access, to the real SmtpClient so it can be used in place of the real SmtpClient.
  <div style="font-weight: bold; font-size: 10pt; background: black; color: white; font-family: consolas">   

<span style="color: #cc7832">namespace</span> TechHeadBrothers.Portal.Services.Mail

{

    <span style="color: #cc7832">public</span> <span style="color: #cc7832">class</span> <span style="color: #ffc66d">SmtpClientProxy</span> : <span style="color: #6897bb">ISmtpClient</span>

    {

        <span style="color: #cc7832">private</span> <span style="color: #cc7832">readonly</span> <span style="color: #ffc66d">SmtpClient</span> smtpClient;

        <span style="color: #cc7832">public</span> SmtpClientProxy()

        {

            smtpClient = <span style="color: #cc7832">new</span> <span style="color: #ffc66d">SmtpClient</span>();

            smtpClient.SendCompleted += smtpClient_SendCompleted;

        }

<span style="color: #da4832">        #region</span> ISmtpClient Members

        <span style="color: #cc7832">public</span> <span style="color: #cc7832">event</span> <span style="color: #6897bb">SendCompletedEventHandler</span> SendCompleted;

        <span style="color: #cc7832">public</span> <span style="color: #ffc66d">X509CertificateCollection</span> ClientCertificates

        {

            <span style="color: #cc7832">get</span> { <span style="color: #cc7832">return</span> smtpClient.ClientCertificates; }

        }

        <span style="color: #cc7832">public</span> <span style="color: #6897bb">ICredentialsByHost</span> Credentials

        {

            <span style="color: #cc7832">get</span> { <span style="color: #cc7832">return</span> smtpClient.Credentials; }

            <span style="color: #cc7832">set</span> { smtpClient.Credentials = <span style="color: #cc7832">value</span>; }

        }
 </div>  

Finally my MailService class with the dependency injection of ISmtpClient interface and a default constructor using my SmtpClientProxy :
  <div style="font-weight: bold; font-size: 10pt; background: black; color: white; font-family: consolas">   

<span style="color: #cc7832">namespace</span> TechHeadBrothers.Portal.Services.Mail

{

    <span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"><summary></span>

    <span style="color: gray">///</span><span style="color: green"> Service to deliver Emails</span>

    <span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"></summary></span>

    <span style="color: #cc7832">public</span> <span style="color: #cc7832">class</span> <span style="color: #ffc66d">MailService</span> : <span style="color: #6897bb">IMailService</span>

    {

        <span style="color: #cc7832">private</span> <span style="color: #cc7832">readonly</span> <span style="color: #6897bb">ISmtpClient</span> smtpClient;

        <span style="color: #cc7832">public</span> MailService() : <span style="color: #cc7832">this</span>(<span style="color: #cc7832">new</span> <span style="color: #ffc66d">SmtpClientProxy</span>())

        {

        }

        <span style="color: #cc7832">public</span> MailService(<span style="color: #6897bb">ISmtpClient</span> smtpClient)

        {

            <span style="color: #cc7832">this</span><span style="font-weight: normal">.smtpClient = smtpClient;</span>

        }
 </div>  

In my unit test I will use the constructor in which I can specify the mock of ISmtpClient, otherwise I will use the default constructor.

So that was for the first issue; having the possibility to mock SmtpClient. Now you certainly have realized the second issue that popped up. In my ISmtpClient I have one event:
  <div style="font-weight: bold; font-size: 10pt; background: black; color: white; font-family: consolas">   

    <span style="color: #cc7832">public</span> <span style="color: #cc7832">interface</span> <span style="color: #6897bb">ISmtpClient</span>

    {

        <span style="color: gray">// Events</span>

        <span style="color: #cc7832">event</span> <span style="color: #6897bb">SendCompletedEventHandler</span> SendCompleted;
 </div>  

This event is for sure also in my proxy class, SmtpClientProxy as it inherit form ISmtpClient. In the constructor of SmtpClientProxy I add an event handler on the SendCompleted event of the SmtpClient. This event handler just fires the event exposed by the proxy class, so that I can have an event handler in MailService class to handle the SendComplete event.
  <div style="font-weight: bold; font-size: 10pt; background: black; color: white; font-family: consolas">   

        <span style="color: #cc7832">private</span> <span style="color: #cc7832">void</span> smtpClient_SendCompleted(<span style="color: #cc7832">object</span> sender, <span style="color: #ffc66d">AsyncCompletedEventArgs</span> e)

        {

            <span style="color: #cc7832">if</span> (SendCompleted != <span style="color: #cc7832">null</span>)

                SendCompleted(sender, e);

        }
 </div>  

Nothing really special. But now the question rise! I need to mock ISmtpClient in my unit test. But my MailService SendMailMessage method call the SmtpClient.SendAsync method and also add an event handler to SmtpClient.SendCompleted event.
  <div style="font-weight: bold; font-size: 10pt; background: black; color: white; font-family: consolas">   

        <span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"><summary></span>

        <span style="color: gray">///</span><span style="color: green"> Sends a MailMessage object using the SMTP settings.</span>

        <span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"></summary></span>

        <span style="color: gray">///</span><span style="color: green"> </span><span style="color: gray"><param name="mailMessage"></span><span style="color: green">Email message to be sent</span><span style="color: gray"></param></span>

        <span style="color: #cc7832">public</span> <span style="color: #cc7832">void</span> SendMailMessage(<span style="color: #ffc66d">MailMessage</span> mailMessage)

        {

            <span style="color: #cc7832">try</span>

            {

                mailMessage.IsBodyHtml = <span style="color: #cc7832">true</span>;

                mailMessage.BodyEncoding = <span style="color: #ffc66d">Encoding</span><span style="font-weight: normal">.UTF8;</span>

                <span style="color: #cc7832">this</span><span style="font-weight: normal">.message = mailMessage;</span>

                smtpClient.SendCompleted += smtpClient_SendCompleted;

                smtpClient.SendAsync(mailMessage, <span style="color: #cc7832">null</span>);

            }

            <span style="color: #cc7832">catch</span> (<span style="color: #ffc66d">SmtpException</span>)

            {

                <span style="color: #cc7832">this</span><span style="font-weight: normal">.OnEmailFailed(mailMessage);</span>

            }

        }

        <span style="color: #cc7832">private</span> <span style="color: #cc7832">void</span> smtpClient_SendCompleted(<span style="color: #cc7832">object</span> sender, <span style="color: #ffc66d">AsyncCompletedEventArgs</span> e)

        {

            <span style="color: #cc7832">this</span><span style="font-weight: normal">.OnEmailSent(message);</span>

        }
 </div>  

In my mock then I need to have the same thing happening, even if I mock the interface ISmtpClient.

Here is the solution I came to:
  <div style="font-weight: bold; font-size: 10pt; background: black; color: white; font-family: consolas">   

<span style="color: #cc7832">namespace</span> TechHeadBrothers.Portal.Services.Tests.Mail

{

    [<span style="color: #ffc66d">TestFixture</span>]

    <span style="color: #cc7832">public</span> <span style="color: #cc7832">class</span> <span style="color: #ffc66d">MailServiceTest</span>

    {

<span style="color: #da4832">        #region</span> Setup/Teardown

        [<span style="color: #ffc66d">SetUp</span>]

        <span style="color: #cc7832">public</span> <span style="color: #cc7832">void</span> SetUp()

        {

            mocks = <span style="color: #cc7832">new</span> <span style="color: #ffc66d">MockRepository</span>();

        }

<span style="color: #da4832">        #endregion</span>

        <span style="color: #cc7832">private</span> <span style="color: #ffc66d">MockRepository</span> mocks;

        [<span style="color: #ffc66d">Test</span>]

        <span style="color: #cc7832">public</span> <span style="color: #cc7832">void</span> SendMailMessageRaiseEmailSentEvent()

        {

            <span style="color: #cc7832">bool</span> emailSentRaised = <span style="color: #cc7832">false</span>;

            <span style="color: #cc7832">var</span> message = <span style="color: #cc7832">new</span> <span style="color: #ffc66d">MailMessage</span>(<span style="color: #a5c25c">"lk@test.com"</span>, <span style="color: #a5c25c">"mk@test.com"</span>);

            <span style="color: #cc7832">var</span> smtpClient = mocks.Stub<<span style="color: #6897bb">ISmtpClient</span><span style="font-weight: normal">>();</span>

            <span style="color: #cc7832">var</span> mailService = <span style="color: #cc7832">new</span> <span style="color: #ffc66d">MailService</span>(smtpClient);

            mailService.EmailSent += ((sender, e) => { emailSentRaised = <span style="color: #cc7832">true</span>; });

            <span style="color: #cc7832">using</span> (mocks.Record())

            {

                <span style="color: #cc7832">var</span> raiser = <span style="color: #ffc66d">Expect</span><span style="font-weight: normal">.Call(() => smtpClient.SendCompleted += </span><span style="color: #cc7832">null</span>)

                                   .IgnoreArguments().GetEventRaiser();

                <span style="color: #ffc66d">Expect</span><span style="font-weight: normal">.Call(() => smtpClient.SendAsync(message, </span><span style="color: #cc7832">null</span>))

                      .Do((<span style="color: #6897bb">Action</span><span style="font-weight: normal"><</span><span style="color: #ffc66d">MailMessage</span>, <span style="color: #cc7832">object</span><span style="font-weight: normal">>) ((arg1, arg2) => raiser.Raise(message, </span><span style="color: #cc7832">null</span>)));

            }

            <span style="color: #cc7832">using</span> (mocks.Playback())

            {

                mailService.SendMailMessage(message);

                <span style="color: #ffc66d">Assert</span><span style="font-weight: normal">.That(emailSentRaised, </span><span style="color: #ffc66d">Is</span><span style="font-weight: normal">.EqualTo(</span><span style="color: #cc7832">true</span>));

            }

        }

    }

}
 </div>  

I create a mock of the interface ISmtpClient. Then I inject this mock into my MailService class. I create a event handler for my MailService.EmailSent event using a lambda. This lambda, if called, will change the boolean value emailSentRaised from false to true. This exactly what I want to test in that unit test; that the EmailSent event is raised.

Then I get raiser object, a Rhino Mock [IEventRaiser](http://www.ayende.com/wiki/Rhino+Mocks+IEventRaiser.ashx), from smtpClient.SendCompleted event.

Finally I set an expectation on smtpClient.SendAsync method adding a [Do() handler](http://www.ayende.com/wiki/Rhino+Mocks+The+Do()+Handler.ashx) that will raise the SendCompleted event.

Last point is using the mailService object, calling the SendMailMessage method and asserting that my boolean emailSentRaised went from false to true.

And here is the result in [ReSharper](http://www.jetbrains.com/resharper) Unit Test Session window. Green!

![](http://farm4.static.flickr.com/3251/2540010420_6643d7e119_o.png) 

<u>**Update**</u>: By the way, I forgot to add a link to a post from [Phil Haack](http://haacked.com/articles/AboutHaacked.aspx) "[Using Rhino Mocks To Unit Test Events on Interfaces](http://haacked.com/archive/2006/06/23/usingrhinomockstounittesteventsoninterfaces.aspx)" and another link to a post from [Jean-Paul S. Boodhoo](http://codebetter.com/blogs/jean-paul_boodhoo/default.aspx) "[Raising events (from a mock) using Rhino Mocks](http://codebetter.com/blogs/jean-paul_boodhoo/archive/2007/05/07/raising-events-from-a-mock-using-rhino-mocks.aspx)". To great post to read!
