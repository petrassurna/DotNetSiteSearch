using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormattingCore
{
  public class HTMLManipulator
  {

    /// <summary>
    /// 
    /// result = HTMLManipulator.AddAttribeToFirst("<p class=\"readMore\"></p>", "p", "class", "b");
    /// Assert.AreEqual(result, "<p class=\"readMore b\"></p>");
    /// result = HTMLManipulator.AddAttribeToFirst("<p class=\"readMore\"><a></a></p>", "p", "", "");
    /// Assert.AreEqual(result, "<p class=\"readMore\"><a></a></p>");
    /// result = HTMLManipulator.AddAttribeToFirst("<p class=\"readMore\"><a></a></p>", "p", "class", "b");
    /// Assert.AreEqual(result, "<p class=\"readMore b\"><a></a></p>");
    /// result = HTMLManipulator.AddAttribeToFirst("<div class=\"readMore\"><p></p></div>", "div", "class", "b");
    /// Assert.AreEqual(result, "<div class=\"readMore b\"><p></p></div>");
    /// result = HTMLManipulator.AddAttribeToFirst("<div class=\"readMore\"><p></p></div><div></div>", "div", "class", "b");
    /// Assert.AreEqual(result, "<div class=\"readMore b\"><p></p></div><div></div>");
    /// </summary>
    /// <param name="html"></param>
    /// <param name="tag"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string AddAttribeToFirst(string html, string tag, string name, string value)
    {
      string str = html;

      if (!string.IsNullOrWhiteSpace(html) && !string.IsNullOrWhiteSpace(tag) && !string.IsNullOrWhiteSpace(name))
      {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);

        HtmlNode node = doc.DocumentNode.FirstChild;// ("//" + tag);

        if (node != null && node.Name == tag && node.Attributes != null)
        {
          HtmlAttribute?  attribute = node.Attributes.FirstOrDefault(a => a.Name == name) ?? null;

          if (attribute != null && attribute.Value != value)
          {
            attribute.Value += " " + value;
          }
          else if (attribute == null)
          {
            node.Attributes.Add(name, value);
          }
        }

        str = doc.DocumentNode.OuterHtml;
      }


      return str;
    }


  }
}
