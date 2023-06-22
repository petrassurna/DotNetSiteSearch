using Microsoft.AspNetCore.Http;
using Searchable;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Core.Services;

namespace SiteSearch.USiteSearch.Notifications.Media
{
  public class MovedNotification : MediaBaseNotification, INotificationHandler<MediaMovedNotification>
  {
    IMediaService _mediaService;

    public MovedNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper, ISearchProvider indexer,
      IHttpClientFactory clientFactory, IMediaService mediaService)
      : base(contextAccessor, umbracoHelper, indexer, clientFactory, mediaService)
    {
      _mediaService = mediaService;
    }

    public void Handle(MediaMovedNotification notification)
    {
      foreach (var content in notification.MoveInfoCollection)
      {
        AddMediaIfNotBlocked(content.Entity);
      }
    }
  }
}


