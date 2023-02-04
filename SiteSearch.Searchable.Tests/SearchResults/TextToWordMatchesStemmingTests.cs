using LuceneSearch;
using Searchable.SearchResults;
using Searchable.SearchResults.Factories;
using Searchable.Stemming;
using Shouldly;

namespace SearchableTests
{
  public class TextToWordMatchesStemmingTests
  {
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void GetIndexesTestsWhenOneItemInCommon()
    {
      IStemmer stemmer = new Stemmer();
      List<string> list1 = new List<string> { "dog", "cat" };
      List<string> list2 = new List<string> { "big daddy", "car", "wheel", "cat" };

      var result = list1.GetIndexes(list2, stemmer).ToList();
      result.Count().ShouldBe(1);
      result[0].ShouldBe(3);
    }


    [Test]
    public void GetIndexesTestsCheckWhenNoItemsInCommon()
    {
      IStemmer stemmer = new Stemmer();
      List<string> list1 = new List<string> { "dog", "catsy" };
      List<string> list2 = new List<string> { "big daddy", "car", "wheel", "cat" };

      var result = list1.GetIndexes(list2, stemmer).ToList();
      result.Count().ShouldBe(0);
    }


    [Test]
    public void GetIndexesTestsCheckWhenTwoItemsInCommon()
    {
      IStemmer stemmer = new Stemmer();
      List<string> list1 = new List<string> { "dog", "cats" };
      List<string> list2 = new List<string> { "big daddy", "cats", "wheel", "cat", "cats" };

      var result = list1.GetIndexes(list2, stemmer).ToList();
      result.Count().ShouldBe(3);
      result[0].ShouldBe(1);
      result[1].ShouldBe(3);
      result[2].ShouldBe(4);
    }


    [Test]
    public void HighlightTests1()
    {
      IStemmer stemmer = new Stemmer();

      string[] words = { "a", "dogs", "and", "a", "cat" };
      AdjoiningWords adj = new AdjoiningWords(words, false, false);
      string[] highlight = { "dog", "cat" };
      string highlighted = adj.Highlight(highlight, stemmer);
      highlighted.ShouldBe("a <strong>dogs</strong> and a <strong>cat</strong>");
    }


    [Test]
    public void HighlightTests2()
    {
      IStemmer stemmer = new Stemmer();

      string[] words = { "a", "Dog", "and", "a", "cat" };
      AdjoiningWords adj = new AdjoiningWords(words, false, false);
      string[] highlight = { "dog", "cat" };
      string highlighted = adj.Highlight(highlight, stemmer);
      highlighted.ShouldBe("a <strong>Dog</strong> and a <strong>cat</strong>");
    }



    [Test]
    public void HighlightTests3()
    {
      IStemmer stemmer = new Stemmer();

      string[] words = { "a", "dog", "and", "a", "cat" };
      AdjoiningWords adj = new AdjoiningWords(words, false, false);
      string[] highlight = { "Dog", "cat" };
      string highlighted = adj.Highlight(highlight, stemmer);
      highlighted.ShouldBe("a <strong>dog</strong> and a <strong>cat</strong>");
    }


    [Test]
    public void TextToWordMatchesTest1()
    {
      IStemmer stemmer = new Stemmer();

      var matches = TextToWordMatches.HighlightPhraseMatchInContent("dog", "I am a dog and a cat", 2, stemmer).ToList();
      matches.Count().ShouldBe(1);
      matches[0].WordsLeft.ToString().ShouldBe("am a");
      matches[0].WordsRight.ToString().ShouldBe("and a");
      matches[0].Word.ShouldBe("dog");

      matches = TextToWordMatches.HighlightPhraseMatchInContent("dog cat", "I am a dog and a cat", 2, stemmer).ToList();

      matches.Count().ShouldBe(2);
      matches[0].WordsLeft.ToString().ShouldBe("am a");
      matches[0].WordsRight.ToString().ShouldBe("and a");
      matches[0].Word.ShouldBe("dog");
      matches[1].WordsLeft.ToString().ShouldBe("and a");
      matches[1].WordsRight.ToString().ShouldBe("");
      matches[1].Word.ShouldBe("cat");

      matches = TextToWordMatches.HighlightPhraseMatchInContent("dog cat", "I am a dog and a cat", 3, stemmer).ToList();

      matches.Count().ShouldBe(1);
      matches[0].WordsLeft.ToString().ShouldBe("I am a");
      matches[0].WordsRight.ToString().ShouldBe("and a cat");
      matches[0].Word.ShouldBe("dog");

      matches = TextToWordMatches.HighlightPhraseMatchInContent("dog cat", "I am a dog and a cat", 4, stemmer).ToList();

      matches.Count().ShouldBe(1);
      matches[0].WordsLeft.ToString().ShouldBe("I am a");
      matches[0].WordsRight.ToString().ShouldBe("and a cat");
      matches[0].Word.ShouldBe("dog");
    }


    [Test]
    public void TextToWordMatchesTest2()
    {
      IStemmer stemmer = new Stemmer();

      var matches = TextToWordMatches.HighlightPhraseMatchInContent("dog", "I am a dog and a cat", 2, stemmer);
      matches.Highlight(stemmer).Count().ShouldBe(1);
      string str = matches.Highlight(stemmer).ToList()[0];
      matches.Highlight(stemmer).ToList()[0].ShouldBe("...am a <strong>dog</strong> and a...");

      matches = TextToWordMatches.HighlightPhraseMatchInContent("dog", "I am a Dog and a cat", 2, stemmer);
      matches.Highlight(stemmer).Count().ShouldBe(1);
      str = matches.Highlight(stemmer).ToList()[0];
      matches.Highlight(stemmer).ToList()[0].ShouldBe("...am a <strong>Dog</strong> and a...");

      matches = TextToWordMatches.HighlightPhraseMatchInContent("dog", "I am a Dog and doG cat", 2, stemmer);
      var m = matches[0];
      matches.Highlight(stemmer).Count().ShouldBe(1);
      str = matches.Highlight(stemmer).ToList()[0];
      matches.Highlight(stemmer).ToList()[0].ShouldBe("...am a <strong>Dog</strong> and <strong>doG</strong>...");

      matches = TextToWordMatches.HighlightPhraseMatchInContent("dog cat", "I am a Dog and doG caT", 1, stemmer);
      m = matches[0];
      matches.Highlight(stemmer).Count().ShouldBe(2);
      str = matches.Highlight(stemmer).ToList()[0];
      matches.Highlight(stemmer).ToList()[0].ShouldBe("...a <strong>Dog</strong> and...");
      str = matches.Highlight(stemmer).ToList()[1];
      matches.Highlight(stemmer).ToList()[1].ShouldBe("...<strong>doG</strong> <strong>caT</strong>");
    }


    [Test]
    public void TextToWordMatchesTest3()
    {
      IStemmer stemmer = new Stemmer();

      var matches = TextToWordMatches.HighlightPhraseMatchInContent("dog", "I am a dog and a cat dog dog dog", 2, stemmer);
      matches.Highlight(stemmer).Count().ShouldBe(1);
      string str = matches.Highlight(stemmer).ToList()[0];
      matches.Highlight(stemmer).ToList()[0].ShouldBe("...am a <strong>dog</strong> and a...");
    }

  }
}
