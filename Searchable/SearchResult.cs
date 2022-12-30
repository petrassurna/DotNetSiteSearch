using Searchable.SearchableContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searchable
{
  public class SearchResult
  {
    public double Score { get; set; }

    public Content Content { get; set; } = null!;
  }
}
