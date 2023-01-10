using LuceneSearch;
using Microsoft.Extensions.DependencyInjection;
using Searchable;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;

namespace USiteSearch.Notifications
{
  public static class UmbracoStartupExtensions
  {

    public static IUmbracoBuilder AddUSiteSearch(this IUmbracoBuilder builder, IServiceCollection services, string path, int wordsEachSide)
    {
      builder.AddNotificationHandler<ContentPublishedNotification, PublishedNotification>();
      builder.AddNotificationHandler<ContentUnpublishedNotification, UnpublishedNotification>();
      builder.AddNotificationHandler<ContentMovedToRecycleBinNotification, RecycleBinNotification>();
      builder.AddNotificationHandler<ContentMovedNotification, MovedNotification>();

      services.AddHttpClient();
      services.AddSingleton(typeof(ISearchProvider), new LuceneProvider(path, wordsEachSide));

      return builder;
    }


  }
}

