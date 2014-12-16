﻿using System;
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

            return converted;
        }

        private string SingleYearNumericDate(string input)
        {
            string converted = null;
            string pattern = @"\b(?:\d*)(?<year>\d{2}$)\b";
            var regex = new Regex(pattern);
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
            string pattern = @"\b(?:[0])(?<month>\d{1,2})/(?:\d*)(?<year>\d{2}$)\b";
            var regex = new Regex(pattern);
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
            string pattern = @"\b(?:[0]?)(?<month>\d{1,2})/(?:[0]?)(?<day>\d{1,2})/(?:\d*)(?<year>\d{2}$)\b";
            var regex = new Regex(pattern);
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

            //try
            //{
            //    // ignore (optional) 0s in front of a number, consider only the last two digits of the year (optionally more digits)
            //    converted = Regex.Replace(input,
            //          @"\b(?:[0])(?<month>\d{1,2})/(?:[0])(?<day>\d{1,2})/(?:\d*)(?<year>\d{2}$)\b",
            //          "${month}/${day}/${year}", RegexOptions.None,
            //          TimeSpan.FromMilliseconds(150));
            //}
            //catch (RegexMatchTimeoutException)
            //{
            //    converted = null;
            //}

            return converted;
        }
    }
}