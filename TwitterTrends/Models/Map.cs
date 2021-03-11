using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
    class Map
    {
        static List<State> states = new List<State>();

        public static string GetState(Coordinate p)
        {
            foreach (var item in states)
            {
                foreach (var jtem in item.Polygons)
                {
                    if (isInside(jtem.Coordinates, p))
                    {
                        return item.StateId;
                    }
                }
            }
            return "UNKNOWN";
        }

        private static bool isInside(List<Coordinate> points, Coordinate p)
        {
            int count = points.Count;
            if (count < 3) return false;
            bool res = false;
            for (int i = 0, j = count - 1; i < count; i++)
            {
                var p1 = points[i];
                var p2 = points[j];
                if (p1.Y < p.Y && p2.Y >= p.Y || p2.Y < p.Y && p1.Y >= p.Y)
                {
                    if (p1.X + (p.Y - p1.Y) / (p2.Y - p1.Y) * (p2.X - p1.X) < p.X)
                    {
                        res = !res;
                    }
                }
                j = i;
            }
            return res;
        }

        public static void GiveStates(List<State> stateList)
        {
            states = stateList;
        }
    }


}

