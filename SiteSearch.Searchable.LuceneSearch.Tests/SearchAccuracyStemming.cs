using Searchable;
using Shouldly;
using Lucene.Net.Store;
using SiteSearch.Searchable.SearchableContent.Factories;
using SiteSearch.Searchable.SearchableContent;
using SiteSearch.Searchable.LuceneSearch;

namespace SiteSearch.LuceneSearcn.Tests
{
  public class SearchAccuracyStemming
  {

    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void OneWordSearchMatchesTitleOrBody()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "content");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("title", 0, 10);
          results.Count().ShouldBe(1);

          results = provider.Search("content", 0, 10);
          results.Count().ShouldBe(1);

          provider.CleanUp();
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void OneWordSearchMatchesTitleOrBodyCaseInsensitive()
    {
      Content content = ContentFactory.WebPage("1", "/url", "tITle", "coNTent");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("title", 0, 10);
          results.Count().ShouldBe(1);

          results = provider.Search("content", 0, 10);
          results.Count().ShouldBe(1);

          provider.CleanUp();
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void OneWordSearchMatchesTitleOrBodyPluralInContent()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "this is the contents of the document");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("content", 0, 10);
          results.Count().ShouldBe(1);

          results = provider.Search("cxntent", 0, 10);
          results.Count().ShouldBe(0);

        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void TwoWordSearchMatchesTitleOrBody1()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title boo", "content");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("title boo", 0, 10);
          results.Count().ShouldBe(1);

          results = provider.Search("boo title", 0, 10);
          results.Count().ShouldBe(1);
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void TwoWordSearchMatchesTitleOrBody2()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title boo", "content ra");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("content ra", 0, 10);
          results.Count().ShouldBe(1);

          results = provider.Search("ra content", 0, 10);
          results.Count().ShouldBe(1);
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void TwoWordSearchMatchesTitleAndBody1()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "content boo");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("title boo", 0, 10);
          results.Count().ShouldBe(1);

          results = provider.Search("boo title", 0, 10);
          results.Count().ShouldBe(1);
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void TwoWordFalseSearchMatchesTitleAndBody1()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "content boo");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("title boo zoo", 0, 10);
          results.Count().ShouldBe(0);

          results = provider.Search("title boo content", 0, 10);
          results.Count().ShouldBe(1);
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void TwoWordFalseSearchMatchesTitleAndBody2()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "content boo zombo fool");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("title boo zoo", 0, 10);
          results.Count().ShouldBe(0);

          results = provider.Search("title boo fool", 0, 10);
          results.Count().ShouldBe(1);

          results = provider.Search("title fool", 0, 10);
          results.Count().ShouldBe(1);

          results = provider.Search("title1 boo fool", 0, 10);
          results.Count().ShouldBe(0);
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void KeyExists()
    {
      Content webPage = ContentFactory.WebPage("id", "url", "title", "text");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        provider.Add(webPage);
        ContentField key = webPage.Key();
        provider.KeyExists(key).ShouldBeTrue();

        provider.CleanUp();
      }
    }
  }
}