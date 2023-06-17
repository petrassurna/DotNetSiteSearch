using Shouldly;
using Searchable;
using SiteSearch.Searchable.LuceneSearch;

namespace WebPageLibraryTests
{
  public class StemmingTests
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PluralizeTests()
    {
      ISearchProvider provider = new LuceneProvider();

      provider.Stem("cats").ShouldBe("cat");
      provider.Stem("contacts").ShouldBe("contact");
      provider.Stem("churches").ShouldBe("church");
      provider.Stem("bases").ShouldBe("basis");
      provider.Stem("photos").ShouldBe("photo");
    }

  }
}