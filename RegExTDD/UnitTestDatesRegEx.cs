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
            string expected = "7/1/01";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_3DigitDay_Test()
        {
            string input = "11/019/1997";
            string expected = "11/19/97";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_ShortYear_Test()
        {
            string input = "01/01/07";
            string expected = "1/1/07";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_NoDayFullYear_Test()
        {
            string input = "07/2001";
            string expected = "7/1/01";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_NoDayShortYear_Test()
        {
            string input = "9/96";
            string expected = "9/1/96";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullYearOnly_Test()
        {
            string input = "2001";
            string expected = "1/1/01";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullYearDash_Test()
        {
            string input = "2008 -";
            string expected = "1/1/08";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullYearDashPresent_Test()
        {
            string input = "1998 - present";
            string expected = "1/1/98";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullYearDashPresentNoSpace_Test()
        {
            string input = "2005-present";
            string expected = "1/1/05";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullDateDashed_Test()
        {
            string input = "05-01-2009";
            string expected = "5/1/09";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullDateDashedComma_Test()
        {
            string input = "05/22,2007";
            string expected = "5/22/07";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullDateQuote_Test()
        {
            string input = "06'09/1988";
            string expected = "6/9/88";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }

        [TestMethod]
        public void SingleFullNumericDate_FullDate5DigitYear_Test()
        {
            string input = "05/31/02006";
            string expected = "5/31/06";

            Assert.AreEqual(expected, rewriter.ConvertDate(input));
        }
    }
}
