using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace TextFormatting
{
  public class LinkWrapper
  {
    public static string WrapLinks(string str)
    {
      var html = Regex.Replace(str, @"((http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*)", @"<a href='$1' target='blank'>$1</a>");
      return html;
    }

    public static string WrapHashTags(string str, string urlPrepend)
    {
      var html = Regex.Replace(str, @"(#)(\S+)(\s|$)", "<a href=\"" + urlPrepend + "$2\" target=\"_blank\">$1$2</a>$3");
      return html;
    }

  }
}
