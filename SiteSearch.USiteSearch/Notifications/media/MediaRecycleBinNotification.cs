using Microsoft.AspNetCore.Http;
using Searchable;
using SiteSearch.USiteSearch.Notifications.Content;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Core.Services;

namespace SiteSearch.USiteSearch.Notifications.Media
{
  public class MediaRecycleBinNotification : BaseNotification, INotificationHandler<MediaMovedToRecycleBinNotification>
  {
    public MediaRecycleBinNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper, ISearchProvider indexer,
      IHttpClientFactory clientFactory, IMediaService mediaService)
      : base(contextAccessor, umbracoHelper, indexer, clientFactory)
    { }

    public void Handle(MediaMovedToRecycleBinNotification notification)
    {
      foreach (var item in notification.MoveInfoCollection)
      {
        Delete(item.Entity.Id);
      }
    }
  }
}


