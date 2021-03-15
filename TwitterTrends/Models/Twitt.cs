using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
    public class Twitt
    {
        public Twitt(Coordinate coordinate, DateTime dateTime, string text)
        {
            TwittCoordinate = coordinate;
            DateTime = dateTime;
            Text = text;
            //idState = Map.GetState(TwittCoordinate);
        }
        public Coordinate TwittCoordinate;
        DateTime DateTime;
        public string Text;
        public string idState;
        public float weight;
    }
}
