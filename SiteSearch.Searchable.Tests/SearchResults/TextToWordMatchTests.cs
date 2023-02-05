using Shouldly;
using SiteSearch.Searchable.LuceneSearch;
using SiteSearch.Searchable.SearchResults;
using SiteSearch.Searchable.SearchResults.Factories;
using SiteSearch.Searchable.Stemming;

namespace SiteSearch.Searchable.Searchable.Searchable.Tests
{
  public class TextToWordMatchTests
  {
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void TextToWordMatchTest1()
    {
      WordMatch match;
      IStemmer stemmer = new Stemmer();

      match = TextToWordMatch.GetMatch("dog", "dog", "this is a dog and this is a cat", 0, stemmer);
      match.Word.ShouldBe("dog");
      match.WordsLeft.Count().ShouldBe(0);
      match.WordsRight.Count().ShouldBe(0);

      match = TextToWordMatch.GetMatch("dog", "dog", "this is a dog and this is a cat", 1, stemmer);
      match.Word.ShouldBe("dog");
      match.WordsLeft.Count().ShouldBe(1);
      match.WordsLeft.ToList()[0].ShouldBe("a");
      match.WordsRight.Count().ShouldBe(1);
      match.WordsRight.ToList()[0].ShouldBe("and");

      match = TextToWordMatch.GetMatch("dog", "dog", "this is a dog and this is a cat", 2, stemmer);
      match.Word.ShouldBe("dog");
      match.WordsLeft.Count().ShouldBe(2);
      match.WordsLeft.ToList()[0].ShouldBe("is");
      match.WordsLeft.ToList()[1].ShouldBe("a");
      match.WordsRight.Count().ShouldBe(2);
      match.WordsRight.ToList()[0].ShouldBe("and");
      match.WordsRight.ToList()[1].ShouldBe("this");

      match = TextToWordMatch.GetMatch("dog", "dog", "this is a dog and this is a cat", 3, stemmer);
      match.Word.ShouldBe("dog");
      match.WordsLeft.Count().ShouldBe(3);
      match.WordsLeft.ToList()[0].ShouldBe("this");
      match.WordsLeft.ToList()[1].ShouldBe("is");
      match.WordsLeft.ToList()[2].ShouldBe("a");
      match.WordsRight.Count().ShouldBe(3);
      match.WordsRight.ToList()[0].ShouldBe("and");
      match.WordsRight.ToList()[1].ShouldBe("this");
      match.WordsRight.ToList()[2].ShouldBe("is");

      match = TextToWordMatch.GetMatch("dog", "dog", "this is a dog and this is a cat", 4, stemmer);
      match.Word.ShouldBe("dog");
      match.WordsLeft.Count().ShouldBe(3);
      match.WordsLeft.ToList()[0].ShouldBe("this");
      match.WordsLeft.ToList()[1].ShouldBe("is");
      match.WordsLeft.ToList()[2].ShouldBe("a");
      match.WordsRight.Count().ShouldBe(4);
      match.WordsRight.ToList()[0].ShouldBe("and");
      match.WordsRight.ToList()[1].ShouldBe("this");
      match.WordsRight.ToList()[2].ShouldBe("is");
      match.WordsRight.ToList()[3].ShouldBe("a");

      match = TextToWordMatch.GetMatch("dog", "dog", "this is a dog and this is a cat", 5, stemmer);
      match.Word.ShouldBe("dog");
      match.WordsLeft.Count().ShouldBe(3);
      match.WordsLeft.ToList()[0].ShouldBe("this");
      match.WordsLeft.ToList()[1].ShouldBe("is");
      match.WordsLeft.ToList()[2].ShouldBe("a");
      match.WordsRight.Count().ShouldBe(5);
      match.WordsRight.ToList()[0].ShouldBe("and");
      match.WordsRight.ToList()[1].ShouldBe("this");
      match.WordsRight.ToList()[2].ShouldBe("is");
      match.WordsRight.ToList()[3].ShouldBe("a");
      match.WordsRight.ToList()[4].ShouldBe("cat");

      match = TextToWordMatch.GetMatch("dog", "dog", "this is a dog and this is a cat", 6, stemmer);
      match.Word.ShouldBe("dog");
      match.WordsLeft.Count().ShouldBe(3);
      match.WordsLeft.ToList()[0].ShouldBe("this");
      match.WordsLeft.ToList()[1].ShouldBe("is");
      match.WordsLeft.ToList()[2].ShouldBe("a");
      match.WordsRight.Count().ShouldBe(5);
      match.WordsRight.ToList()[0].ShouldBe("and");
      match.WordsRight.ToList()[1].ShouldBe("this");
      match.WordsRight.ToList()[2].ShouldBe("is");
      match.WordsRight.ToList()[3].ShouldBe("a");
      match.WordsRight.ToList()[4].ShouldBe("cat");
    }


    [Test]
    public void TextToWordMatchTest2()
    {
      WordMatch match;
      IStemmer stemmer = new Stemmer();

      match = TextToWordMatch.GetMatch("this", "this", "this is a dog and this is a cat", 0, stemmer);
      match.Word.ShouldBe("this");
      match.WordsLeft.Count().ShouldBe(0);
      match.WordsRight.Count().ShouldBe(0);

      match = TextToWordMatch.GetMatch("this", "this", "this is a dog and this is a cat", 1, stemmer);
      match.Word.ShouldBe("this");
      match.WordsLeft.Count().ShouldBe(0);
      match.WordsRight.Count().ShouldBe(1);
      match.WordsRight.ToList()[0].ShouldBe("is");

      match = TextToWordMatch.GetMatch("this", "this", "this is a dog and this is a cat", 10, stemmer);
      match.Word.ShouldBe("this");
      match.WordsLeft.Count().ShouldBe(0);
      match.WordsRight.Count().ShouldBe(8);
      match.WordsRight.ToList()[7].ShouldBe("cat");

      match = TextToWordMatch.GetMatch("this", "this", "this, is a dog and this is a cat", 10, stemmer);
      match.Word.ShouldBe("this,");
      match.WordsLeft.Count().ShouldBe(0);
      match.WordsRight.Count().ShouldBe(8);
      match.WordsRight.ToList()[7].ShouldBe("cat");
    }

  }
}
