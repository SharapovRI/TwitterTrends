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



        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            states = JsonParser.ParseStates(JSON_PATH);
            Map.GiveStates(states);
            Dictionary<string, float> d = SantimentsParser.ParseWords(SENTIMENTS_PATH);
            DrawMap();           
            
            DrawMap();*/

            /*string s = "Бык тупогуб, тупогубенький бычок, у быка губа бела была тупа";
            string pat = @"\w*";
            string pattern = pat + @"\S";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(s);*/
            List<Twitt> twitts = new List<Twitt>();
            twitts.Add(new Twitt(new Coordinate(12, 12), System.DateTime.Now, "Aladin qwe abatable"));
            twitts.Add(new Twitt(new Coordinate(12, 12), System.DateTime.Now, "a great deal gulyzt'"));
            twitts.Add(new Twitt(new Coordinate(12, 12), System.DateTime.Now, "qwqr abruptly-pinnate leaf qwqwrqrq"));

            Searching searching = new Searching(twitts, SantimentsParser.ParseWords(SENTIMENTS_PATH));
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
                        plg.Points.Add(new Point(coordinate.X* XCOMPRESSION + XOFFSET, coordinate.Y* YCOMPRESSION + YOFFSET));
                    }
                    plg.Stroke = Brushes.Black;
                    plg.Fill = Brushes.Gray;
                    gridMap.Children.Add(plg);                                    
                }                
            }            
        }         
    }
}
