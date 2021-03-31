using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterTrends.Models;

namespace ServiceAction
{
    public interface IService
    {
        List<State> GetStates();

        List<Tweet> GetTweets();

        void PaintStates();

        void PaintTweets();

        void FormMap(string JSON_PATH, int YComp, int XComp, int YOffset, int XOffset);

        void AnalizeTweets(string TWEETS_PATH);
    }
}
