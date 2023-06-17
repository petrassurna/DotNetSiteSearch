using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SiteSearch.TextFormatting;

namespace SiteSearch.TextFormatting.Tests
{
  [TestClass]
  public class UriExtensionTests
  {

    [TestMethod]
    public void UriExtensionTest()
    {
      Uri uri = new Uri("http://www.website.com/page");
      Assert.IsTrue(uri.RelativeUrl() == "page");

      uri = new Uri("http://www.website.com/");
      Assert.IsTrue(uri.RelativeUrl() == "/");

      uri = new Uri("http://www.website.com/page1/page2");
      Assert.IsTrue(uri.RelativeUrl() == "page2");
    }

  }
}
