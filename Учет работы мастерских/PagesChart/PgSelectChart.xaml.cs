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

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для PgSelectChart.xaml
    /// </summary>
    public partial class PgSelectChart : Page
    {
        public PgSelectChart()
        {
            InitializeComponent();
            LoadPages.SwitchChart = FrameChart;
            ComboBoxSelectChart.SelectedIndex = 0;
        }

        private void ComboBoxSelectChart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBoxSelectChart.SelectedIndex)
            {
                case 0:
                    LoadPages.SwitchChart.Navigate(new PgChartViewEquip());
                    break;

                case 1:
                    LoadPages.SwitchChart.Navigate(new PgChartViewCountTaked());
                    break;
            }
        }
    }
}
