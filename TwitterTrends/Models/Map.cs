using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
        public Dictionary<string, float?> CalculateStatesMood()
        {            
            Dictionary<string, float?> statesMood = new Dictionary<string, float?>();            
            for(int i = 0; i < currentStates.Count; i++ )
            {                
                List<Twitt> twitts = CurrentTwitts.Where(t => t.idState == currentStates[i].StateId).ToList();
                if(twitts.Where(t => t.weight == null).Count() == twitts.Count())
                {
                    statesMood.Add(currentStates[i].StateId, null);
                    currentStates[i].weight = null;
                }
                else
                {
                    float stateMood = 0;
                    foreach (var twitt in twitts.Where(t => t.weight != null))
                    {
                        stateMood += (float)twitt.weight;
                    }                    
                    statesMood.Add(currentStates[i].StateId, stateMood / twitts.Where(t => t.weight != null).Count());
                    currentStates[i].weight = stateMood / twitts.Where(t => t.weight != null).Count();
                }                               
            }            
            return statesMood;
        }

        public void PaintStates()
        {
            var mood = CalculateStatesMood();
            float? mostNegative = mood.Values.Min();            
            float? mostPositive = mood.Values.Max();            

            for (int i = 0; i < currentStates.Count(); i++)
            {
                PaitPolygons(currentStates[i], GetStateColor(currentStates[i].weight, mostNegative, mostPositive));
            }
        }

        private void PaitPolygons(State state, Brush brush)
        {
            for(int i = 0; i < state.Polygons.Count; i++)
            {
                state.Polygons[i].graphicalPolygon.Fill = brush;
            }
        }
        private Brush GetStateColor(float? stateMood, float? mosNegative, float? mostPositive)
        {
            if (stateMood == null)
            {
                return Brushes.Gray;
            }
            else if (stateMood == 0)
            {
                return Brushes.White;
            }
            else if (stateMood > 0 && stateMood < mostPositive/2)
            {
                float rgbValue = (float)(255 - (float)(stateMood/2 * 255 / mostPositive));
                return new SolidColorBrush(Color.FromRgb(255, 255, (byte)rgbValue));
            }
            else if (stateMood >= mostPositive/2)
            {
                float rgbValue = (float)(255 - (float)(stateMood * 255 / mostPositive));
                return new SolidColorBrush(Color.FromRgb(255, (byte)rgbValue, 0));
            }
            else if (stateMood < 0 && stateMood > mosNegative/2)
            {
                float rgbValue = (float)(255 - (-1)*(float)(stateMood / 2 * 255 / mosNegative));
                return new SolidColorBrush(Color.FromRgb((byte)rgbValue, 255, 255));
            }
            else
            {
                float rgbValue = (float)(255 - (-1) * (float)(stateMood * 255 / mosNegative));
                return new SolidColorBrush(Color.FromRgb(0, (byte)rgbValue, 255));
            }
        }

    }
}
