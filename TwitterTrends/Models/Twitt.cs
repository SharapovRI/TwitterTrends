using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

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
            //GetIdStateAsync(coordinate);
        }
        public Coordinate TwittCoordinate;
        DateTime DateTime;
        public string Text;
        public string idState;
        public float? weight;

        /*async private void GetIdStateAsync(Coordinate coordinates)
        {
            Action action;
            await Task.Run(() => Dispatcher.CurrentDispatcher.Invoke(action = () => idState = Map.GetState((Coordinate)coordinates)));
        }*/
    }
}
