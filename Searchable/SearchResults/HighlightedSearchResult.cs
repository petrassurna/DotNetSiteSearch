using System.Text.RegularExpressions;

namespace Searchable.SearchResults
{
  public class HighlightedSearchResult
  {
    public string Id { get; set; } = null!;

    public string Path { get; set; } = null!;

    public double Score { get; set; }

    public string Title { get; set; } = null!;

    public IEnumerable<string> WordMatches { get; set; } = null!;

  }
}
