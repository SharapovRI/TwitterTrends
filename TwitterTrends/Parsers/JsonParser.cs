﻿using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using TwitterTrends.Models;

namespace TwitterTrends.Parsers
{
    public static class JsonParser
    {
        public static Map ParseStates(string pathToFile)
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
                            Coordinate coordinate = new Coordinate(float.Parse(crdn.Last.ToString().Replace('.', ',')), float.Parse(crdn.First.ToString().Replace('.', ',')));
                            polygon.Coordinates.Add(coordinate);
                        }
                        state.Polygons.Add(polygon);
                    }
                }
                states.Add(state);
            }
            return new Map() { states = states };
        }
    }
}
