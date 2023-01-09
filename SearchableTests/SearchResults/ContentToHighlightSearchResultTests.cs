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
          int wordsEachSide = 2;

          string query = "dog";

          var results = provider.Search(query, 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], query, wordsEachSide, stemmer);

          result.Title.Highlight(query).ShouldBe("The <strong>Dog</strong> and the Cat");
          result.ContentMatches.Count().ShouldBe(1);

          string str = result.ContentMatches.ToList()[0].Highlight(query);

          str.ShouldBe("...all about <strong>dog</strong> and cats");
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

          string query = "dog";

          var results = provider.Search(query, 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], query, 2, stemmer);

          result.Title.Highlight(query).ShouldBe("The <strong>Dog</strong> and the Cat");
          result.ContentMatches.Count().ShouldBe(1);

          string str = result.ContentMatches.ToList()[0].Highlight(query);
          str.ShouldBe("all about <strong>dog</strong> and big...");
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

          string query = "dog";

          var results = provider.Search(query, 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], query, 2, stemmer);

          result.Title.Highlight(query).ShouldBe("The <strong>Dog</strong> and the Cat");
          result.ContentMatches.Count().ShouldBe(1);

          string str = result.ContentMatches.ToList()[0].Highlight(query);
          result.Title.Highlight(query).ShouldBe("The <strong>Dog</strong> and the Cat");
          str.ShouldBe("...really big <strong>Dog</strong> and big...");
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

          string query = "Dog";

          var results = provider.Search(query, 0, 10);
          results.Count().ShouldBe(1);

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], query, 2, stemmer);

          result.Title.Highlight(query).ShouldBe("The <strong>dog</strong> and the Cat");
          result.ContentMatches.Count().ShouldBe(1);

          string str = result.ContentMatches.ToList()[0].Highlight(query);
          str.ShouldBe("...really big <strong>dog</strong> and big...");
        }
        finally
        {
          provider.CleanUp();
        }

      }
    }



    [Test]
    public void MatchWithMultipleWordsInTitle()
    {
      IStemmer stemmer = new Stemmer();

      Content content = ContentFactory.WebPage("url", "/url", "All About Animal in the early vast world", "During the early Tertiary, Africa was covered by a vast evergreen forest inhabited by an endemic forest fauna with manymost of the forest was destroyed, the forest animals taking refuge in the remaining forest islands. At the same \r\n");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          string searchPhrase = "animals early vast";

          var results = provider.Search(searchPhrase, 0, 10);
          results.Count().ShouldBe(1);

          int wordsEachSide = 2;

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], searchPhrase, wordsEachSide, stemmer);

          result.Title.Highlight(searchPhrase).ShouldBe("All About <strong>Animal</strong> in the <strong>early</strong> <strong>vast</strong> world");

          string str = result.ContentMatches.ToList()[0].Highlight(searchPhrase);
          //str.ShouldBe("the forest <strong>animals</strong> taking refuge...");

          //str = result.WordMatches.ToList()[1].Highlight(query.Split(' ', StringSplitOptions.RemoveEmptyEntries), stemmer);
          //str.ShouldBe("During the <strong>early</strong> Tertiary, Africa...");
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void MatchMultipleWordsDistributedEvenly()
    {
      IStemmer stemmer = new Stemmer();

      Content content = ContentFactory.WebPage("url", "/url", "All About Animal", "During the early Tertiary, Africa was covered by a vast evergreen forest inhabited by an endemic forest fauna with manymost of the forest was destroyed, the forest animals taking refuge in the remaining forest islands. At the same \r\n");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          string query = "animal early vast";

          var results = provider.Search(query, 0, 10);
          results.Count().ShouldBe(1);

          int wordsEachSide = 2;

          var result = ContentToHighlightSearchResult.GetSearchResults(results.ToList()[0], query, wordsEachSide, stemmer);

          result.Title.Highlight(query).ShouldBe("All About <strong>Animal</strong>");
          result.ContentMatches.Count().ShouldBe(3);

          string str = result.ContentMatches.ToList()[0].Highlight(query);
          str.ShouldBe("...the forest <strong>animals</strong> taking refuge...");

          str = result.ContentMatches.ToList()[1].Highlight(query);
          str.ShouldBe("During the <strong>early</strong> Tertiary, Africa...");
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }



  }
}
