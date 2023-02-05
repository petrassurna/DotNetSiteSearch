using SiteSearch.Searchable.SearchableContent;

namespace Searchable
{
  /// <summary>
  /// This interface defines the methods a SearchProvider needs to have
  /// At the time of writing there is a LuceneProivder but alternative providers
  /// such as Examine or AzureSearch could be implemented
  /// AzureSearch is intended to be developed at some stage as it is a more 
  /// performanat alternative to Lucene for sites using multiple web servers 
  /// where Lucene doesn't work too well
  /// </summary>
  public interface ISearchProvider : IDisposable
  {
    /// <summary>
    /// The number of words to highlight each side of a word match
    /// </summary>
    int WordsEachSide { get; }

    /// <summary>
    /// Adds the content to the index 
    /// Warning: If called more than once can result in duplicated content
    /// Use AddIfIdDoesntExist to stop duplicates
    /// </summary>
    /// <param name="content"></param>
    void Add(Content content);

    /// <summary>
    /// Adds the content to the index 
    /// Does not allow dupclicates
    /// Throws an exception if duplicate
    /// Use this instead of Add for practical indexing
    /// </summary>
    /// <param name="content"></param>
    void AddIfIdDoesntExist(Content content);

    /// <summary>
    /// Adds the content to the index or updates it if it doesnt exist
    /// If it is exists, a delete and then an add is performed
    /// </summary>
    /// <param name="content"></param>
    void AddOrUpdate(Content content);

    void CleanUp();

    void Delete(Content content);

    bool KeyExists(ContentField field);

    IEnumerable<SearchResult> Search(string searchTerm, int skip, int maximumResults);

    IEnumerable<SearchResult> SearchById(ContentField id);

    /// <summary>
    /// Convert a string to its stem or singular version
    /// This should use the same algorithm as the search system
    /// </summary>
    /// <param name="word">dogs</param>
    /// <returns>dog</returns>
    string Stem(string word);

    void Update(Content content);
  }
}
