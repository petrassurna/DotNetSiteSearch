using Searchable.Stemming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searchable.SearchResults
{
  public class WordMatches : List<WordMatch>
  {
    public string[] SearchTerms { get; set; }

    public WordMatches()
    {
      SearchTerms = new string[0];
    }

    public WordMatches(string[] searchTerms)
    {
      SearchTerms = searchTerms;
    }

    public bool Contains(string word)
    {
      bool contains = false;

      foreach (WordMatch wordMatch in this)
      {
        if (wordMatch.Contains(word))
        {
          contains = true;
          break;
        }
      }

      return contains;
    }


    public IEnumerable<string> Highlight(IStemmer stemmer)
    {
      List<string> parts = new List<string>();

      foreach (var match in this)
      {
        string str = match.Highlight(SearchTerms, stemmer, false, false);
        parts.Add(str);
      }

      return parts;
    }

    public string HighlightInOneLine(IStemmer stemmer) => String.Join(" ", Highlight(stemmer)).Trim();


  }
}
