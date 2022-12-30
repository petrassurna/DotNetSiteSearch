using Searchable.Stemming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searchable.SearchResults.Factories
{
  public class TextToWordMatches
  {

    public static WordMatches HighlightPhraseMatchInTitle(string phrase, string content, IStemmer stemmer)
      => HighlightPhraseMatchInContent(phrase, content, 1000000, stemmer);


    public static WordMatches HighlightPhraseMatchInContent(string phrase, string content, int wordsEachSide, IStemmer stemmer)
    {
      WordMatches matches = HighlightPhraseMatchInContentDirect(phrase, content, wordsEachSide, stemmer);

      if (matches.Count() == 0)
      {
        WordMatch match = new WordMatch()
        {
          WordsLeft = new AdjoiningWords(),
          Word = "",
          WordsRight = WordsAfter.Get(content, 0, wordsEachSide)
        };

        matches.Add(match);
      }

      return matches;
    }


    private static WordMatches HighlightPhraseMatchInContentDirect(string phrase, string content, int wordsEachSide, IStemmer stemmer)
    {
      string[] words = phrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      WordMatches matches = new(words);

      foreach (string word in words)
      {
        if (!matches.Contains(word))
        {
          WordMatch match = TextToWordMatch.GetMatch(word, content, wordsEachSide, stemmer);

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
