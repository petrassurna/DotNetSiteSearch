using Searchable.Stemming;


namespace Searchable.SearchResults
{
  public class WordMatch
  {
    public AdjoiningWords WordsLeft { get; set; } = null!;

    public string Word { get; set; } = null!;

    public AdjoiningWords WordsRight { get; set; } = null!;

    public bool Contains(string word) => 
      WordsLeft.Contains(word) || 
      Word.ToLower() == word.ToLower() || 
      WordsRight.Contains(word);

    public string Highlight(IEnumerable<string> words, IStemmer stemmer, bool wordsLeft, bool wordsRight)
    {
      string str =  $"{WordsLeft.Highlight(words, stemmer)} {Word.Highlight(words, stemmer)} {WordsRight.Highlight(words, stemmer)}".Trim();

      if (wordsLeft)
      {
        str = "..." + str;
      }

      if (wordsRight)
      {
        str += "...";
      }

      return str;
    }

  }
}



