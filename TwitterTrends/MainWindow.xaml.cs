using GMap.NET;

using GMap.NET.WindowsPresentation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
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
        HashSet<string> hashset = new HashSet<string>();
        Map map = new Map();



        public MainWindow()
        {
            FormWindow();
            InitializeComponent();
            StateChecker.GiveStates(map.states);            
            List<Twitt> twitts = Tweetparcer.Twittparce(@"../../Files/cali_tweets2014.txt");            
            new Searching(twitts, SantimentsParser.ParseWords(SENTIMENTS_PATH, ref hashset), map.states, hashset);            
            FormMap();
            DrawMap();                       
        }

        public async Task ParseTw(string path)
        {
            var result = Task.Run(async () => { return await Tweetparcer.AsyncParse(path); }).Result;
        }

        async private void GetId(List<Twitt> twitts)
        {
            await Task.Run(() => StateChecker.AsyncFromTweets(twitts));
        }

        public void DrawMap()
        {            
            foreach (var state in map.states)
            {                               
                foreach (var polygon in state.Polygons)
                {                                                      
                    foreach(var coordinate in polygon.Coordinates)
                    {
                       polygon.polygon.Points.Add(new Point(coordinate.Y* map.YCOMPRESSION + map.YOFFSET, coordinate.X* map.XCOMPRESSION + map.XOFFSET));                        
                    }
                    polygon.polygon.Stroke = Brushes.Black;
                    polygon.polygon.Fill = Brushes.Green;
                    gridMap.Children.Add(polygon.polygon);                    
                }
                
            }            
        }
        private void ZoomViewbox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var position = e.GetPosition(gridMap);
            stMap.CenterX = position.X;
            stMap.CenterY = position.Y;


            if (e.Delta > 0)
            {         
                
                stMap.ScaleX += 0.2;
                stMap.ScaleY += 0.2;
            }
            else
            {
                if (stMap.ScaleX > 1 && stMap.ScaleY > 1)
                {
                    stMap.ScaleX -= 0.2;
                    stMap.ScaleY -= 0.2;
                }
            }
        }
        private void FormMap()
        {
            map.states = JsonParser.ParseStates(JSON_PATH);
            map.YCOMPRESSION = 14;
            map.XCOMPRESSION = -20;
            map.YOFFSET = 2500;
            map.XOFFSET = 1500;
        }
        private void FormWindow()
        {
            this.WindowState = WindowState.Maximized;
        }
    }
}
