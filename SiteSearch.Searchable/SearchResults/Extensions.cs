

using Searchable.Stemming;
using System.Runtime.CompilerServices;

namespace Searchable.SearchResults
{
  public static class Extensions
  {

    public static bool AreSameForMatch(this string word, string possibleMatch, IStemmer stemmer)
    {
      if (word.StemmForMatch() == possibleMatch.StemmForMatch() || stemmer.Stem(word).StemmForMatch() == stemmer.Stem(possibleMatch).StemmForMatch())
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    public static bool Contains(this List<string> list, string word)
          => list.Select(s => s.ToLower()).Contains(word.ToLower());


    /// <summary>
    /// Return the index of every match of list1 in list2 taking into account stemming
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    public static IEnumerable<int> GetIndexes(this List<string> list1, List<string> list2, IStemmer stemmer)
    {
      // Create a list to store the indexes
      var indexes = new List<int>();

      // Loop through each element in list1
      foreach (var element in list1)
      {
        // Loop through each element in list2
        for (int i = 0; i < list2.Count(); i++)
        {
          // If the element in list1 matches the element in list2
          if (element.AreSameForMatch(list2[i].StemmForMatch(), stemmer))
          {
            // Add the index to the list
            indexes.Add(i);
          }
        }
      }

      // Return the list of indexes
      return indexes;
    }


    public static string Highlight(this string word, IEnumerable<string> words, IStemmer stemmer)
    {
      if (!string.IsNullOrWhiteSpace(word))
      {
        foreach (string str in words)
        {
          if (word.AreSameForMatch(str, stemmer))
          {
            return word.Highlight();
          }
        }
      }

      return word;
    }

    public static string Highlight(this IEnumerable<string> list, string word)
    {
      string str = "";

      foreach (string s in list)
      {
        if (!string.IsNullOrWhiteSpace(str))
        {
          str += " ";
        }

        if (s.ToLower() == word.ToLower())
        {
          str += s.Highlight();
        }
        else
        {
          str += s;
        }
      }

      return str;
    }

    public static string Highlight(this string word)
    {
      return $"<strong>{word}</strong>";
    }

    public static string HighlightStemmed(this string contentToSearch, IEnumerable<string> query, IStemmer stemmer)
    {
      IEnumerable<string> content = contentToSearch.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      return content.HighlightStemmed(query, stemmer);
    }

    public static string HighlightStemmed(this IEnumerable<string> contentToSearch, IEnumerable<string> wordsToHighlight, IStemmer stemmer)
    {
      string highlighted = "";

      var wordsToHighlightStemmed = wordsToHighlight.Stem(stemmer).ToList();
      var originalStemmed = contentToSearch.Stem(stemmer).ToList();

      var matches = GetIndexes(wordsToHighlightStemmed, originalStemmed, stemmer).ToList();

      for (int i = 0; i < contentToSearch.Count(); i++)
      {
        if (!string.IsNullOrWhiteSpace(highlighted))
        {
          highlighted += " ";
        }

        if (matches.Contains(i))
        {
          highlighted += contentToSearch.ElementAt(i).Highlight();
        }
        else
        {
          highlighted += contentToSearch.ElementAt(i);
        }
      }

      return highlighted;
    }


    public static string StemmForMatch(this string str)
    {
      string cleaned = new string((from c in str
                                   where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                                   select c
       ).ToArray());

      return cleaned.ToLower();
    }


  }
}
