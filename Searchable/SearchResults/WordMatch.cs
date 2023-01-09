using Searchable.Stemming;


namespace Searchable.SearchResults
{
  public class WordMatch
  {
    public AdjoiningWords WordsLeft { get; set; } = null!;

    public string Word { get; set; } = null!;

    public AdjoiningWords WordsRight { get; set; } = null!;

    public IStemmer Stemmer { get; set; } = null!;

    public int WordsEachSide { get; set; }

    public WordMatch(IStemmer stemmer, int wordsEachSide)
    {
      Stemmer= stemmer;
      WordsEachSide= wordsEachSide;
    }


    public bool Contains(string word) => 
      WordsLeft.Contains(word) || 
      Word.ToLower() == word.ToLower() || 
      WordsRight.Contains(word);

    public string Highlight(string searchPharse)
    {
      IEnumerable<string> words = searchPharse.Split(' ', StringSplitOptions.RemoveEmptyEntries);

      string str =  $"{WordsLeft.Highlight(words, Stemmer)} {Word.Highlight(words, Stemmer)} {WordsRight.Highlight(words, Stemmer)}".Trim();

      if (WordsLeft.WordsRemainLeft)
      {
        str = "..." + str;
      }

      if (WordsRight.WordsRemainRight)
      {
        str += "...";
      }

      return str;
    }

    public IEnumerable<string> NonMatchedWords(IEnumerable<string> words) => words.Where(w => !Contains(w));

  }
}



