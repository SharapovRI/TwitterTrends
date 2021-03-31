using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TwitterTrends.Analize;
using TwitterTrends.Models;
using TwitterTrends.Parsers;
using System.Windows.Input;

namespace TwitterTrends
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private const string JSON_PATH = @"../../Files/states.json";
        private const string SENTIMENTS_PATH = @"../../Files/sentiments.csv";
        Map map = new Map();
        HashSet<string> hashset = new HashSet<string>();
        private static List<Tweet> twitts1 = new List<Tweet>();



        public MainWindow()
        {
            InitializeComponent();
            FormWindow();
            FormMap();
            StateChecker.GiveStates(map.CurrentStates);           
            List<Tweet> twitts = Tweetparcer.Twittparce(@"../../Files/Tweets/football_tweets2014.txt");
            var hashtable = SantimentsParser.ParseWords(SENTIMENTS_PATH, ref hashset);
            new Searching(twitts, hashtable, this.hashset);
            map.CurrentTweets = twitts;
            DrawMap();
        }

        //public MainWindow()
        //{
        //    InitializeComponent();
        //    FormWindow();
        //    FormMap();
        //    StateChecker.GiveStates(map.CurrentStates);
        //    //List<Twitt> twitts = Tweetparcer.Twittparce(@"../../Files/weekend_tweets2014.txt");
        //    List<Twitt> twitts = Tweetparcer.Twittparce(@"../../Files/Tweets/football_tweets2014.txt");
        //    //Tweetparcer.AsyncParse(@"../../Files/tweets2011.txt");
        //    //ParseTw(@"../../Files/tweets2011.txt");
        //    //Tweetparcer.AsyncParse(@"../../Files/weekend_tweets2014.txt");
        //    //GetId(twitts1);
        //    new Searching(twitts, SantimentsParser.ParseWords(SENTIMENTS_PATH, ref hashset), hashset);
        //    map.CurrentTwitts = twitts;
        //    DrawMap();
        //}        

        internal static void GiveTwitts(List<Tweet> tweets)
        {
            twitts1 = tweets;
        }
        
        public void DrawMap()
        {
            DrawStates();
            DrawTweets();
        }
        public void DrawStates()
        {
            map.PaintStates();
            foreach (var state in map.CurrentStates)
            {                               
                foreach (var polygon in state.Polygons)
                {
                    System.Windows.Shapes.Polygon currentPolygon = new System.Windows.Shapes.Polygon();
                    foreach (var coordinate in polygon.Coordinates)
                    {
                        currentPolygon.Points.Add(new Point(coordinate.Y * map.YCOMPRESSION + map.YOFFSET, coordinate.X * map.XCOMPRESSION + map.XOFFSET));                                                
                    }                   
                    currentPolygon.Stroke = Brushes.Black;
                    currentPolygon.StrokeThickness = 0.4;
                    currentPolygon.Fill = state.Color;
                    gridMap.Children.Add(currentPolygon);                    
                }                
            }            
        }
        public void DrawTweets()
        {
            map.PaintTweets();
            foreach(var t  in map.CurrentTweets)
            {
                System.Windows.Shapes.Polygon polygon = new System.Windows.Shapes.Polygon();
                if (t.Weight == null)
                    continue;
                polygon.Points.Add(new Point(t.TwittCoordinate.Y*map.YCOMPRESSION+ map.YOFFSET+2, t.TwittCoordinate.X * map.XCOMPRESSION+map.XOFFSET+ 2));
                polygon.Points.Add(new Point(t.TwittCoordinate.Y*map.YCOMPRESSION+ map.YOFFSET+ 2, t.TwittCoordinate.X * map.XCOMPRESSION+map.XOFFSET- 2));
                polygon.Points.Add(new Point(t.TwittCoordinate.Y*map.YCOMPRESSION+ map.YOFFSET- 2, t.TwittCoordinate.X * map.XCOMPRESSION+map.XOFFSET- 2));
                polygon.Points.Add(new Point(t.TwittCoordinate.Y*map.YCOMPRESSION+ map.YOFFSET- 2, t.TwittCoordinate.X * map.XCOMPRESSION+map.XOFFSET+ 2));
                polygon.Stroke = Brushes.Black;
                polygon.StrokeThickness = 0.1;
                polygon.Fill = t.Color;
                gridMap.Children.Add(polygon);
            }
        }

        private void SomeMethod(object sender, MouseEventArgs e)
        {
            //((System.Windows.Shapes.Polygon)sender).RenderSize = new Size(((System.Windows.Shapes.Polygon)sender).RenderSize.Width * 2, ((System.Windows.Shapes.Polygon)sender).RenderSize.Height * 2);
            /*int i = gridMap.Children.IndexOf((System.Windows.Shapes.Polygon)sender);
            gridMap.Children[i].RenderSize = new Size(((System.Windows.Shapes.Polygon)sender).RenderSize.Width + 200, ((System.Windows.Shapes.Polygon)sender).RenderSize.Height * 2);*/
            ((System.Windows.Shapes.Polygon)sender).StrokeThickness = 8;
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
            map.CurrentTweets = twitts1;
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
                TreeViewItem new_item = new TreeViewItem();
                new_item.Header = file;
                tviChooseFile.Items.Add(new_item);
                new_item.Selected += Item_Selected;
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
                TreeViewItem new_item = new TreeViewItem();
                new_item.Header = ofd.SafeFileName;
                tviChooseFile.Items.Add(new_item);
            }
        }

        private void Item_Selected(object sender, RoutedEventArgs e)
        {
            string new_filename;
            TreeViewItem selectedItem = (TreeViewItem)tvFiles.SelectedItem;
            new_filename = selectedItem.Header.ToString();
            gridMap.Children.Clear();
            List<Tweet> twitts = Tweetparcer.Twittparce(new_filename);
            new Searching(twitts, SantimentsParser.ParseWords(SENTIMENTS_PATH, ref hashset), hashset);
            map.CurrentTweets = twitts;
            DrawMap();
        }
    }
}
