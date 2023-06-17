using SiteSearch.Searchable.WebPages.Factories;

namespace WebPageLibraryTests
{
  public class WebPageDataToTitleTests
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void WebPageDataToTitleTest()
    {
      string title = WebPageDataToTitle.ToTitle(new Uri("http://www.website.com"), "<title>the title</title>");
      Assert.IsTrue(title == "the title");

      title = WebPageDataToTitle.ToTitle(new Uri("http://www.website.com/the-page"), "the title");
      Assert.IsTrue(title == "The Page");
    }
  }
}