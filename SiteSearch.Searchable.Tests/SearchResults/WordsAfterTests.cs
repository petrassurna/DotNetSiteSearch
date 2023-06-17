using Shouldly;
using SiteSearch.Searchable.SearchResults;
using SiteSearch.Searchable.SearchResults.Factories;

namespace SiteSearch.Searchable.Tests.SearchResults
{
  public class WordsAfterTest
  {
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void WordsBeforeTest1()
    {
      string str = "is a string which has a cat and a dog";
      string[] contents = str.Split();

      AdjoiningWords words = TextToWordMatch.WordsBefore(contents, 2, 2);
      words.Count().ShouldBe(2);
      words.WordsRemainLeft = false;
      words.WordsRemainRight = true;
      words[0].ShouldBe("is");
      words[1].ShouldBe("a");

      str = "is a string which has a cat and a dog";
      contents = str.Split();
      words = TextToWordMatch.WordsBefore(contents, 2, 3);
      words.Count().ShouldBe(2);
      words.WordsRemainLeft = false;
      words.WordsRemainRight = false;
      words[0].ShouldBe("is");
      words[1].ShouldBe("a");

      str = "is a string which has a cat and a dog";
      contents = str.Split();
      words = TextToWordMatch.WordsBefore(contents, 2, 4);
      words.Count().ShouldBe(2);
      words.WordsRemainLeft = false;
      words.WordsRemainRight = false;
      words[0].ShouldBe("is");
      words[1].ShouldBe("a");

      str = "is a string which has a cat and a dog";
      contents = str.Split();
      words = TextToWordMatch.WordsBefore(contents, 2, 1);
      words.Count().ShouldBe(1);
      words.WordsRemainLeft = true;
      words.WordsRemainRight = false;
      words[0].ShouldBe("a");

      str = "is a string which has a cat and a dog";
      contents = str.Split();
      words = TextToWordMatch.WordsBefore(contents, 2, 0);
      words.Count().ShouldBe(0);
      words.WordsRemainLeft = false;
      words.WordsRemainRight = false;
    }


    [Test]
    public void WordsAfterTest1()
    {
      string str = "is a string which has a cat and a dog";
      string[] contents = str.Split();

      AdjoiningWords words = TextToWordMatch.WordsAfter(contents, 2, 2);
      words.Count().ShouldBe(2);
      words.WordsRemainLeft = false;
      words.WordsRemainRight = true;
      words[0].ShouldBe("which");
      words[1].ShouldBe("has");

      str = "is a string which has a cat and a dog";
      contents = str.Split();
      words = TextToWordMatch.WordsAfter(contents, 2, 7);
      words.Count().ShouldBe(7);
      words.WordsRemainLeft = false;
      words.WordsRemainRight = false;
      words[0].ShouldBe("which");
      words[1].ShouldBe("has");
      words[2].ShouldBe("a");
      words[3].ShouldBe("cat");
      words[4].ShouldBe("and");
      words[5].ShouldBe("a");
      words[6].ShouldBe("dog");

      str = "is a string which has a cat and a dog";
      contents = str.Split();
      words = TextToWordMatch.WordsAfter(contents, 2, 8);
      words.Count().ShouldBe(7);
      words.WordsRemainLeft = false;
      words.WordsRemainRight = false;
      words[0].ShouldBe("which");
      words[1].ShouldBe("has");
      words[2].ShouldBe("a");
      words[3].ShouldBe("cat");
      words[4].ShouldBe("and");
      words[5].ShouldBe("a");
      words[6].ShouldBe("dog");

      str = "is a string which has a cat and a dog";
      contents = str.Split();
      words = TextToWordMatch.WordsAfter(contents, 2, 1);
      words.Count().ShouldBe(1);
      words.WordsRemainLeft = true;
      words.WordsRemainRight = false;
      words[0].ShouldBe("which");

      str = "is a string which has a cat and a dog";
      contents = str.Split();
      words = TextToWordMatch.WordsAfter(contents, 2, 0);
      words.Count().ShouldBe(0);
      words.WordsRemainLeft = false;
      words.WordsRemainRight = false;

      str = "and a dog";
      contents = str.Split();
      words = TextToWordMatch.WordsAfter(contents, 2, 1);
      words.Count().ShouldBe(0);
      words.WordsRemainLeft = false;
      words.WordsRemainRight = false;
    }


    [Test]
    public void WordsAfterTest2()
    {
      var list = WordsAfter.Get("dog I dog am a dog", 9, 2).ToList();
      list.Count().ShouldBe(2);
      list[0].ShouldBe("am");
      list[1].ShouldBe("a");
      WordsAfter.Get("dog I dog am a dog", 9, 2).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 9, 2).WordsRemainRight.ShouldBeTrue();

      list = WordsAfter.Get("dog I dog am a dog", 10, 2).ToList();
      list.Count().ShouldBe(2);
      list[0].ShouldBe("am");
      list[1].ShouldBe("a");
      WordsAfter.Get("dog I dog am a dog", 9, 2).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 9, 2).WordsRemainRight.ShouldBeTrue();

      list = WordsAfter.Get("dog I dog am a dog", 11, 2).ToList();
      list.Count().ShouldBe(2);
      list[0].ShouldBe("m");
      list[1].ShouldBe("a");
      WordsAfter.Get("dog I dog am a dog", 11, 2).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 11, 2).WordsRemainRight.ShouldBeTrue();

      list = WordsAfter.Get("dog I dog am a dog", 12, 2).ToList();
      list.Count().ShouldBe(2);
      list[0].ShouldBe("a");
      list[1].ShouldBe("dog");
      WordsAfter.Get("dog I dog am a dog", 12, 2).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 12, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsAfter.Get("dog I dog am a dog", 13, 2).ToList();
      list.Count().ShouldBe(2);
      list[0].ShouldBe("a");
      list[1].ShouldBe("dog");
      WordsAfter.Get("dog I dog am a dog", 13, 2).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 13, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsAfter.Get("dog I dog am a dog", 14, 2).ToList();
      list.Count().ShouldBe(1);
      list[0].ShouldBe("dog");
      WordsAfter.Get("dog I dog am a dog", 14, 2).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 14, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsAfter.Get("dog I dog am a dog", 15, 2).ToList();
      list.Count().ShouldBe(1);
      list[0].ShouldBe("dog");
      WordsAfter.Get("dog I dog am a dog", 15, 2).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 15, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsAfter.Get("dog I dog am a dog", 16, 2).ToList();
      list.Count().ShouldBe(1);
      list[0].ShouldBe("og");
      WordsAfter.Get("dog I dog am a dog", 16, 2).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 16, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsAfter.Get("dog I dog am a dog", 17, 2).ToList();
      list.Count().ShouldBe(1);
      list[0].ShouldBe("g");
      WordsAfter.Get("dog I dog am a dog", 17, 2).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 17, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsAfter.Get("dog I dog am a dog", 18, 2).ToList();
      list.Count().ShouldBe(0);
      WordsAfter.Get("dog I dog am a dog", 18, 2).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 18, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsAfter.Get("dog I dog am a dog", 3000, 2).ToList();
      list.Count().ShouldBe(0);
      WordsAfter.Get("dog I dog am a dog", 3000, 2).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 3000, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsAfter.Get("dog I dog am a dog", 3, 0).ToList();
      list.Count().ShouldBe(0);
      WordsAfter.Get("dog I dog am a dog", 3, 0).WordsRemainLeft.ShouldBeFalse();
      WordsAfter.Get("dog I dog am a dog", 3, 0).WordsRemainRight.ShouldBeTrue();

    }


  }
}
