
using System.Runtime.Versioning;
using System.Security.Cryptography;

string version = "999.999";

int size = Environment.GetCommandLineArgs().Length;
var arguments = Environment.GetCommandLineArgs();

if (Environment.GetCommandLineArgs().Length > 1)
{
  version = Environment.GetCommandLineArgs()[1];
}


string folder = Path.GetDirectoryName(Environment.ProcessPath) ?? "";

string pathToUSiteSearch = $"{folder}\\..\\..\\..\\..\\SiteSearch.USiteSearch\\";
string pathToSolution = $"{folder}\\..\\..\\..\\..\\";
string template = $"{pathToUSiteSearch}README-template.md";

Console.WriteLine(pathToUSiteSearch);

string contents = File.ReadAllText(template);

File.WriteAllText($"{pathToUSiteSearch}README.md", contents.Replace("VERSION", version));

File.Copy($"{pathToUSiteSearch}README.md", $"{pathToSolution}README.md", true);