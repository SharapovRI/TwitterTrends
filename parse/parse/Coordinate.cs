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
            this.x = x;
            this.y = y;
        }
        private float x;
        private float y;

        public float X
        {
            get
            {
                return x;
            }
            set
            {
                if (value > 90 || value < -90)
                    //throw new ArgumentOutOfRangeException("Неверное значение широты");
                x = value;
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value > 180 || value < -180)
                    //throw new ArgumentOutOfRangeException("Неверное значение долготы");
                y = value;
            }
        }
    }
}