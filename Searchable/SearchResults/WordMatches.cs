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
    public string SearchPhrase { get; set; } = null!;

    public WordMatches()
    {
    }

    public WordMatches(string searchPhrase)
    {
      SearchPhrase = searchPhrase;
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
        string str = match.Highlight();
        parts.Add(str);
      }

      return parts;
    }


  }
}
