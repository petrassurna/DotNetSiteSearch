using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSearch.TextFormatting.Tests
{
  [TestClass]
  public class DoubleSpacerTests
  {

    [TestMethod]
    public void TestSpaces()
    {
      List<int> columns;

      columns = TextFormatting.Formatter.HomePageColumns(0);
      Assert.AreEqual(columns.Count(), 0);

      columns = TextFormatting.Formatter.HomePageColumns(1);
      Assert.AreEqual(columns.Count(), 1);
      Assert.AreEqual(columns[0], 1);

      columns = TextFormatting.Formatter.HomePageColumns(2);
      Assert.AreEqual(columns.Count(), 1);
      Assert.AreEqual(columns[0], 2);

      columns = TextFormatting.Formatter.HomePageColumns(3);
      Assert.AreEqual(columns.Count(), 1);
      Assert.AreEqual(columns[0], 3);

      columns = TextFormatting.Formatter.HomePageColumns(4);
      Assert.AreEqual(columns.Count(), 2);
      Assert.AreEqual(columns[0], 2);
      Assert.AreEqual(columns[1], 2);

      columns = TextFormatting.Formatter.HomePageColumns(5);
      Assert.AreEqual(columns.Count(), 2);
      Assert.AreEqual(columns[0], 2);
      Assert.AreEqual(columns[1], 3);

      columns = TextFormatting.Formatter.HomePageColumns(6);
      Assert.AreEqual(columns.Count(), 2);
      Assert.AreEqual(columns[0], 3);
      Assert.AreEqual(columns[1], 3);

      columns = TextFormatting.Formatter.HomePageColumns(7);
      Assert.AreEqual(columns.Count(), 3);
      Assert.AreEqual(columns[0], 2);
      Assert.AreEqual(columns[1], 2);
      Assert.AreEqual(columns[2], 3);

      columns = TextFormatting.Formatter.HomePageColumns(8);
      Assert.AreEqual(columns.Count(), 3);
      Assert.AreEqual(columns[0], 2);
      Assert.AreEqual(columns[1], 3);
      Assert.AreEqual(columns[2], 3);

      columns = TextFormatting.Formatter.HomePageColumns(9);
      Assert.AreEqual(columns.Count(), 3);
      Assert.AreEqual(columns[0], 3);
      Assert.AreEqual(columns[1], 3);
      Assert.AreEqual(columns[2], 3);

      columns = TextFormatting.Formatter.HomePageColumns(10);
      Assert.AreEqual(columns.Count(), 4);
      Assert.AreEqual(columns[0], 2);
      Assert.AreEqual(columns[1], 2);
      Assert.AreEqual(columns[2], 3);
      Assert.AreEqual(columns[3], 3);

      columns = TextFormatting.Formatter.HomePageColumns(11);
      Assert.AreEqual(columns.Count(), 4);
      Assert.AreEqual(columns[0], 2);
      Assert.AreEqual(columns[1], 3);
      Assert.AreEqual(columns[2], 3);
      Assert.AreEqual(columns[3], 3);

    }

  }
}
