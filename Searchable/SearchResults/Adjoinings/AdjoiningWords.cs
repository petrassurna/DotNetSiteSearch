using Searchable.Stemming;

namespace Searchable.SearchResults
{
  public class AdjoiningWords
  {
    public bool WordsRemainLeft { get; set; } = false;

    public bool WordsRemainRight { get; set; } = false;

    public List<string> WordList { get; set; } = null!;

    public AdjoiningWords()
    {
      WordList = new List<string>();
    }

    public AdjoiningWords(string[] words, bool wordsRemainLeft, bool wordsRemainRight)
    {
      WordList = words.ToList();
      WordsRemainLeft = wordsRemainLeft;
      WordsRemainRight = wordsRemainRight;
    }

    public AdjoiningWords(IEnumerable<string> words, bool wordsRemainLeft, bool wordsRemainRight)
    {
      WordList = words.ToList();
      WordsRemainLeft = wordsRemainLeft;
      WordsRemainRight = wordsRemainRight;
    }


    public string this[int index] => WordList[index];


    public bool Contains(string word) => WordList.Contains(word);


    public int Count() => WordList.Count();


    public string Highlight(IStemmer stemmer)
    {
      string highlighted = "";

      foreach (string word in WordList)
      {
        highlighted += word.Highlight(WordList, stemmer);
      }

      return highlighted;
    }

    public string Highlight(string word) => WordList.Highlight(word);

    public string Highlight(IEnumerable<string> wordsToHighlight, IStemmer stemmer)
    {
      return WordList.HighlightStemmed(wordsToHighlight, stemmer);
    }

    public List<string> ToList() => WordList.ToList();

    public new string ToString() => String.Join(" ", WordList);


  }
}
