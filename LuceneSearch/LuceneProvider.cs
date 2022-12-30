using Lucene.Net.Analysis;
using Lucene.Net.Analysis.En;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Store;
using Lucene.Net.Tartarus.Snowball.Ext;
using Lucene.Net.Util;
using LuceneSearch.LuceneIndexWriters;
using Searchable;
using Searchable.SearchableContent;
using Searchable.SearchableContent.Factories;
using System;
using System.Collections.Generic;

namespace LuceneSearch
{
  public class LuceneProvider : ISearchProvider
  {
    string _path;
    LuceneIndexWriter _luceneIndexWriter;

    LuceneIndexer _postIndexer;
    LuceneSearcher _postEnglishSearcher;
    LuceneSearcher _postStandardSearcher;

    Analyzer _EnglishAnalyzer;
    Analyzer _StandardAnalyzer;

    Content _template;

    int _wordsEachSide = 0;

    LuceneVersion _luceneVersion = LuceneVersion.LUCENE_48;

    public LuceneProvider()
    {
      _EnglishAnalyzer = new EnglishAnalyzer(_luceneVersion);
      _StandardAnalyzer = new StandardAnalyzer(_luceneVersion);
    }

    public LuceneProvider(RAMDirectory rAMDirectory)
    {
      _EnglishAnalyzer = new EnglishAnalyzer(_luceneVersion);
      _luceneIndexWriter = new LuceneMemoryIndexWriter(rAMDirectory, _EnglishAnalyzer, _luceneVersion);
      _postIndexer = new LuceneIndexer(_luceneIndexWriter, _EnglishAnalyzer, _luceneVersion);
      _template = ContentFactory.WebPage("", "", "", "");
      _postEnglishSearcher = new LuceneSearcher(_luceneIndexWriter, _template, _EnglishAnalyzer, _luceneVersion);
      _postStandardSearcher = new LuceneSearcher(_luceneIndexWriter, _template, _StandardAnalyzer, _luceneVersion);
      _wordsEachSide = 12;
    }

    /// <summary>
    /// Configuration of LuceneProvider
    /// </summary>
    /// <param name="path">The relative path where to store the Lucene index</param>
    /// <param name="wordsEachSide">When displaying search results, the number of words to display each side of a word match</param>
    public LuceneProvider(string path, int wordsEachSide)
    {
      _path = path;

      //_analyzer = new EnglishAnalyzer(_luceneVersion);
      _EnglishAnalyzer = new StandardAnalyzer(_luceneVersion);

      _luceneIndexWriter = new LuceneDiskIndexWriter(FSDirectory(_path), _EnglishAnalyzer, _luceneVersion);
      _postIndexer = new LuceneIndexer( _luceneIndexWriter, _EnglishAnalyzer, _luceneVersion);
      _template = ContentFactory.WebPage("", "", "", "");
      _postEnglishSearcher = new LuceneSearcher( _luceneIndexWriter, _template, _EnglishAnalyzer, _luceneVersion);
      _postStandardSearcher = new LuceneSearcher( _luceneIndexWriter, _template, _StandardAnalyzer, _luceneVersion);

      _wordsEachSide = wordsEachSide;
    }

    public int WordsEachSide
    {
      get { return _wordsEachSide; }
    }

    public void Add(Content content) => _postIndexer.Add(content);

    public void AddIfIdDoesntExist(Content content) => _postIndexer.AddIfIdDoesntExist(content);

    public void AddOrUpdate(Content content)
    {
      if (KeyExists(content.Key()))
      {
        _postIndexer.Delete(content.Key());
      }

      _postIndexer.Add(content);
    }

    public void CleanUp() => DeleteIndex();

    public void Delete(Content content) => _postIndexer.Delete(content.Field(ContentFactory.ID));

    public void DeleteIndex() => _luceneIndexWriter.DeleteIndex();

    public void Dispose()
    {
      if (_luceneIndexWriter != null)
        _luceneIndexWriter.Dispose();

      if (_EnglishAnalyzer != null)
        _EnglishAnalyzer.Dispose();

      if (_StandardAnalyzer != null)
        _StandardAnalyzer.Dispose();
    }

    private FSDirectory FSDirectory(string path)
    {
      if (!System.IO.Directory.Exists(path))
      {
        System.IO.Directory.CreateDirectory(path);
      }

      var dir = Lucene.Net.Store.FSDirectory.Open(path);

      return dir;
    }

    public bool KeyExists(ContentField field) => _postStandardSearcher.KeyExists(field);

    public IEnumerable<SearchResult> Search(string searchTerm, int skip, int maximumResults)
      => _postEnglishSearcher.Results(searchTerm, skip, maximumResults);

    public IEnumerable<SearchResult> SearchById(ContentField field) => _postEnglishSearcher.SearchById(field);


    /// <summary>
    ///  This is used by the EnglishAnalyser and really sholud be extracted from it somehow
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public string Stem(string word)
    {
      Stemmer stemmer = new Stemmer();
      return stemmer.Stem(word);
    }

    public void Update(Content content) => _postIndexer.Update(content);

  }
}
