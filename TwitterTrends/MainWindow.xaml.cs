﻿using System.Collections.Generic;
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
        private const float XCOMPRESSION = 14;
        private const float YCOMPRESSION = -20;
        private const float XOFFSET = 2500;
        private const float YOFFSET = 1500;



        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
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
