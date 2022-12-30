using Searchable.SearchableContent;
using Searchable.WebPages;
using Searchable.WebPages.Factories;

namespace WebPageLibraryTests
{
  public class UriToWebPageTests
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task UriToWebPageTest()
    {
      Content page = UriToWebPage.GetWebPage(1, new Uri("http://www.website.com"), "content");

      Assert.IsTrue(page.Field("Id").Value == "1");
      Assert.IsTrue(page.Field("Url").Value == "/");
      Assert.IsTrue(page.Field("Title").Value == "");
      Assert.IsTrue(page.Field("Text").Value == "content");

      page = UriToWebPage.GetWebPage(1, new Uri("http://www.website.com"), "<h1>the title</h1> content");

      Assert.IsTrue(page.Field("Url").Value == "/");
      Assert.IsTrue(page.Field("Title").Value == "");
      Assert.IsTrue(page.Field("Text").Value == "the title content");

      string url = "https://www.yart.com.au/blog/woocommerce-hike-pos-plugin-to-improve-synchronisation/";
      Uri uri = new Uri(url);
      page = await UriToWebPage.GetWebPage(1, uri);
      Assert.IsTrue(page.Field("Url").Value == "/blog/woocommerce-hike-pos-plugin-to-improve-synchronisation/");
      string content = "Help me with WooCommerce MENU";
      Assert.IsTrue(page.Field("Title").Value == "Hike WooCommerce custom synchronisation plugin");
      Assert.IsTrue(page.Field("Text").Value.Substring(0, content.Length) == content);
    }


  }
}