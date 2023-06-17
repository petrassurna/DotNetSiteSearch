using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSearch.Searchable.SearchableContent
{
  public class Content : List<ContentField>
  {
    public Content Clone()
    {
      ContentField nextField;
      Content content = new Content();  

      foreach (var field in this)
      {
        if (field is ContentField<int>)
        {
          nextField = new ContentField<int>(field.Description, field.Value, field.IsKey);
        }
        else if (field is ContentField<string>)
        {
          nextField = new ContentField<string>(field.Description, field.Value, field.IsKey);
        }
        else
        {
          throw new Exception();
        }

        content.Add(nextField);
      }

      return content;
    }

    public bool IsEmpty() => this.Count() == 0;

    public ContentField Field(string name)
    {
      return this.Single(f => f.Description == name);
    }

    public ContentField Key()
    {
      return this.Single(f => f.IsKey);
    }

  }
}
