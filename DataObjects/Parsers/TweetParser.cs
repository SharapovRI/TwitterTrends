using System;
using System.Collections.Generic;
using System.IO;
using TwitterTrends.Models;

namespace TwitterTrends.Parsers
{
    public class Tweetparcer
    {
        internal static List<Tweet> Twittparce(string path)
        {
            List<Tweet> tweet = new List<Tweet>();
            string line;
            using (StreamReader reader = new StreamReader(path, true))
            {
                int twittId = 1;
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
                        tweet.Add(new Tweet(coordinate1, Convert.ToDateTime(fourParts[2]), fourParts[3]) { Id = twittId++});
                    }
                }
            }
            return tweet;
        }
    }
}