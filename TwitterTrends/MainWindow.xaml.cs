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
using System.Globalization;
using System.ComponentModel;
using TwitterTrends.Properties;
using System.Runtime.CompilerServices;

namespace TwitterTrends
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FormWindow();
            presenter.Content = new MainPage(this);
        }
        private void FormWindow()
        {
            this.WindowState = WindowState.Maximized;
        }    
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
