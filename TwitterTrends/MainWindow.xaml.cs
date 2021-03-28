using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
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
        private static List<Twitt> twitts1 = new List<Twitt>();



        public MainWindow()
        {            
            InitializeComponent();
            FormWindow();
            FormMap();
            StateChecker.GiveStates(map.CurrentStates);
            //List<Twitt> twitts = Tweetparcer.Twittparce(@"../../Files/weekend_tweets2014.txt");
            List<Twitt> twitts = Tweetparcer.Twittparce(@"../../Files/Tweets/texas_tweets2014.txt");
            //Tweetparcer.AsyncParse(@"../../Files/tweets2011.txt");
            //ParseTw(@"../../Files/tweets2011.txt");
            //Tweetparcer.AsyncParse(@"../../Files/weekend_tweets2014.txt");
            //GetId(twitts1);
            new Searching(twitts, SantimentsParser.ParseWords(SENTIMENTS_PATH, ref hashset), map.CurrentStates, hashset);
            twitts1 = twitts;
            map.CurrentTwitts = twitts;
            //Thread.Sleep(10000);
            DrawMap();                       
        }

        public async Task ParseTw(string path)
        {
            var result = Task.Run(async () => { return await Tweetparcer.AsyncParse(path); }).Result;
        }

        internal static void GiveTwitts(List<Twitt> twitts)
        {
            twitts1 = twitts;
        }

        async private void GetId(List<Twitt> twitts)
        {
            await Task.Run(() => StateChecker.AsyncFromTweets(twitts));
        }

        public void DrawMap()
        {
            map.PaintStates();
            foreach (var state in map.CurrentStates)
            {                               
                foreach (var polygon4 in state.Polygons)
                {                                                         
                    foreach(var coordinate in polygon4.Coordinates)
                    {
                        polygon4.graphicalPolygon.Points.Add(new Point(coordinate.Y* map.YCOMPRESSION + map.YOFFSET, coordinate.X* map.XCOMPRESSION + map.XOFFSET));                        
                    }                   
                    polygon4.graphicalPolygon.Stroke = Brushes.Black;
                    
                    gridMap.Children.Add(polygon4.graphicalPolygon);                    
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
            map.CurrentStates= JsonParser.ParseStates(JSON_PATH);
            map.YCOMPRESSION = 14;
            map.XCOMPRESSION = -20;
            map.YOFFSET = 2500;
            map.XOFFSET = 1500;
            map.CurrentTwitts = twitts1;
        }
        private void FormWindow()
        {
            this.WindowState = WindowState.Maximized;
            FormTreeView();
        }
        private void FormTreeView()
        {
            var TweetFiles = Directory.GetFiles(@"../../Files/Tweets");    
            foreach(var file in TweetFiles)
            {
                tviChooseFile.Items.Add(file);
            }
        }
        private void btnNewFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Title = "Choose image",
                Filter = "TXT|*.txt;",
            };
            if(ofd.ShowDialog() == true)
            {
                File.Copy(ofd.FileName, @"../../Files/Tweets/" + ofd.SafeFileName);
                tviChooseFile.Items.Add(ofd.SafeFileName);
            }
        }
    }
}
