using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SiteSearch.TextFormatting;

namespace SiteSearch.TextFormatting.Tests
{
  [TestClass]
  public class HTMLExtractorTests
  {
    [TestMethod]
    public void HtmlExtraction1()
    {
      string html = TextFormatting.WebPageExtracter.ExtractTextFromHtml("");
      Assert.IsTrue(html == "");

      html = TextFormatting.WebPageExtracter.ExtractTextFromHtml("<p></p>");
      Assert.IsTrue(html == "");

      html = TextFormatting.WebPageExtracter.ExtractTextFromHtml("<p>1</p>");
      Assert.IsTrue(html == "1");

      html = TextFormatting.WebPageExtracter.ExtractTextFromHtml("<p>1</p><p>2</p>");
      Assert.IsTrue(html == "1 2");

      html = TextFormatting.WebPageExtracter.ExtractTextFromHtml("<p>  1           </p><p>      2  </p>");
      Assert.IsTrue(html == "1 2");
    }


    [TestMethod]
    public void HtmlExtraction2()
    {
      string html = TextFormatting.WebPageExtracter.ExtractTextFromHtml("<div><p>1 <a href=\"http://www.google.com\">link</a></p></div>");
      Assert.IsTrue(html == "1 link");

      html = "<article class=\"_2yRSr\"><header class=\"_16lwx _2-5AL\"><div class=\"_2JwnJ\"><div class=\"JwYux\"><div class=\"_2YAR2 _1dnWn\"><ul><li class=\"_26JA0 ykJgH\">Breaking</li><li class=\"_3wukJ\" data-testid=\"category\"><a title=\"Politics\" href=\"/politics\">Politics</a></li><li class=\"_3wukJ\" data-testid=\"category\"><a title=\"Federal\" href=\"/politics/federal\">Federal</a></li><li class=\"_1pMb9\" data-testid=\"tag-name\"><a title=\"Morrison portfolio saga\" href=\"/topic/morrison-portfolio-saga-6fp0\">Morrison portfolio saga</a></li></ul>";
      html = TextFormatting.WebPageExtracter.ExtractTextFromHtml(html);
      Assert.IsTrue(html == "Breaking Politics Federal Morrison portfolio saga");

      html = "<script>1</script>";
      html = TextFormatting.WebPageExtracter.ExtractTextFromHtml(html);
      Assert.IsTrue(html == "");

      html = "<script>1</script> <style>1</style> 2  ";
      html = TextFormatting.WebPageExtracter.ExtractTextFromHtml(html);
      Assert.IsTrue(html == "2");
    }


    [TestMethod]
    public void WebExtraction()
    {
      string html = TextFormatting.WebPageExtracter.ExtractTextFromWithinBody("1");
      Assert.IsTrue(html == "1");

      html = TextFormatting.WebPageExtracter.ExtractTextFromWithinBody("1 <body>2</body>");
      Assert.IsTrue(html == "2");

      html = "<html><head></head><body><article class=\"_2yRSr\"><header class=\"_16lwx _2-5AL\"><div class=\"_2JwnJ\"><div class=\"JwYux\"><div class=\"_2YAR2 _1dnWn\"><ul><li class=\"_26JA0 ykJgH\">Breaking</li><li class=\"_3wukJ\" data-testid=\"category\"><a title=\"Politics\" href=\"/politics\">Politics</a></li><li class=\"_3wukJ\" data-testid=\"category\"><a title=\"Federal\" href=\"/politics/federal\">Federal</a></li><li class=\"_1pMb9\" data-testid=\"tag-name\"><a title=\"Morrison portfolio saga\" href=\"/topic/morrison-portfolio-saga-6fp0\">Morrison portfolio saga</a></li></ul></body></html>";
      html = TextFormatting.WebPageExtracter.ExtractTextFromHtml(html);
      Assert.IsTrue(html == "Breaking Politics Federal Morrison portfolio saga");
    }


  }
}
