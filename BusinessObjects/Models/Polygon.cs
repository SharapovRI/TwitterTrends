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
        public Coordinate Max_lat = new Coordinate(0,-180);
        public Coordinate Max_lng = new Coordinate(-180, 0); //x
        public Coordinate Min_lat = new Coordinate(0, 180); //y
        public Coordinate Min_lng = new Coordinate(180, 0);
    }
}
