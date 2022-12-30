using Searchable.Stemming;
using System.Text.RegularExpressions;

namespace Searchable.SearchResults.Factories
{

  public class TextToWordMatch
  {

    public static WordMatch GetMatch(string word, string content, int wordsEachSide, IStemmer stemmer)
    {
      WordMatch match = null!;

      if(string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(content))
      {
        return match;
      }

      string[] contents = content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      int index = Index(contents, word, stemmer);

      if (index != -1)
      {
        match = new WordMatch();
        match.WordsLeft = WordsBefore(contents, index, wordsEachSide);
        match.Word = contents[index];
        match.WordsRight = WordsAfter(contents, index, wordsEachSide);

      }

      return match;
    }


    public static AdjoiningWords WordsBefore(string[] contents, int index, int wordsEachSide)
    {
      if (index < 0)
        throw new ArgumentException();

      AdjoiningWords words = new AdjoiningWords();
      words.WordsRemainRight = false;

      if (wordsEachSide > 0)
      {
        int start = index - wordsEachSide;
        int end = start + wordsEachSide;

        if (start < 0)
        {
          start = 0;
          end += start;
          words.WordsRemainLeft = false;
        }
        else
        {
          words.WordsRemainLeft = true;
        }

        if (start + wordsEachSide >= index)
        {
          end = index - start;
        }

        words.WordList = contents.ToList().GetRange(start, end);
      }
      else
      {
        words.WordsRemainLeft = false;
      }

      return words;
    }


    public static AdjoiningWords WordsAfter(string[] contents, int index, int wordsEachSide)
    {
      if (index < 0)
        throw new ArgumentException();

      AdjoiningWords words = new AdjoiningWords();
      words.WordsRemainLeft = false;

      if(wordsEachSide > 0)
      {
        int start = index + 1;

        if (start + wordsEachSide > contents.Length - 1)
        {
          words.WordsRemainRight = false;
          wordsEachSide = contents.Length - start;
        }
        else
        {
          words.WordsRemainRight = true;
        }

        words.WordList = contents.ToList().GetRange(index + 1, wordsEachSide);
      }
      else
      {
        words.WordsRemainRight = false;
      }

      return words;
    }


    private static int Index(string[] contents, string word, IStemmer stemmer)
    {
      int index = -1;

      for(int i = 0; i < contents.Length; i++)
      {
        if (contents[i].AreSameForMatch(word, stemmer))
        {
          index = i;
          break;
        }
      }

      return index;
    }

  }

}
