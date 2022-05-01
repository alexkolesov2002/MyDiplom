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
        #endregion
        public PgChartViewCountTaked()
        {
            InitializeComponent();
            List<journal_use_workshop> journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.ToList();
           
            var result = journal_Use_Workshops.GroupBy(n => n.id_workshop).Select(m => new { m.Key, Count = m.Count()});
            SeriesCollection = new SeriesCollection { };
            foreach (workshops workshops in BaseModel.BaseConnect.workshops.ToList())
            {
                var a = result.FirstOrDefault(x => x.Key == workshops.id_workshop);
                if (a != null)
                {
                    SeriesCollection.Add(new PieSeries { Title = workshops.title_workshop, Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToInt32(a.Count)) }, DataLabels= true});
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

        public event PropertyChangedEventHandler PropertyChanged;



       

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

        private void RemoveSeriesOnClick(object sender, RoutedEventArgs e)
        {
            if (SeriesCollection.Count > 0)
                SeriesCollection.RemoveAt(0);
        }

        private void AddValueOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            foreach (var series in SeriesCollection)
            {
                series.Values.Add(new ObservableValue(r.Next(0, 10)));
            }
        }

        private void RemoveValueOnClick(object sender, RoutedEventArgs e)
        {
            foreach (var series in SeriesCollection)
            {
                if (series.Values.Count > 0)
                    series.Values.RemoveAt(0);
            }
        }

        private void RestartOnClick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true, true);
        }
    }
}
