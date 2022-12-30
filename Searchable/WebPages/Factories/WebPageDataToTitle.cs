using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TextFormatting;

namespace Searchable.WebPages.Factories
{
  public class WebPageDataToTitle
  {

    public static string ToTitle(Uri uri, string html)
    {
      string title = "";

      var tags = TagExtractor.GetTags(html, "title");

      if(tags.Any())
      {
        title = tags[0];
      }
      else
      {
        title = uri.RelativeUrl();
        title = Formatter.FormatTitle(title);

        if(title == "/")
        {
          title = "";
        }
      }

      return title;
    }

  }
}
