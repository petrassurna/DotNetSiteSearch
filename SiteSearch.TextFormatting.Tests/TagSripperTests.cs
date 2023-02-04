using SiteSearch.TextFormatting;

namespace SiteSearch.TextFormatting.Tests
{
  [TestClass]
  public class TagSripperTests
  {
    [TestMethod]
    public void RemoveAttributesWithValues1()
    {
      string html = "<p>Hello, <span class='highlight'>world</span>!</p>";
      string cleanedHtml = TagStripper.RemoveTagsWithAttribute(html, "class", "highlight");
      Assert.AreEqual(cleanedHtml, "<p>Hello, !</p>");

      html = "<div><p>this is embedded <a href=\"/\" class='highlight'>link</a></p></div>";
      cleanedHtml = TagStripper.RemoveTagsWithAttribute(html, "class", "highlight");
      Assert.AreEqual(cleanedHtml, "<div><p>this is embedded </p></div>");

      html = "<div><p>this is embedded <ul><li>number 1<a href=\"/\" class='highlight'>link</a></li><li>number 2</li><li class='highlight'>number 3</li></ul></p></div>";
      cleanedHtml = TagStripper.RemoveTagsWithAttribute(html, "class", "highlight");
      Assert.AreEqual(cleanedHtml, "<div><p>this is embedded <ul><li>number 1</li><li>number 2</li></ul></div>");
    }


    [TestMethod]
    public void RemoveAttributesWithValues2()
    {
      string html = "<p>Hello, <span usitesearch-exclude='true'>world</span>!</p>";
      string cleanedHtml = TagStripper.RemoveTagsWithAttribute(html, "usitesearch-exclude", "true");
      Assert.AreEqual(cleanedHtml, "<p>Hello, !</p>");

      html = "<div><p>this is embedded <a href=\"/\" usitesearch-exclude=\"true\">link</a></p></div>";
      cleanedHtml = TagStripper.RemoveTagsWithAttribute(html, "usitesearch-exclude", "true");
      Assert.AreEqual(cleanedHtml, "<div><p>this is embedded </p></div>");

      html = "<div><p>this is embedded <ul><li>number 1<a href=\"/\" usitesearch-exclude=\"true\">link</a></li><li>number 2</li><li class='highlight'>number 3</li></ul></p></div>";
      cleanedHtml = TagStripper.RemoveTagsWithAttribute(html, "usitesearch-exclude", "true");
      Assert.AreEqual(cleanedHtml, "<div><p>this is embedded <ul><li>number 1</li><li>number 2</li><li class='highlight'>number 3</li></ul></div>");
    }
  }
}
