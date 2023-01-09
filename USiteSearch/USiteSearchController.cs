using LuceneSearch;
using Searchable;
using Searchable.SearchResults.Factories;
using Searchable.SearchResults.HighlightedSearchResults;
using Searchable.Stemming;
using Umbraco.Cms.Web.Common.Controllers;

namespace USiteSearch
{
    public class USiteSearchController : UmbracoApiController
  {
    ISearchProvider _provider;

    public USiteSearchController(ISearchProvider indexer)
    {
      _provider = indexer;
    }


    /// <summary>
    /// /Umbraco/Api/USiteSearch/Results
    /// </summary>
    /// <returns></returns>
    public IEnumerable<HighlightedSearchResult> Results(string query, int skip)
    {
      IStemmer stemmer = new Stemmer();
      var searchResults = _provider.Search(query, skip, 10);

      var displayResults = searchResults.Select(c => ContentToHighlightSearchResult.GetSearchResults(c, query, _provider.WordsEachSide, stemmer)).ToList();

      return displayResults;
    }

  }
}

