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
    /// Логика взаимодействия для PgRating.xaml
    /// </summary>
    public partial class PgRating : Page
    {
        List<students> students = new List<students>();
        List<criteria_in_event> criteria_in_events;
        int index;
        public PgRating(int index)
        {
            this.index = index;
            InitializeComponent();
            criteria_in_events = BaseModel.BaseConnect.criteria_in_event.Where(x => x.id_event == index).ToList();
            foreach (criteria_in_event student in criteria_in_events)
            {
                students studentsS = BaseModel.BaseConnect.students.FirstOrDefault(x=>x.id_student == student.id_student);
                students.Add(studentsS);
            }
            ListRating.ItemsSource = students.Distinct();

        }

        private void TxtName_Loaded(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void Lenin_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox textBlock = (ListBox)sender;
            int id = Convert.ToInt32(textBlock.Uid);
            textBlock.ItemsSource = criteria_in_event.getlistCriteria(index, id);
        }

        private void BtnSaveRating_Click(object sender, RoutedEventArgs e)
        {
            BaseModel.BaseConnect.SaveChanges();
        }
    }
}
