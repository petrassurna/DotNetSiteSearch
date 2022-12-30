using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextFormattingTests
{
  [TestClass]
  public class LinkWrapperTests1
  {

    [TestMethod]
    public void Wrap1()
    {
      string str = TextFormatting.LinkWrapper.WrapLinks("here is a link http://www.abc.com ").ToString();
      Assert.AreEqual(str, "here is a link <a href='http://www.abc.com' target='blank'>http://www.abc.com</a> ");

      str = TextFormatting.LinkWrapper.WrapLinks("here is a link").ToString();
      Assert.AreEqual(str, "here is a link");

      str = TextFormatting.LinkWrapper.WrapLinks("http://www.abc.com").ToString();
      Assert.AreEqual(str, "<a href='http://www.abc.com' target='blank'>http://www.abc.com</a>");

      str = TextFormatting.LinkWrapper.WrapLinks("123 http://www.abc.com 456").ToString();
      Assert.AreEqual(str, "123 <a href='http://www.abc.com' target='blank'>http://www.abc.com</a> 456");

      str = TextFormatting.LinkWrapper.WrapLinks("https://www.abc.com").ToString();
      Assert.AreEqual(str, "<a href='https://www.abc.com' target='blank'>https://www.abc.com</a>");

      str = TextFormatting.LinkWrapper.WrapLinks("https://www.abc1.com https://www.abc2.com").ToString();
      Assert.AreEqual(str, "<a href='https://www.abc1.com' target='blank'>https://www.abc1.com</a> <a href='https://www.abc2.com' target='blank'>https://www.abc2.com</a>");
    }


    [TestMethod]
    public void Wrap2()
    {
      string str = TextFormatting.LinkWrapper.WrapHashTags("", "https://twitter.com/");
      Assert.AreEqual(str.ToString(), "");

      str = TextFormatting.LinkWrapper.WrapHashTags("xyz", "https://twitter.com/");
      Assert.AreEqual(str.ToString(), "xyz");

      str = TextFormatting.LinkWrapper.WrapHashTags("#xyz", "https://twitter.com/");
      Assert.AreEqual(str.ToString(), "<a href=\"https://twitter.com/xyz\" target=\"_blank\">#xyz</a>");

      str = TextFormatting.LinkWrapper.WrapHashTags("#xyz ", "https://twitter.com/");
      Assert.AreEqual(str.ToString(), "<a href=\"https://twitter.com/xyz\" target=\"_blank\">#xyz</a> ");

      str = TextFormatting.LinkWrapper.WrapHashTags("#xyza", "https://twitter.com/");
      Assert.AreEqual(str.ToString(), "<a href=\"https://twitter.com/xyza\" target=\"_blank\">#xyza</a>");
    }

  }
}
