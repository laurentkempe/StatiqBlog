---
title: "Note to self - How can I change the database owner?"
permalink: /2007/09/27/Note-to-self-How-can-I-change-the-database-owner/
date: 9/27/2007 4:39:41 PM
updated: 9/27/2007 4:39:41 PM
disqusIdentifier: 20070927043941
tags: ["Note to self", "SQL Server"]
alias:
 - /post/Note-to-self-How-can-I-change-the-database-owner.aspx/index.html
---
From: [http://www.mssqlcity.com/FAQ/Admin/DBOwner.htm](http://www.mssqlcity.com/FAQ/Admin/DBOwner.htm "http://www.mssqlcity.com/FAQ/Admin/DBOwner.htm")

> **Answer:**
<!-- more -->
> You can use the **sp_changedbowner** system stored procedure to change the database owner.
> Read about the **sp_changedbowner** stored procedure in SQL Server Books Online.
> This is the example to make the user John the owner of the pubs database:
> 
> USE pubs
GO
EXEC sp_changedbowner 'John'
GO
