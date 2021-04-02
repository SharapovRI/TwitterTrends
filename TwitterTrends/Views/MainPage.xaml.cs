using Microsoft.Win32;
using ServiceAction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwitterTrends.Models;
using TwitterTrends.Views;

namespace TwitterTrends
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        private const string JSON_PATH = @"../../../DataObjects/Files/states.json";
        private const string SENTIMENTS_PATH = @"../../../DataObjects/Files/sentiments.csv";
        private const string DEFAULT_TWEETS_PATH = @"../../../DataObjects/Files/Tweets/cali_tweets2014.txt";
        Map map = Map.GetInstance();
        static Service service = new Service();
        MainWindow MainWindow;

        public MainPage(MainWindow mainWindow)
        {
            this.MainWindow = mainWindow;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
            for (int i = 0; i <= LanList.Items.Count - 1; i++)
                if (((ComboBoxItem)LanList.Items.GetItemAt(i)).Content.ToString() == Properties.Settings.Default.Language)
                    LanList.SelectedIndex = i;
            FormComboBox();
            FormMap();
            DrawMap();
        }
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
                polygon.ToolTip = "Text: " + tweet.Text + "\n" + "Weight: " + tweet.Weight + "\n" + "State: " + tweet.StateId;
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
        private void FormComboBox()
        {
            var TweetFiles = Directory.GetFiles(@"../../../DataObjects/Files/Tweets");

            foreach (var file in TweetFiles)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = file.Replace(@"../../../DataObjects/Files/Tweets\", "");
                comboBoxItem.Selected += ComboBoxItem_Selected;
                cbFiles.Items.Add(comboBoxItem);
            }
        }
        async private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            gridMap.Children.Clear();
            var name = ((ComboBoxItem)sender).Content.ToString();
            await Task.Run(() => service.AnalizeTweets(@"../../../DataObjects/Files/Tweets\" + name));
            DrawMap();
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
                ComboBoxItem new_item = new ComboBoxItem();
                new_item.Content = ofd.SafeFileName;
                new_item.Selected += ComboBoxItem_Selected;
                cbFiles.Items.Add(new_item);
            }
        }

        private void LanList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ComboBoxItem)LanList.SelectedItem).Content.ToString())
            {
                case "RU":
                    {
                        Properties.Settings.Default.Language = "ru-RU";
                        Properties.Settings.Default.Save();
                        break;
                    }
                case "EN":
                    {
                        Properties.Settings.Default.Language = "en-EN";
                        Properties.Settings.Default.Save();
                        break;
                    }
                case "DE":
                    {
                        Properties.Settings.Default.Language = "de-DE";
                        Properties.Settings.Default.Save();
                        break;
                    }
                case "FR":
                    {
                        Properties.Settings.Default.Language = "fr-FR";
                        Properties.Settings.Default.Save();
                        break;
                    }
                case "SP":
                    {
                        Properties.Settings.Default.Language = "es-ES";
                        Properties.Settings.Default.Save();
                        break;
                    }
                default:
                    Properties.Settings.Default.Language = "en-EN";
                    Properties.Settings.Default.Save();
                    break;
            }
            string message = "The language will be changed after the app is restarted.";
            MessageBox.Show(message);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.MainWindow.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutView aboutView = AboutView.GetInstace(MainWindow, this);
            MainWindow.presenter.Content = aboutView;
        }
    }
}
