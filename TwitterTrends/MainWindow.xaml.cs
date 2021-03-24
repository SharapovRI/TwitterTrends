using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
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
        private Map map;
        private List<Twitt> currentTwitts = new List<Twitt>();
        public MainWindow()
        {
            InitializeComponent();
            FormWindow();
            FormMap();            
            DrawMap();
        }

        public void DrawMap()
        {
            foreach (var state in map.states)
            {
                foreach (var polygon4 in state.Polygons)
                {
                    System.Windows.Shapes.Polygon plg = new System.Windows.Shapes.Polygon();
                    foreach (var coordinate in polygon4.Coordinates)
                    {
                        plg.Points.Add(new Point(coordinate.Y * map.YCOMPRESSION + map.YOFFSET, coordinate.X * map.XCOMPRESSION + map.XOFFSET));
                    }
                    plg.Stroke = Brushes.Black;
                    plg.Fill = Brushes.Green;
                    gridMap.Children.Add(plg);
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
            map = JsonParser.ParseStates(JSON_PATH);
            map.YCOMPRESSION = 14;
            map.XCOMPRESSION = -20;
            map.YOFFSET = 2500;
            map.XOFFSET = 1500;
        }
        private void FormWindow()
        {
            this.WindowState = WindowState.Maximized;
        }

        private void btnAddFile_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
