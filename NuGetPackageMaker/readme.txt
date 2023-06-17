This project exists to utilise project postbuild events so the USiteSearch NuGet package can be built.

Postbuild events are used to create the USiteSearch package.
Originally they were in SiteSearch.USiteSearch.Umbraco10Sample but this stops non-Windows users from running the source unless
they are on Windows.
Ideally what this project does needs to be part of a build, but that work needs to be done.

Don't run this solution unless you want to make a package, instead run SiteSearch.sln

