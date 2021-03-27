using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
    public class Polygon
    {

       // public System.Windows.Shapes.Polygon graphicalPolygon = new System.Windows.Shapes.Polygon();
        public Polygon(string StateName)
        {
            this.StateId = StateName;
        }
        public string StateId;
        public List<Coordinate> Coordinates = new List<Coordinate>();
        public float max_lat = -180;
        public float max_lng = -180; //x
        public float min_lat = 180; //y
        public float min_lng = 180;
    }
}
