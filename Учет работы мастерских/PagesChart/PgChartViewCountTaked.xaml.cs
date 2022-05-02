using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для PgChartView.xaml
    /// </summary>
    public partial class PgChartViewCountTaked : UserControl, INotifyPropertyChanged

    {

        #region Глобальные переменные
        public SeriesCollection SeriesCollection { get; set; } 
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public bool CheckComboBox { get; set; } = false;
        List<journal_use_workshop> journal_Use_Workshops = new List<journal_use_workshop>();
        #endregion
        public PgChartViewCountTaked()
        {
            InitializeComponent();
            ComboBoxDate.SelectedIndex = 0;
            if (CheckComboBox == false)
            {
                
            }
            else
            {
                BuildChart();
            }
            


        }
        private void ComboBoxDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckComboBox = true;
            switch (ComboBoxDate.SelectedIndex)
            {
                case 0:
                    journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.ToList();
                    if (Chart.Series != null)
                    {
                        Chart.Series.Clear();
                        BuildChart();
                        PropertyChanged(this, new PropertyChangedEventArgs("SeriesCollection"));
                        Chart.Update(true, true);
                    }
                    else
                    {
                        BuildChart();
                        Chart.Update(true, true);
                    }
                   
                    break;
                case 1:
                    var dateTimeLastMonth = DateTime.Now.AddMonths(-1);
                    journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.Where(x => x.date_use > dateTimeLastMonth).ToList();
                    Chart.Series.Clear();
                    BuildChart();
                    PropertyChanged(this, new PropertyChangedEventArgs("SeriesCollection"));
                    Chart.Update(true, true);
                    break;
                case 2:
                    var dateTimeHalfYear = DateTime.Now.AddMonths(-6);
                    journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.Where(x => x.date_use > dateTimeHalfYear).ToList();
                    Chart.Series.Clear();
                    BuildChart();
                    PropertyChanged(this, new PropertyChangedEventArgs("SeriesCollection"));
                    Chart.Update(true, true);
                    break;
                case 3:
                    var dateTimeYear = DateTime.Now.AddYears(-1);
                    journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.Where(x => x.date_use > dateTimeYear).ToList();
                    Chart.Series.Clear();
                    BuildChart();
                    PropertyChanged(this, new PropertyChangedEventArgs("SeriesCollection"));
                    Chart.Update(true, true);
                    break;
            }



        }

        void BuildChart()
        {
            SeriesCollection = new SeriesCollection();
            BaseModel.BaseConnect = new Entities();

            var result = journal_Use_Workshops.GroupBy(n => n.id_workshop).Select(m => new { m.Key, Count = m.Count() });
            SeriesCollection = new SeriesCollection { };
            foreach (workshops workshops in BaseModel.BaseConnect.workshops.ToList())
            {
                var workShop = result.FirstOrDefault(x => x.Key == workshops.id_workshop);
                if (workShop != null)
                {
                    SeriesCollection.Add(new PieSeries { Title = workshops.title_workshop, Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToInt32(workShop.Count)) }, DataLabels = true, FontSize = 20 });
                }
                else
                {

                }

            }

            //adding values or series will update and animate the chart automatically
            //SeriesCollection.Add(new PieSeries());
            //SeriesCollection[0].Values.Add(5);

            DataContext = this;

        }







        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();

            foreach (var series in SeriesCollection)
            {
                foreach (var observable in series.Values.Cast<ObservableValue>())
                {
                    observable.Value = r.Next(0, 10);
                }
            }
        }

        private void AddSeriesOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            var c = SeriesCollection.Count > 0 ? SeriesCollection[0].Values.Count : 5;

            var vals = new ChartValues<ObservableValue>();

            for (var i = 0; i < c; i++)
            {
                vals.Add(new ObservableValue(r.Next(0, 10)));
            }

            SeriesCollection.Add(new PieSeries
            {
                Values = vals
            });
        }

      

        private void RestartOnClick(object sender, RoutedEventArgs e)
        {
            Chart.Series.Clear();
            BuildChart();
            PropertyChanged(this, new PropertyChangedEventArgs("SeriesCollection"));
            Chart.Update(true, true);
        }


    }
}
