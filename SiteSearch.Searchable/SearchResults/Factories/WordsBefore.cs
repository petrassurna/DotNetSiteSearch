namespace SiteSearch.Searchable.SearchResults.Factories
{

  public class WordsBefore
  {

    public static AdjoiningWords Get(string content, int index, int words)
    {
      if (index < 0 || index >= content.Length)
      {
        return new AdjoiningWords();
      }

      string [] arr = content.Substring(0, index).Split(' ', StringSplitOptions.RemoveEmptyEntries);

      IEnumerable<string> result = arr.Skip(Math.Max(0, arr.Count() - words));

      return new AdjoiningWords(result, arr.Count() > words, false);
    }


  }

}
