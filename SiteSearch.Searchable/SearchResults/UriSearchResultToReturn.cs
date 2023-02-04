namespace SiteSearch.Searchable.SearchResults
{
  public class UriSearchResultToReturn
  {
    public string Id { get; set; } = null!;

    public string Path { get; set; } = null!;

    public double Score { get; set; }

    public string Title { get; set; } = null!;

    public IEnumerable<string> ContentMatches { get; set; } = null!;
  }
}
