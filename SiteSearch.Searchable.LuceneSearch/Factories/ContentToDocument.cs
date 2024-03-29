﻿using Lucene.Net.Documents;
using SiteSearch.Searchable.SearchableContent;
using Field = Lucene.Net.Documents.Field;


namespace SiteSearch.Searchable.LuceneSearch.Factories
{
  public class ContentToDocument
  {
    public static Document ToDocument(Content content)
    {
      Document document = new Document(); 

      foreach(var field in content)
      {
        document.Add(new TextField(field.Description, field.Value, Field.Store.YES));
      }

      return document;
    }
  }
}
