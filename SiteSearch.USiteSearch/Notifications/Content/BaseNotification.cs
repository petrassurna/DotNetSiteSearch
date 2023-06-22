using Microsoft.AspNetCore.Http;
using Searchable;
using SiteSearch.Searchable.SearchableContent.Factories;
using SiteSearch.Searchable.WebPages.Factories;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;

namespace SiteSearch.USiteSearch.Notifications.Content
{
  public class BaseNotification
  {
    protected readonly IHttpClientFactory _ClientFactory;
    protected readonly IHttpContextAccessor _ContextAccessor;
    protected readonly ISearchProvider _Provider;
    protected readonly UmbracoHelper _UmbracoHelper;

    public BaseNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper,
       ISearchProvider provider, IHttpClientFactory clientFactory)
    {
      _ClientFactory = clientFactory;
      _ContextAccessor = contextAccessor;
      _UmbracoHelper = umbracoHelper;
      _Provider = provider;

    }

    protected void AddPageIfNotBlocked(IContent content) => AddPageIfNotBlocked(_UmbracoHelper.Content(content.Id));


    public void AddPageIfNotBlocked(IPublishedContent content)
    {
      string url = GetUrl(content.Id);

      if (!PageBlocked(content))
      {
        Searchable.SearchableContent.Content webPage = UriToWebPage.GetWebPage(content.Id, new Uri(url), _ClientFactory).Result;

        if (!webPage.IsEmpty())
        {
          _Provider.AddOrUpdate(webPage);
        }
      }
      else
      {
        Delete(content.Id);
      }
    }


    protected void Delete(int contentId)
    {
      Searchable.SearchableContent.Content toUnpublish = ContentFactory.WebPage(contentId.ToString(), "", "", "");
      _Provider.Delete(toUnpublish);
    }

    protected string GetUrl(int contentId)
    {
      IPublishedContent publishedContent = _UmbracoHelper.Content(contentId);
      string url = $"{Url()}";

      string path = publishedContent.Url();

      if (path != "/")
      {
        url += path;
      }

      return url;
    }


    private bool PageBlocked(IPublishedContent content)
    {
      bool pageBlocked = false;

      if (content.HasProperty("blockFromSearch"))
      {
        var blockFromSearch = content.Value<string>("blockFromSearch");

        if (blockFromSearch == "True")
        {
          pageBlocked = true;
        }
      }

      return pageBlocked;
    }


    protected string Url()
    {
      return $"{_ContextAccessor.HttpContext.Request.Scheme}://{_ContextAccessor.HttpContext.Request.Host}";
    }

  }
}