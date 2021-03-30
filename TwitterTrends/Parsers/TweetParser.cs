using System;
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
        async public static Task<int> AsyncParse(string path)
        {
            List<Tweet> tweet = new List<Tweet>();
            string line;
            using (StreamReader reader = new StreamReader(path, true))
            {
                int i = 0;
                List<string> tw = new List<string>();
                while ((line = reader.ReadLine()) != null)
                {
                    i++;
                    tw.Add(line);
                    if (i == 300000)
                    {
                        await Task.Run(() => Twittparce(tw));
                        tw.Clear();
                        i = 0;
                    }
                }
                await Task.Run(() => Twittparce(tw));
            }
            //return twitts;
            Action action;
            Dispatcher.CurrentDispatcher.Invoke(action = () => MainWindow.GiveTwitts(twitts));
            return 0;
        }

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

        public static void Twittparce(List<string> path)
        {
            List<Tweet> tweet1 = new List<Tweet>();
                foreach (string line in path)
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
                        tweet1.Add(new Tweet(coordinate1, Convert.ToDateTime(fourParts[2]), fourParts[3]));
                    }
                }
            Action action;
            Dispatcher.CurrentDispatcher.Invoke(action = () => twitts.AddRange(tweet1));
            //Dispatcher.CurrentDispatcher.Thread.Abort();
        }
    }
}