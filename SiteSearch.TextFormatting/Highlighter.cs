using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextFormatting
{
  public class HighlightMatch
  {
    static readonly string PUNCTUATION = @"'\-,\.""\$\{\[\(\|\)\*\?!";

    private static bool ContainsWord(Dictionary<string, string> matches, string word)
    {
      bool contains = false;

      foreach (KeyValuePair<string, string> entry in matches)
      {
        if (WordMatch(entry.Value, word))
        {
          contains = true;
          break;
        }
      }

      return contains;
    }


    private static bool ContainsWordPart(Dictionary<string, string> matches, string word)
    {
      bool contains = false;

      foreach (KeyValuePair<string, string> entry in matches)
      {
        if (WordPartMatch(entry.Value, word))
        {
          contains = true;
          break;
        }
      }

      return contains;
    }


    public static string DotsBeforeAndAfter(string text, string word, int num)
    {
      string words;

      List<string> parts = ParseWordsWithPunctuation(text);
      int index = IndexOfRegexWordPartMatch(parts, word);

      if (index != -1 && !string.IsNullOrWhiteSpace(word))
      {
        string start = Extract(parts, index - num, index);
        string end = Extract(parts, index + 1, index + num + 1);

        words = start + " " + parts[index] + " " + end;

        if (num < index)
        {
          words = "..." + words;
        }

        if (parts.Count() - index - 1 > num)
        {
          words += "...";
        }
      }
      else
      {
        words = "";
      }

      return words.Trim();
    }


    private static string EscapeWord(string word)
    {
      string escape = @"\[]()*+?{}-";

      foreach (char c in escape)
      {
        word = word.Replace(c.ToString(), @"\" + c.ToString());
      }

      return word;
    }

    private static string Extract(List<string> words, int start, int end)
    {
      string str = "";

      if (start < 0)
        start = 0;

      if (end > words.Count())
        end = words.Count();

      for (int i = start; i < end; i++)
      {
        str += words[i] + " ";
      }

      return str.Trim(); ;
    }

    public static string HighlightPhrase(string phrase, string word, string tag)
    {
      string str = phrase;
      string[] parts = word.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      foreach (string part in parts)
      {
        str = HighlightWord(str, part, tag);
      }

      return str;
    }


    public static string HighlightPhrasePartial(string phrase, string word, string tag)
    {
      string str = phrase;
      string[] parts = word.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      foreach (string part in parts)
      {
        str = HighlightWordPart(str, part, tag);
      }

      return str;
    }

    public static string HighlightWord(string text, string word, string tag)
    {
      word = EscapeWord(word);

      string search = @"(^|\s+)([" + PUNCTUATION + "]*)(" + word + @")([" + PUNCTUATION + @"]*)(\s+|$)";


      text = Regex.Replace(text, search, " <" + tag + ">$2$3$4</" + tag + ">$5 ", RegexOptions.IgnoreCase | RegexOptions.Multiline);
      text = Regex.Replace(text, search, " <" + tag + ">$2$3$4</" + tag + ">$5 ", RegexOptions.IgnoreCase | RegexOptions.Multiline);

      return Formatter.RemoveDoubleSpaces(text).Trim();
    }


    public static string HighlightWordPart(string text, string word, string tag)
    {
      string highlighted = text;
      int match, startAt = 0;
      do
      {
        match = highlighted.ToLower().IndexOf(word.ToLower(), startAt);

        if (match != -1)
        {
          string left = highlighted.Substring(0, match);
          string right = highlighted.Substring(match + word.Length);
          string wordMatch = highlighted.Substring(match, word.Length);

          highlighted = Highlight(left, right, wordMatch, tag);

          startAt = match + 2 * tag.Length + 5;
        }

      } while (match != -1);

      return Formatter.RemoveDoubleSpaces(highlighted).Trim();
    }


    private static string Highlight(string left, string right, string part, string tag)
    {
      string str = left + "<" + tag + ">" + part + "</" + tag + ">" + right;
      return str;
    }


    public static string HighlightPhrase(string text, string query)
    {
      string html = "";

      Dictionary<string, string> matches = SearchResult(text, query, 6, "strong");

      foreach (KeyValuePair<string, string> entry in matches)
      {
        html += "<p>" + entry.Value + "</p>";
      }

      return html;
    }


    public static int IndexOfRegexWordMatch(List<string> list, string word)
    {
      int match = -1;

      for (int i = 0; i < list.Count(); i++)
      {
        if (WordMatch(list[i], word))
        {
          match = i;
          break;
        }
      }

      return match;
    }


    public static int IndexOfRegexWordPartMatch(List<string> list, string word)
    {
      int match = -1;

      for (int i = 0; i < list.Count(); i++)
      {
        if (WordPartMatch(list[i], word))
        {
          match = i;
          break;
        }
      }

      return match;
    }


    public static Dictionary<string, string> SearchResult(string text, string searchPhrase, int wordsBeforeAfter, string highlightTag)
    {
      string stripped = StripIllegalHightlightCharacters(searchPhrase);
      Dictionary<string, string> matches = SearchResultMatch(text, stripped, wordsBeforeAfter);

      matches = SearchResultHighlight(matches, stripped, highlightTag);

      return matches;
    }


    private static string StripIllegalHightlightCharacters(string searchPhrase)
    {
      string stripped = searchPhrase.Replace("\"", "");
      return stripped;
    }

    public static string Test()
    {
      return "";
    }


    public static string SearchResultFastPhraseFormatted(string text, string searchPharse, string tag, int charsBeforeAfter)
    {
      string result = "";
      List<string> matches = SearchResultFastPhrase(text, searchPharse, tag, charsBeforeAfter);

      foreach (string str in matches.Where(s => !string.IsNullOrWhiteSpace(s)))
      {
        result += "<p>.." + str + "..</p>";
      }

      return result;
    }


    public static List<string> SearchResultFastPhrase(string text, string searchPharse, string tag, int charsBeforeAfter)
    {
      string[] words = searchPharse.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      List<string> results = new List<string>(words.Length);

      for (int i = 0; i < words.Count(); i++)
      {
        if (!HighlightFast(i, ref results, words[i], tag))
        {
          results.Add(SearchResultFastWord(text, words[i], tag, charsBeforeAfter));
        }
      }

      return results;
    }



    private static bool HighlightFast(int i, ref List<string> results, string word, string tag)
    {
      Match match= null!;

      for (int j = 0; j < results.Count; j++)
      {
        match = HasMatch(results[j], word);

        if (match.Success)
        {
          results[j] = SearchResultFastWord(results[j], word, tag, results[j].Length);
        }
      }

      return match == null ? false : match.Success;
    }


    public static Match HasMatch(string text, string word)
    {
      return Regex.Match(text.ToLower(), @"(^|\b)" + word.ToLower() + @"($|\b)");
    }


    public static string SearchResultFastWord(string text, string word, string tag, int charsBeforeAfter)
    {
      string lowerCaseText = text.ToLower();

      Match match = HasMatch(lowerCaseText, word);

      int start = match.Index - charsBeforeAfter;
      if (start < 0)
      {
        start = 0;
      }

      string left = text.Substring(start, match.Index - start);
      string matched = text.Substring(match.Index, match.Length);
      string middle = $"<{tag}>{matched}</{tag}>";

      int lengthAfter = Math.Min(charsBeforeAfter, text.Length - (match.Index + match.Length));

      string right = text.Substring(match.Index + match.Length, lengthAfter);

      return left + middle + right;
    }


    public static Dictionary<string, string> SearchResultPartial(string text, string searchPhrase, int wordsBeforeAfter, string highlightTag)
    {
      Dictionary<string, string> matches = SearchResultMatchPartial(text, searchPhrase, wordsBeforeAfter);

      matches = SearchResultHighlightPartial(matches, searchPhrase, highlightTag);

      return matches;
    }

    public static Dictionary<string, string> SearchResultHighlight(Dictionary<string, string> matches, string searchPhrase, string highlightTag)
    {
      Dictionary<string, string> highlights = new Dictionary<string, string>();

      foreach (KeyValuePair<string, string> entry in matches)
      {
        string highlighted = HighlightPhrase(entry.Value, searchPhrase, highlightTag);

        highlights.Add(entry.Key, highlighted);
      }

      return highlights;
    }


    public static Dictionary<string, string> SearchResultHighlightPartial(Dictionary<string, string> matches, string searchPhrase, string highlightTag)
    {
      Dictionary<string, string> highlights = new Dictionary<string, string>();

      foreach (KeyValuePair<string, string> entry in matches)
      {
        string highlighted = HighlightPhrasePartial(entry.Value, searchPhrase, highlightTag);

        highlights.Add(entry.Key, highlighted);
      }

      return highlights;
    }



    private static Dictionary<string, string> SearchResultMatch(string text, string searchPhrase, int wordsBeforeAfter)
    {
      string[] searches = searchPhrase.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      Dictionary<string, string> matches = new Dictionary<string, string>();

      foreach (string word in searches)
      {
        if (!ContainsWord(matches, word) && WordMatch(text, word))
        {
          string line = DotsBeforeAndAfter(text, word, wordsBeforeAfter);

          if (!string.IsNullOrWhiteSpace(line))
          {
            matches.Add(word, line);
          }
        }
      }

      return matches;
    }


    private static Dictionary<string, string> SearchResultMatchPartial(string text, string searchPhrase, int wordsBeforeAfter)
    {
      string[] searches = searchPhrase.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      Dictionary<string, string> matches = new Dictionary<string, string>();

      foreach (string word in searches)
      {
        if (!ContainsWordPart(matches, word))
        {
          string line = DotsBeforeAndAfter(text, word, wordsBeforeAfter);

          if (!string.IsNullOrWhiteSpace(line))
          {
            matches.Add(word, line);
          }
        }
      }

      return matches;
    }


    private static List<string> ParseWordsWithPunctuation(string text)
    {
      var collection = Regex.Matches(text, @"([^\w\s]*(\w+)[^\w\s]*)+");
      List<string> matches = new List<string>(collection.Count);

      foreach (Match match in collection)
      {
        matches.Add(match.Value);
      }

      return matches;
    }


    public static string TerminateIncompleteWords(string sentence)
    {
      string answer = sentence;

      if (!string.IsNullOrWhiteSpace(sentence))
      {
        int first = sentence.IndexOf(" ");
        int last = sentence.LastIndexOf(" ");
        int start = 0, end = sentence.Length;

        if (first != -1)
        {
          start = first + 1;
        }

        if (last != -1)
        {
          end = last - 1;
        }

        if (start > end)
        {
          answer = sentence.Substring(0, start - 1);
        }
        else if (!(first == -1 && last == -1))
        {
          answer = sentence.Substring(start, end - start + 1);
        }
      }

      return answer;
    }


    public static string WordsBeforeAndAfter(string text, string word, int num)
    {
      string words;
      List<string> parts = ParseWordsWithPunctuation(text);
      List<string> partsToLower = ParseWordsWithPunctuation(text.ToLower());

      int index = IndexOfRegexWordMatch(partsToLower, word.ToLower());

      if (index != -1)
      {
        string start = Extract(parts, index - num, index);
        string end = Extract(parts, index + 1, index + num + 1);

        words = start + " " + parts[index] + " " + end;
      }
      else
      {
        words = "";
      }

      return words.Trim();
    }


    public static string WordsBeforeAndAfterPart(string text, string word, int num)
    {
      string words;

      List<string> parts = ParseWordsWithPunctuation(text);
      List<string> partsToLower = ParseWordsWithPunctuation(text.ToLower());

      int index = IndexOfRegexWordPartMatch(partsToLower, word.ToLower());

      if (index != -1)
      {
        string start = Extract(parts, index - num, index);
        string end = Extract(parts, index + 1, index + num + 1);

        words = start + " " + parts[index] + " " + end;
      }
      else
      {
        words = "";
      }

      return words.Trim();
    }


    public static bool WordMatch(string str, string word)
    {
      bool isMatch;

      word = word.Replace("(", "");
      word = word.Replace(")", "");
      word = word.Replace("+", "");
      word = word.Replace("?", "");
      word = word.Replace("*", "");
      word = word.Replace("[", "");
      word = word.Replace("]", "");
      word = word.Replace(".", "");

      string pattern = word + @"[" + PUNCTUATION + @"]*($|\s)";
      isMatch = Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);

      return isMatch;
    }

    public static bool WordPartMatch(string str, string word)
    {
      return str.ToLower().Contains(word.ToLower());
    }

  }
}
