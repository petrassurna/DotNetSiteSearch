using Microsoft.AspNetCore.Http;
using Searchable;
using SiteSearch.USiteSearch.Notifications.Content;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Core.Services;
using SiteSearch.PDFToText;
using SiteSearch.Searchable.SearchableContent.Factories;

namespace SiteSearch.USiteSearch.Notifications.Media
{
  public class SavedNotification : MediaBaseNotification, INotificationHandler<MediaSavedNotification>
  {
    IMediaService _mediaService;

    public SavedNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper, ISearchProvider indexer,
      IHttpClientFactory clientFactory, IMediaService mediaService)
      : base(contextAccessor, umbracoHelper, indexer, clientFactory)
    {
      _mediaService = mediaService;
    }


    private void AddMediaIfNotBlocked(IMedia content)
    {
      string umbracoFile = "umbracoFile";
      var media = _mediaService.GetById(content.Id);

      if (media.Properties.Any(p => p.Alias == umbracoFile))
      {
        string path = media.Properties.Single(p => p.Alias == umbracoFile).GetValue().ToString();
        string mediaUrl = $"{_ContextAccessor.HttpContext.Request.Scheme}://{_ContextAccessor.HttpContext.Request.Host}{path}";
        string text = GetTextFromPDFFile(new Uri(mediaUrl), _ClientFactory);
        string title = content.Name;

        Searchable.SearchableContent.Content webPage = ContentFactory.WebPage(content.Id.ToString(), path, title, text);

        if (!webPage.IsEmpty())
        {
          _Provider.AddOrUpdate(webPage);
        }
      }
    }


    public static string GetTextFromPDFFile(Uri uri, IHttpClientFactory clientFactory)
    {
      string text = "";

      var request = new HttpRequestMessage(HttpMethod.Get, uri);
      var client = clientFactory.CreateClient();
      var response = client.Send(request);

      if (response.IsSuccessStatusCode)
      {
        using var responseStream = response.Content.ReadAsStream();
        Stream file = response.Content.ReadAsStream();
        text = PDFTextExtractor.PDFToText(file);
      }

      return text;
    }

    public void Handle(MediaSavedNotification notification)
    {
      foreach (var content in notification.SavedEntities)
      {
        AddMediaIfNotBlocked(content);
      }
    }

  }
}


