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
    }
}
