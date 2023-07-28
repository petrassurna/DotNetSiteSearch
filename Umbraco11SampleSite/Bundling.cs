using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.WebAssets;
using Umbraco.Cms.Core;

namespace UmbracoExtensions
{
	public class Bundling : INotificationHandler<UmbracoApplicationStartingNotification>
	{
		private readonly IRuntimeMinifier _runtimeMinifier;
		private readonly IRuntimeState _runtimeState;

		public Bundling(IRuntimeMinifier runtimeMinifier, IRuntimeState runtimeState)
		{
			_runtimeMinifier = runtimeMinifier;
			_runtimeState = runtimeState;
		}

		public void Handle(UmbracoApplicationStartingNotification notification)
		{
			if (_runtimeState.Level == RuntimeLevel.Run)
			{
				_runtimeMinifier.CreateCssBundle("usitesearch-css-bundle",
						BundlingOptions.NotOptimizedAndComposite,
						new[] { "~/USiteSearch/css/usitesearch.min.css" });

				_runtimeMinifier.CreateJsBundle("usitesearch-js-bundle",
						BundlingOptions.NotOptimizedAndComposite,
						new[] { "~/USiteSearch/scripts/usitesearch.js" });

			}
		}
	}
}
