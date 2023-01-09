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
        /// <param name="query">The search query</param>
        /// <param name="wordsEachSide">The number of words to include each side of a highlighted match</param>
        /// <param name="stemmer">The stemmer to use to identify a match</param>
        /// <returns></returns>

        public static HighlightedSearchResult GetSearchResults(SearchResult result, string query, int wordsEachSide, IStemmer stemmer)
        {
            HighlightedSearchResult highlightedResult = new HighlightedSearchResult();
            string[] terms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var titleMatch = TextToWordMatches.HighlightPhraseMatchInTitle(query, result.Content.Field("Title").Value, stemmer);

            if (titleMatch.Count() != 0)
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
        .HighlightPhraseMatchInContent(query, result.Content.Field("Text").Value, wordsEachSide, stemmer);
         //     .Select(m => m.Highlight(terms, stemmer, m.WordsLeft.WordsRemainLeft, m.WordsRight.WordsRemainRight));

            return highlightedResult;
        }

    }
}
