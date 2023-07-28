using Microsoft.Extensions.DependencyInjection;
using Searchable;
using SiteSearch.Searchable.LuceneSearch;
using SiteSearch.USiteSearch.Notifications.Content;
using SiteSearch.USiteSearch.Notifications.Media;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace SiteSearch.USiteSearch.Notifications
{
  public static class UmbracoStartupExtensions
  {
    public static IUmbracoBuilder AddUSiteSearch(this IUmbracoBuilder builder, IServiceCollection services, string path, int wordsEachSide)
    {
      builder.AddNotificationHandler<ContentPublishedNotification, PublishedNotification>();
      builder.AddNotificationHandler<ContentUnpublishedNotification, UnpublishedNotification>();
      builder.AddNotificationHandler<ContentMovedToRecycleBinNotification, RecycleBinNotification>();
      builder.AddNotificationHandler<ContentMovedNotification, Content.MovedNotification>();

      //builder.AddNotificationHandler<MediaSavedNotification, SavedNotification>();
      //builder.AddNotificationHandler<MediaMovedToRecycleBinNotification, MediaRecycleBinNotification>();
      //builder.AddNotificationHandler<MediaMovedNotification, Media.MovedNotification>();

      services.AddHttpClient();
      services.AddSingleton(typeof(ISearchProvider), new LuceneProvider(path, wordsEachSide));

      return builder;
    }
  }
}
