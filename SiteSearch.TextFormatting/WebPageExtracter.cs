using HtmlAgilityPack;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace SiteSearch.TextFormatting
{
  public class WebPageExtracter
  {

    public static string Clean(string str)
    {
      if (string.IsNullOrWhiteSpace(str))
        return "";

      str = Regex.Replace(str, @"[^\u0000-\u007F]+", string.Empty);

      str = str.Replace("\r", "");
      str = str.Replace("\n", " ");

      return Formatter.RemoveDoubleSpaces(str).Trim();
    }


    public static string ExtractFromUrl(string url)
    {
      if (string.IsNullOrWhiteSpace(url))
        return "";

      HtmlWeb web = new HtmlWeb();
      HtmlDocument doc = web.Load(url);
      return Clean(doc.DocumentNode.InnerText);
    }


    public static string ExtractFromUrl(string url, List<Tag> tags)
    {
      HtmlWeb web = new HtmlWeb();
      HtmlDocument doc = web.Load(url);
      string html = doc.DocumentNode.SelectSingleNode("//html").OuterHtml;

      return ExtractFromHtml(html, tags, "body");
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="html">Complete html from a website</param>
    /// <param name="tags"></param>
    /// <returns></returns>
    public static string ExtractFromHtml(string html, List<Tag> tags) => ExtractFromHtml(html, tags, "body");


    public static string ExtractFromHtml(string html, List<Tag> tags, string node)
    {
      if (string.IsNullOrWhiteSpace(html))
        return "";

      HtmlWeb web = new HtmlWeb();
      HtmlDocument doc = new HtmlDocument();
      string text = "";

      if (tags != null)
      {
        foreach (Tag tag in tags)
        {
          html = TagStripper.RemoveTag(html, tag.TagName, tag.AttributeName, tag.AttributeValue);
        }
      }

      //text in adjacent tags, without spaces, will be incorrectly joined without this
      //<p>the dog</p><p>the cat</p> becomes
      //the dogthe cat
      html = html.Replace(">", "> ");

      doc.LoadHtml(html);

      HtmlNode body = doc.DocumentNode.SelectSingleNode("//body");

      if (!string.IsNullOrWhiteSpace(node) && body != null)
        text = body.InnerText;
      else
        text = doc.DocumentNode.InnerText;

      return Clean(text);
    }


    public static string ExtractBody(string html)
    {
      string extracted = html;
      HtmlWeb web = new HtmlWeb();
      HtmlDocument doc = new HtmlDocument();

      doc.LoadHtml(html);

      HtmlNode body = doc.DocumentNode.SelectSingleNode("//body");

      if (!string.IsNullOrWhiteSpace(html) && body != null)
        extracted = body.InnerHtml;
      else
        extracted = html;

      return extracted;
    }


    public static string ExtractTextFromWithinBody(string html)
    {
      HtmlWeb web = new HtmlWeb();
      HtmlDocument doc = new HtmlDocument();
      string extracted = html;

      doc.LoadHtml(html);

      extracted = ExtractBody(html);

      return ExtractTextFromHtml(extracted);
    }


    /// <summary>
    /// "<p>abc<a href="">123</a>def</p>
    /// returns "abc 123 def";
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string ExtractTextFromHtml(string html)
    {
      html = Formatter.ReplaceSmartQuotes(html);//necessary as code below strips funny chars 
      string [] tags = { "script", "style", "meta", "noscript" };
      html = RemoveTags( html, tags);

      if (string.IsNullOrWhiteSpace(html))
        return "";

      string noHTML = Regex.Replace(html, @"<[^>]+>|&nbsp;", " ");

      return Clean(noHTML);
    }


    public static string GetTitle(string html)
    {
      if (string.IsNullOrWhiteSpace(html))
        return "";

      HtmlDocument doc = null!;
      doc.LoadHtml(html);

      string title = doc.DocumentNode.SelectSingleNode("//head/title").InnerText;

      if (string.IsNullOrWhiteSpace(title))
      {
        title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
      }

      if (string.IsNullOrWhiteSpace(title) && doc != null)
      {
        var x = doc.DocumentNode.Descendants("title").FirstOrDefault();

        if(x != null)
        {
          title = x.InnerText;
        }
      }

      if (title == null)
      {
        title = "";
      }

      return Clean(title);
    }

    public static string RemoveTags(string html, string[] tags)
    {
      string stripped = html;

      foreach (string tag in tags)
      {
        stripped = TagStripper.RemoveTag(stripped, tag);
      }

      return stripped;
    }

  }


  public class Tag
  {
    public string TagName { get; set; } = null!;
    public string AttributeName { get; set; } = null!;
    public string AttributeValue { get; set; } = null!;
  }

}
