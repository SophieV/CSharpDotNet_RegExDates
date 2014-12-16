using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegEx
{
    public class DatesRewriter
    {
        public string ConvertDate(string input)
        {
            string converted = null;

            converted = SingleFullNumericDate(input);

            if (converted == null)
            {
                converted = SingleMonthAndYearNumericDate(input);
            }

            if (converted == null)
            {
                converted = SingleYearNumericDate(input);
            }

            if (converted == null)
            {
                converted = YearNumericDashedDateRange(input);
            }

            return converted;
        }

        private string YearNumericDashedDateRange(string input)
        {
            string converted = null;
            string pattern = @"\b(?:\d*)(?<year>\d{2}) \W? \w*\b";
            var regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            MatchCollection allMatches = regex.Matches(input);

            foreach (Match match in allMatches)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append('1');
                builder.Append('/');
                // default value
                builder.Append('1');
                builder.Append('/');
                builder.Append(match.Groups["year"].Value);
                converted = builder.ToString();
            }

            return converted;
        }

        private string SingleYearNumericDate(string input)
        {
            string converted = null;
            string pattern = @"\b(?:\d*)(?<year>\d{2}$)\b";
            var regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            MatchCollection allMatches = regex.Matches(input);

            foreach (Match match in allMatches)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append('1');
                builder.Append('/');
                // default value
                builder.Append('1');
                builder.Append('/');
                builder.Append(match.Groups["year"].Value);
                converted = builder.ToString();
            }

            return converted;
        }

        private string SingleMonthAndYearNumericDate(string input)
        {
            string converted = null;
            string pattern = @"\b(?:[0]?)(?<month>\d{1,2})/(?:\d*)(?<year>\d{2}$)\b";
            var regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            MatchCollection allMatches = regex.Matches(input);

            foreach (Match match in allMatches)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(match.Groups["month"].Value);
                builder.Append('/');
                // default value
                builder.Append('1');
                builder.Append('/');
                builder.Append(match.Groups["year"].Value);
                converted = builder.ToString();
            }

            return converted;
        }

        private string SingleFullNumericDate(string input)
        {
            string converted = null;
            string pattern = @"\b(?:[0]?)(?<month>\d{1,2})\W{1}(?:[0]?)(?<day>\d{1,2})\W{1}(?:\d*)(?<year>\d{2}$)\b";
            var regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            MatchCollection allMatches = regex.Matches(input);

            foreach (Match match in allMatches)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(match.Groups["month"].Value);
                builder.Append('/');
                builder.Append(match.Groups["day"].Value);
                builder.Append('/');
                builder.Append(match.Groups["year"].Value);
                converted = builder.ToString();
            }

            return converted;
        }
    }
}
