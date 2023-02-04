using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteSearch.TextFormatting;

namespace SiteSearch.TextFormatting.Tests
{
  [TestClass]
  public class DateTimeTests
  {

    [TestMethod]
    public void DateTimeDifference()
    {
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2020, 2, 15), new DateTime(2021, 2, 1)), 11);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2020, 1, 1), new DateTime(2021, 2, 2)), 13);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2020, 1, 1), new DateTime(2021, 2, 1)), 13);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2020, 1, 1), new DateTime(2021, 1, 1)), 12);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2018, 4, 1), new DateTime(2019, 5, 1)), 13);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2018, 4, 1), new DateTime(2019, 5, 2)), 13);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2019, 7, 8), new DateTime(2020, 7, 8)), 12);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2019, 7, 8), new DateTime(2020, 8, 8)), 13);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2019, 7, 8), new DateTime(2020, 8, 7)), 12);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2019, 1, 1), new DateTime(2019, 12, 31)), 11);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2019, 1, 1), new DateTime(2020, 1, 1)), 12);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2019, 1, 1), new DateTime(2020, 1, 2)), 12);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2019, 1, 1), new DateTime(2020, 1, 30)), 12);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2019, 1, 1), new DateTime(2020, 2, 1)), 13);
      Assert.AreEqual(Formatter.MonthDifference(new DateTime(2019, 1, 1), new DateTime(2020, 2, 2)), 13);

    }

  }
}
