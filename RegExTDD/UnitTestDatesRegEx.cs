using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegEx;

namespace RegExTDD
{
    [TestClass]
    public class UnitTestDatesRegEx
    {
        private DatesRewriter rewriter = new DatesRewriter();

        [TestMethod]
        public void SingleFullNumericDate_FullYear_Test()
        {
            string input = "07/01/2001";
            string[] expected = {"7/1/01"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_3DigitDay_Test()
        {
            string input = "11/019/1997";
            string[] expected = {"11/19/97"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_ShortYear_Test()
        {
            string input = "01/01/07";
            string[] expected = {"1/1/07"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_NoDayFullYear_Test()
        {
            string input = "07/2001";
            string[] expected = {"7/1/01"};

           CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_NoDayShortYear_Test()
        {
            string input = "9/96";
            string[] expected = {"9/1/96"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullYearOnly_Test()
        {
            string input = "2001";
            string[] expected = {"1/1/01"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullYearDash_Test()
        {
            string input = "2008 -";
            string[] expected = {"1/1/08"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullYearDashPresent_Test()
        {
            string input = "1998 - present";
            string[] expected = {"1/1/98"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullYearDashPresentNoSpace_Test()
        {
            string input = "2005-present";
            string[] expected = {"1/1/05"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullDateDashed_Test()
        {
            string input = "05-01-2009";
            string[] expected = {"5/1/09"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void FullNumericDateRange_FullDateDashed_Test()
        {
            string input = "4/1/99-7/1/14";
            string[] expected = { "4/1/99","7/1/14" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void MixMonthYearDateRange_FullDateDashed_Test()
        {
            string input = "09/1993-02/94";
            string[] expected = { "9/1/93", "2/1/94" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void CompactMonthYearDateRange_FullDateDashed_Test()
        {
            string input = "07/2004-09";
            string[] expected = { "7/1/04", "7/1/09" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullDateDashedComma_Test()
        {
            string input = "05/22,2007";
            string[] expected = {"5/22/07"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullDateQuote_Test()
        {
            string input = "06'09/1988";
            string[] expected = {"6/9/88"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullDate5DigitYear_Test()
        {
            string input = "05/31/02006";
            string[] expected = {"5/31/06"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_3YearsFullComma_Test()
        {
            string input = "2005,2006,2007";
            string[] expected = {"1/1/05","1/1/06","1/1/07"};

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_2YearsFullDash_Test()
        {
            string input = "1986-1990";
            string[] expected = { "1/1/86", "1/1/90" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_2YearsFullSlash_Test()
        {
            string input = "1995/1996";
            string[] expected = { "1/1/95", "1/1/96" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_2YearsFullSlashSpace_Test()
        {
            string input = "1995/ 1996";
            string[] expected = { "1/1/95", "1/1/96" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_2YearsFullAmpersandSpace_Test()
        {
            string input = "2004 & 2006";
            string[] expected = { "1/1/04", "1/1/06" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_2YearsFullCommaSpace_Test()
        {
            string input = "1991,1992";
            string[] expected = { "1/1/91", "1/1/92" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_2YearsFull1DigitDash_Test()
        {
            string input = "2003-9";
            string[] expected = { "1/1/03", "1/1/09" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_MonthAlphaCommaFullYear_Test()
        {
            string input = "June, 2008";
            string[] expected = { "6/1/08" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_MonthAlphaCommaFullYears_Test()
        {
            string input = "June, 2008-September, 2009";
            string[] expected = { "6/1/08", "9/1/09" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_2YearsFullShortDash_Test()
        {
            string input = "2005-06";
            string[] expected = { "1/1/05", "1/1/06" };

            CollectionAssert.AreEqual(expected, rewriter.ConvertDate(input));
        }
    }
}
