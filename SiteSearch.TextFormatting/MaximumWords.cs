using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SiteSearch.TextFormatting
{
  public static class MaximumWords
  {
    private static char[] DELIMITER_CHARS = { ' ', ',', '.', ':', '\t' };


    public static string Format(string html, int maxWords, int maxCharacters)
    {
      string str = Format(html, maxWords);

      int no = maxWords;

      while (str.Length > maxCharacters)
      {
        no--;
        str = Format(html, no);
      }

      return str;
    }


    public static string Format(string html, int maxWords)
    {
      var doc = new HtmlDocument();
      doc.LoadHtml(html);
      doc.OptionWriteEmptyNodes = true;
      int no = maxWords;
      List<HtmlNode> nodes = new List<HtmlNode>();

      nodes = FormatNodes(doc.DocumentNode.ChildNodes, maxWords);

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


    private static List<HtmlNode> FormatNodes(HtmlNodeCollection nodes, int no)
    {
      List<HtmlNode> result = new List<HtmlNode>();

      foreach (HtmlNode node in nodes)
      {
        if (node.ChildNodes.Count() > 1)
        {
          List<HtmlNode> children = FormatNodes(node.ChildNodes, no);

          HtmlDocument doc = new HtmlDocument();
          doc.OptionWriteEmptyNodes = true;
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

      return str.Trim();
    }


    public static string FirstSentence(string words)
    {
      int pos = words.IndexOf(".");

      if (pos != -1)
      {
        words = words.Substring(0, pos + 1);
      }

      return words;
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


    private static HtmlNode FormatHtml(HtmlNode html, int max)
    {
      string str = FormatSimpleString(html.InnerText, max);

      HtmlDocument doc = new HtmlDocument();
      doc.OptionWriteEmptyNodes = true;
      HtmlNode textNode = doc.CreateElement(html.Name);
      textNode.InnerHtml = HtmlDocument.HtmlEncode(str);


      return textNode;
    }


    public static string FormatSimpleString(string str, int max)
    {
      string formatted = "";
      char[] delimiters = { ' ', ',', ':', '\t' };

      string[] items = str.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

      foreach (string item in items.Take(max))
      {
        if (!string.IsNullOrWhiteSpace(formatted))
          formatted += " ";

        formatted += item;
      }

      return formatted;
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


    public static int NumberOfWords(string str)
    {
      HtmlDocument doc = new HtmlDocument();
      doc.LoadHtml(str);

      return doc.DocumentNode.InnerText.Split(DELIMITER_CHARS, StringSplitOptions.RemoveEmptyEntries).Length;
    }

  }
}
