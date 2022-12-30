using Searchable.SearchableContent;

namespace Searchable.SearchableContent.Factories
{
  public class ContentFactory
  {

    public const string ID = "Id";
    public const string URL = "Url";
    public const string TITLE = "Title";
    public const string TEXT = "Text";

    public static Content WebPage(string id, string url, string title, string text)
    {
      Content webPage = new Content();

      webPage.Add(new ContentField<string>(ID, id, true, true));
      webPage.Add(new ContentField<string>(URL, url, false));
      webPage.Add(new ContentField<string>(TITLE, title, true));
      webPage.Add(new ContentField<string>(TEXT, text, true));

      return webPage;
    }

  }
}