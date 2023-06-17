using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Util;
using SiteSearch.Searchable.LuceneSearch.Factories;
using SiteSearch.Searchable.LuceneSearch.LuceneIndexWriters;
using SiteSearch.Searchable.SearchableContent;
using System;
using System.Linq;

namespace SiteSearch.Searchable.LuceneSearch
{
  internal class LuceneIndexer : IDisposable
  {

    Analyzer _analyzer;
    LuceneVersion _luceneVersion;
    LuceneIndexWriter _luceneIndexWriter;

    public LuceneIndexer()
    { }

    public LuceneIndexer(LuceneIndexWriter writer, Analyzer analyzer, LuceneVersion luceneVersion)
    {
      _analyzer = analyzer;
      _luceneVersion = luceneVersion;
      _luceneIndexWriter = writer;
    }


    /// <summary>
    /// Must call Commit() when done
    /// Then call CleanUp
    /// </summary>
    /// <param name="content"></param>
    public void Add(Content content)
    {
      try
      {
        Document doc = ContentToDocument.ToDocument(content);
        _luceneIndexWriter.AddDocument(doc);
      }
      catch(Exception)
      {

      }
      finally
      {
        _luceneIndexWriter.Commit();
      }
    }


    public void AddIfIdDoesntExist(Content content)
    {
      LuceneSearcher searcher 
        = new LuceneSearcher(_luceneIndexWriter, content, _analyzer, _luceneVersion);

      if (searcher.SearchById(content.Key()).Any())
      {
        throw new Exception($"SearchableContent with Id {content.Key().Description} already exists");
      }
      else
      {
        Add(content);
      }
    }


    public void Commit()
    {
      _luceneIndexWriter.Commit();
    }


    public void Delete(ContentField field)
    {
      var analyzer = new StandardAnalyzer(_luceneVersion);

      MultiFieldQueryParser parser = new MultiFieldQueryParser(
            LuceneVersion.LUCENE_48,
            new[] { nameof(field.Description) },
            analyzer
            );

      Term term = new Term(field.Description, field.Value);

      _luceneIndexWriter.DeleteDocuments(term);
      _luceneIndexWriter.Commit();
    }


    public void Dispose()
    {
      _analyzer?.Dispose();
      _luceneIndexWriter?.Dispose();
    }

    public void Update(Content content)
    {
      try
      {
        Document doc = ContentToDocument.ToDocument(content);
        _luceneIndexWriter.UpdateDocument(content, doc);

      }
      finally
      {
        _luceneIndexWriter.Commit();
      }
    }


  }
}
