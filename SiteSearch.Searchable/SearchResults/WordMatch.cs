using Searchable.Stemming;


namespace Searchable.SearchResults
{
  public class WordMatch
  {
    public string SearchPhrase { get; set; }

    public IStemmer Stemmer { get; set; } = null!;

    public int WordsEachSide { get; set; }

    public AdjoiningWords WordsLeft { get; set; } = null!;

    public string Word { get; set; } = null!;

    public AdjoiningWords WordsRight { get; set; } = null!;


    public WordMatch(string searchPhrase, IStemmer stemmer, int wordsEachSide)
    {
      SearchPhrase = searchPhrase;
      Stemmer= stemmer;
      WordsEachSide= wordsEachSide;
    }

    public bool Contains(string word) => 
      WordsLeft.Contains(word) || 
      Word.ToLower() == word.ToLower() || 
      WordsRight.Contains(word);

    public string Highlight()
    {
      IEnumerable<string> words = Split(SearchPhrase);

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


    private IEnumerable<string> Split(string searchPharse) => searchPharse.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    
    public IEnumerable<string> NonMatchedWords(string searchPhrase) => Split(searchPhrase).Where(w => !Contains(w));

    public IEnumerable<string> NonMatchedWords(IEnumerable<string> words) => words.Where(w => !Contains(w));

  }
}



