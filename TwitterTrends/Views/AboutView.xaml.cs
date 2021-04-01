using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace TwitterTrends.Views
{
    /// <summary>
    /// Логика взаимодействия для AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl
    {
        MainWindow MainWindow;
        MainPage MainPage;
        public AboutView(MainWindow mainWindow, MainPage mainPage)
        {
            this.MainWindow = mainWindow;
            this.MainPage = mainPage;
            InitializeComponent();           
        }

        private static AboutView instance;

        public static AboutView GetInstace(MainWindow mainWindow, MainPage mainPage)
        {
            if (instance == null)
                instance = new AboutView(mainWindow, mainPage);
            return instance;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.presenter.Content = MainPage;
        }
    }
}
