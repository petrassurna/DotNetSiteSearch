using SiteSearch.Searchable.Stemming;

namespace SiteSearch.Searchable.SearchResults.Factories
{
  public class TextToWordMatches
  {

    public static WordMatches HighlightPhraseMatchInTitle(string phrase, string content, IStemmer stemmer)
      => HighlightPhraseMatchInContent(phrase, content, 1000000, stemmer);


    public static WordMatches HighlightPhraseMatchInContent(string searchPhrase, string content, int wordsEachSide, IStemmer stemmer)
    {
      WordMatches matches = HighlightPhraseMatchInContentDirect(searchPhrase, content, wordsEachSide, stemmer);

      if (matches.Count() == 0)
      {
        WordMatch match = new WordMatch(searchPhrase, stemmer, wordsEachSide)
        {
          WordsLeft = WordsBefore.Get(content, 0, wordsEachSide),
          Word = "",
          WordsRight = WordsAfter.Get(content, 0, wordsEachSide)
        };

        matches.Add(match);
      }

      return matches;
    }


    private static WordMatches HighlightPhraseMatchInContentDirect(string searchPhrase, string content, int wordsEachSide, IStemmer stemmer)
    {
      string[] words = searchPhrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      WordMatches matches = new(searchPhrase);

      foreach (string word in words)
      {
        if (!matches.Contains(word))
        {
          WordMatch match = TextToWordMatch.GetMatch(word, searchPhrase, content, wordsEachSide, stemmer);

          if (match != null)
          {
            matches.Add(match);
          }
        }
      }

      return matches;
    }


  }
}
