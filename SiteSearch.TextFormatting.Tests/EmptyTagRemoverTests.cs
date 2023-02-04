using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextFormatting;

namespace TextFormattingTests
{
  [TestClass]
  public class EmptyTagRemoverTests
  {

    [TestMethod]
    public void RemoveEmptyTags1()
    {
      string html = TagStripper.RemoveEmptyTags("<p></p>", "p");
      Assert.AreEqual(html, "");
    }



  }
}
