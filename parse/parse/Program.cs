using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<Twitt> tweet = new List<Twitt>();
            string path= "D:/VS/C#/2/parse/parse/movie.txt";
            tweet = Tweetparcer.Twittparce(path);

        }
    }
}
