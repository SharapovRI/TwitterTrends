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
        //private static Regex locationRegex = new Regex(@"[-]?[0-9]{1,3}[.][0-9]{1,15}");
        //private static Regex locationRegex = new Regex(@"[-]?[0-9]+[.][0-9]+[,][\s][-]?[0-9]+[.][0-9]+");
        //private static Regex dateRegex = new Regex(@"[0-9]{4}([-][0-9]{2}){2}.([0-9]{2}[:]){2}[0-9]{2}");
        //private static Regex textRegex = new Regex(@"\d\t.+");

        public static List<Twitt> Twittparce(string path)
        {
            List<Twitt> tweet = new List<Twitt>();
            string line;
            using (StreamReader reader = new StreamReader(path, true))
            {
                int i = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    var twoParts = line.Split('\t');
                    if (twoParts.Length < 4)
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(line) || twoParts[0][0] != '[') continue;

                    //string coord = locationRegex.Match(line).Value;
                    //coord.Trim((char[])(new char[] { '[', ']' }));

                    //twoParts[0].Trim((char[])(new char[] { '[', ']' }));
                    string[] latlong = twoParts[0].Trim((char[])(new char[] { '[', ']' })).Split(',');
                    if (latlong.Length == 2)
                    {
                        Coordinate coordinate1 = new Coordinate(Convert.ToSingle(latlong[0].Replace('.', ',')), Convert.ToSingle(latlong[1].Replace('.', ',')));
                        //tweet.Add(new Twitt(coordinate1, Convert.ToDateTime(dateRegex.Match(line).Value), textRegex.Match(line).Value));
                        tweet.Add(new Twitt(coordinate1, Convert.ToDateTime(twoParts[2]), twoParts[3]));
                    }
                    i++;
                    if (i == 1000)
                    {
                        int y = 0;
                    }
                }
            }
            return tweet;
        }
    }
}