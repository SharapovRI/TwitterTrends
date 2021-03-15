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
        private static Regex locationRegex = new Regex(@"[-]?[0-9]+[.][0-9]+[,][\s][-]?[0-9]+[.][0-9]+");
        private static Regex dateRegex = new Regex(@"[0-9]{4}([-][0-9]{2}){2}.([0-9]{2}[:]){2}[0-9]{2}");
        private static Regex textRegex = new Regex(@"\d\t.+");

        public static List<Twitt> Twittparce(string path)
        {
            List<Twitt> tweet = new List<Twitt>();
            string line;
            using (StreamReader reader = new StreamReader(path, true))
            {
                int i = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line) || line[0] != '[') continue;
                    //var regex = new Regex(@"\d\t");
                    //MatchCollection match = locationRegex.Matches(line);
                    //Match match1 = dateRegex.Match(line)  ;
                    //Match match2 = textRegex.Match(line);
                    string coord = locationRegex.Match(line).Value;
                    coord.Trim((char[])(new char[] { '[', ']' }));
                    string[] latlong = coord.Split(',');
                    if (latlong.Length == 2)
                    {
                        Coordinate coordinate1 = new Coordinate(Convert.ToSingle(latlong[0].Replace('.', ',')), Convert.ToSingle(latlong[1].Replace('.', ',')));
                        tweet.Add(new Twitt(coordinate1, Convert.ToDateTime(dateRegex.Match(line).Value), textRegex.Match(line).Value));
                    }
                    i++;
                    if (i == 1757676 || i == 60000)
                    {
                        int y = 0;
                    }
                }
            }
            return tweet;
        }

        /*private static string GetMessage(Match message, Regex regex)
        {
            return regex.Replace(message.Value, "");
        }*/
        /*private static Coordinate GetCoordinate(string coordinate)
        {
            *//*CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            float x = float.Parse(match[0].Value, NumberStyles.Any, ci);
            float y = float.Parse(match[1].Value, NumberStyles.Any, ci);
            Coordinate coordinate = new Coordinate(x, y);*//*
            //Coordinate coordinate = new Coordinate(float.Parse(match[0].Value.Replace('.', ',')), float.Parse(match[1].Value.Replace('.', ',')));

            coordinate.Trim((char[])(new char[] { '[', ']' }));
            string[] latlong = coordinate.Split(',');

            Coordinate coordinate1 = new Coordinate(Convert.ToSingle(latlong[0].Replace('.', ',')), Convert.ToSingle(latlong[1].Replace('.', ',')));
            return coordinate1;
        }*/
    }
}