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
        private List<State> states;
        public MainWindow()
        {
            InitializeComponent();
            states = JsonParser.ParseStates(JSON_PATH);
            DrawMap();
        }



        public void DrawMap()
        {
            
            foreach(var state in states)
            {
                foreach(var polygon4 in state.Polygons)
                {
                    System.Windows.Shapes.Polygon plg = new System.Windows.Shapes.Polygon();                    
                    foreach(var coordinate in polygon4.Coordinates)
                    {
                        plg.Points.Add(new Point(coordinate.X*(10)+2000, coordinate.Y*(-10)+1000));
                    }
                    plg.Stroke = Brushes.Red;
                    plg.Fill = Brushes.Black;
                    gridMap.Children.Add(plg);
                }
            }            
        }         
    }
}
