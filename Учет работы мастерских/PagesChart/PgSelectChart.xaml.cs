using System.Windows;
using System.Windows.Controls;

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для PgSelectChart.xaml
    /// </summary>
    public partial class PgSelectChart : Page
    {
        public PgSelectChart()
        {
            try
            {
                InitializeComponent();
                LoadPages.SwitchChart = FrameChart;
                ComboBoxSelectChart.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboBoxSelectChart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
