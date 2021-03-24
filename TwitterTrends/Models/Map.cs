using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTrends.Models
{
    public class Map
    {
        private List<State> currentStates = new List<State>();
        public List<Twitt> CurrentTwitts = new List<Twitt>();
        public float YCOMPRESSION;
        public float XCOMPRESSION;
        public float YOFFSET;
        public float XOFFSET;        
        public List<State> CurrentStates
        {
            get
            {
                return new List<State>(currentStates);
            }
            set
            {
                currentStates = value;
                foreach(var s in currentStates)
                {
                    s.weight = 0;
                }
            }
        }
        public float? currentMood;


        //метод который будт считать настроянния всех штатаов и расскрашивать их(либо могу сделать отдельный метод, который считает и отдельный который разукрашивает, я думаю это будет правильней)
        //это пишет Дима
        public Dictionary<string, float> CalculateStatesMood()
        {
            Dictionary<string, float> statesMood = new Dictionary<string, float>(); 
            foreach(var s in currentStates)
            {
                List<Twitt> twitts = CurrentTwitts.Where(t => t.idState == s.StateId).ToList();
                float stateMood = 0;                
                foreach(var twitt in twitts.Where(t => t.weight!=null))
                {
                    stateMood += (float)twitt.weight;
                }
                statesMood.Add(s.StateId, stateMood/ twitts.Where(t => t.weight != null).Count());
            }
            return statesMood;
        }

        public void PaintStates()
        {

        }
    }
}
