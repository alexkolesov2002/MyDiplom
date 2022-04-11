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
    /// Логика взаимодействия для PgTakeWorkShop.xaml
    /// </summary>
    public partial class PgTakeWorkShop : Page
    {
        public PgTakeWorkShop()
        {
            List<equipments> equipments = new List<equipments>();
            InitializeComponent();

            List<Equipments_In_Workshop> firswork = BaseModel.BaseConnect.Equipments_In_Workshop.Where(x => x.id_workshop == 1).ToList();
         
            foreach (Equipments_In_Workshop q in firswork)
            {
                equipments.Add(q.equipments);
            }    

           
          
            ListEquip.ItemsSource = equipments;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

        }



        //private void Accept_Click(object sender, RoutedEventArgs e)
        //{
        //    FruitListBox.Items.Add(FruitTextBox.Text);
        //    FruitListBox.Items.Refresh();
        //}
    }
}
