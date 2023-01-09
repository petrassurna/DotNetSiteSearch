using Searchable.SearchableContent;
using Searchable.SearchResults.HighlightedSearchResults;
using Searchable.Stemming;

namespace Searchable.SearchResults.Factories
{
  public class ContentToHighlightSearchResult
  {

    /// <summary>
    /// Returns a SearchResult where each word in query is highlighted at least once according to the follwoing criteria
    /// 
    /// If all words in query are in the title, it returns the title and the first wordsEachSide words of the content 
    /// whether they contain matches or not
    /// 
    /// If some words in query are in the title, it returns the title and WordMatches such that the total matches in the title
    /// and WordMatches highlight all search terms
    /// 
    /// If no words in query are in the title or content, it returns the result "Error" as this shouldn't be possible
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <param name="searchPhrase">The search query</param>
    /// <param name="wordsEachSide">The number of words to include each side of a highlighted match</param>
    /// <param name="stemmer">The stemmer to use to identify a match</param>
    /// <returns></returns>

    public static HighlightedSearchResult GetSearchResults(SearchResult result, string searchPhrase, int wordsEachSide, IStemmer stemmer)
    {
      HighlightedSearchResult highlightedResult = new HighlightedSearchResult(stemmer, wordsEachSide, searchPhrase);
      string[] terms = searchPhrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);

      var titleMatch = TextToWordMatches.HighlightPhraseMatchInTitle(searchPhrase, result.Content.Field("Title").Value, stemmer);

      highlightedResult.Title = TextToWordMatches
        .HighlightPhraseMatchInContent(searchPhrase, result.Content.Field("Title").Value, 
        10000000, //infinte words each side
        stemmer)[0];

      highlightedResult.Path = result.Content.Field("Url").Value;
      highlightedResult.Score = result.Score;
      highlightedResult.Id = result.Content.Field("Id").Value;

      highlightedResult.ContentMatches = TextToWordMatches
        .HighlightPhraseMatchInContent(searchPhrase, result.Content.Field("Text").Value, wordsEachSide, stemmer);

      return highlightedResult;
    }

  }
}
