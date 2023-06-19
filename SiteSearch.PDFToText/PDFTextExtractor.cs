using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;
using System.Text.RegularExpressions;

namespace SiteSearch.PDFToText
{
  public class PDFTextExtractor
  {

    public static string PDFToText(Stream pdf)
    {
      string combinedString = String.Empty;

      using (PdfDocument document = PdfDocument.Open(pdf))
      {
        int count = 0;
        int pages = document.GetPages().Count();

        foreach (Page page in document.GetPages())
        {

          IEnumerable<Word> words = page.GetWords();
          combinedString += " " + string.Join(" ", words);

          //combinedString = Regex.Replace(combinedString, @"\s+", " "); //remove multiple spaces
          //combinedString = Regex.Replace(combinedString, @"[^\u0000-\u007F]+", string.Empty); //remove non ascii
          count++;

          if(count % 50 == 0)
          { 
          }
        }

        return combinedString;
      }
    }


  }
}