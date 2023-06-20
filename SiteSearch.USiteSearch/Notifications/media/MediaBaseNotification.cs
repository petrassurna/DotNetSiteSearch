using Microsoft.AspNetCore.Http;
using Searchable;
using SiteSearch.USiteSearch.Notifications.Content;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Core.Services;
using SiteSearch.PDFToText;
using SiteSearch.Searchable.SearchableContent.Factories;

namespace SiteSearch.USiteSearch.Notifications.Media
{
  public class MediaBaseNotification : BaseNotification
  {


    public MediaBaseNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper, ISearchProvider indexer,
      IHttpClientFactory clientFactory)
      : base(contextAccessor, umbracoHelper, indexer, clientFactory)
    { }


  }
}


