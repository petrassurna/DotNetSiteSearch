using Microsoft.AspNetCore.Http;
using Searchable;
using Searchable.WebPages.Factories;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common;


namespace USiteSearch.Notifications
{
  public class MovedNotification : BaseNotification, INotificationHandler<ContentMovedNotification>
  {

    public MovedNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper, ISearchProvider indexer, IHttpClientFactory clientFactory)
      : base(contextAccessor, umbracoHelper, indexer,  clientFactory)
    {
    }

    public void Handle(ContentMovedNotification notification)
    {
      int id = notification.MoveInfoCollection.First().Entity.Id;

      IPublishedContent publishedContent = _umbracoHelper.Content(id);
      AddPageIfNotBlocked(publishedContent);
    }
  }
}
