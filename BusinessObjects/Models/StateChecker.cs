using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
    class StateChecker
    {
        public static string GetState(Coordinate p)
        {
            foreach (var item in Map.GetInstance().CurrentStates)
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
        private static bool isNearState(Polygon polygon, Coordinate p)
        {
            bool res = false;

            return res;
        }
        private static bool isInside(Polygon polygon, Coordinate p)
        {
            int count = polygon.Coordinates.Count;
            if (count < 3) return false;
            bool res = false;
            if (p.Y > polygon.Min_lat && p.Y < polygon.Max_lat && p.X > polygon.Min_lng && p.X < polygon.Max_lng) {
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
    }


}

