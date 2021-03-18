using GMap.NET;

using GMap.NET.WindowsPresentation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using TwitterTrends.Analize;
using TwitterTrends.Models;
using TwitterTrends.Parsers;

namespace TwitterTrends
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private const string JSON_PATH = @"../../Files/states.json";
        private const string SENTIMENTS_PATH = @"../../Files/sentiments.csv";
        private List<State> states;
        private const float XCOMPRESSION = 14;
        private const float YCOMPRESSION = -20;
        private const float XOFFSET = 2500;
        private const float YOFFSET = 1500;
        HashSet<string> hashset = new HashSet<string>();



        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            states = JsonParser.ParseStates(JSON_PATH);
            Map.GiveStates(states);
            List<Twitt> twitts = Tweetparcer.Twittparce(@"../../Files/weekend_tweets2014.txt");
            //List<Twitt> twitts = Tweetparcer.Twittparce(@"../../Files/tweets2011.txt");
            GetId(twitts);
            new Searching(twitts, SantimentsParser.ParseWords(SENTIMENTS_PATH, ref hashset), states, hashset);
            DrawMap();                       
        }

        async private void GetId(List<Twitt> twitts)
        {
            await Task.Run(() => Map.AsyncFromTweets(twitts));
        }

        public void DrawMap()
        {            
            foreach(var state in states)
            {                
                foreach (var polygon4 in state.Polygons)
                {
                    System.Windows.Shapes.Polygon plg = new System.Windows.Shapes.Polygon();                    
                    foreach(var coordinate in polygon4.Coordinates)
                    {
                        plg.Points.Add(new Point(coordinate.Y* XCOMPRESSION + XOFFSET, coordinate.X* YCOMPRESSION + YOFFSET));
                    }
                    plg.Stroke = Brushes.Black;
                    plg.Fill = Brushes.Gray;
                    gridMap.Children.Add(plg);                                    
                }                
            }            
        }         
    }
}
