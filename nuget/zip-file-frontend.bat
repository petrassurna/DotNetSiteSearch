set version=%1
echo %version%
echo on
del "C:\Program Files\7-Zip\7z.exe" a "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip"
"C:\Program Files\7-Zip\7z.exe" a "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\Umbraco10\wwwroot\" 
"C:\Program Files\7-Zip\7z.exe" a "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\Umbraco10\Views\"
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\creativesuite
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\simplesite
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\dotnetsearch
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\sample-site-U11
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\media
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\Partials\blocklist
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\Partials\grid
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\MacroPartials
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\_ViewImports.cshtml
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\CreativeStudioMaster.cshtml
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\CreativeStudioPage.cshtml
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\SimpleSiteMaster.cshtml
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\SimpleSitePage.cshtml
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\U11SampleSiteMaster.cshtml
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\U11SampleSitePage.cshtml

"C:\Yart\Clients and Jobs\DotNetSiteSearch\source\usitesearch\PostBuildActions\bin\Debug\net6.0\PostBuildActions.exe" %version%