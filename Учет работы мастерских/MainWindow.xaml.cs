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
using MaterialDesignColors;
using MaterialDesignThemes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
              InitializeComponent();
          //  MainFrame.Navigate( new PgAutorise());
           // LoadPages.SwitchPages = MainFrame;
            LeftBorder.Visibility = Visibility.Collapsed;
            MainScreen.CornerRadius =  new CornerRadius(10);
        }

        private void MainScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Button2.Style = (Style)this.Resources["menuButtonActive"];   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
