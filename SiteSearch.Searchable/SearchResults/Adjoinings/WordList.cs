using SiteSearch.Searchable.Stemming;

namespace SiteSearch.Searchable.SearchResults.Adjoinings
{
  public class WordList : List<string>
  {

    public new bool Contains(string word) => this.Select(s => s.ToLower()).Contains(word.ToLower());


    /// <summary>
    /// Return the index of every match of list1 in list2 taking into account stemming
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    public static IEnumerable<int> GetIndexes(List<string> list1, List<string> list2)
    {
      // Create a list to store the indexes
      var indexes = new List<int>();

      // Loop through each element in list1
      foreach (var element in list1)
      {
        // Loop through each element in list2
        for (int i = 0; i < list2.Count(); i++)
        {
          // If the element in list1 matches the element in list2
          if (element.ToLower() == list2[i].ToLower())
          {
            // Add the index to the list
            indexes.Add(i);
          }
        }
      }

      // Return the list of indexes
      return indexes;
    }


    public string Highlight(string word) => Highlight(word);


    public string HighlightStemmed(IEnumerable<string> wordsToHighlight, IStemmer stemmer)
    {
      string highlighted = "";

      var wordsToHighlightStemmed = wordsToHighlight.Stem(stemmer).ToList();
      var originalStemmed = this.Stem(stemmer).ToList();

      var matches = GetIndexes(wordsToHighlightStemmed, originalStemmed).ToList();

      for (int i = 0; i < this.Count(); i++)
      {
        if (!string.IsNullOrWhiteSpace(highlighted))
        {
          highlighted += " ";
        }

        if(matches.Contains(i))
        {
          highlighted += this.ElementAt(i).Highlight();
        }
        else
        {
          highlighted += this.ElementAt(i);
        }
      }

      return highlighted;
    }


    public List<string> ToList() => this.ToList();

    public new string ToString() => string.Join(" ", this);


  }
}
