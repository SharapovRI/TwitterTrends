using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media;

namespace TwitterTrends.Models
{
    public class Map
    {
        private static Map instance;
        public static Map getInstance()
        {
            if (instance == null)
                instance = new Map();
            return instance;
        }


        public List<State> CurrentStates 
        {
            get
            {
                return currentStates;
            }
            set
            {
                currentStates = value;
                PaintStates();
            }
        }
        public List<Tweet> CurrentTweets
        {
            get
            {
                return currentTweets;
            }
            set
            {
                currentTweets = value;
                PaintTweets();
            }
        }

        private List<State> currentStates = new List<State>();
        private List<Tweet> currentTweets = new List<Tweet>();
        public float YCOMPRESSION;
        public float XCOMPRESSION;
        public float YOFFSET;
        public float XOFFSET;        
               


        //метод который будт считать настроянния всех штатаов и расскрашивать их(либо могу сделать отдельный метод, который считает и отдельный который разукрашивает, я думаю это будет правильней)
        //это пишет Дима
        public void PaintTweets()
        {
            float? mostNegative = currentTweets.Min(t=>t.Weight);
            float? mostPositive = currentTweets.Max(t=>t.Weight);
            foreach(var tweet in currentTweets)
            {
                tweet.Color = GetTweetColor(tweet.Weight, mostNegative, mostPositive);
            }
        }
        private Brush GetTweetColor(float? tweetMood, float? mosNegative, float? mostPositive)
        {
            if (tweetMood == null)
            {
                return Brushes.Gray;
            }
            else if (tweetMood == 0)
            {
                return Brushes.White;
            }
            else if (tweetMood > 0 && tweetMood < mostPositive / 2)
            {
                float rgbValue = (float)(255 - (float)(tweetMood / 2 * 255 / mostPositive));
                return new SolidColorBrush(Color.FromRgb(255, 255, (byte)rgbValue));
            }
            else if (tweetMood >= mostPositive / 2)
            {
                float rgbValue = (float)(255 - (float)(tweetMood * 255 / mostPositive));
                return new SolidColorBrush(Color.FromRgb(255, (byte)rgbValue, 0));
            }
            else if (tweetMood < 0 && tweetMood > mosNegative / 2)
            {
                float rgbValue = (float)(255 - (-1) * (float)(tweetMood / 2 * 255 / mosNegative));
                return new SolidColorBrush(Color.FromRgb((byte)rgbValue, 255, 255));
            }
            else
            {
                float rgbValue = (float)(255 - (-1) * (float)(tweetMood * 255 / mosNegative));
                return new SolidColorBrush(Color.FromRgb(0, (byte)rgbValue, 255));
            }
        }
        public Dictionary<string, float?> CalculateStatesMood()
        {            
            Dictionary<string, float?> statesMood = new Dictionary<string, float?>();            
            for(int i = 0; i < CurrentStates.Count; i++ )
            {                
                List<Tweet> twitts = CurrentTweets.Where(t => t.StateId == CurrentStates[i].StateId).ToList();
                if(twitts.Where(t => t.Weight == null).Count() == twitts.Count())
                {
                    statesMood.Add(CurrentStates[i].StateId, null);
                    CurrentStates[i].weight = null;
                }
                else
                {
                    float stateMood = 0;
                    foreach (var twitt in twitts.Where(t => t.Weight != null))
                    {
                        stateMood += (float)twitt.Weight;
                    }                    
                    statesMood.Add(CurrentStates[i].StateId, stateMood / twitts.Where(t => t.Weight != null).Count());
                    CurrentStates[i].weight = stateMood / twitts.Where(t => t.Weight != null).Count();
                }                               
            }            
            return statesMood;
        }
        public void PaintStates()
        {
            var mood = CalculateStatesMood();
            float? mostNegative = mood.Values.Min();
            float? mostPositive = mood.Values.Max();

            for (int i = 0; i < CurrentStates.Count(); i++)
            {
                PaitPolygons(CurrentStates[i], GetStateColor(CurrentStates[i].weight, mostNegative, mostPositive));
            }
        }
        private void PaitPolygons(State state, Brush brush)
        {
            for (int i = 0; i < state.Polygons.Count; i++)
            {
                state.Color = brush;
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
            else if (stateMood > 0 && stateMood < mostPositive / 2)
            {
                float rgbValue = (float)(255 - (float)(stateMood / 2 * 255 / mostPositive));
                return new SolidColorBrush(Color.FromRgb(255, 255, (byte)rgbValue));
            }
            else if (stateMood >= mostPositive / 2)
            {
                float rgbValue = (float)(255 - (float)(stateMood * 255 / mostPositive));
                return new SolidColorBrush(Color.FromRgb(255, (byte)rgbValue, 0));
            }
            else if (stateMood < 0 && stateMood > mosNegative / 2)
            {
                float rgbValue = (float)(255 - (-1) * (float)(stateMood / 2 * 255 / mosNegative));
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
