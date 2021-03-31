using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace TwitterTrends.Models
{
    public class Tweet
    {
        public Tweet(Coordinate coordinate, DateTime dateTime, string text)
        {
            TwittCoordinate = coordinate;
            DateTime = dateTime;
            Text = text;
            StateId = StateChecker.GetState(TwittCoordinate);
            //GetIdStateAsync(coordinate);
        }
        public Coordinate TwittCoordinate;
        DateTime DateTime;
        public string Text;
        public string StateId;
        public float? Weight;
        public Brush Color;
        /*async private void GetIdStateAsync(Coordinate coordinates)
        {
            Action action;
            await Task.Run(() => Dispatcher.CurrentDispatcher.Invoke(action = () => idState = Map.GetState((Coordinate)coordinates)));
        }*/
    }
}
