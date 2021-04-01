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
            foreach (var state in Map.GetInstance().CurrentStates)
            {
                foreach (var pol in state.Polygons)
                {
                    if (isInside(pol, p))
                    {
                        return state.StateId;
                    }
                }
            }

            return isNearState(p);
        }
        private static float Min(float a1, float a2, float a3, float a4)
        {
            return Math.Min(a1,Math.Min(a2, Math.Min(a3,a4)));
        }
        private static string isNearState(Coordinate p)
        {
            string res="UNKNOWN";
            float distXmax = (float)Math.Sqrt(Math.Pow(p.X - Map.GetInstance().CurrentStates[0].Polygons[0].Max_lng.X, 2) + Math.Pow(p.Y - Map.GetInstance().CurrentStates[0].Polygons[0].Max_lng.Y, 2));
            float distYmax = (float)Math.Sqrt(Math.Pow(p.X - Map.GetInstance().CurrentStates[0].Polygons[0].Max_lat.X, 2) + Math.Pow(p.Y - Map.GetInstance().CurrentStates[0].Polygons[0].Max_lat.Y, 2));
            float distXmin = (float)Math.Sqrt(Math.Pow(p.X - Map.GetInstance().CurrentStates[0].Polygons[0].Min_lng.X, 2) + Math.Pow(p.Y - Map.GetInstance().CurrentStates[0].Polygons[0].Min_lng.Y, 2));
            float distYmin = (float)Math.Sqrt(Math.Pow(p.X - Map.GetInstance().CurrentStates[0].Polygons[0].Min_lat.X, 2) + Math.Pow(p.Y - Map.GetInstance().CurrentStates[0].Polygons[0].Min_lat.Y, 2));
            float min_dist = Min(distXmax,distYmax,distXmin,distYmin);
            res = Map.GetInstance().CurrentStates[0].Polygons[0].StateId;
           
            foreach (var state in Map.GetInstance().CurrentStates)
            {
                foreach (var pol in state.Polygons)
                {
                    if(p.X== 50.01291)
                    {
                        int ads = 0;

                    }
                    distXmax = (float)Math.Sqrt(Math.Pow(p.X - pol.Max_lng.X, 2) + Math.Pow(p.Y - pol.Max_lng.Y, 2));
                    distYmax = (float)Math.Sqrt(Math.Pow(p.X - pol.Max_lat.X, 2) + Math.Pow(p.Y - pol.Max_lat.Y, 2));
                    distXmin = (float)Math.Sqrt(Math.Pow(p.X - pol.Min_lng.X, 2) + Math.Pow(p.Y - pol.Min_lng.Y, 2));
                    distYmin = (float)Math.Sqrt(Math.Pow(p.X - pol.Min_lat.X, 2) + Math.Pow(p.Y - pol.Min_lat.Y, 2));
                    float buf_minDist = Min(distXmax, distYmax, distXmin, distYmin);
                    if (buf_minDist < min_dist)
                    {
                        min_dist = buf_minDist;
                        res = pol.StateId;
                    }

                }
            }
            return res;
        }
        private static bool isInside(Polygon polygon, Coordinate p)
        {
            int count = polygon.Coordinates.Count;
            if (count < 3) return false;
            bool res = false;
            if (p.Y > polygon.Min_lat.Y && p.Y < polygon.Max_lat.Y && p.X > polygon.Min_lng.X && p.X < polygon.Max_lng.X) {
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

