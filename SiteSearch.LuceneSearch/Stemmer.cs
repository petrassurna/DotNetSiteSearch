using Lucene.Net.Tartarus.Snowball.Ext;
using PluralizeService.Core;
using SiteSearch.Searchable.Stemming;

namespace SiteSearch.LuceneSearch
{
  public class Stemmer : IStemmer
  {

    public bool IsSingular(string word)
      => word.ToLower() == PluralizationProvider.Singularize(word.ToLower());

    public string Stem(string word)
    {
      if (!string.IsNullOrWhiteSpace(word) && !IsSingular(word))
      {
        word = PluralizationProvider.Singularize(word);
      }
      return word;
    }


    /// <summary>
    /// problem, this turns animal into anima, use above
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public string StemViaPorterStemmer(string word)
    {
      if (!IsSingular(word))
      {
        PorterStemmer stemmer = new PorterStemmer();
        stemmer.SetCurrent(word);
        stemmer.Stem();

        return stemmer.Current;
      }
      else
      {
        return word;
      }
    }

  }
}
