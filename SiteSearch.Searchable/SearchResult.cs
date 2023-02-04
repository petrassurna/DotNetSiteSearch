using SiteSearch.Searchable.SearchableContent;

namespace Searchable
{
  public class SearchResult
  {
    public double Score { get; set; }

    public Content Content { get; set; } = null!;
  }
}
