using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
    public class Map
    {
        public List<State> states = new List<State>();
        public float YCOMPRESSION;
        public float XCOMPRESSION;
        public float YOFFSET;
        public float XOFFSET;
        public List<Twitt> currentTwitts = new List<Twitt>();



        //метод который будт считать настроянния всех штатаов и расскрашивать их(либо могу сделать отдельный метод, который считает и отдельный который разукрашивает, я думаю это будет правильней)
        //это пишет Дима
        public void CalculateMood()
        {

        }
    }
}
