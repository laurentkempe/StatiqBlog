---
title: "Application Acceptance Testing"
permalink: /2010/07/15/Application-Acceptance-Testing/
date: 7/15/2010 1:01:39 AM
updated: 7/15/2010 1:01:39 AM
disqusIdentifier: 20100715010139
tags: ["Agile", "BDD", "innoveo solutions", "Jobping", "acceptance test"]
alias:
 - /post/Application-Acceptance-Testing.aspx/index.html
---
Yesterday evening I found a set of Google blog posts talking about web application acceptance testing which reflect Google experience through “Several years of experience across multiple project teams”.

This reflect lot of points I brought into our discussions either at [Innoveo](http://www.innoveo.com/) or [Jobping](http://www.jobping.com); best practices, screen/page object, dev. language, recording/coding, BDD..
<!-- more -->

Here are the blog posts:

*   **[Survival techniques for acceptance tests of web applications (Part 1)](http://googletesting.blogspot.com/2009/04/survival-techniques-for-acceptance.html)**
*   **[Survival techniques for web app acceptance tests, Part 2: increasing effectiveness and utility](http://googletesting.blogspot.com/2009/05/survival-techniques-for-web-app.html)**
*   **[Web app acceptance test survival techniques, Part 3: Musings](http://googletesting.blogspot.com/2009/05/web-app-acceptance-test-survival.html)**  

And the key points I extracted from those three blog posts:

**Best practices to have long-lived tests**

> “Acceptance tests must meet the needs of several groups, including the users and the developers. **Long-lived tests must be written in the language of each group, using terms users will recognize and a programming language and style in which the developers are competent**.”
> 
> “**Utilities such as recording tools can help reduce the effort required to discover how to interact with the web application**. The open-source test automation tool Selenium ([http://seleniumhq.org/](http://seleniumhq.org/)) includes a simple IDE record and playback tool that runs in the Firefox browser. **Recorded scripts can help bootstrap your automated tests. However, don’t be tempted to consider the recorded scripts as automated tests: they’re unlikely to be useful for long.** **Instead, plan to design and implement your test code properly, using good software design techniques**.”
> 
> “Several years of experience across multiple project teams have taught us that the **tests are more likely to survive when they’re familiar and close to the developers**. **Use their programming language, put them in their codebase, use their test automation framework** (and even their operating system). We need to reduce the effort of maintaining the tests to a minimum. Get the developers to review the automated tests (whether they write them or you do) and actively involve them when designing and implementing the tests.”
> 
> “**Isolate things that change from those that don’t**. For example, separate user account data from your test code. The separation makes changes easier, faster, and safer to implement, compared to making updates in the code for each test.”
> 
> “Robust tests can continue to operate correctly even when things change in the application being tested or in the environment. Web applications use HTML, so **try to add IDs and CSS classes to relevant elements of the application
> **…
> **Try to avoid brittle identifiers, such as xpath expressions that rely on positional data**. For example, /div[3]/div[1] becomes unreliable as soon as any of the positions change – and problems may be hard to identify unless the change is easy to identify.
> …
> **Add guard conditions that assert your assumptions are still accurate**. Design the tests to fail if any of the assumptions prove false.
> …
> **Try to only make positive assertions**. For example, if you expect an action to cause an item to be added to a list, assert that after the action the list contains the expected value, not that the list has changed size (because other functionality may affect the size). **Also, if it's not something your test is concerned about, don't make assertions about it.**”
> 
> “Informative tests
> Help your tests to help others by being informative. **Use a combination of meaningful error messages and more detailed logs to help people to tell whether the tests are working as intended and, if problems occur, to figure out what’s going wrong**.”
> 
> “**Taking screenshots of the UI when a problem occurs can help** to debug the issue and disambiguate between mismatches in our assumptions vs. problems in the application.
> …
> **Debug traces are useful for diagnosing acute problems**, and range from simple debug statements like ‘I made it to this point’ to dumps of the entire state of values returned from the application by our automation tool. In comparison, logging is intended for longer-term tracking of behaviour which enables larger-scale thinking, such as enabling a test to be reproduced reliably over time.
> **…
> ****Good error messages should say what’s expected and include the actual values being compared**. Here are two examples of combinations of tests and assert messages, the second more helpful than the first:
> 
> 1. Int actualResult = addTwoRandomOddNumbers();
> 
> assertTrue("Something wrong with calculation", actualResult % 2 == 0);
> 
> 2. Int actualResult = addTwoRandomOddNumbers(number1, number2);
> 
> assertEquals(String.format("Adding two odd numbers [%d] and [%d] should return an even result. Calculated result = %d", number1, number2, actualResult) actualResult % 2 == 0);”
> 
> “My advice for developing acceptance tests for Web applications: **start simple, keep them simple, and find ways to build and establish trust in your automation code.
> **…
> **The value of the tests, and their ability to act as safety rails, is directly related to how often failing tests are a "false positive." Too many false positives, and a team loses trust in their acceptance tests entirely**.
> …
> **Acceptance tests aren’t a ‘silver bullet.’** They don’t solve all our problems or provide complete confidence in the system being tested (real life usage generates plenty of humbling experiences). They should be backed up by comprehensive automated unit tests and tests for quality attributes such as performance and security. **Typically, unit tests should comprise 70% of our functional tests, integration tests 20%, and acceptance tests the remaining 10%.**”
> 
> “Lots of bugs are discovered by means other than automated testing – they might be reported by users, for example. Once these bugs are fixed, the fixes must be tested. The tests must establish whether the problem has been fixed and, where practical, show that the root cause has been addressed. Since we want to make sure the bug doesn’t resurface unnoticed in future releases, having automated tests for the bug seems sensible. **Create the acceptance tests first, and make sure they expose the problem; then fix the bug and run the tests again to ensure the fix works**.”

**About Screen/Page objects**

> “**Effective test designs**
> 
> By using effective test designs, we can make tests easier to implement and maintain. The initial investment is minor compared to the benefits. One of my favourite designs is called **Page Objects (see [PageObjects on the Google Code site](http://code.google.com/p/webdriver/wiki/PageObjects)**). **A PageObject represents part or all of a page in a web application – something a user would interact with. A PageObject provides services to your test automation scripts and encapsulates the nitty-gritty details of how these services are performed. By encapsulating the nitty-gritty stuff, many changes to the web application, such as the reordering or renaming of elements, can be reflected in one place in your tests. A well-designed PageObject separates the ‘what’ from the ‘how’.**”

**BDD – Behavior Driven Development**

> “**Another effective test design** is based on three simple words: ‘given’, ‘when’, and ‘then’. As a trio they reflect the essential elements of many tests: given various preconditions and expectations, when such-and-such happens, then I expect a certain result.
> 
> // Given I have a valid user account and am at the login page,
> 
> // When I enter the account details and select the Enter button,
> 
> // Then I expect the inbox to be displayed with the most recent email selected.”

To me all those points are valid ! And the best is that most of them aren’t bound to a technology, a framework or whatsoever. 

I could apply those principles on different applications built using [ASP.NET MVC](http://www.asp.net/mvc), [WPF](http://msdn.microsoft.com/en-us/library/ms754130.aspx), [Java JSF](http://java.sun.com/javaee/javaserverfaces/) with different frameworks like [MSpec](http://github.com/machine/machine.specifications), [Watin](http://watin.sourceforge.net/), [White](http://white.codeplex.com/), [Selenium](http://seleniumhq.org/), [JUnit](http://www.junit.org/)/[NUnit](http://nunit.org/)…

You might read more about this on the following posts:

*   [Automated WPF functional tests using White](http://www.laurentkempe.com/post/Automated-WPF-functional-tests-using-White.aspx)
*   [White’s tip for your automated WPF functional tests](http://www.laurentkempe.com/post/Whitee28099s-tip-for-your-automated-WPF-functional-tests.aspx)
*   [Automated functional tests using Watin and MSpec](http://www.laurentkempe.com/post/Automated-functional-tests-using-Watin-and-MSpec.aspx)
*   [ASP.NET MVC 2, MSpec and Watin](http://www.laurentkempe.com/post/ASPNET-MVC-2-MSpec-and-Watin.aspx)
*   [From WPF functional Unit Tests to Specifications using MSpec and White](http://www.laurentkempe.com/post/From-WPF-functional-Unit-Tests-to-Specifications-using-MSpec-and-White.aspx)
