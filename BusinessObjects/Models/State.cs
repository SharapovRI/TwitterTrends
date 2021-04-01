using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TwitterTrends.Models
{
    public class State
    {
        public string StateId;
        public List<Polygon> Polygons = new List<Polygon>();
        public float? weight;
        public Brush Color = Brushes.Gray;
    }
}
