using GMap.NET;

using GMap.NET.WindowsPresentation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        List<Twitt> twitts;



        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            states = JsonParser.ParseStates(JSON_PATH);
            Map.GiveStates(states);
            twitts = Tweetparcer.Twittparce(@"../../Files/cali_tweets2014.txt");
            Searching searching = new Searching(twitts, SantimentsParser.ParseWords(SENTIMENTS_PATH), states);
            DrawMap();
            DrawTwitts();
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

        public void DrawTwitts()
        {
            foreach(var twitt in twitts)
            {
                Ellipse ellipse = new Ellipse { Width = 5, Height = 5 };
                double left = (twitt.TwittCoordinate.Y * XCOMPRESSION + XOFFSET);
                double top = (twitt.TwittCoordinate.X * YCOMPRESSION + YOFFSET);
                ellipse.Margin = new Thickness(left, top, 0, 0);
                ellipse.Fill = Brushes.Red;               
                gridMap.Children.Add(ellipse);                               
            }
        }      
    }
}
