using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Util;
using Searchable;
using SiteSearch.LuceneSearch.LuceneIndexWriters;
using SiteSearch.Searchable.SearchableContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiteSearch.LuceneSearch
{
  internal class LuceneSearcher : IDisposable
  {

    public enum SORT_ORDER { RELEVANCE = 0, DATE = 1 }

    Analyzer _analyzer;
    LuceneVersion _appLuceneVersion;
    LuceneIndexWriter _luceneEnglishIndexWriter;
    Content _template;

    public LuceneSearcher(LuceneIndexWriter luceneIndexWriter,
      Content template, Analyzer analyzer, LuceneVersion appLuceneVersion)
    {
      _analyzer = analyzer;
      _appLuceneVersion = appLuceneVersion;
      _luceneEnglishIndexWriter = luceneIndexWriter;
      _template = template;
    }

    private string CleanUp(string searchTerm)
    {
      string str = "";

      if (!string.IsNullOrWhiteSpace(searchTerm))
      {
        foreach (string word in searchTerm.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
        {
          string next = word;

          if (word.EndsWith("'s"))
          {
            next = word.Substring(0, word.Length - 2);
          }
          else if (word.EndsWith("\"s"))
          {
            next = word.Substring(0, word.Length - 2);
          }
          else if (word.EndsWith("’s"))
          {
            next = word.Substring(0, word.Length - 2);
          }

          if (!string.IsNullOrEmpty(str))
          {
            str += " ";
          }

          str += next;
        }
      }

      return str;
    }


    public void Dispose()
    {
      _analyzer?.Dispose();
      _luceneEnglishIndexWriter?.Dispose();
    }

    public bool KeyExists(ContentField key)
    {
      bool exists = false;
      List<Content> results = new List<Content>();

      if (!key.IsKey)
      {
        throw new Exception($"{key.Description} is not a key");
      }

      Query query = new TermQuery(new Term(key.Description, key.Value));

      using (var reader = _luceneEnglishIndexWriter.GetReader(applyAllDeletes: true)) //creates a lock
      {
        var searcher = new IndexSearcher(reader);
        TopDocs topDocs = searcher.Search(query, n: 1);

        if (topDocs != null && topDocs.ScoreDocs.Count() > 0)
        {
          exists = true;
        }
      }

      return exists;
    }

    public static SearchResult Make(float score, Document doc, string searchTerm, Content template)
    {
      string url = doc.Get("Url");
      url = Regex.Replace(url, @"(-/$|-$)", "/");

      SearchResult searchResult = new SearchResult();
      Content contentResult = template.Clone();

      foreach (var field in doc.Fields)
      {
        contentResult.Field(field.Name).Value = doc.Get(field.Name);
      }

      searchResult.Content = contentResult;
      searchResult.Score = score;

      return searchResult;
    }

    private static List<string> ParseEnclosed(string str, char ch)
    {

      var result = str
        .Split(ch)
        .Select((element, index) => index % 2 == 0  // If even index
        ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
        : new string[] { element })  // Keep the entire item
        .SelectMany(element => element).ToList();

      return result;
    }

    private static Query ParseQuery(string searchQuery, QueryParser parser)
    {
      Query query;
      string phrase;
      List<string> queries = ParseEnclosed(searchQuery, '"');
      BooleanQuery booleanQuery = new BooleanQuery();

      //phrases not taken into account
      foreach (string searchTerm in queries)
      {
        phrase = searchTerm.Trim();
        query = parser.Parse(phrase);
        booleanQuery.Add(query, Occur.MUST);
      }

      return booleanQuery;
    }


    //needs to be different if searching for a key
    private Query Query(string searchTerm)
    {
      MultiFieldQueryParser parser = new MultiFieldQueryParser(_appLuceneVersion, SearchFields(), _analyzer);
      string cleanedSearch = SearchQueryNoStopWords(searchTerm);
      var textQuery = ParseQuery(cleanedSearch, parser);

      var query = new BooleanQuery();
      query.Add(textQuery, Occur.MUST);

      return query;
    }

    public IEnumerable<SearchResult> Results(string searchTerm, int skip, int page_size)
    {
      IEnumerable<SearchResult> results = null;

      try
      {
        results = ResultsDirect(searchTerm, skip, page_size);
      }
      catch (Lucene.Net.Store.LockObtainFailedException)
      {
        Task.Delay(500).Wait();
        _luceneEnglishIndexWriter.Commit();
        results = ResultsDirect(searchTerm, skip, page_size);
      }
      finally
      {
      }

      return results;
    }


    private IEnumerable<SearchResult> ResultsDirect(string searchTerm, int skip, int page_size)
    {
      IEnumerable<SearchResult> results = null;
      ScoreDoc[] hits = null;

      searchTerm = CleanUp(searchTerm);

      var indexConfig = new IndexWriterConfig(_appLuceneVersion, _analyzer);

      Query query = Query(searchTerm);

      using (var reader = _luceneEnglishIndexWriter.GetReader(applyAllDeletes: true)) //creates a lock
      {
        try
        {
          var searcher = new IndexSearcher(reader);
          hits = searcher.Search(query, page_size + skip).ScoreDocs;

          results = hits.OrderByDescending(h => h.Score)
            .Skip(skip)
            .Select(h => Make(h.Score, searcher.Doc(h.Doc), searchTerm, _template)).ToList();
        }
        finally
        {
          _luceneEnglishIndexWriter.Unlock();//absolutely required
        }
      }

      return results;
    }


    public List<SearchResult> SearchById(ContentField field)
    {
      List<SearchResult> results = new List<SearchResult>();

      Query query = new TermQuery(new Term(field.Description, field.Value));

      using (var reader = _luceneEnglishIndexWriter.GetReader(applyAllDeletes: true)) //creates a lock
      {
        var searcher = new IndexSearcher(reader);
        TopDocs topDocs = searcher.Search(query, n: 100);

        if (topDocs != null && topDocs.ScoreDocs.Count() > 0)
        {
          Document resultDoc = searcher.Doc(topDocs.ScoreDocs[0].Doc);
          results = topDocs.ScoreDocs.Select(d => Make(d.Score, searcher.Doc(d.Doc), field.Value, _template)).ToList();
        }
      }

      return results;
    }

    private string[] SearchFields() => _template
      .Where(f => f.IsSearchable == true)
      .Select(s => s.Description).ToArray();


    private static string SearchQueryNoStopWords(string searchQuery)
    {
      string result = "";
      string[] search = searchQuery.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      string[] stopWords = { "a", "an", "and", "are", "as", "at", "be", "but", "by",
        "for", "if", "in", "into", "is", "it",
        "no", "not", "of", "on", "or", "such",
        "that", "the", "their", "then", "there", "these",
        "they", "this", "to", "was", "will", "with" };

      foreach (string word in search)
      {
        if (!stopWords.Contains(word))
        {
          result += word + " ";
        }
      }

      return result.Trim();
    }
  }
}
