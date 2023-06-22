using SiteSearch.TextFormatting;
using SiteSearch.Searchable.SearchableContent;
using SiteSearch.Searchable.SearchableContent.Factories;

namespace SiteSearch.Searchable.WebPages.Factories
{
  public class UriToWebPage
  {
    public const string ROOT_PAGE = "RootPagePathString";


    private static async Task<string> GetHtml(Uri uri, IHttpClientFactory clientFactory)
    {
      string html = string.Empty;

      var request = new HttpRequestMessage(HttpMethod.Get, uri);
      var client = clientFactory.CreateClient();
      var response = await client.SendAsync(request);

      if (response.IsSuccessStatusCode)
      {
        using var responseStream = await response.Content.ReadAsStreamAsync();
        var responseValue = string.Empty;

        Task task = response.Content.ReadAsStreamAsync().ContinueWith(t =>
        {
          var stream = t.Result;
          using (var reader = new StreamReader(stream))
          {
            html = reader.ReadToEnd();
          }
        });

        task.Wait();
      }

      return html;
    }


    public static async Task<Content> GetWebPage(int id, Uri uri, IHttpClientFactory clientFactory)
    {
      string html = await GetHtml(uri, clientFactory);
      Content content = new Content();

      if (html != "-1")
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
