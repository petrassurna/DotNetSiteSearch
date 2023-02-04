using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiteSearch.TextFormatting;

namespace SiteSearch.TextFormatting.Tests
{
  [TestClass]
  public class MaximumWordTests
  {



    [TestMethod]
    public void TestMaximumWords()
    {
      string max;

      max = MaximumWords.Format("<p>the dog and the cat</p>", 3);
      Assert.AreEqual("<p>the dog and...</p>", max);

      max = MaximumWords.Format("<p>the dog <a>and</a> the cat</p>", 3);
      Assert.AreEqual("<p>the dog <a>and</a>...</p>", max);

      max = MaximumWords.Format("<p>the dog <a>and</a> the cat</p>", 4);
      Assert.AreEqual("<p>the dog <a>and</a> the...</p>", max);

      max = MaximumWords.Format("<p>the dog <a>and</a> the cat</p>", 5);
      Assert.AreEqual("<p>the dog <a>and</a> the cat</p>", max);

      max = MaximumWords.Format("<p>the dog <a>and</a> the cat</p>", 6);
      Assert.AreEqual("<p>the dog <a>and</a> the cat</p>", max);

      max = MaximumWords.Format("<p>the dog <a href=\"test.html\">and</a> the cat</p>", 3);
      Assert.AreEqual("<p>the dog <a href=\"test.html\">and</a>...</p>", max);

      max = MaximumWords.Format("<p>the dog and the cat</p>", 8);
      Assert.AreEqual("<p>the dog and the cat</p>", max);

      max = MaximumWords.Format("<p>the dog and the cat</p><p>the dog and</p>", 3);
      Assert.AreEqual("<p>the dog and...</p>", max);

      max = MaximumWords.Format("<p>the dog and the cat</p><p>the dog and</p>", 6);
      Assert.AreEqual("<p>the dog and the cat</p> <p>the...</p>", max);

      max = MaximumWords.Format("<p>the dog and the cat</p><p>the dog and</p>", 60);
      Assert.AreEqual("<p>the dog and the cat</p> <p>the dog and</p>", max);

      max = MaximumWords.Format("the dog and the cat", 3);
      Assert.AreEqual("the dog and...", max);

      max = MaximumWords.Format("the dog and the cat", 5);
      Assert.AreEqual("the dog and the cat", max);

      max = MaximumWords.Format("the dog and the cat and", 5);
      Assert.AreEqual("the dog and the cat...", max);

      max = MaximumWords.Format("<p>one</p><p></p><p>two</p><p></p><p>three</p><p></p>", 5);
      Assert.AreEqual("<p>one</p> <p></p> <p>two</p> <p></p> <p>three</p> <p></p>", max);
      
      max = MaximumWords.Format("<p>one</p><p></p><p>two</p><p></p><p>three</p><p></p>", 2);
      Assert.AreEqual("<p>one</p> <p></p> <p>two</p>", max);

      max = MaximumWords.Format("the dog.", 5);
      Assert.AreEqual("the dog.", max);

      max = MaximumWords.Format("<p>the dog.</p><p>and the cat</p>", 5);
      Assert.AreEqual("<p>the dog.</p> <p>and the cat</p>", max);
    }


    [TestMethod]
    public void FirstSentence()
    {
      string str;

      str = MaximumWords.FirstSentence("");
      Assert.AreEqual("", str);

      str = MaximumWords.FirstSentence(".");
      Assert.AreEqual(".", str);

      str = MaximumWords.FirstSentence("I am a sentence");
      Assert.AreEqual("I am a sentence", str);

      str = MaximumWords.FirstSentence("I am a sentence. ");
      Assert.AreEqual("I am a sentence.", str);

      str = MaximumWords.FirstSentence("I am a sentence. Here is another.");
      Assert.AreEqual("I am a sentence.", str);

      str = MaximumWords.FirstSentence("I am, a sentence. Here is another.");
      Assert.AreEqual("I am, a sentence.", str);

    }


    [TestMethod]
    public void TestTagRemoval()
    {
      string str;

      str = TagStripper.RemoveTag("<p class=\"big\">the dog <a class=\"x\">link</a> cat</p>", "a", "class", "x");
      Assert.AreEqual("<p class=\"big\">the dog  cat</p>", str);

      str = TagStripper.RemoveTag("<a class=\"x\">link</a>", "a", "class", "x");
      Assert.AreEqual("", str);

      str = TagStripper.RemoveTag("<ul><li>111</li><li>222</li><li>333</li></ul>", "a", "class", "x");
      Assert.AreEqual("<ul><li>111</li><li>222</li><li>333</li></ul>", str);
      
      str = TagStripper.RemoveTag("<ul><li>111</li><li><a class=\"x\"></a></li><li>333</li></ul>", "a", "class", "x");
      Assert.AreEqual("<ul><li>111</li><li></li><li>333</li></ul>", str);
      
      str = TagStripper.RemoveTag("<ul><li>111</li><li><a class1=\"x\"></a></li><li>333</li></ul>", "a", "class", "x");
      Assert.AreEqual("<ul><li>111</li><li><a class1=\"x\"></a></li><li>333</li></ul>", str);

      str = TagStripper.RemoveTag("<p class=\"big\">the dog <a class=\"x\">link</a> cat <a class=\"x\">link1</a> <ul><li>111</li><li><a class=\"x\"></a></li><li>333</li></ul></p>", "a", "class", "x");
      Assert.AreEqual("<p class=\"big\">the dog  cat  </p><ul><li>111</li><li></li><li>333</li></ul>", str);

      str = TagStripper.RemoveTag("<p>The document</p><a class=\"grey\" href=\"/media/3ugdz5cd/aasb1_05-09_erdrjun10_07-09.pdf\">Accounting Standard</a>", "a", "class", "grey");
      Assert.AreEqual("<p>The document</p>", str);

      str = TagStripper.RemoveTag("<p><span>The document</span></p>", "a", "class", "grey");
      Assert.AreEqual("<p><span>The document</span></p>", str);

      str = TagStripper.RemoveTag("<p><span>The document <a class=\"grey\">hi there</a>is big</span></p>", "a", "class", "grey");
      Assert.AreEqual("<p><span>The document is big</span></p>", str);

      str = TagStripper.RemoveTag("<div>1</div><div id=\"x\">2</div><div>3</div><div>4</div>", "div", "id", "x");
      Assert.AreEqual("<div>1</div><div>3</div><div>4</div>", str);
      
      str = TagStripper.RemoveTag("<div>1</div><div id=\"x\"><div><div id=\"y\">y1</div><div>y2</div></div></div><div>3</div><div>4</div>", "div", "id", "y");
      Assert.AreEqual("<div>1</div><div id=\"x\"><div><div>y2</div></div></div><div>3</div><div>4</div>", str);

    }

  }
}
