using Lucene.Net.Store;
using Searchable;
using Shouldly;
using SiteSearch.Searchable.LuceneSearch;
using SiteSearch.Searchable.SearchableContent;
using SiteSearch.Searchable.SearchableContent.Factories;
using SiteSearch.Searchable.SearchResults.Factories;
using SiteSearch.Searchable.Stemming;

namespace SiteSearch.Searchable.Tests
{
  public class MultipleSearchTermsInMatchTests
  {
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void MatchWhenAllWordsInFirstMatch()
    {
      IStemmer stemmer = new Stemmer();

      Content content = ContentFactory.WebPage("url", "/url", "All About Animals EarLy", "During when the days were animal the early Tertiary, Africa was covered by a vast evergreen forest inhabited by an endemic forest fauna with manymost of the forest was destroyed, the forest animals taking refuge in the remaining forest islands. At the same \r\n");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          string query = "animal early";

          var results = provider.Search(query, 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], query, 2, stemmer);
          result.Title.Highlight().ShouldBe("All About <strong>Animals</strong> <strong>EarLy</strong>");

          result.ContentMatches.Count().ShouldBe(1);
          string str = result.ContentMatches.ToList()[0].Highlight();
          str.ShouldBe("...days were <strong>animal</strong> the <strong>early</strong>...");
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }

    [Test]
    public void MatchWhenAllButOneWordsInFirstMatch()
    {
      IStemmer stemmer = new Stemmer();

      Content content = ContentFactory.WebPage("url", "/url", "All About Animals EarLy dog", "During when the days were animal the early Tertiary, Africa was covered by a dog animal early vast evergreen forest inhabited by an endemic forest fauna with manymost of the forest was destroyed, the forest animals taking refuge in the remaining forest islands. At the same \r\n");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          string searchPhrase = "animal early dog";

          var results = provider.Search(searchPhrase, 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], searchPhrase, 3, stemmer);
          result.Title.Highlight().ShouldBe("All About <strong>Animals</strong> <strong>EarLy</strong> <strong>dog</strong>");

          result.ContentMatches.Count().ShouldBe(2);
          string str = result.ContentMatches.ToList()[0].Highlight();
          str.ShouldBe("...the days were <strong>animal</strong> the <strong>early</strong> Tertiary,...");

          str = result.ContentMatches.ToList()[1].Highlight();
          str.ShouldBe("...covered by a <strong>dog</strong> <strong>animal</strong> <strong>early</strong> vast...");
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }

    [Test]
    public void MatchWhenAllButOneWordsInLastMatch()
    {
      IStemmer stemmer = new Stemmer();

      Content content = ContentFactory.WebPage("url", "/url", "All About Animals EarLy dog", "During when the days were animal the early Tertiary, Africa was covered by a dog animal early vast evergreen forest inhabited by an endemic forest fauna with manymost of the forest was destroyed, the forest animals taking refuge in the remaining forest islands. At the same \r\n");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          string searchPhrase = "days dog animal early";

          var results = provider.Search(searchPhrase, 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], searchPhrase, 3, stemmer);
          result.Title.Highlight().ShouldBe("All About <strong>Animals</strong> <strong>EarLy</strong> <strong>dog</strong>");

          result.ContentMatches.Count().ShouldBe(2);
          string str = result.ContentMatches.ToList()[0].Highlight();
          str.ShouldBe("During when the <strong>days</strong> were <strong>animal</strong> the...");

          str = result.ContentMatches.ToList()[1].Highlight();
          str.ShouldBe("...covered by a <strong>dog</strong> <strong>animal</strong> <strong>early</strong> vast...");
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }



  }
}
