## Warning

This repo is coming soon. There is no functional code as yet. Peace.

## Why Create USiteSearch?

I build websites using the Umbraco content management system, and one feature that is often necessary 
is a site search. While you can use Examine for this purpose, my sites often require a Google-like search 
rather than a search specific to the site's content. Additionally, I need a user-friendly interface for 
the search and syntax highlighting to make the results more readable. 

In the past, I have copied and pasted my site search code between projects, but this has not been 
efficient and has resulted in bugs and other issues. To improve the quality of my code and make it easier to 
use, I have decided to add unit and integration tests and turn it into a system that can be easily installed in just 15 minutes. 
This system is called USiteSearch.

Earlier versions of the code used in USiteSearch can be seen in action on the [Energy Council](https://www.energycouncil.com.au) 
and [Mysterious Universe](https://mysteriousuniverse.org/) (20,000 blog posts), 
which have a total of 20,000 blog posts.

If you need help on this system or find bugs I respond to queries fairly quickly at petras @ yart dot com dot au

## Installation

Let's get **USiteSearch** working in a sample Umbraco website so you can see exactly how it works. Installing on the sample
site will teach you how to install it in your own site. Set up should take no longer than 15 minutes.

Note: This help can also be viewed as a [YouTube tutorial](https://www.youtube.com) if you prefer seeing a video. The video
should solve most installation problems.

### Installation steps

1. Clone this basic [Umbraco 10 website](https://github.com/petrassurna/umbraco10samplesite.git). 
The website comes with an embedded database so it should run without further modifications. 
The back office login for the website is:

	Username: admin@admin.com  
	Password: admin12345

	Alternatively clone the same website in [Umbraco 11](https://github.com/petrassurna/umbraco11samplesite.git) if you are using Umbraco 11.  \
	Check the website runs and you can log in to the backoffice.

2. Install the nuget package USiteSearch via the NuGet package manager:

	```

	dotnet add package USiteSearch --version 0.65-alpha
	or update-package USiteSearch -0.65-alpha    (if replacing an older version)
	```

3. Install https://github.com/petrassurna/usitesearch/raw/main/nuget/USiteSearchProjectFiles-0.65-alpha.zip  
   Unzip the files in the project folder.

	This installs some css, images, javascript and a partial view


	![Installed files](https://raw.githubusercontent.com/petrassurna/usitesearch/main/USiteSearch/images/setup-sample.jpg)

4. Run the project to view the sample website. Open startup.cs and uncomment these four using statements:

	```

	using Umbraco.Cms.Core.Notifications;
	using UmbracoExtensions;

	//uncomment the below
	using LuceneSearch;
	using Searchable;
	using Umbraco.Cms.Core.Notifications;
	using USiteSearch.Notifications;

	```

	and also the two lines in *ConfigureServices*:

	```

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddUmbraco(_env, _config)
          .AddBackOffice()
          .AddWebsite()
          .AddComposers()
          //.AddUSiteSearch() <- uncomment this
          .AddNotificationHandler<UmbracoApplicationStartingNotification, Bundling>()
          .Build();

      //uncomment the below
      services.AddSingleton(typeof(ISearchProvider), new LuceneProvider("app_data/USiteSearch", 10));
    }
	```

	Note this line:

	```

      services.AddSingleton(typeof(ISearchProvider), new LuceneProvider("app_data/USiteSearch", 10));

	```

	This configures the USiteSearch to a LuceneProvider search provider

	*app_data/USiteSearch* - is the relative path where Lucene will store data.

	*10* - When displaying search results, the number of words to display each side of a word match.


5. Next we need to enable the css and javascript used by USiteSearch. Uncomment these lines in *views/master.cshtml*:

	```

    <link rel="stylesheet" href="usitesearch-css-bundle" />
    <script src="usitesearch-js-bundle"></script>

	```

	This references:
	
	*~/USiteSearch/css/usitesearch.min.css* and   
	*~/USiteSearch/scripts/usitesearch.js*
			
			
	which are included by bundling in *Bundling.cs*. Read more about [Umbraco Bundling](https://docs.umbraco.com/umbraco-cms/fundamentals/design/stylesheets-javascript#bundling-and-minification-for-javascript-and-css)

6.  Uncomment these lines in *Views/master.cshtml* so the html for USiteSearch is engaged:

	```

    @*
    <section id="dns-search-bar" class="dns-search-bar" usitesearch-exclude="true" zzz>
    <div id="dns-search-fields" class="dns-search-fields">
    <input id="dns-search-input" type="text" placeholder="search">
    <img id="dns-search-loader" src="/usitesearch/images/spinner.gif" width="50" height="50">
    <a href="#" id="dns-search-close" title="Close"><img src="/usitesearch/images/icon-close.svg"></a>
    </div>

    <div id="dns-search-results" class="dns-search-results">
    </div>
    </section>
    <div id="dns-search-overlay"></div>
    *@

	```

7. Next we need to index the site, to do this, log into the backoffice and save and publish each of the pages.  
   This will create the lucene index under the app_data folder as specified in *startup.cs*:

	![app_data folder](https://raw.githubusercontent.com/petrassurna/usitesearch/main/USiteSearch/images/app-data.jpg)

	This folder can be deleted to reset the search.

8. In order to engage the search, we need to give a front end element the attribute *id="site-search"*  
This has been done for you in *master.cshtml*

	```

	<li>
		<a href="#" id="site-search" title="Search the site">Site search</a>
	</li>

	```

	Click on the element and you should see the search bar, enter the term *animals*. After typing a few characters you should 
	see these results:

	![app_data folder](https://raw.githubusercontent.com/petrassurna/usitesearch/main/USiteSearch/images/search-animals.jpg)

	Refine the search, search for *animals lions*. Note the results reduce and each search term is highlighted with 10 words either
	side of the word match. This is the variable we set earlier in *startup.cs*.

9. Search for the term *play close* and notice it matches all pages: 

	![Search for pay close](https://raw.githubusercontent.com/petrassurna/usitesearch/main/USiteSearch/images/search-pay-close.jpg)

	This is because this text is in the margin. In order to remove this text from the search, add this tag to html elements you want excluded from the site search:

	![Search exclusion](https://raw.githubusercontent.com/petrassurna/usitesearch/main/USiteSearch/images/search-exclude.jpg)

	Typically you would exclude the navigation, footer and common margins. You will need to resave each page to reindex these pages.

10. If you want to remove whole pages from the site search, add the property *blockFromSearch* to document types:

	![Search exclusion](https://raw.githubusercontent.com/petrassurna/usitesearch/main/USiteSearch/images/block-from-search.jpg)

	This is already present in the sample project and when you check it on, pages will be removed from the search.