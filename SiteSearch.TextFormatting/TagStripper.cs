using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace SiteSearch.TextFormatting
{
  public static class TagStripper
  {
    private static char[] DELIMITER_CHARS = { ' ', ',', '.', ':', '\t' };

    private static string CreateTag(HtmlNode node, string text)
    {
      var tag = new TagBuilder(node.Name);
      var dictionaryObj = new Dictionary<string, object>();

      foreach (var at in node.Attributes)
      {
        if (!dictionaryObj.ContainsKey(at.Name))
        {
          dictionaryObj.Add(at.Name, at.Value);
        }
        else
        {
          throw new Exception("Invalid html, duplicate attribute " + at.Name + " in " + node.OuterHtml);
        }
      }

      tag.MergeAttributes(dictionaryObj);
      tag.InnerHtml.AppendHtml(text);

      return GetString(tag);
    }


    public static string GetString(IHtmlContent content)
    {
      using (var writer = new System.IO.StringWriter())
      {
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
      }
    }


    public static string RemoveTag(string html, string tagName)
    {
      return RemoveTag(html, tagName, "", "");
    }


    public static string RemoveTag(string html, string tagName, string attributeName, string attributeValue)
    {
      var doc = new HtmlDocument();
      doc.LoadHtml(html);

      return RemoveTag(doc.DocumentNode.ChildNodes, tagName, attributeName, attributeValue);
    }


    private static string RemoveTag(HtmlNodeCollection nodes, string tagName, string attributeName, string attributeValue)
    {
      string str = "";

      foreach (HtmlNode node in nodes)
      {
        if (IsTag(node, tagName, attributeName, attributeValue))
        {
          //do nothing, we don't want this tag
        }
        else if (node.ChildNodes.Count() > 0)
        {
          string tag = RemoveTag(node.ChildNodes, tagName, attributeName, attributeValue);
          str += CreateTag(node, tag);
        }
        else
        {
          str += RemoveTag(node, tagName, attributeName, attributeValue);
        }
      }

      return str;
    }



    private static bool IsTag(HtmlNode node, string tagName, string attributeName, string attributeValue)
    {
      bool isTag = false;

      isTag = node.Name == tagName;

      if (!string.IsNullOrWhiteSpace(attributeName))
      {
        isTag = node.Attributes.Any(a => a.Name == attributeName && TextContainsValue(a.Value, attributeValue));
      }

      return isTag;
    }


    public static string RemoveEmptyTags(string str, string tag)
    {
      var doc = new HtmlDocument();
      doc.LoadHtml(str);

      RemoveEmptyTags(doc.DocumentNode.ChildNodes, tag);

      return doc.DocumentNode.InnerText;
    }


    private static HtmlNodeCollection RemoveEmptyTags(HtmlNodeCollection children, string tag)
    {
      for (int i = children.Count - 1; i >= 0; i--)
      {
        if (IsEmpty(children[i], tag))
        {
          if (IsEmpty(children[i], tag))
          {
            children.RemoveAt(i);
          }
          else
          {
            RemoveEmptyTags(children[i].ChildNodes, tag);
          }
        }
      }

      return children;
    }


    private static bool IsEmpty(HtmlNode node, string tag)
    {
      bool isEmpty = false;

      if (node.Name == tag && string.IsNullOrWhiteSpace(node.InnerText))
      {
        isEmpty = true;
      }

      return isEmpty;
    }


    private static HtmlNode RemoveEmptyTags(HtmlNode containerNode, string tag)
    {
      HtmlNode node = null!;

      if (containerNode.Name == tag)
      {
        node = containerNode;
      }

      return node;
    }


    private static string RemoveTag(HtmlNode node, string tagName, string attributeName, string attributeValue)
    {
      string str = "";

      if (!IsTag(node, tagName, attributeName, attributeValue))
      {
        str = node.OuterHtml;
      }

      return str;
    }

    public static string RemoveTagsWithAttribute(string html, string attributeName, string attributeValue)
    {
      HtmlDocument doc = new HtmlDocument();
      doc.LoadHtml(html);

      var nodes = doc.DocumentNode.SelectNodes("//*[@" + attributeName + "='" + attributeValue + "']");
      if (nodes != null)
      {
        foreach (var node in nodes)
        {
          node.Remove();
        }
      }

      return doc.DocumentNode.OuterHtml;
    }

    public static string Format(string html, int max)
    {
      var doc = new HtmlDocument();
      doc.LoadHtml(html);
      int no = max;
      List<HtmlNode> nodes = new List<HtmlNode>();

      nodes = FormatNodes(doc.DocumentNode.ChildNodes, max);

      int numberWords = NumberOfWords(Text(nodes));
      int htmlWords = NumberOfWords(html);

      if (numberWords < htmlWords)
      {
        AddDots(ref nodes);
      }

      return Html(nodes);
    }


    private static void AddDots(ref List<HtmlNode> nodes)
    {
      HtmlNode last = nodes.Last();

      HtmlDocument doc = new HtmlDocument();
      HtmlNode lastWithDots = doc.CreateElement(last.Name);

      foreach (var attribute in last.Attributes)
      {
        lastWithDots.Attributes.Add(attribute);
      }

      lastWithDots.InnerHtml = last.InnerHtml + "...";

      nodes[nodes.Count - 1] = lastWithDots;
    }


    private static HtmlNode FormatHtml(HtmlNode html, int max)
    {
      string str = MaximumWords.FormatSimpleString(html.InnerText, max);

      HtmlDocument doc = new HtmlDocument();
      HtmlNode textNode = doc.CreateElement(html.Name);
      textNode.InnerHtml = HtmlDocument.HtmlEncode(str);

      return textNode;
    }


    private static List<HtmlNode> FormatNodes(HtmlNodeCollection nodes, int no)
    {
      List<HtmlNode> result = new List<HtmlNode>();

      foreach (HtmlNode node in nodes)
      {
        if (node.ChildNodes.Count() > 1)
        {
          List<HtmlNode> children = FormatNodes(node.ChildNodes, no);

          HtmlDocument doc = new HtmlDocument();
          HtmlNode next = doc.CreateElement(node.Name);

          foreach (var attribute in node.Attributes)
          {
            next.Attributes.Add(attribute);
          }

          next.InnerHtml = Html(children);

          result.Add(next);
        }
        else
        {
          HtmlNode next = FormatHtml(node, no);

          foreach (var attribute in node.Attributes)
          {
            next.Attributes.Add(attribute);
          }

          result.Add(next);
        }

        no -= NumberOfWords(result);

        if (no <= 0)
          break;
      }

      return result;
    }


    private static string Html(List<HtmlNode> nodes)
    {
      string str = "";

      foreach (HtmlNode node in nodes)
      {
        if (node.Name == "#text")
          str += node.InnerText + " ";
        else
          str += node.OuterHtml + " ";
      }

      return str;
    }


    private static string Text(List<HtmlNode> nodes)
    {
      string str = "";

      foreach (HtmlNode node in nodes)
      {
        if (node.Name == "#text")
          str += node.InnerText + " ";
        else
          str += node.InnerText + " ";
      }

      return str;
    }


    private static bool TextContainsValue(string text, string value)
    {
      bool contains = false;

      string[] parts = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      foreach (string part in parts)
      {
        if (part == value)
        {
          contains = true;
          break;
        }
      }

      return contains;
    }

    private static int NumberOfWords(List<HtmlNode> nodes)
    {
      int size = 0;

      foreach (HtmlNode node in nodes)
      {
        if (node.ChildNodes.Count() > 1)
        {
          size = NumberOfWords(node.ChildNodes.ToList());
        }
        else
        {
          size = NumberOfWords(node);
        }
      }

      return size;
    }


    private static int NumberOfWords(HtmlNode str)
    {
      return NumberOfWords(str.InnerText);
    }


    private static int NumberOfWords(string str)
    {
      HtmlDocument doc = new HtmlDocument();
      doc.LoadHtml(str);

      return doc.DocumentNode.InnerText.Split(DELIMITER_CHARS, StringSplitOptions.RemoveEmptyEntries).Length;
    }


    public static string ReplaceMultipleSpaces(string sentence)
    {
      RegexOptions options = RegexOptions.None;
      Regex regex = new Regex("[ ]{2,}", options);
      sentence = regex.Replace(sentence, " ");

      return sentence;
    }


  }
}
