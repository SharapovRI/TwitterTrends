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
using TwitterTrends.Parsers;
using System.Windows.Input;
using ServiceAction;
using BusinessObjects;
using DataObjects;
using TwitterTrends.Models;

namespace TwitterTrends
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string JSON_PATH = @"../../../DataObjects/Files/states.json";
        private const string SENTIMENTS_PATH = @"../../../DataObjects/Files/sentiments.csv";
        private const string DEFAULT_TWEETS_PATH = @"../../../DataObjects/Files/Tweets/football_tweets2014.txt";
        Map map = Map.getInstance();
        static Service service = new Service();


        public MainWindow()
        {
            InitializeComponent();
            FormWindow();
            FormMap();
            service.AnalizeTweets(DEFAULT_TWEETS_PATH);
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

        public void DrawMap()
        {
            DrawStates();
            DrawTweets();
        }
        public void DrawStates()
        {
            service.PaintStates();
            foreach (var state in service.GetStates())
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
                    currentPolygon.ToolTip = state.StateId;
                    gridMap.Children.Add(currentPolygon);
                }
            }
        }

        public void DrawTweets()
        {
            service.PaintTweets();
            foreach (var tweet in service.GetTweets())
            {
                System.Windows.Shapes.Polygon polygon = new System.Windows.Shapes.Polygon() { Stroke = Brushes.Black, StrokeThickness = 0.1 };
                if (tweet.Weight == null)
                    continue;
                polygon.Points.Add(new Point(tweet.TwittCoordinate.Y * map.YCOMPRESSION + map.YOFFSET + 2, tweet.TwittCoordinate.X * map.XCOMPRESSION + map.XOFFSET + 2));
                polygon.Points.Add(new Point(tweet.TwittCoordinate.Y * map.YCOMPRESSION + map.YOFFSET + 2, tweet.TwittCoordinate.X * map.XCOMPRESSION + map.XOFFSET - 2));
                polygon.Points.Add(new Point(tweet.TwittCoordinate.Y * map.YCOMPRESSION + map.YOFFSET - 2, tweet.TwittCoordinate.X * map.XCOMPRESSION + map.XOFFSET - 2));
                polygon.Points.Add(new Point(tweet.TwittCoordinate.Y * map.YCOMPRESSION + map.YOFFSET - 2, tweet.TwittCoordinate.X * map.XCOMPRESSION + map.XOFFSET + 2));
                polygon.Fill = tweet.Color;
                gridMap.Children.Add(polygon);
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
            service.FormMap(JSON_PATH, 14, -20, 2500, 1500);
        }
        private void FormWindow()
        {
            this.WindowState = WindowState.Maximized;
            FormTreeView();
        }
        private void FormTreeView()
        {
            var TweetFiles = Directory.GetFiles(@"../../../DataObjects/Files/Tweets");
            foreach (var file in TweetFiles)
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
            if (ofd.ShowDialog() == true)
            {
                File.Copy(ofd.FileName, @"../../../DataObjects/Files/Tweets/" + ofd.SafeFileName);
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
            service.AnalizeTweets(new_filename);
            DrawMap();
        }
    }
}
