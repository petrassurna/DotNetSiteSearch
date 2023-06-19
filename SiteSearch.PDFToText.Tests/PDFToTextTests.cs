using Microsoft.VisualBasic;
using Shouldly;

namespace SiteSearch.PDFToText.Tests
{
  public class Tests
  {
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void Long_ASIC_design_examples()
    {
      string solution_dir = Directory.GetCurrentDirectory();

      string path = $"{solution_dir}/PDFs/asic-design-examples-big-document.pdf";

      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        MemoryStream memoryStream = new MemoryStream();
        fileStream.CopyTo(memoryStream);
        memoryStream.Position = 0;

        string allText = PDFTextExtractor.PDFToText(memoryStream);


        //text near start
        int pos = allText.IndexOf("information presented in this publication has been prepared following recognized principles of design");
        pos.ShouldBe(383);

        //text in middle
        pos = allText.IndexOf("the block shear rupture strength of the beam web, assuming a total beam");
        pos.ShouldBe(762210);

        //text near end
        pos = allText.IndexOf("Assume that the load acts at the same point as in Solution A");
        pos.ShouldBe(978182);
      }
    }


    [Test]
    public void YDHS_Job_Ad()
    {
      string solution_dir = Directory.GetCurrentDirectory();
      string path = $"{solution_dir}/PDFs/ydhs-job-add.pdf";

      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        MemoryStream memoryStream = new MemoryStream();
        fileStream.CopyTo(memoryStream);
        memoryStream.Position = 0;

        string allText = PDFTextExtractor.PDFToText(memoryStream);

        //text near start
        int pos = allText.IndexOf("Food Services Assistant – Part Time");
        pos.ShouldBe(1);

        //text in middle
        pos = allText.IndexOf("Be organised to meet deadlines and prioritise");
        pos.ShouldBe(1671);

        //text near end
        pos = allText.IndexOf("Selection Criteria and current resume");
        pos.ShouldBe(3080);
      }
    }
  }
}