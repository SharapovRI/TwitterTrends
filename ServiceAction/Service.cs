using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterTrends.Analize;
using TwitterTrends.Models;
using TwitterTrends.Parsers;

namespace ServiceAction
{
    public class Service: IService
    {
        static readonly IDataDao dataDao = new DataDao();
        public List<State> GetStates()
        {
            return Map.getInstance().CurrentStates;
        }

        public List<Tweet> GetTweets()
        {
            return Map.getInstance().CurrentTweets;
        }

        public void PaintStates()
        {
            Map.getInstance().PaintStates();
        }

        public void PaintTweets()
        {
            Map.getInstance().PaintTweets();
        }

        public void FormMap(string JSON_PATH, int YComp, int XComp, int YOffset, int XOffset)
        {
            Map map = Map.getInstance();
            map.CurrentStates = dataDao.ParseStates(JSON_PATH);
            map.YCOMPRESSION = YComp;
            map.XCOMPRESSION = XComp;
            map.YOFFSET = YOffset;
            map.XOFFSET = XOffset;
            //map.CurrentTweets = Tweetparcer.Twittparce(@"../../../DataObjects/Files/Tweets/football_tweets2014.txt");
        }

        public void AnalizeTweets(string TWEETS_PATH)
        {
            string SENTIMENTS_PATH = @"../../../DataObjects/Files/sentiments.csv";
            Map map = Map.getInstance();

            map.CurrentTweets = dataDao.Twittparce(TWEETS_PATH);
            HashSet<string> hashset = new HashSet<string>();
            var hashtable = dataDao.ParseSantiments(SENTIMENTS_PATH, ref hashset);
            new Searching(map.CurrentTweets, hashtable, hashset);
        }
    }
}
