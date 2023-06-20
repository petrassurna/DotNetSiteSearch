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
  public class MovedNotification : BaseNotification, INotificationHandler<MediaMovedNotification>
  {
    IMediaService _mediaService;

    public MovedNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper, ISearchProvider indexer,
      IHttpClientFactory clientFactory, IMediaService mediaService)
      : base(contextAccessor, umbracoHelper, indexer, clientFactory)
    {
      _mediaService = mediaService;
    }

    public void Handle(MediaMovedNotification notification)
    {
      foreach (var content in notification.MoveInfoCollection)
      {

      }
    }
  }
}


