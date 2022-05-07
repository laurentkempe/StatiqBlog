---
title: "NFOP new way of creating the Pdf"
permalink: /2005/03/18/NFOP-new-way-of-creating-the-Pdf/
date: 3/18/2005 3:26:00 AM
updated: 3/18/2005 3:26:00 AM
disqusIdentifier: 20050318032600
tags: ["Tech Head Brothers", ".NET Development"]
---
I was asked about the new way to work with [NFOP](http://nfop.sourceforge.net/) to generate the pdf without the old Engine class. So here is what I am doing:

```csharp
#region  Using

using System.Net;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;
using java.io;
using log4net;
using org.apache.fop.apps;
using org.xml.sax;
using TechHeadBrothers.Portal.UI;
using StringWriter = System.IO.StringWriter;

#endregion

namespace TechHeadBrothers.TechHeadBrothers.Portal.UI
{
 /// <summary>
 /// Strategy to print PDF using FOP
 /// </summary>
 public class FOPPrintStrategy : PrintStrategy
 {
  #region Logging Definition

  private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

  #endregion

  protected override void Print(PrintArticle page, string xmlpath, string xslpah, XsltArgumentList xsltargs)
  {
   string fullFoDoc = XmlContentToFoContent(page, xmlpath, xslpah);

   InputSource source = new InputSource(fullFoDoc);
   ByteArrayOutputStream output = new ByteArrayOutputStream();

   try
   {
    Driver driver = new Driver(source, output);
    driver.setRenderer(Driver.RENDER_PDF);
    driver.run();
    output.close();

    int sz = output.buf.Length;
    byte[] pdf = new byte [sz];
    for (int i = 0; i < sz; i++)
     pdf[i] = (byte)output.buf[i];

    page.Response.ClearHeaders();
    page.Response.Clear();

    page.Response.AddHeader("Content-Disposition", "attachment; filename=TechHeadBrothers.pdf");
    page.Response.ContentType = "application/octet-stream";

    page.Response.Flush();
    page.Response.BinaryWrite(pdf);
    page.Response.End();
   }
   catch (FOPException fope)
   {
    if (log.IsDebugEnabled)
     log.Debug(fope.getMessage());
   }
  }
 }
}
```
