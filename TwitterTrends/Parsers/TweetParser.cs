﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using TwitterTrends.Models;

namespace TwitterTrends.Parsers
{
    class Tweetparcer
    {
        public static List<Tweet> twitts = new List<Tweet>();

        public static List<Tweet> Twittparce(string path)
        {
            List<Tweet> tweet = new List<Tweet>();
            string line;
            using (StreamReader reader = new StreamReader(path, true))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    var fourParts = line.Split('\t');
                    if (fourParts.Length < 4)
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(line) || fourParts[0][0] != '[') continue;
                    string[] latlong = fourParts[0].Trim((char[])(new char[] { '[', ']' })).Split(',');
                    if (latlong.Length == 2)
                    {
                        Coordinate coordinate1 = new Coordinate(Convert.ToSingle(latlong[0].Replace('.', ',')), Convert.ToSingle(latlong[1].Replace('.', ',')));
                        tweet.Add(new Tweet(coordinate1, Convert.ToDateTime(fourParts[2]), fourParts[3]));
                    }
                }
            }
            return tweet;
        }
    }
}