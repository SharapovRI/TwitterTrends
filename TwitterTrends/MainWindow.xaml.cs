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
            foreach (var state in states)
            {
                foreach (var polygon in state.Polygons)
                {
                    //каждый полигон будет представляться объектом Path.
                    Path polygonPath = new Path();
                    polygonPath.Fill = Brushes.Black;

                    //в свою очередь объект Path берет данные из объекта PathGeometry
                    PathGeometry polygonPathGeometry = new PathGeometry();
                    polygonPath.Data = polygonPathGeometry;


                    PathFigure polygonPathFigure = new PathFigure();
                    polygonPathFigure.StartPoint = new Point(polygon.Coordinates[polygon.Coordinates.Count - 1].X * 5 * 1.4 + 2900, polygon.Coordinates[polygon.Coordinates.Count - 1].Y * 5 * -2 + 1500);

                    PolyLineSegment polygonPolyLineSegment = new PolyLineSegment();


                    for (int i = 0; i < polygon.Coordinates.Count; i++)
                    {
                        polygonPolyLineSegment.Points.Add(new Point(polygon.Coordinates[i].X * 5 * 1.4 + 2900, polygon.Coordinates[i].Y * 5 * -2 + 1500));
                    }

                    polygonPathFigure.Segments.Add(polygonPolyLineSegment);
                    polygonPathGeometry.Figures.Add(polygonPathFigure);
                    gridMap.Children.Add(polygonPath);
                }
            }
        }

        private void DrawMap1()
        {
            foreach (var state in states)
            {
                foreach (var polygon in state.Polygons)
                {
                    //каждый полигон будет представляться объектом Path.
                    Path polygonPath = new Path();
                    polygonPath.Fill = Brushes.Black;

                    //в свою очередь объект Path берет данные из объекта PathGeometry
                    PathGeometry polygonPathGeometry = new PathGeometry();
                    polygonPath.Data = polygonPathGeometry;


                    PathFigure polygonPathFigure = new PathFigure();
                    polygonPathFigure.StartPoint = new Point(polygon.Coordinates[polygon.Coordinates.Count - 1].X * 5 * 1.4 + 2900, polygon.Coordinates[polygon.Coordinates.Count - 1].Y * 5 * -2 + 1500);

                    PolyLineSegment polygonPolyLineSegment = new PolyLineSegment();


                    for (int i = 0; i < polygon.Coordinates.Count; i++)
                    {
                        polygonPolyLineSegment.Points.Add(new Point(polygon.Coordinates[i].X * 5 * 1.4 + 2900, polygon.Coordinates[i].Y * 5 * -2 + 1500));
                    }

                    polygonPathFigure.Segments.Add(polygonPolyLineSegment);
                    polygonPathGeometry.Figures.Add(polygonPathFigure);
                    gridMap.Children.Add(polygonPath);
                }
            }
            //System.Windows.Shapes.Polygon p = new System.Windows.Shapes.Polygon();
            //foreach (var state in states)
            //{
            //    foreach (var polygon in state.Polygons)
            //    {
            //        System.Windows.Shapes.Polygon p = new System.Windows.Shapes.Polygon();
            //        p.Fill = Brushes.Red;
            //        p.Stroke = Brushes.Blue;
            //        p.StrokeThickness = 4;
            //        foreach (var coordinate in polygon.Coordinates)
            //        {
            //            p.Points.Add(new Point(coordinate.X * 5 * 1.4 + 2900, coordinate.Y * 5 * -2 + 1500));
            //        }
            //        gridMap.Children.Add(p);
            //    }
            //}
        }
    }
}
