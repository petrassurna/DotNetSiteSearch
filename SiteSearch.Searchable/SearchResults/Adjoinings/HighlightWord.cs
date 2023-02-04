using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSearch.Searchable.SearchResults.Adjoinings
{
  public class HighlightWord
  {
    public bool Highlight { get; set; }
    public string Word { get; set; }

    public HighlightWord(string word, bool highlight )
    {
      Highlight = highlight;  
      Word = word;  
    }

  }
}
