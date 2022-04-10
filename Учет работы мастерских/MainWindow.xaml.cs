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
            //MainFrame.Navigate( new PgAutorise());
            //LoadPages.SwitchPages = MainFrame;
           
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
            Button1.Style = (Style)this.Resources["menuButton"];
            Button3.Style = (Style)this.Resources["menuButton"];
            Button4.Style = (Style)this.Resources["menuButton"];
        }

       

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Button4.Style = (Style)this.Resources["menuButtonActive"];
            Button2.Style = (Style)this.Resources["menuButton"];
            Button3.Style = (Style)this.Resources["menuButton"];
            Button1.Style = (Style)this.Resources["menuButton"];
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button3.Style = (Style)this.Resources["menuButtonActive"];
            Button1.Style = (Style)this.Resources["menuButton"];
            Button2.Style = (Style)this.Resources["menuButton"];
            Button4.Style = (Style)this.Resources["menuButton"];
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Button1.Style = (Style)this.Resources["menuButtonActive"];
            Button2.Style = (Style)this.Resources["menuButton"];
            Button3.Style = (Style)this.Resources["menuButton"];
            Button4.Style = (Style)this.Resources["menuButton"];
        }
    }
}
