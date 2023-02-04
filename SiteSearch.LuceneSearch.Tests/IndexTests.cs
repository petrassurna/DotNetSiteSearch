using Searchable;
using Shouldly;
using Lucene.Net.Store;
using SiteSearch.LuceneSearch;
using SiteSearch.Searchable.SearchableContent;
using SiteSearch.Searchable.SearchableContent.Factories;

namespace SiteSearch.LuceneSearcn.Tests
{
  public class IndexTests
  {

    [SetUp]
    public void Setup()
    {
    }



    [Test]
    public void OneDocumentCanBeAddedToIndex()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "content");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);
          var results = provider.Search("title", 0, 10);
          results.Count().ShouldBe(1);
        }
        finally
        {
          provider.CleanUp();
        }
      }

    }


    [Test]
    public void OneDocumentCanBeAddedToIndexTwice()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "content");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        provider.Add(content);
        provider.Add(content);

        var results = provider.SearchById(content.Field("Id"));
        Assert.That(results.Count() == 2);

        provider.CleanUp();
      }

    }


    [Test]
    public void OneDocumentCantBeAddedToIndexTwice1()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "content");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          var results = provider.SearchById(content.First());
          Assert.That(results.Count() == 0);

          provider.AddIfIdDoesntExist(content);
          results = provider.SearchById(content.First());
          Assert.That(results.Count() == 1);

          try
          {
            provider.AddIfIdDoesntExist(content);
            Assert.Fail();
          }
          catch (Exception)
          {
          }
        }
        finally
        {
          provider.CleanUp();
        }
      }

    }


    [Test]
    public void OneDocumentCantBeAddedToIndexTwice2()
    {
      Content content = ContentFactory.WebPage("abcdefgjhjggjyfftftftftcbccfcffcf", "url", "title", "content");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          var results = provider.SearchById(content.First());
          Assert.That(results.Count() == 0);

          provider.AddIfIdDoesntExist(content);
          results = provider.SearchById(content.First());
          Assert.That(results.Count() == 1);

          try
          {
            provider.AddIfIdDoesntExist(content);
            Assert.Fail();
          }
          catch (Exception)
          {
          }
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void DocumentCanBeAddedAndDeleted()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "content");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);

          var results = provider.Search("title", 0, 10);
          results.Count().ShouldBe(1);

          provider.Delete(content);

          results = provider.Search("title", 0, 10);
          results.Count().ShouldBe(0);
        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void DocumentCanBeAddedAndUpdated()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "content");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);
          provider.CleanUp();

          var results = provider.Search("title", 0, 10);
          results.Count().ShouldBe(1);

          content.Field("Title").Value = "boo";
          provider.Update(content);

          results = provider.Search("title", 0, 10);
          results.Count().ShouldBe(0);

          results = provider.Search("boo", 0, 10);
          results.Count().ShouldBe(1);
        }
        catch (Exception)
        {

        }
        finally
        {
          provider.CleanUp();
        }
      }
    }


    [Test]
    public void ManyDocumentsCanBeAddedToIndex()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "content");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          for (int i = 0; i < 100; i++)
          {
            content.Field("Id").Value = i.ToString();
            provider.Add(content);
          }

          var results = provider.Search("title", 0, 1000);
          results.Count().ShouldBe(100);
        }
        finally
        {
          provider.CleanUp();
        }
      }

    }


    [Test]
    public void AddOrUpdateTests1()
    {
      Content content = ContentFactory.WebPage("1", "/url", "title", "content");

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          provider.Add(content);
          var results = provider.Search("title", 0, 10);
          results.Count().ShouldBe(1);

          provider.AddOrUpdate(content);
          results = provider.Search("title", 0, 10);
          results.Count().ShouldBe(1);
        }
        finally
        {
          provider.CleanUp();
        }
      }

    }



    [Test]
    public void AddOrUpdateTests2()
    { 
      string word = "1047376276726767767d6767d00";
      Content content = ContentFactory.WebPage(word, $"/{word}", word, word);

      using (ISearchProvider provider = new LuceneProvider(new RAMDirectory()))
      {
        try
        {
          var results = provider.Search(word, 0, 10);
          int count = results.Count();
          results.Count().ShouldBe(0);

          provider.Add(content);
          results = provider.Search(word, 0, 10);
          count = results.Count();
          results.Count().ShouldBe(1);

          provider.Add(content);
          results = provider.Search(word, 0, 10);
          count = results.Count();
          results.Count().ShouldBe(2);

          provider.AddOrUpdate(content);
          results = provider.Search(word, 0, 10);
          count = results.Count();
          results.Count().ShouldBe(1);

          provider.AddOrUpdate(content);
          results = provider.Search(word, 0, 10);
          count = results.Count();
          results.Count().ShouldBe(1);

        }
        finally
        {
          provider.CleanUp();
        }
      }
    }
  }
}