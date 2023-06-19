using Microsoft.AspNetCore.Http;
using Searchable;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common;


namespace SiteSearch.USiteSearch.Notifications.Content
{
    public class RecycleBinNotification : BaseNotification, INotificationHandler<ContentMovedToRecycleBinNotification>
    {

        public RecycleBinNotification(IHttpContextAccessor contextAccessor,
          UmbracoHelper umbracoHelper, ISearchProvider indexer, IHttpClientFactory clientFactory)
          : base(contextAccessor, umbracoHelper, indexer, clientFactory)
        {
        }

        public void Handle(ContentMovedToRecycleBinNotification notification)
        {
            foreach (var item in notification.MoveInfoCollection)
            {
                Delete(item.Entity.Id);
            }
        }
    }
}
