using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TwitterTrends.Models;

namespace TwitterTrends.Parsers
{
    class Tweetparcer
    {
        private static Regex locationRegex = new Regex(@"[-]?[0-9]{1,3}[.][0-9]{1,15}");
        private static Regex dateRegex = new Regex(@"[0-9]{4}([-][0-9]{2}){2}.([0-9][0-9][:]){2}[0-9][0-9]");
        private static Regex textRegex = new Regex(@"\d\t.+");

        public static List<Twitt> Twittparce(string path)
        {
            List<Twitt> tweet = new List<Twitt>();
            string line;
            using (StreamReader reader = new StreamReader(path, true))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line) || line[0] != '[') continue;
                    var regex = new Regex(@"\d\t");
                    MatchCollection match = locationRegex.Matches(line);
                    Match match1 = dateRegex.Match(line);
                    Match match2 = textRegex.Match(line);
                    if (match.Count == 2)
                    {
                        tweet.Add(new Twitt(GetCoordinate(match), DateTime.Parse(match1.Value), GetMessage(match2, regex)));
                    }
                }
            }
            return tweet;
        }

        private static string GetMessage(Match message, Regex regex)
        {
            return regex.Replace(message.Value, "");
        }
        private static Coordinate GetCoordinate(MatchCollection match)
        {
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            //avarage = double.Parse("0.0", NumberStyles.Any, ci);
            float x = float.Parse(match[0].Value, NumberStyles.Any, ci);
            float y = float.Parse(match[1].Value, NumberStyles.Any, ci);
            Coordinate coordinate = new Coordinate(x, y);
            return coordinate;
        }
    }
}