using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSearch.Searchable.Stemming
{
  public static class Extensions
  {

    public static IEnumerable<string> Stem(this IEnumerable<string> list, IStemmer stemmer)
    {
      return list.Select(w => stemmer.Stem(w));
    }

  }
}
