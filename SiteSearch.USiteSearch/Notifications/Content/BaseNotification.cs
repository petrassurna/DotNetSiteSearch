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
        private readonly IHttpClientFactory _clientFactory;
        protected readonly IHttpContextAccessor _contextAccessor;
        protected readonly UmbracoHelper _umbracoHelper;
        protected readonly ISearchProvider _Provider;

        public BaseNotification(IHttpContextAccessor contextAccessor,
          UmbracoHelper umbracoHelper,
           ISearchProvider provider, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _contextAccessor = contextAccessor;
            _umbracoHelper = umbracoHelper;
            _Provider = provider;

        }


        protected void AddPageIfNotBlocked(IContent content) => AddPageIfNotBlocked(_umbracoHelper.Content(content.Id));


        public void AddPageIfNotBlocked(IPublishedContent content)
        {
            string url = GetUrl(content.Id);

            if (!PageBlocked(content))
            {
                Searchable.SearchableContent.Content webPage = UriToWebPage.GetWebPage(content.Id, new Uri(url), _clientFactory).Result;

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
            IPublishedContent publishedContent = _umbracoHelper.Content(contentId);
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
            return $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";
        }

    }
}