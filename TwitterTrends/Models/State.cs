using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
    public class State
    {
        public string StateId { get; set; }
        public List<Polygon> Polygons = new List<Polygon>();
        public float weight;       
    }
}
