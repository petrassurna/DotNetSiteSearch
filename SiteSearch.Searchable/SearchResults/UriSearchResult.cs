using Searchable.Stemming;

namespace Searchable.SearchResults
{
  public class UriSearchResult
  {
    public WordMatches ContentMatches { get; set; } = new();

    public string Id { get; set; } = null!;

    public string Path { get; set; } = null!;

    public double Score { get; set; }

    private IStemmer Stemmer { get; set; }

    public WordMatch Title { get; set; } = null!;

    private int WordsEachSide { get; set; }

    public UriSearchResult(string searchPhrase, IStemmer stemmer, int wordsEachSide)
    {
      Stemmer = stemmer;
      WordsEachSide = wordsEachSide;
      Title = new(searchPhrase, stemmer, WordsEachSide);
    }

    public UriSearchResultToReturn ToHighlightedSearchResultToReturn()
    {
      return new UriSearchResultToReturn()
      {
        Id = Id,
        Path = Path,
        Score = Score,
        Title = Title.Highlight(),
        ContentMatches = ContentMatches.Select(c => c.Highlight())
      };
    }

  }
}
