---
title: "Mocking .NET framework SmtpClient class"
permalink: /2008/06/01/Mocking-NET-framework-SmtpClient-class/
date: 6/1/2008 7:41:20 AM
updated: 5/7/2010 7:52:14 AM
disqusIdentifier: 20080601074120
tags: ["unit test", "mock"]
---
This Saturday like the last two I planned to work on my wooden terrace, but with the weather we have for this year's spring, it was almost impossible. So I replaced that with some development.

I am using [Rhino.Mocks](http://www.ayende.com/projects/rhino-mocks.aspx) as mock object framework and went to the following solution to mock [SmtpClient](http://msdn.microsoft.com/en-us/library/system.net.mail.smtpclient.aspx).
<!-- more -->

Capabilities of Rhino.Mock are to mock interfaces, delegates and virtual methods of classes!

My goal was to test my MailService class which use [SmtpClient](http://msdn.microsoft.com/en-us/library/system.net.mail.smtpclient.aspx) and in particular the method [SendAsync](http://msdn.microsoft.com/en-us/library/x5x13z6h.aspx), which is not a virtual method. SmtpClient inherit from System.Object so no way to use an interface for the unit test. 

Next step was then to make an interface, ISmtpClient, out of the SmtpClient of the .NET Framework using [Reflector for .Net](http://www.aisto.com/roeder/dotnet/). Then I modified the dependency of my MailService class from SmtpClient of the .NET framework to my interface ISmtpClient.

```csharp
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace TechHeadBrothers.Portal.Services.Mail
{
    public interface ISmtpClient
    {
        // Events
        event SendCompletedEventHandler SendCompleted;

        // Properties
        X509CertificateCollection ClientCertificates { get; }

        ICredentialsByHost Credentials { get; set; }

        SmtpDeliveryMethod DeliveryMethod { get; set; }

        bool EnableSsl { get; set; }

        string Host { get; set; }

        string PickupDirectoryLocation { get; set; }

        int Port { get; set; }

        ServicePoint ServicePoint { get; }

        int Timeout { get; set; }

        bool UseDefaultCredentials { get; set; }

        // Methods
        void Send(MailMessage message);

        void Send(string from, string recipients, string subject, string body);

        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]

        void SendAsync(MailMessage message, object userToken);

        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]

        void SendAsync(string from, string recipients, string subject, string body, object userToken);

        void SendAsyncCancel();
    }
}
```

Then I wrote the class SmtpClientProxy. It is based on the [design pattern Proxy](http://en.wikipedia.org/wiki/Proxy_pattern). So it basically maintains a reference, and controls access, to the real SmtpClient so it can be used in place of the real SmtpClient.

```csharp
namespace TechHeadBrothers.Portal.Services.Mail
{
    public class SmtpClientProxy : ISmtpClient
    {
        private readonly SmtpClient smtpClient;

        public SmtpClientProxy()
        {
            smtpClient = new SmtpClient();

            smtpClient.SendCompleted += smtpClient_SendCompleted;
        }

        #region ISmtpClient Members

        public event SendCompletedEventHandler SendCompleted;

        public X509CertificateCollection ClientCertificates
        {
            get { return smtpClient.ClientCertificates; }
        }

        public ICredentialsByHost Credentials
        {
            get { return smtpClient.Credentials; }

            set { smtpClient.Credentials = value; }
        }
```

Finally my MailService class with the dependency injection of ISmtpClient interface and a default constructor using my SmtpClientProxy :

```csharp
namespace TechHeadBrothers.Portal.Services.Mail
{
    ///
    /// Service to deliver Emails
    ///
    public class MailService : IMailService
    {
        private readonly ISmtpClient smtpClient;

        public MailService() : this(new SmtpClientProxy())
        {
        }

        public MailService(ISmtpClient smtpClient)
        {

            this.smtpClient = smtpClient;
        }
```

In my unit test I will use the constructor in which I can specify the mock of ISmtpClient, otherwise I will use the default constructor.

So that was for the first issue; having the possibility to mock SmtpClient. Now you certainly have realized the second issue that popped up. In my ISmtpClient I have one event:

```csharp
public interface ISmtpClient
{
    // Events
    event SendCompletedEventHandler SendCompleted;
```

This event is for sure also in my proxy class, SmtpClientProxy as it inherit form ISmtpClient. In the constructor of SmtpClientProxy I add an event handler on the SendCompleted event of the SmtpClient. This event handler just fires the event exposed by the proxy class, so that I can have an event handler in MailService class to handle the SendComplete event.

```csharp
private void smtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
{
    if (SendCompleted != null)
        SendCompleted(sender, e);
}
```

Nothing really special. But now the question rise! I need to mock ISmtpClient in my unit test. But my MailService SendMailMessage method call the SmtpClient.SendAsync method and also add an event handler to SmtpClient.SendCompleted event.

```csharp
///
/// Sends a MailMessage object using the SMTP settings.
///
/// Email message to be sent
public void SendMailMessage(MailMessage mailMessage)
{
    try
    {
        mailMessage.IsBodyHtml = true;

        mailMessage.BodyEncoding = Encoding.UTF8;

        this.message = mailMessage;

        smtpClient.SendCompleted += smtpClient_SendCompleted;

        smtpClient.SendAsync(mailMessage, null);
    }
    catch (SmtpException)
    {
        this.OnEmailFailed(mailMessage);
    }
}

private void smtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
{
    this.OnEmailSent(message);
}
```

In my mock then I need to have the same thing happening, even if I mock the interface ISmtpClient.

Here is the solution I came to:

```csharp
namespace TechHeadBrothers.Portal.Services.Tests.Mail
{
    [TestFixture]
    public class MailServiceTest
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
        }

        #endregion

        private MockRepository mocks;

        [Test]
        public void SendMailMessageRaiseEmailSentEvent()
        {
            bool emailSentRaised = false;

            var message = new MailMessage("lk@test.com", "mk@test.com");

            var smtpClient = mocks.Stub<ISmtpClient>();

            var mailService = new MailService(smtpClient);

            mailService.EmailSent += ((sender, e) => { emailSentRaised = true; });

            using (mocks.Record())

            {
                var raiser = Expect.Call(() => smtpClient.SendCompleted += null)
                                   .IgnoreArguments().GetEventRaiser();

                Expect.Call(() => smtpClient.SendAsync(message, null))
                      .Do((Action<MailMessage, object>) ((arg1, arg2) => raiser.Raise(message, null)));
            }

            using (mocks.Playback())
            {
                mailService.SendMailMessage(message);

                Assert.That(emailSentRaised, Is.EqualTo(true));
            }
        }
    }
}
```

I create a mock of the interface ISmtpClient. Then I inject this mock into my MailService class. I create a event handler for my MailService.EmailSent event using a lambda. This lambda, if called, will change the boolean value emailSentRaised from false to true. This exactly what I want to test in that unit test; that the EmailSent event is raised.

Then I get raiser object, a Rhino Mock [IEventRaiser](http://www.ayende.com/wiki/Rhino+Mocks+IEventRaiser.ashx), from smtpClient.SendCompleted event.

Finally I set an expectation on smtpClient.SendAsync method adding a [Do() handler](http://www.ayende.com/wiki/Rhino+Mocks+The+Do()+Handler.ashx) that will raise the SendCompleted event.

Last point is using the mailService object, calling the SendMailMessage method and asserting that my boolean emailSentRaised went from false to true.

And here is the result in [ReSharper](http://www.jetbrains.com/resharper) Unit Test Session window. Green!

![](http://farm4.static.flickr.com/3251/2540010420_6643d7e119_o.png)

<u>**Update**</u>: By the way, I forgot to add a link to a post from [Phil Haack](http://haacked.com/articles/AboutHaacked.aspx) "[Using Rhino Mocks To Unit Test Events on Interfaces](http://haacked.com/archive/2006/06/23/usingrhinomockstounittesteventsoninterfaces.aspx)" and another link to a post from [Jean-Paul S. Boodhoo](http://codebetter.com/blogs/jean-paul_boodhoo/default.aspx) "[Raising events (from a mock) using Rhino Mocks](http://codebetter.com/blogs/jean-paul_boodhoo/archive/2007/05/07/raising-events-from-a-mock-using-rhino-mocks.aspx)". To great post to read!
