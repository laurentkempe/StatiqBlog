---
title: "Unit Test using test doubles, aka Mock, Stub, Fake, Dummy"
permalink: /2010/07/17/Unit-Test-using-test-doubles-aka-Mock-Stub-Fake-Dummy/
date: 7/17/2010 12:31:53 AM
updated: 7/17/2010 12:31:53 AM
disqusIdentifier: 20100717123153
alias:
 - /post/Unit-Test-using-test-doubles-aka-Mock-Stub-Fake-Dummy.aspx/index.html
---
Following my post about [Application Acceptance Testing](http://www.laurentkempe.com/post/Application-Acceptance-Testing.aspx), we went, beginning of that week, in some very interesting discussions during a meeting at [Innoveo](http://www.innoveo.com/) in which I presented the differences between Mock and Stub in Unit Test. After the meeting as I often do I gather up from the web some posts which expressed in more details what I was talking about and made some extract of the posts.

This time I found some interesting content follow-up on my explanations on the site of [Rhino.Mock](http://ayende.com/Wiki/Rhino+Mocks+3.5.ashx) framework website and a post from Martin Fowler; [Mock’s aren’t Stubs](http://martinfowler.com/articles/mocksArentStubs.html)
<!-- more -->

Understanding the difference between a Stub and a Mock is an important distinction to do, because they have each one purpose which isn’t the same.

Some framework like the excellent [mockito](http://mockito.googlecode.com/) decided not to make the distinction in the way you create the two, some other framework like the also excellent [Rhino.Mock](http://ayende.com/Wiki/Rhino+Mocks.ashx) does this distinction. At the end what is important is that the developer understand the difference.

**Extract from **[**Rhino.Mock website**](http://ayende.com/Wiki/Rhino+Mocks+3.5.ashx) 

> “In short term, **mock are for behavior verification**, **and stub are for state verification**.
> 
> In longer term, **a mock is an object that we can set expectations on, and which will verify that the expected actions have indeed occurred**. **A stub is an object that you use in order to pass to the code under test**. You can setup expectations on it, so it would act in certain ways, but those expectations will never be verified.
> 
> In general, it is recommended to follow the principle of "**Test only one thing per test**". So each unit test should validate no more than one significant interaction with another object. This generally implies that **a given test should have no more than one mock object, but it may have several stubs**, as needed. (An exception to this would be if the "one thing" tested inherently requires expectations on two dependent objects, for example, verifying that a method on one object is only called after a method on another object has been called.)
> 
> If you want **to verify the behavior of the code under test, you will use a mock** with the appropriate expectation, and verify that. If you want **just to pass a value that may need to act in a certain way, but isn't the focus of this test, you will use a stub**.
> 
> **IMPORTANT: A stub will never cause a test to fail.”**

**Extract from [Martin Fowler post](http://martinfowler.com/articles/mocksArentStubs.html)**

> “This difference is actually two separate differences. On the one hand there is a difference in how test results are verified: a **distinction between state verification and behavior verification**. On the other hand is **a whole different philosophy to the way testing and design play together**, which I term here as the classical and mockist styles of Test Driven Development.”

Fowler in his post use an older way (post from 2007) of expressing Arrange-Act-Assert, mixed with Fixture terms: setup, exercise, verify, teardown.

Today’s the expressions Arrange-Act-Assert or Record-Replay (which he is talking later on in the post) are common.

*Stub*

> “This style of testing uses **state verification**: which means that we **determine whether the exercised method worked correctly by examining the state of the SUT and its collaborators** after the method was exercised. As we'll see, mock objects enable a different approach to verification.”

*Mock*

> “The SUT is the same - an order. However **the collaborator isn't a warehouse object, instead it's a mock warehouse** - technically an instance of the class Mock.
> 
> The second part of the **setup creates expectations on the mock object**.**The expectations indicate which methods should be called on the mocks when the SUT is exercised.**
> 
> Once all the expectations are in place I exercise the SUT. After the exercise I then **do verification, which has two aspec**ts. I run **asserts against the SUT - much as before**. However I also **verify the mocks - checking that they were called according to their expectations**.
> 
> The **key difference** here is **how we verify that the order did the right thing in its interaction with the warehouse.** With **state verification** we do this by **asserts against the warehouse's state**. **Mocks** use behavior verification, where we instead **check to see if the order made the correct calls on the warehouse**. We do this check by telling the mock what to expect during setup and asking the mock to verify itself during verification. Only the order is checked using asserts, and if the method doesn't change the state of the order there's no asserts at all.”

> “When you're doing testing like this, you're **focusing on one element of the software at a time** -hence the common term **unit testing**. The problem is that **to make a single unit work, you often need other units** - hence the need for some kind of warehouse in our example.”

Mock/Stub are also called test doubles inspired from stunt double in movies! I think the analogy is quite good.

> “Meszaros then defined four particular kinds of double:
> 
> *   **Dummy** objects are passed around but never actually used. Usually they are just **used to fill parameter lists**.
> *   **Fake** objects actually **have working implementations, but usually take some shortcut which makes them not suitable for production** (**an in memory database is a good example**).
> *   **Stubs** provide canned **answers to calls made during the test**, usually not responding at all to anything outside what's programmed in for the test. Stubs **may also record information about calls**, such as an email gateway stub that remembers the messages it 'sent', or maybe only how many messages it 'sent'.
> *   **Mocks** are what we are talking about here: **objects pre-programmed with expectations which form a specification of the calls they are expected to receive**.
> 
> Of these kinds of doubles, **only mocks insist upon behavior verification**. The other doubles can, and usually do, use state verification. Mocks actually do behave like other doubles during the exercise phase, as they need to make the SUT believe it's talking with its real collaborators - but **mocks differ in the setup and the verification phases**.”

> “**Mock objects always use behavior verification, a stub can go either way**. **Meszaros refers to stubs that use behavior verification as a Test Spy**. The difference is in how exactly the double runs and verifies and I'll leave that for you to explore on your own.”

*Test Driven Development*

> “**The classical TDD style is to use real objects if possible and a double if it's awkward to use the real thing**. So a classical TDDer would use a real warehouse and a double for the mail service. The kind of double doesn't really matter that much.
> 
> **A mockist TDD practitioner, however, will always use a mock for any object with interesting behavior**. In this case for both the warehouse and the mail service.

> ”An important offshoot of the mockist style is that of Behavior Driven Development (BDD). BDD was originally developed by my colleague Dan North as a technique to better help people learn Test Driven Development by focusing **<font color="#ff0000">on how TDD operates as a design technique</font>**. This led to **renaming tests as behaviors** to better explore where TDD helps with thinking about what an object needs to do. BDD takes a mockist approach, but it expands on this, both with its naming styles, and with its desire to integrate analysis within its technique. I won't go into this more here, as the only relevance to this article is that **BDD is another variation on TDD that tends **to use mockist testing. I'll leave it to you to follow the link for more information.”

> “Occasionally you do **run into things that are really hard to use state verification** on, even if they aren't awkward collaborations. A great **example of this is a cache**. The whole point of a cache is that **you can't tell from its state whether the cache hit or missed** - **this is a case where behavior verification would be the wise choice** for even a hard core classical TDDer. I'm sure there are other exceptions in both directions.”
> 
> “It's at this point that I should stress that<font color="#ff0000"> **whichever style of test you use, you must combine it with coarser grained acceptance tests that operate across the system as a whole**</font>. I've often come across projects which were late in using acceptance tests and regretted it.”
> 
> “**TDD's origins were a desire to get strong automatic regression testing that supported evolutionary design**. Along the way its practitioners discovered that<font color="#ff0000"> **writing tests first made a significant improvement to the design process**.</font>”
> 
> “As I've learned from Test Driven Development itself, **it's often hard to judge a technique without trying it seriously**.”
