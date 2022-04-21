---
title: "Interview of Christof Wittig CEO of db4o"
permalink: /2006/12/02/Interview-of-Christof-Wittig-CEO-of-db4o/
date: 12/2/2006 6:51:14 PM
updated: 12/2/2006 6:51:14 PM
disqusIdentifier: 20061202065114
tags: [".NET Framework 2.0"]
alias:
 - /post/Interview-of-Christof-Wittig-CEO-of-db4o.aspx/index.html
---
[Patrice Lamarche](http://blogs.codes-sources.com/patrice) had the opportunity to [interview Christoff Wittig](http://blogs.codes-sources.com/patrice/archive/2006/11/30/interview-christof-wittig-ceo-de-db4o.aspx), CEO of [db4objects](http://www.db4o.com).

db4objects sponsors the open source database db4o which is a native object database engine for .NET and Java.
<!-- more -->

There are several aspects that I particularly like in what I read:

*   The way the company seems to work with distributed employees all around the world
*   The open source development model 

And from a technical aspect I really like this kind of code:

    IList<Student> students = db.Query<Student>(delegate(Student student){  
        return student.Age < 20  
          && student.Grade == gradeA;  
      });

It is named Native Queries, and it is 100% type safe, I like when the compiler already tells me that my code has a defect.

There are other good points about it that I will have to test, when I get time...
