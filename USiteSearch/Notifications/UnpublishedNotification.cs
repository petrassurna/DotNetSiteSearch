using Microsoft.AspNetCore.Http;
using Searchable;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common;

namespace USiteSearch.Notifications
{
  public class UnpublishedNotification : BaseNotification, INotificationHandler<ContentUnpublishedNotification>
  {

    public UnpublishedNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper, ISearchProvider indexer, IHttpClientFactory clientFactory)
      : base(contextAccessor, umbracoHelper, indexer, clientFactory)
    {
    }

    public void Handle(ContentUnpublishedNotification notification)
    {
      IContent content = notification.UnpublishedEntities.First();
      Delete(content.Id);
    }

  }
}