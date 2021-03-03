using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
    public class Coordinate
    {
        public Coordinate(float x, float y)
        {
            X = x;
            Y = y;
        }
        public float X;
        public float Y;
    }
}
