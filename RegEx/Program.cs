using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegEx
{
    class Program
    {
        static void Main(string[] args)
        {
            DatesRewriter rewriter = new DatesRewriter();
            string dateString = "11/019/1997"; //DateTime.Today.ToString("d", DateTimeFormatInfo.InvariantInfo);
            string resultString = rewriter.ConvertDate(dateString);
            Console.WriteLine("Converted {0} to {1}.", dateString, resultString);
            Console.Read();
        }

        static string MDYToDMY(string input)
        {
            try
            {
                return Regex.Replace(input,
                      "\\b(?<month>\\d{1,2})/(?<day>\\d{1,2})/(?<year>\\d{2,4})\\b",
                      "${day}-${month}-${year}", RegexOptions.None,
                      TimeSpan.FromMilliseconds(150));
            }
            catch (RegexMatchTimeoutException)
            {
                return input;
            }
        }
    }
}
