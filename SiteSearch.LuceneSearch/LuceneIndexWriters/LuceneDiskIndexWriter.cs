using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System.IO;

namespace SiteSearch.LuceneSearch.LuceneIndexWriters
{
  internal class LuceneDiskIndexWriter : LuceneIndexWriter
  {

    string WRITE_LOCK = "write.lock";

    public LuceneDiskIndexWriter(FSDirectory directory, Analyzer analyzer,
      LuceneVersion appLuceneVersion) : base(directory, analyzer, appLuceneVersion)
    { }

    public override void Commit()
    {
      _writer.Commit();
      Unlock();

      string directoryName = (_Directory as FSDirectory).Directory.FullName;

      if (System.IO.Directory.Exists(directoryName))
      {
        File.Delete($"{directoryName}\\{WRITE_LOCK}");
      }
    }


    private void DeleteFiles()
    {
      string directoryName = (_Directory as FSDirectory).Directory.FullName;

      if (System.IO.Directory.Exists(directoryName))
      {
        DirectoryInfo di = new DirectoryInfo(directoryName);

        foreach (FileInfo file in di.GetFiles())
        {
          file.Delete();
        }
      }
    }


    public override void DeleteIndex()
    {
      string directoryName = (_Directory as FSDirectory).Directory.FullName;

      if (System.IO.Directory.Exists(directoryName))
      {
        Unlock();
        _writer.Dispose();
        DeleteFiles();
        System.IO.Directory.Delete(directoryName, true);
      }
    }


    public override void Dispose()
    {
      Unlock();
      _writer.Dispose();

      if (System.IO.Directory.Exists((_Directory as FSDirectory).Directory.FullName))
      {
        File.Delete($"{(_Directory as FSDirectory).Directory.FullName}\\{WRITE_LOCK}");
      }
    }



    internal override void Unlock()
    {
      string directoryName = (_Directory as FSDirectory).Directory.FullName;

      if (System.IO.Directory.Exists(directoryName))
      {
        IndexWriter.Unlock(_Directory);
      }
    }

  }
}
