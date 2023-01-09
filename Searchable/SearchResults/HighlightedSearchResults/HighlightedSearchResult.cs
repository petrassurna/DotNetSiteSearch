using System.Text.RegularExpressions;

namespace Searchable.SearchResults.HighlightedSearchResults
{
    public class HighlightedSearchResult
    {
        public string Id { get; set; } = null!;

        public string Path { get; set; } = null!;

        public double Score { get; set; }

        public string Title { get; set; } = null!;

        public WordMatches WordMatches { get; set; } = new ();

    }
}
