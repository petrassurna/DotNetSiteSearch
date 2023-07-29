set version=%1

set solutionDir=%2
set solutionDir=%solutionDir:"=%
echo %solutionDir%
echo on

del "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip"


"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" a     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" "%solutionDir%\Umbraco11SampleSite\wwwroot\" 
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" a     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" "%solutionDir%\Umbraco11SampleSite\Views\"


"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\MacroPartials
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\Partials\blockgrid
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\Partials\blocklist
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\Partials\grid

"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\_ViewImports.cshtml
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\master.cshtml
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" Views\page.cshtml

"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\sample-site
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\media
"%solutionDir%\SiteSearch.USiteSearch.nuget\7z.exe" d     "%solutionDir%SiteSearch.USiteSearch.nuget\USiteSearchProjectFiles-%version%.zip" wwwroot\favicon.ico

"%solutionDir%SiteSearch.USiteSearch.PostBuildActions\bin\release\net6.0\SiteSearch.USiteSearch.PostBuildActions.exe" %version%