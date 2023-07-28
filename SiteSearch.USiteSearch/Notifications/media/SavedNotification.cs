using Microsoft.AspNetCore.Http;
using Searchable;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Core.Services;

namespace SiteSearch.USiteSearch.Notifications.Media
{
  public class SavedNotification : MediaBaseNotification, INotificationHandler<MediaSavedNotification>
  {
    IMediaService _mediaService;

    public SavedNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper, ISearchProvider indexer,
      IHttpClientFactory clientFactory, IMediaService mediaService)
      : base(contextAccessor, umbracoHelper, indexer, clientFactory, mediaService)
    {
      _mediaService = mediaService;
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


