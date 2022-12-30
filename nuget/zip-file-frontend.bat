set version=%1
echo %version%
echo on
"C:\Program Files\7-Zip\7z.exe" a "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\DotNetSiteSearch\Umbraco10\wwwroot\" 
"C:\Program Files\7-Zip\7z.exe" a "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\DotNetSiteSearch\Umbraco10\Views\"
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\creativesuite
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\simplesite
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\media
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\Partials\blocklist
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\Partials\grid
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\MacroPartials
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\_ViewImports.cshtml
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\CreativeStudioMaster.cshtml
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\CreativeStudioPage.cshtml
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\SimpleSiteMaster.cshtml
"C:\Program Files\7-Zip\7z.exe" d "C:\Yart\Clients and Jobs\DotNetSiteSearch\source\dotnetsitesearch\nuget\USiteSearchProjectFiles-%version%.zip" Views\SimpleSitePage.cshtml
