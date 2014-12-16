using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegEx
{
    // (?:[0]*): ignore "trailing" zeros
    // (?<month>\d{1,2}) : consider the next one or two digits as the month
    // \W+: at least one non-alphanumeric character
    // (\d{4})\d{2}(?<year>\d{2})|(?<year>\d{2}): if year has 4 digits, ignore two and take last two. else take 2. (will fail if only 1)
    // [a-zA-Z]+ : at least one alphabetical character

    public class DatesRewriter
    {
        public string[] ConvertDate(string input)
        {
            List<string> converted = new List<string>();

            converted.AddRange(SingleFullNumericDate(input));

            if (converted.Count() == 0)
            {
                converted.AddRange(SingleMonthAndYearsNumericDate(input));
            }

            if (converted.Count() == 0)
            {
                converted.AddRange(SingleMonthAndYearNumericDate(input));
            }

            if (converted.Count() == 0)
            {
                converted.AddRange(WrittenMonthAndYear(input));
            } 

            if (converted.Count() == 0)
            {
                converted.AddRange(YearNumericDashedDateRange(input));
            }
            
            if (converted.Count() == 0)
            {
                converted.AddRange(YearNumericFullThenSingleDateRange(input));
            }

            if (converted.Count() == 0)
            {
                // is less specific than YearNumericFullThenSingleDateRange
                converted.AddRange(MultipleYearsRange(input));
            }

            return converted.ToArray();
        }

        // identifies "2003-now" or "98 - present"
        private List<string> YearNumericDashedDateRange(string input)
        {
            List<string> converted = new List<string>();
            string pattern = @"\b(?:[0]*)(?(\d{4})\d{2}(?<year>\d{2})|(?<year>\d{2}))\W+[a-zA-Z]+\b";
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
                converted.Add(builder.ToString());
            }

            return converted;
        }

        // identifies "2004-8" or "04-8"
        private List<string> YearNumericFullThenSingleDateRange(string input)
        {
            List<string> converted = new List<string>();
            string pattern = @"\b(?:[0]*)(?(\d{4})\d{2}(?<year1>\d{2})|(?<year1>\d{2}))\W+(?<year2>\d{1})\b";
            var regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            MatchCollection allMatches = regex.Matches(input);

            foreach (Match match in allMatches)
            {
                StringBuilder builder = new StringBuilder();
                // default value
                builder.Append('1');
                builder.Append('/');
                // default value
                builder.Append('1');
                builder.Append('/');
                builder.Append(match.Groups["year1"].Value);
                converted.Add(builder.ToString());

                builder = new StringBuilder();
                // default value
                builder.Append('1');
                builder.Append('/');
                // default value
                builder.Append('1');
                builder.Append('/');
                // missing digit of year - must be 2
                builder.Append('0');
                builder.Append(match.Groups["year2"].Value);
                converted.Add(builder.ToString());
            }

            return converted;
        }

        // identifies a year, could be just as many as wanted
        // such as "2002,2003" or "1998-1999-2000"
        // "19911992" is not recognized though
        private List<string> MultipleYearsRange(string input)
        {
            List<string> converted = new List<string>();
            string pattern = @"\b(?:[0]*)(?(\d{4})\d{2}(?<year>\d{2})|(?<year>\d{2}))\b";
            var regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            MatchCollection allMatches = regex.Matches(input);

            foreach (Match match in allMatches)
            {
                StringBuilder builder = new StringBuilder();
                // default value
                builder.Append('1');
                builder.Append('/');
                // default value
                builder.Append('1');
                builder.Append('/');
                builder.Append(match.Groups["year"].Value);
                converted.Add(builder.ToString());
            }

            return converted;
        }

        // identifies "09/1988"
        private List<string> SingleMonthAndYearNumericDate(string input)
        {
            List<string> converted = new List<string>();
            string pattern = @"\b(?:[0]*)(?<month>\d{1,2})\W+(?:[0]*)(?(\d{4})\d{2}(?<year>\d{2})|(?<year>\d{2}))\b";
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
                converted.Add(builder.ToString());
            }

            return converted;
        }

        // identifies "09/1988-89"
        private List<string> SingleMonthAndYearsNumericDate(string input)
        {
            List<string> converted = new List<string>();
            string pattern = @"\b(?:[0]*)(?<month>\d{1,2})\W+(?:[0]*)(?(\d{4})\d{2}(?<year1>\d{2})|(?<year1>\d{2}))\W+(?:[0]*)(?(\d{4})\d{2}(?<year2>\d{2})|(?<year2>\d{2}))$\b";
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
                builder.Append(match.Groups["year1"].Value);
                converted.Add(builder.ToString());

                builder = new StringBuilder();
                builder.Append(match.Groups["month"].Value);
                builder.Append('/');
                // default value
                builder.Append('1');
                builder.Append('/');
                builder.Append(match.Groups["year2"].Value);
                converted.Add(builder.ToString());
            }

            return converted;
        }

        // identifies "04/11/1987" or "5-8-08"
        private List<string> SingleFullNumericDate(string input)
        {
            List<string> converted = new List<string>();
            string pattern = @"\b(?:[0]*)(?<month>\d{1,2})\W+(?:[0]*)(?<day>\d{1,2})\W+(?:[0]*)(?(\d{4})\d{2}(?<year>\d{2})|(?<year>\d{2}))\b";
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
                converted.Add(builder.ToString());
            }

            return converted;
        }

        // recognizes "June, 2008" as well as "June,2007-September,2010"
        private List<string> WrittenMonthAndYear(string input)
        {
            List<string> converted = new List<string>();
            string pattern = @"\b(?<month>[a-zA-Z]+)\W+(?:[0]*)(?(\d{4})\d{2}(?<year>\d{2})|(?<year>\d{2}))\b";
            var regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            MatchCollection allMatches = regex.Matches(input);

            foreach (Match match in allMatches)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(DateTime.ParseExact(match.Groups["month"].Value, "MMMM", CultureInfo.InvariantCulture).Month);
                builder.Append('/');
                // default value
                builder.Append('1');
                builder.Append('/');
                builder.Append(match.Groups["year"].Value);
                converted.Add(builder.ToString());
            }

            return converted;
        }
    }
}
