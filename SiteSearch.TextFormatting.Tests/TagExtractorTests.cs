using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TextFormatting;

namespace TextFormattingTests
{
  [TestClass]
  public class TagExtractorTests
  {

    [TestMethod]
    public void TagExtractorTest1()
    {
      var results = TagExtractor.GetTags("", "h1");
      Assert.IsTrue(results.Count == 0);

      results = TagExtractor.GetTags("<h1></h1>", "h1");
      Assert.IsTrue(results.Count == 1);
      Assert.IsTrue(results[0] == "");

      results = TagExtractor.GetTags("<h1>heading</h1>", "h1");
      Assert.IsTrue(results.Count == 1);
      Assert.IsTrue(results[0] == "heading");

      results = TagExtractor.GetTags("<p><h1>heading</h1></p>", "h1");
      Assert.IsTrue(results.Count == 1);
      Assert.IsTrue(results[0] == "heading");

      results = TagExtractor.GetTags("<html><head></head><body><p><h1>heading</h1></p></body></html>", "h1");
      Assert.IsTrue(results.Count == 1);
      Assert.IsTrue(results[0] == "heading");

      results = TagExtractor.GetTags("<html><head><h1>heading1</h1></head><body><div><h1>heading2</h1></div></body></html>", "h1");
      Assert.IsTrue(results.Count == 2);
      Assert.IsTrue(results[0] == "heading1");
      Assert.IsTrue(results[1] == "heading2");

      results = TagExtractor.GetTags("<html><head><h1>heading1</h1></head><body><div><h1>heading2</h1></div><h1>heading3</h1></body></html>", "h1");
      Assert.IsTrue(results.Count == 3);
      Assert.IsTrue(results[0] == "heading1");
      Assert.IsTrue(results[1] == "heading2");
      Assert.IsTrue(results[2] == "heading3");

      results = TagExtractor.GetTags("<html><head><h1>heading1</h1></head><body><div><h1>heading2</h1></div><h1>heading3</h1><div>1<div>2</div><div>3<div>4<h1>heading4</h1></div></div></div></body></html>", "h1");
      Assert.IsTrue(results.Count == 4);
      Assert.IsTrue(results[0] == "heading1");
      Assert.IsTrue(results[1] == "heading2");
      Assert.IsTrue(results[2] == "heading3");
      Assert.IsTrue(results[3] == "heading4");
    }


    [TestMethod]
    public void TagExtractorTest2()
    {
      var results = TagExtractor.GetTags("<html><head><title>this is the title</title></head><body><p><h1>heading</h1></p></body></html>", "title");
      Assert.IsTrue(results.Count == 1);
      Assert.IsTrue(results[0] == "this is the title");

      results = TagExtractor.GetTags("<html><head><h1>heading1</h1></head><body><div><h1>heading2</h1></div></body></html>", "title");
      Assert.IsTrue(results.Count == 0);

      results = TagExtractor.GetTags("<html><head><title>title1</title><title>title2</title></head><body><p><h1>heading</h1></p></body></html>", "title");
      Assert.IsTrue(results.Count == 2);
      Assert.IsTrue(results[0] == "title1");
      Assert.IsTrue(results[1] == "title2");
    }
  }
}
