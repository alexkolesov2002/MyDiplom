using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class PgTakeWorkShop : Page, INotifyPropertyChanged
    {
        #region глобальные переменные
        List<equipments> ListEqForDelite;
        List<equipments> ListMainEq;
        List<equipments> ListMainEq1;
        equipments BufClassObjectEq;
        List<equipments> EquipmentsBufList = new List<equipments>();
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        public int IndexRow { get; set; } = 0;
        #endregion
        public PgTakeWorkShop()
        {
            try
            {
                ListMainEq = new List<equipments>();
                InitializeComponent();

                List<Equipments_In_Workshop> firswork = BaseModel.BaseConnect.Equipments_In_Workshop.Where(x => x.id_workshop == 1).ToList();
               
                foreach (Equipments_In_Workshop q in firswork)
                {  
                    ListMainEq.Add(q.equipments);
                }
               
                    ListEquip.ItemsSource = ListMainEq;
                ListPickedEquip.DisplayMemberPath = "title_equipment";
            }
            catch
            {

            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListEqForDelite = new List<equipments>();
                ListEqForDelite = EquipmentsBufList;
                int j = 1;
                foreach (equipments i in EquipmentsBufList)
                {
                    i.IndexRow = j;
                    j++;
                }
                //PropertyChanged(this, new PropertyChangedEventArgs("IndexRow"));


                ListPickedEquip.ItemsSource = EquipmentsBufList;
                ListPickedEquip.Items.Refresh();
                

               
                
                
            }
            catch
            {

            }


        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox checkBox = (CheckBox)sender;
                int id = Convert.ToInt32(checkBox.Uid);
                BufClassObjectEq = ListMainEq.FirstOrDefault(x => x.id_equipment == id);
                EquipmentsBufList.Add(BufClassObjectEq);
            }
            catch
            {

            }

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox checkBox = (CheckBox)sender;
                int id = Convert.ToInt32(checkBox.Uid);
                BufClassObjectEq = ListMainEq.FirstOrDefault(x => x.id_equipment == id);
                EquipmentsBufList.Remove(BufClassObjectEq);
            }
            catch
            {

            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button checkBox = (Button)sender;
                int id = Convert.ToInt32(checkBox.Uid);
                equipments equipments = new equipments();
                equipments = ListEqForDelite.FirstOrDefault(x => x.id_equipment == id);
                ListEqForDelite.Remove(equipments);
                int j = 1;
            foreach (equipments i in EquipmentsBufList)
                {
                    i.IndexRow = j;
                    j++;
                }
                ListPickedEquip.ItemsSource = ListEqForDelite;
              
                ListPickedEquip.Items.Refresh();
            }
            catch
            {

            }

        }

        private void ListEquip_Loaded(object sender, RoutedEventArgs e)
        {
            ListEquip.Items.Refresh();
        }

        private void BtnAddEq_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void BtnRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            ListEqForDelite.Clear();
            EquipmentsBufList.Clear();
            ListPickedEquip.ItemsSource = EquipmentsBufList;
            ListPickedEquip.Items.Refresh();
        }
    }
}
