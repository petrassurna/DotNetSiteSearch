using Searchable.SearchableContent;
using Searchable.SearchableContent.Factories;
using System.Net;
using TextFormatting;

namespace Searchable.WebPages.Factories
{
  public class UriToWebPage
  {
    public const string ROOT_PAGE = "aaarootaaa";

    private static async Task<string> GetHtml(Uri uri)
    {
      string html = "";
      HttpClient Client = new HttpClient();
      var response = await Client.GetAsync(uri.ToString());

      using (HttpContent httpContent = response.Content)
      {
        if(response.StatusCode == HttpStatusCode.OK)
        {
          html = httpContent.ReadAsStringAsync().Result;
        }
        else
        {
          html = "-1";
        }
      }

      return html;
    }


    public static async Task<Content> GetWebPage(int id, Uri uri)
    {
      string html = await GetHtml(uri);
      Content content = new Content();
      
      if(html != "-1")
      {
        content = GetWebPage(id, uri, html);
      }

      return content;
    }


    public static Content GetWebPage(int id, Uri uri, string html)
    {
      string title = WebPageDataToTitle.ToTitle(uri, html);
      string url = UriToWebPage.PathToUniqueSearchableId(uri);

      html = TagStripper.RemoveTagsWithAttribute(html, "usitesearch-exclude", "true");
      html = WebPageExtracter.ExtractTextFromWithinBody(html);

      return ContentFactory.WebPage(id.ToString(), uri.LocalPath, title, html);
    }


    public static string PathToUniqueSearchableId(Uri uri)
    {
      string path = uri.LocalPath;

      path = path.Replace("/", "");

      if (string.IsNullOrWhiteSpace(path))
      {
        path = ROOT_PAGE;
      }

      return path;
    }

  }
}
