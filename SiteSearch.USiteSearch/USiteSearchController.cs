using Searchable;
using SiteSearch.LuceneSearch;
using SiteSearch.Searchable.SearchResults;
using SiteSearch.Searchable.SearchResults.Factories;
using SiteSearch.Searchable.Stemming;
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
    public IEnumerable<UriSearchResultToReturn> Results(string query, int skip)
    {
      IStemmer stemmer = new Stemmer();
      var searchResults = _provider.Search(query, skip, 10);

      var displayResults = searchResults.Select(c => ContentToHighlightSearchResult.GetSearchResults(c, query, _provider.WordsEachSide, stemmer).ToHighlightedSearchResultToReturn()).ToList();

      return displayResults;
    }

  }
}

