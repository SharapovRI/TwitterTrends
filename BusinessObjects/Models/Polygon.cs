using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
    public class Polygon
    {       
        public Polygon(string StateName)
        {
            this.StateId = StateName;
        }
        public string StateId;
        public List<Coordinate> Coordinates = new List<Coordinate>();
        public float Max_lat = -180;
        public float Max_lng = -180; //x
        public float Min_lat = 180; //y
        public float Min_lng = 180;
    }
}
