using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Util;
using SiteSearch.Searchable.SearchableContent;
using System;
using Directory = Lucene.Net.Store.Directory;

namespace SiteSearch.LuceneSearch.LuceneIndexWriters
{
  internal abstract class LuceneIndexWriter : IDisposable
  {
    Analyzer _analyzer;
    protected Directory _Directory;
    protected IndexWriter _writer;
    LuceneVersion _appLuceneVersion;

    public LuceneIndexWriter(Directory directory, Analyzer analyzer, LuceneVersion appLuceneVersion)
    {
      _analyzer = analyzer;
      _appLuceneVersion = appLuceneVersion;
      _Directory = directory;

      var indexConfig = new IndexWriterConfig(_appLuceneVersion, _analyzer);
      _writer = new IndexWriter(_Directory, indexConfig);
    }

    internal void AddDocument(Document doc)
    {
      _writer.AddDocument(doc);
    }


    public abstract void Commit();


    internal void DeleteDocuments(Term term)
    {
      _writer.DeleteDocuments(term);
    }

    public abstract void DeleteIndex();

    public abstract void Dispose();


    internal DirectoryReader GetReader(bool applyAllDeletes)
    {
      return _writer.GetReader(applyAllDeletes: applyAllDeletes); //creates a lock
    }


    internal abstract void Unlock();


    internal void UpdateDocument(Content content, Document doc)
    {
      try
      {
        _writer.UpdateDocument(new Term(content.Key().Description, content.Key().Value), doc); ;
      }
      finally
      {
        _writer.Commit();
      }
    }

  }
}
