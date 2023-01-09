
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

string path = $"{folder}\\..\\..\\..\\..\\USiteSearch\\";
string template = $"{path}README-template.md";

Console.WriteLine(path);

string contents = File.ReadAllText(template);

File.WriteAllText($"{path}README.md", contents.Replace("VERSION", version));
