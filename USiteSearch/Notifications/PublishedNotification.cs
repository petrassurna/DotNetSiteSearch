using Microsoft.AspNetCore.Http;
using Searchable;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common;


namespace USiteSearch.Notifications
{
  public class PublishedNotification : BaseNotification, INotificationHandler<ContentPublishedNotification>
  {

    public PublishedNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper, ISearchProvider indexer)
      : base(contextAccessor, umbracoHelper, indexer)
    {
    }

    public void Handle(ContentPublishedNotification notification)
    {
      IContent content = notification.PublishedEntities.First();
      AddPageIfNotBlocked(content);
    }

  }
}
