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
            this.StateName = StateName;
        }
        public string StateName;
        public List<Coordinate> Coordinates = new List<Coordinate>();
    }
}
