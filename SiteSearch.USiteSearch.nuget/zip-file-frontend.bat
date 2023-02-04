set version=%1

set solutionDir=%2
set solutionDir=%solutionDir:"=%
echo %solutionDir%
echo on

del "%solutionDir%SiteSearch.USiteSearch.nuget\nuget\USiteSearchProjectFiles-%version%.zip"
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" a     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\Umbraco10\wwwroot\" 
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" a     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" "C:\Yart\Clients and Jobs\dotnetsitesearch\source\USiteSearch\Umbraco10\Views\"
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\creativesuite
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\simplesite
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\dotnetsearch
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\sample-site-U11
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\media
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\Partials\blocklist
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\Partials\grid
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\MacroPartials
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\_ViewImports.cshtml
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\CreativeStudioMaster.cshtml
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\CreativeStudioPage.cshtml
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\SimpleSiteMaster.cshtml
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\SimpleSitePage.cshtml
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\U11SampleSiteMaster.cshtml
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\U11SampleSitePage.cshtml

"%solutionDir%SiteSearch.USiteSearch.PostBuildActions\bin\Debug\net6.0\PostBuildActions.exe" %version%