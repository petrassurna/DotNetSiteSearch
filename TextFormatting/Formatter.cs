using Microsoft.AspNetCore.Html;
using System.Globalization;
using System.Text.RegularExpressions;


namespace TextFormatting
{
  public class Formatter
  {

    public static HtmlString AddToImageFileNames(HtmlString html, string addition)
    {
      return new HtmlString(AddToImageFileNames(html.Value, addition));
    }


    public static string AddToImageFileNames(string html, string addition)
    {
      if (html != null)
      {
        if (!string.IsNullOrWhiteSpace(addition))
        {
          html = Regex.Replace(html, @"\.((jpg)|(jpeg)|(gif)|(png)|(svg))", $@"{addition}.$1".ToLower(), RegexOptions.IgnoreCase);
        }

      }
      else
      {
        html = "";
      }

      return html;
    }


    public static double MonthDifference(DateTime start, DateTime end)
    {
      double difference = 0;

      if (end < start)
      {
        return -MonthDifference(start, end);
      }
      else
      {
        difference = 12 * (end.Year - start.Year);
        difference += end.Month - start.Month;

        if (end.Month == start.Month)
        {
          if (end.Day < start.Day)
          {
            difference -= 1;
          }
        }
        else if (end.Day < start.Day)
        {
          difference -= 1;
        }
      }

      return difference;
    }

    public static string FormatTitle(string str)
    {
      TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

      str = str.Replace("_", " ");
      str = str.Replace("-", " ");
      str = str.ToLower();

      str = myTI.ToTitleCase(str);

      return str;
    }

    public static string MakeUrlFriendly(string text)
    {
      return Regex.Replace(text, @"[^A-Za-z0-9_\.~]+", "-");
    }



    public static string FormatTitleSimple(string str)
    {
      TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

      str = str.Replace("_", " ");

      return str;
    }


    public static string GetDigits(string str)
    {
      return new String(str.Where(Char.IsDigit).ToArray());
    }


    public static bool IsEmail(string email)
    {
      try
      {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == email;
      }
      catch
      {
        return false;
      }
    }


    public static List<int> HomePageColumns(int number)
    {
      List<int> columns = new List<int>();
      int remainder = number;
      int current = 3;

      if (number > 0)
      {
        if (remainder % 3 != 0)
        {
          current = number > 1 ? 2 : 1;
        }

        columns.Add(current);

        if (number - current > 0)
        {
          columns.AddRange(HomePageColumns(number - current));
        }
      }

      return columns;
    }


    public static string[] NameFromEmail(string email)
    {
      string[] fullName = null!;

      if (IsEmail(email))
      {
        fullName = SplitOnFirst(email, new string[] { ".", "-", "_" });

        if (fullName == null)
        {
          string first = FormatTitle(email.Substring(0, email.IndexOf("@")));
          string last = "";

          fullName = new string[] { first, last };
        }
      }
      else
      {
        fullName = new string[] { "", "" };
      }

      return fullName;
    }


    public static string RemoveDoubleSpaces(string str)
    {
      RegexOptions options = RegexOptions.None;
      Regex regex = new Regex("[ ]{2,}", options);
      str = regex.Replace(str, " ");

      return str;
    }


    public static string ReplaceSmartQuotes(string str)
    {
      string replaced = str.Replace("‘", "'");
      replaced = replaced.Replace("’", "'");
      replaced = replaced.Replace("“", "\"");
      replaced = replaced.Replace("”", "\"");

      return replaced;
    }


    private static string[] SplitOnFirst(string str, string[] splitters)
    {
      string[] fullName = null!;

      foreach (var splitter in splitters)
      {
        fullName = SplitOnSeparator(str, splitter);

        if (fullName != null)
        {
          break;
        }
      }

      return fullName ?? Array.Empty<string>();
    }


    private static string[] SplitOnSeparator(string str, string separator)
    {
      string[] fullName = null!;

      int pos = str.IndexOf(separator);
      int pos_a = str.IndexOf("@");

      if (pos != -1 && pos < pos_a)
      {
        string first = FormatTitle(str.Substring(0, pos));
        string last = FormatTitle(str.Substring(pos + 1, pos_a - pos - 1));

        fullName = new string[] { first, last };
      }

      return fullName ?? Array.Empty<string>();
    }


  }
}
