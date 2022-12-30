using Searchable.SearchableContent;
using Searchable.Stemming;

namespace Searchable.SearchResults.Factories
{
  public class ContentToHighlightSearchResult
  {

    public static HighlightedSearchResult GetSearchResults(SearchResult result, string query, int wordsEachSide, IStemmer stemmer)
    {
      HighlightedSearchResult highlightedResult = new HighlightedSearchResult();
      string [] terms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);

      var titleMatch = TextToWordMatches.HighlightPhraseMatchInTitle(query, result.Content.Field("Title").Value, stemmer);

      if(titleMatch.Count() != 0)
      {
        List<string> items = result.Content.Field("Title")
          .Value
          .Split(' ', StringSplitOptions.RemoveEmptyEntries)
          .ToList();

        highlightedResult.Title = items.HighlightStemmed(terms, stemmer);        
      }
      else
      {
        highlightedResult.Title = result.Content.Field("Title").Value.HighlightStemmed(terms, stemmer);
      }

      highlightedResult.Path = result.Content.Field("Url").Value;
      highlightedResult.Score = result.Score;
      highlightedResult.Id = result.Content.Field("Id").Value;

      highlightedResult.WordMatches = TextToWordMatches
        .HighlightPhraseMatchInContent(query, result.Content.Field("Text").Value, wordsEachSide, stemmer)
        .Select(m => m.Highlight(terms, stemmer, m.WordsLeft.WordsRemainLeft, m.WordsRight.WordsRemainRight));

      var xxx = highlightedResult.WordMatches.ToList();

      return highlightedResult;
    }

  }
}
