using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using TwitterTrends.Models;

namespace TwitterTrends.Parsers
{
    public static class JsonParser
    {
        internal static List<State> ParseStates(string pathToFile)
        {
            string jsonString;
            using (StreamReader sr = new StreamReader(pathToFile))
                jsonString = sr.ReadToEnd();

            JObject jsonStates = JObject.Parse(jsonString);

            List<State> states = new List<State>();

            foreach (var st in jsonStates)
            {
                State state = new State();
                state.StateId = st.Key;
                foreach (var polygonsList in st.Value)
                {
                    foreach (var plgn in polygonsList)
                    {
                        Polygon polygon = new Polygon(state.StateId);
                        foreach (var crdn in plgn)
                        {
                            float y = float.Parse(crdn.First.ToString().Replace('.', ','));
                            float x = float.Parse(crdn.Last.ToString().Replace('.', ','));
                            Coordinate coordinate = new Coordinate(x, y);
                            polygon.Coordinates.Add(coordinate);
                            if (polygon.Max_lat.Y < y)
                            {
                                polygon.Max_lat.Y = y;
                                polygon.Max_lat.X = x;
                            }
                            if (polygon.Min_lat.Y > y)
                            {
                                polygon.Min_lat.Y = y;
                                polygon.Min_lat.X = x;
                            }
                            if (polygon.Max_lng.X < x)
                            {
                                polygon.Max_lng.X = x;
                                polygon.Max_lng.Y = y;
                            }
                            if (polygon.Min_lng.X > x)
                            {
                                polygon.Min_lng.X = x;
                                polygon.Min_lng.Y = y;
                            }

                        }

                        state.Polygons.Add(polygon);
                    }
                }
                states.Add(state);
            }
            return states;
        }
    }
}
