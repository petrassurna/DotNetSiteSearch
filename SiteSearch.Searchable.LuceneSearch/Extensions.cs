using Lucene.Net.Analysis.En;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis.TokenAttributes;


namespace SiteSearch.Searchable.LuceneSearch
{
  public static class Extensions
  {

    public static string Pluralize1(this string word)
    {

      /*
      // Create a tokenizer to tokenize the input text
      var tokenizer = new StandardTokenizer(LuceneVersion.LUCENE_48, new StringReader(word));

      // Create a PorterStemFilter to apply the PorterStemmer to the tokens
      var stemmer = new PorterStemFilter(tokenizer);

      // Get the first (and only) token
      var token = stemmer.IncrementToken();

      var termAttribute = stemmer.GetAttribute<ITermAttribute>();
      var singularizedToken = termAttribute.Term;

      return singularizedToken;
      */

      return "";

    }

  }
}
