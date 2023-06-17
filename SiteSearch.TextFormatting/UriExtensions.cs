using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSearch.TextFormatting
{
  public static class UriExtensions
  {
    public static string RelativeUrl(this Uri uri)
    {
      return uri.Segments[uri.Segments.Length - 1];
    }
  }
}
