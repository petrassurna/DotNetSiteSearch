using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searchable.SearchableContent
{
  public abstract class ContentField
  {
    public string Description { get; set; } = null!;

    public bool IsKey { get; set; }

    public bool IsSearchable { get; set; } = false;

    public string Value { get; set; } = null!;

    public ContentField(string description, string value, bool isSearchable, bool isKey = false)
    {
      Description = description;
      Value = value;
      IsSearchable = isSearchable;
      IsKey = isKey;
    }
  }

  public class ContentField<T> : ContentField
  {
    public ContentField(string description, string value, bool isSearchable, bool isKey = false) 
      : base(description, value, isSearchable, isKey)
    {
    }


  }
}
