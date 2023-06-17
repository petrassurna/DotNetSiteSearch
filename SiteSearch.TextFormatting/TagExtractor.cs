using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiteSearch.TextFormatting
{
  public class TagExtractor
  {
    /// <summary>
    /// Return the content of all tags
    /// </summary>
    /// <param name="html"></param>
    /// <param name="tag">The tag name
    /// </param>
    /// <returns></returns>
    public static List<string> GetTags(string html, string tag)
    {
      List<string> tags = new List<string>();

      if (string.IsNullOrWhiteSpace(html))
        return tags;

      HtmlDocument doc = new HtmlDocument();
      doc.LoadHtml(html);

      GetTags(ref tags, doc.DocumentNode.ChildNodes, tag);

      return tags;
    }

    private static void GetTags(ref List<string> tags, HtmlNodeCollection nodes, string tag)
    {
      foreach(var node in nodes)
      {
        if(node.Name == tag)
        {
          tags.Add(node.InnerText);
        }

        if(node.ChildNodes.Any())
        {
          GetTags(ref tags, node.ChildNodes, tag);
        }
      }
    }

  }
}
