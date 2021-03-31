using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterTrends.Models;
using TwitterTrends.Parsers;

namespace DataObjects
{
    public class DataDao: IDataDao
    {
        public List<State> ParseStates(string pathToFile)
        {
            return JsonParser.ParseStates(pathToFile);
        }

        public Hashtable ParseSantiments(string filePath, ref HashSet<string> hashset)
        {
            return SantimentsParser.ParseWords(filePath, ref hashset);
        }

        public List<Tweet> Twittparce(string path)
        {
            return Tweetparcer.Twittparce(path);
        }
    }
}
