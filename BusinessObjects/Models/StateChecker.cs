using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
    class StateChecker
    {
        static List<State> states = new List<State>();

        public static void AsyncFromTweets(List<Twitt> twitts)
        {
            foreach (var item in twitts)
            {
                item.idState = GetState(item.TwittCoordinate);
            }
        }

        public static string GetState(Coordinate p)
        {
            foreach (var item in states)
            {
                foreach (var jtem in item.Polygons)
                {
                    if (isInside(jtem, p))
                    {
                        return item.StateId;
                    }
                }
            }
            return "UNKNOWN";
        }

        private static bool isInside(Polygon polygon, Coordinate p)
        {
            int count = polygon.Coordinates.Count;
            if (count < 3) return false;
            bool res = false;
            if (p.Y > polygon.min_lat && p.Y < polygon.max_lat && p.X > polygon.min_lng && p.X < polygon.max_lng) {
                for (int i = 0, j = count - 1; i < count; i++)
                {
                    var p1 = polygon.Coordinates[i];
                    var p2 = polygon.Coordinates[j];
                    if (p1.Y < p.Y && p2.Y >= p.Y || p2.Y < p.Y && p1.Y >= p.Y)
                    {
                        if (p1.X + (p.Y - p1.Y) / (p2.Y - p1.Y) * (p2.X - p1.X) < p.X)
                        {
                            res = !res;
                        }
                    }
                    j = i;
                }
            }
            return res;
        }

        public static void GiveStates(List<State> stateList)
        {
            states = stateList;
        }
    }


}

