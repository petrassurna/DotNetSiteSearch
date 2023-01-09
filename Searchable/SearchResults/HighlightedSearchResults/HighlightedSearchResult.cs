using Searchable.Stemming;
using System.Text.RegularExpressions;

namespace Searchable.SearchResults.HighlightedSearchResults
{
  public class HighlightedSearchResult
  {
    public string Id { get; set; } = null!;

    public string Path { get; set; } = null!;

    public double Score { get; set; }

    public WordMatch Title { get; set; } = null!;

    public WordMatches ContentMatches { get; set; } = new();

    private IStemmer Stemmer { get; set; }

    private int WordsEachSide { get; set; }

    public HighlightedSearchResult(IStemmer stemmer, int wordsEachSide)
    {
      Stemmer = stemmer;
      WordsEachSide = wordsEachSide;
      Title = new(Stemmer, WordsEachSide);
    }

  }
}
