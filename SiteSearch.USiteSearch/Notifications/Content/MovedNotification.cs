using Microsoft.AspNetCore.Http;
using Searchable;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common;


namespace SiteSearch.USiteSearch.Notifications.Content
{
    public class MovedNotification : BaseNotification, INotificationHandler<ContentMovedNotification>
    {

        public MovedNotification(IHttpContextAccessor contextAccessor,
          UmbracoHelper umbracoHelper, ISearchProvider indexer, IHttpClientFactory clientFactory)
          : base(contextAccessor, umbracoHelper, indexer, clientFactory)
        {
        }

        public void Handle(ContentMovedNotification notification)
        {
            foreach (var item in notification.MoveInfoCollection)
            {
                IPublishedContent publishedContent = _umbracoHelper.Content(item.Entity.Id);
                AddPageIfNotBlocked(publishedContent);
            }
        }

    }
}
