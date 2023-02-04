namespace SiteSearch.Searchable.SearchResults.Factories
{

  public class WordsAfter
  {

    public static AdjoiningWords Get(string content, int index, int words)
    {
      if (index < 0 || index >= content.Length)
      {
        return new AdjoiningWords();
      }

      string [] arr = content.Substring(index).Split(' ', StringSplitOptions.RemoveEmptyEntries);

      IEnumerable<string> result = arr.Take(words);

      return new AdjoiningWords(result, false, arr.Count() > words);
    }


  }

}
