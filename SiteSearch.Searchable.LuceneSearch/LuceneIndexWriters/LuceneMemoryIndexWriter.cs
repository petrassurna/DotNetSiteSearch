using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace SiteSearch.Searchable.LuceneSearch.LuceneIndexWriters
{
  internal class LuceneMemoryIndexWriter : LuceneIndexWriter
  {
    public LuceneMemoryIndexWriter(RAMDirectory directory, Analyzer analyzer,
      LuceneVersion appLuceneVersion) : base(directory, analyzer, appLuceneVersion)
    { }

    public override void Commit()
    {
      _writer.Commit();
      Unlock();
    }

    public override void DeleteIndex()
    {
      Unlock();
      _writer.Dispose();
    }

    public override void Dispose()
    {
      Unlock();
      _writer.Dispose();
    }

    internal override void Unlock()
    {
      IndexWriter.Unlock(_Directory);
    }

  }
}
