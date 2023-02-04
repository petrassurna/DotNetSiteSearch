using Searchable.SearchResults.Factories;
using Shouldly;

namespace SearchableTests.SearchResults
{
  public class WordsBeforeTest1
  {
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void WordsBeforeTest()
    {
      var list = WordsBefore.Get("dog I dog am a dog", 9, 2).ToList();
      list.Count().ShouldBe(2);
      list[0].ShouldBe("I");
      list[1].ShouldBe("dog");
      WordsBefore.Get("dog I dog am a dog", 9, 2).WordsRemainLeft.ShouldBeTrue();
      WordsBefore.Get("dog I dog am a dog", 9, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsBefore.Get("dog I dog am a dog", 0, 2).ToList();
      list.Count().ShouldBe(0);
      WordsBefore.Get("dog I dog am a dog", 0, 2).WordsRemainLeft.ShouldBeFalse();
      WordsBefore.Get("dog I dog am a dog", 0, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsBefore.Get("dog I dog am a dog", 1, 2).ToList();
      list.Count().ShouldBe(1);
      list[0].ShouldBe("d");
      WordsBefore.Get("dog I dog am a dog", 1, 2).WordsRemainLeft.ShouldBeFalse();
      WordsBefore.Get("dog I dog am a dog", 1, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsBefore.Get("dog I dog am a dog", 2, 2).ToList();
      list.Count().ShouldBe(1);
      list[0].ShouldBe("do");
      WordsBefore.Get("dog I dog am a dog", 2, 2).WordsRemainLeft.ShouldBeFalse();
      WordsBefore.Get("dog I dog am a dog", 2, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsBefore.Get("dog I dog am a dog", 3, 2).ToList();
      list.Count().ShouldBe(1);
      list[0].ShouldBe("dog");
      WordsBefore.Get("dog I dog am a dog", 3, 2).WordsRemainLeft.ShouldBeFalse();
      WordsBefore.Get("dog I dog am a dog", 3, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsBefore.Get("dog I dog am a dog", 4, 2).ToList();
      list.Count().ShouldBe(1);
      list[0].ShouldBe("dog");
      WordsBefore.Get("dog I dog am a dog", 4, 2).WordsRemainLeft.ShouldBeFalse();
      WordsBefore.Get("dog I dog am a dog", 4, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsBefore.Get("dog I dog am a dog", 5, 2).ToList();
      list.Count().ShouldBe(2);
      list[0].ShouldBe("dog");
      list[1].ShouldBe("I");
      WordsBefore.Get("dog I dog am a dog", 5, 2).WordsRemainLeft.ShouldBeFalse();
      WordsBefore.Get("dog I dog am a dog", 5, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsBefore.Get("dog I dog am a dog", -1, 2).ToList();
      list.Count().ShouldBe(0);
      WordsBefore.Get("dog I dog am a dog", -1, 2).WordsRemainLeft.ShouldBeFalse();
      WordsBefore.Get("dog I dog am a dog", -1, 2).WordsRemainRight.ShouldBeFalse();

      list = WordsBefore.Get("dog I dog am a dog", 10, 0).ToList();
      list.Count().ShouldBe(0);
      WordsBefore.Get("dog I dog am a dog", 10, 0).WordsRemainLeft.ShouldBeTrue();
    }


  }
}
