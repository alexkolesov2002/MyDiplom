using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;


namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для PgChartView.xaml
    /// </summary>
    public partial class PgChartView : UserControl//, INotifyPropertyChanged

    {

        #region Глобальные переменные
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        #endregion
        public PgChartView()
        {
            InitializeComponent();
            List<journal_use_workshop> journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.ToList();
            var s = journal_Use_Workshops.GroupBy(x => new { x.id_equipment, x.id_workshop }).Select(a => new { Count = a.Sum(b => b.count_equipment), id_workshop = a.Key }).ToList();

            SeriesCollection = new SeriesCollection
            {
            };

            foreach (workshops workshops in BaseModel.BaseConnect.workshops.ToList())
            {
                ChartValues<int> values = new ChartValues<int>();
                foreach (equipments equipments in BaseModel.BaseConnect.equipments.ToList())
                {
                    var a = s.FirstOrDefault(x => x.id_workshop.id_workshop == workshops.id_workshop && x.id_workshop.id_equipment == equipments.id_equipment);
                    if (a != null)
                    {
                        values.Add(Convert.ToInt32(a.Count));
                    }
                    else
                    {
                        values.Add(Convert.ToInt32("0"));
                    }
                }
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = workshops.title_workshop,
                    Values = values
                });
            }
            string[] labels = new string[BaseModel.BaseConnect.equipments.Count()];
            int i = 0;
            foreach (equipments equipments1 in BaseModel.BaseConnect.equipments.ToList())
            {
                labels[i] = equipments1.types_equipment.title_type_equipment +" "+ equipments1.title_equipment;
                i++;
            }
            Labels = labels;
            Formatter = value => value.ToString("N");
            DataContext = this;
        }

    
    }
}
