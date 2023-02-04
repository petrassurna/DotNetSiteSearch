using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TextFormatting;

namespace TextFormattingTests
{
  [TestClass]
  public class WebPageExtractorTests
  {
    //[TestMethod]
    public void Extractor1()
    {
      List<Tag> exclude = new List<Tag>();

      exclude.Add(new Tag() { TagName = "section", AttributeName = "class", AttributeValue = "header" });
      exclude.Add(new Tag() { TagName = "section", AttributeName = "class", AttributeValue = "nav-wrapper" });
      exclude.Add(new Tag() { TagName = "form", AttributeName = "id", AttributeValue = "mobile-search-form" });
      exclude.Add(new Tag() { TagName = "section", AttributeName = "class", AttributeValue = "breadcrumbs" });
      exclude.Add(new Tag() { TagName = "footer", AttributeName = "", AttributeValue = "" });
      exclude.Add(new Tag() { TagName = "section", AttributeName = "class", AttributeValue = "subscribe" });

      string html = TextFormatting.WebPageExtracter.ExtractFromUrl("http://aasb.staging.yartdigital.com/internal.html", exclude);

      string str1 = "Pronouncements The Australian Accounting Standards Board ";
      Assert.IsTrue(html.Substring(0, str1.Length) == str1);

      string str2 = "VIC More View more";
      Assert.IsTrue(html.Substring(html.Length - str2.Length) == str2);
    }

    [TestMethod]
    public void Extractor2()
    {
      string html = TextFormatting.WebPageExtracter.ExtractFromHtml("<p>abc</p>", new List<Tag>(), "");
      string str1 = "abc";
      Assert.IsTrue(html.Substring(0, str1.Length) == str1);
    }


    [TestMethod]
    public void Extractor3()
    {
      string html = TextFormatting.WebPageExtracter.ExtractTextFromHtml(@"<p>abc<a href="">123</a>def</p>");
      string str1 = "abc 123 def";
      Assert.IsTrue(html.Substring(0, str1.Length) == str1);
    }


    [TestMethod]
    public void Extractor4()
    {
      string html = "<p class=\"AECAddresslines\"> </p><p>The announcement of Victoria’s new renewable energy target is ambitious and leaves many important technical details unanswered, the energy industry said today.</p><p>The Victorian Government target proposes 5400MW of new large-scale renewable generation to be built in Victoria by 2025.  This is more than the current total of large-scale renewable generation in the National Electricity Market(around 4300MW).</p><p>The Australian Energy Council’s Chief Executive, Matthew Warren, said “the energy industry is committed to the decarbonisation of electricity as quickly as possible, at the lowest cost whilst maintaining reliable energy supply.</p><p>“Energy systems are complex.You cannot expect to build the equivalent of more than 50 new wind farms(1,400 turbines) in Victoria in 8 years without significant impacts on energy costs and reliability to consumers, and without a broader national plan for the sustainable transformation of the energy sector.To put this in context, thre are currently 17 wind farms(comprising 596 turbines) in Victoria.</p><p>\"Victoria’s electricity system is interconnected to other states as part of a national grid.  Decisions made here affect other states.  That’s why major policy measures to reduce emissions should be implemented at a national level,” Mr Warren said.</p><p>“Australia has had a bi-partisan national renewable energy target in place since 2009.  Despite the best efforts of all stakeholders, we have seen that targets alone struggle to deliver the efficient and reliable transformation of energy supply.</p><p>“As the industry which will make the vast majority of investment and manage customer impacts we are concerned and disappointed that the Victorian Government has not consulted with the energy industry on the development of this policy,” Mr Warren said.</p><p> </p><p>About the Australian Energy Council</p><p>The Council represents 22 major electricity and downstream natural gas businesses operating in competitive wholesale and retail energy markets. These businesses collectively generate the overwhelming majority of electricity in Australia and sell gas and electricity to over 10 million homes and businesses.</p><p> </p>";

      html = TextFormatting.WebPageExtracter.ExtractTextFromHtml(html);

      string str1 = "The announcement of Victoria";

      Assert.IsTrue(html.Substring(0, str1.Length) == str1);

      int index = html.IndexOf("Energy systems are complex");
      Assert.IsTrue(index == 633);

      index = html.IndexOf("About the Australian Energy Council");
      Assert.IsTrue(index == 1777);
    }


    [TestMethod]
    public void Extractor5()
    {
      string html = "<p><img id = \"__mcenew\" src=\"/media/15654/twitter-podcast-png.png\" alt=\"\" rel=\"21533\" data-id=\"21533\" /></p><p>Tim Nelson, former Executive General Manager Strategy and Economic Analysis at the Australian Energy Market Commission(AEMC), provides an overview of recent price trends modelling and other quantitative insights from the AEMC analytics team.The presentation also outlines the integrated work program of the AEMC, ESB and other market bodies. <a href = \"https://soundcloud.com/energycouncilau/tim-nelson-aemc-insights\" target=\"_blank\">Listen to the discussion here</a> or below.</p><p>View Tim's <a href=\"/media/18197/presentation-tim-nelson_20022020.pdf\" target=\"_blank\" title=\"Tim Nelson AEMC powerpoint presentation\">accompanying PowerPoint presentation here</a>.</p><p><iframe scrolling = \"no\" src=\"https://w.soundcloud.com/player/?visual=true&amp;url=https%3A%2F%2Fapi.soundcloud.com%2Ftracks%2F791883070&amp;show_artwork=true&amp;maxwidth=360&amp;maxheight=240\" width=\"360\" height=\"240\" frameborder=\"no\"></iframe></p><p><a href = \"https://soundcloud.com/energycouncilau\" target=\"_blank\">Visit here</a> to listen to more AEC podcasts. We will produce regular podcasts on areas and issues of interest and look forward to sharing them in future.Stay updated: <strong><a href = \"https://australianenergycouncil.cmail20.com/t/r-i-jtdrjhdy-l-dj/\" > Twitter </ a ></ strong >< strong ></ strong >< strong >| </ strong >< strong >< a href= \"https://australianenergycouncil.cmail20.com/t/r-i-jtdrjhdy-l-dt/\" > Linkedin </ a ></ strong ></ p >";

      html = TextFormatting.WebPageExtracter.ExtractTextFromHtml(html);

      string str1 = "Tim Nelson, former Executive General Manager Strategy";

      Assert.IsTrue(html.Substring(0, str1.Length) == str1);

      int index = html.IndexOf("Listen to the discussion here or below");
      Assert.IsTrue(index == 342);

      index = html.IndexOf("Stay updated: Twitter | Linkedin");
      Assert.IsTrue(index == 588);
    }


    [TestMethod]
    public void Extractor6()
    {
      string html = "<p>I am, a sentence</p>";

      html = TextFormatting.WebPageExtracter.ExtractTextFromHtml(html);

      Assert.AreEqual(html, "I am, a sentence");

    }
  }
}
