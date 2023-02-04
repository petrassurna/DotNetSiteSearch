using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System.IO;

namespace LuceneSearch.LuceneIndexWriters
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


    private void DeleteFiles()
    {
    }


    public override void DeleteIndex()
    {
      //string directoryName = (_Directory as FSDirectory).Directory.FullName;

      //if (System.IO.Directory.Exists(directoryName))
      {
        Unlock();
        _writer.Dispose();
      }
    }


    public override void Dispose()
    {
      Unlock();
      _writer.Dispose();
    }


    internal override void Unlock()
    {
     // string directoryName = (_Directory as FSDirectory).Directory.FullName;

      //if (System.IO.Directory.Exists(directoryName))
      {
        IndexWriter.Unlock(_Directory);
      }
    }

  }
}
