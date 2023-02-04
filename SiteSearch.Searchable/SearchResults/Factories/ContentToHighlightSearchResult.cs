using Searchable.SearchableContent;
using Searchable.Stemming;

namespace Searchable.SearchResults.Factories
{
  public class ContentToHighlightSearchResult
  {

    /// <summary>
    /// Returns a SearchResult where each word in query is highlighted at least once according to the follwoing criteria
    /// 
    /// Matches all words in the title
    /// And matches all words in the content at least once if they exist
    /// </summary>
    /// <param name="result"></param>
    /// <param name="searchPhrase">The search query</param>
    /// <param name="wordsEachSide">The number of words to include each side of a highlighted match</param>
    /// <param name="stemmer">The stemmer to use to identify a match</param>
    /// <returns></returns>

    public static UriSearchResult GetSearchResults(SearchResult result, string searchPhrase, int wordsEachSide, IStemmer stemmer)
    {
      UriSearchResult highlightedResult = new UriSearchResult(searchPhrase, stemmer, wordsEachSide);

      highlightedResult.Path = result.Content.Field("Url").Value;
      highlightedResult.Score = result.Score;
      highlightedResult.Id = result.Content.Field("Id").Value;

      highlightedResult.Title = TextToWordMatches
        .HighlightPhraseMatchInContent(searchPhrase, result.Content.Field("Title").Value,
        10000000, //infinte words each side
        stemmer)[0];

      highlightedResult.ContentMatches = TextToWordMatches
        .HighlightPhraseMatchInContent(searchPhrase, result.Content.Field("Text").Value, wordsEachSide, stemmer);

      return highlightedResult;
    }

  }
}
