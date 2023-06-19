using Microsoft.AspNetCore.Http;
using Searchable;
using SiteSearch.USiteSearch.Notifications.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common;
using static Umbraco.Cms.Core.Constants.Conventions;
using Umbraco.Cms.Web.Common.UmbracoContext;
using Umbraco.Cms.Core.Services;
using System.Security.Policy;
using SiteSearch.PDFToText;

namespace SiteSearch.USiteSearch.Notifications.media
{
  public class SavedNotification : BaseNotification, INotificationHandler<MediaSavedNotification>
  {
    IMediaService _mediaService;

    public SavedNotification(IHttpContextAccessor contextAccessor,
      UmbracoHelper umbracoHelper, ISearchProvider indexer,
      IHttpClientFactory clientFactory, IMediaService mediaService)
      : base(contextAccessor, umbracoHelper, indexer, clientFactory)
    {
      _mediaService = mediaService;
    }

    public void Handle(MediaSavedNotification notification)
    {
      foreach (var content in notification.SavedEntities)
      {
        AddMediaIfNotBlocked(content);
      }
    }

    private void AddMediaIfNotBlocked(IMedia content)
    {
      string umbracoFile = "umbracoFile";
      var media = _mediaService.GetById(content.Id);

      if (media.Properties.Any(p => p.Alias == umbracoFile))
      {
        string path = media.Properties.Single(p => p.Alias == umbracoFile).GetValue().ToString();
        string mediaUrl = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}{path}";

        if (Path.GetExtension(mediaUrl).ToLower() == ".pdf")
        {
          using (HttpClient httpClient = new HttpClient())
          {
            HttpResponseMessage response = httpClient.GetAsync(mediaUrl).Result;
            if (response.IsSuccessStatusCode)
            {
              Stream pdfStream =  response.Content.ReadAsStream();
              string text = PDFTextExtractor.PDFToText(pdfStream);
            }
            else
            {
              throw new Exception("Failed to retrieve PDF: " + response.StatusCode);
            }
          }
        }
      }
    }
  }
}


