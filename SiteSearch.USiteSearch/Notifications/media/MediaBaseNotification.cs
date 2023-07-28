using Microsoft.AspNetCore.Http;
using Searchable;
using SiteSearch.USiteSearch.Notifications.Content;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Core.Services;
using SiteSearch.Searchable.SearchableContent.Factories;

namespace SiteSearch.USiteSearch.Notifications.Media
{
  public class MediaBaseNotification : BaseNotification
  {
    IMediaService _mediaService;

    public MediaBaseNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper, ISearchProvider indexer,
      IHttpClientFactory clientFactory, IMediaService mediaService)
      : base(contextAccessor, umbracoHelper, indexer, clientFactory)
    {
      _mediaService = mediaService;
    }

    protected void AddMediaIfNotBlocked(IMedia content)
    {
      string umbracoFile = "umbracoFile";

      if (content.ContentType.Name == "File")
      {
        var media = _mediaService.GetById(content.Id);

        if (media.Properties.Any(p => p.Alias == umbracoFile))
        {
          string path = media.Properties.Single(p => p.Alias == umbracoFile).GetValue().ToString();
          string mediaUrl = $"{_ContextAccessor.HttpContext.Request.Scheme}://{_ContextAccessor.HttpContext.Request.Host}{path}";
          string text = GetTextFromPDFFile(new Uri(mediaUrl), _ClientFactory);
          string title = content.Name;

          Searchable.SearchableContent.Content webPage = ContentFactory.WebPage(content.Id.ToString(), path, title, text);
          bool mediaBlocked = MediaBlocked(content);

          if (!webPage.IsEmpty() && !MediaBlocked(content))
          {
            _Provider.AddOrUpdate(webPage);
          }
          else if(mediaBlocked)
          {
            _Provider.Delete(webPage);
          }
        }
      }
    }

    protected static string GetTextFromPDFFile(Uri uri, IHttpClientFactory clientFactory)
    {
      string text = "";

      var request = new HttpRequestMessage(HttpMethod.Get, uri);
      var client = clientFactory.CreateClient();
      var response = client.Send(request);

      if (response.IsSuccessStatusCode)
      {
        using var responseStream = response.Content.ReadAsStream();
        Stream file = response.Content.ReadAsStream();
        //?text = PDFTextExtractor.PDFToText(file);
      }

      return text;
    }

    private bool MediaBlocked(IMedia content)
    {
      bool pageBlocked = false;

      if (content.HasProperty("blockFromSearch"))
      {
        var blockFromSearch = content.GetValue<string>("blockFromSearch");

        if (blockFromSearch == "1")
        {
          pageBlocked = true;
        }
      }

      return pageBlocked;
    }


  }
}


