using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace USiteSearch.Notifications
{
  public static class UmbracoStartupExtensions
  {

    public static IUmbracoBuilder AddUSiteSearch(this IUmbracoBuilder builder)
    {
      builder.AddNotificationHandler<ContentPublishedNotification, PublishedNotification>();
      builder.AddNotificationHandler<ContentUnpublishedNotification, UnpublishedNotification>();
      builder.AddNotificationHandler<ContentMovedToRecycleBinNotification, RecycleBinNotification>();
      builder.AddNotificationHandler<ContentMovedNotification, MovedNotification>();

      return builder;
    }


  }
}

