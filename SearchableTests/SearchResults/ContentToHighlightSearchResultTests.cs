using Lucene.Net.Store;
using LuceneSearch;
using Searchable;
using Searchable.SearchableContent;
using Searchable.SearchableContent.Factories;
using Searchable.SearchResults.Factories;
using Searchable.Stemming;
using Shouldly;

namespace SearchableTests
{
  public class ContentToHighlightSearchResultTests
  {
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void MatchWithDotsLeft()
    {
      IStemmer stemmer = new Stemmer();
      Content content = ContentFactory.WebPage("url", "/url", "The Dog and the Cat", "this is all about dog and cats");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("dog", 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], "dog", 2, stemmer);

          result.Title.ShouldBe("The <strong>Dog</strong> and the Cat");
          result.WordMatches.Count().ShouldBe(1);

          string str = result.WordMatches.ToList()[0];

          result.WordMatches.ToList()[0].ShouldBe("...all about <strong>dog</strong> and cats");
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void MatchWithDotsRight()
    {
      IStemmer stemmer = new Stemmer();
      Content content = ContentFactory.WebPage("url", "/url", "The Dog and the Cat", "all about dog and big cats");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("dog", 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], "dog", 2, stemmer);

          result.Title.ShouldBe("The <strong>Dog</strong> and the Cat");
          result.WordMatches.Count().ShouldBe(1);

          string str = result.WordMatches.ToList()[0];

          result.WordMatches.ToList()[0].ShouldBe("...all about <strong>dog</strong> and big...");
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void MatchWithDotsLeftAndRightCase1()
    {
      IStemmer stemmer = new Stemmer();
      Content content = ContentFactory.WebPage("url", "/url", "The Dog and the Cat", "about this really big Dog and big cats which were furry");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("dog", 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], "dog", 2, stemmer);

          result.Title.ShouldBe("The <strong>Dog</strong> and the Cat");
          result.WordMatches.Count().ShouldBe(1);

          string str = result.WordMatches.ToList()[0];
          result.Title.ShouldBe("The <strong>Dog</strong> and the Cat");
          result.WordMatches.ToList()[0].ShouldBe("...really big <strong>Dog</strong> and big...");
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void MatchWithDotsLeftAndRightCase2()
    {
      IStemmer stemmer = new Stemmer();
      Content content = ContentFactory.WebPage("url", "/url", "The dog and the Cat", "about this really big dog and big cats which were furry");
      
      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("Dog", 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], "dog", 2, stemmer);

          result.Title.ShouldBe("The <strong>dog</strong> and the Cat");
          result.WordMatches.Count().ShouldBe(1);
          result.WordMatches.ToList()[0].ShouldBe("...really big <strong>dog</strong> and big...");
        }
        finally
        {
          provider.CleanUp();
        }

      }
    }


    [Test]
    public void MatchWithUmbracoSample()
    {
      IStemmer stemmer = new Stemmer();

      Content content = ContentFactory.WebPage("url", "/url", "All About Animals", "During the early Tertiary, Africa was covered by a vast evergreen forest inhabited by an endemic forest fauna with manymost of the forest was destroyed, the forest animals taking refuge in the remaining forest islands. At the same \r\n");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("animal", 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], "animal", 2, stemmer);

          result.Title.ShouldBe("All About <strong>Animals</strong>");
          result.WordMatches.Count().ShouldBe(1);

          string str = result.WordMatches.ToList()[0];
          result.WordMatches.ToList()[0].ShouldBe("...the forest <strong>animals</strong> taking refuge...");
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }
  }
}
