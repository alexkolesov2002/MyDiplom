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
        List<equipments> equipments;
        equipments equipmentsbuf;
        List<equipments> EquipmentsBufList = new List<equipments>();
        public PgTakeWorkShop()
        {
            equipments = new List<equipments>();
            InitializeComponent();

            List<Equipments_In_Workshop> firswork = BaseModel.BaseConnect.Equipments_In_Workshop.Where(x => x.id_workshop == 1).ToList();
         
            foreach (Equipments_In_Workshop q in firswork)
            {
                equipments.Add(q.equipments);
            }    

            ListEquip.ItemsSource = equipments;
            ListPickedEquip.DisplayMemberPath = "title_equipment";
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
           
            ListPickedEquip.ItemsSource = EquipmentsBufList;
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int id = Convert.ToInt32(checkBox.Uid);
            equipmentsbuf = equipments.FirstOrDefault(x => x.id_equipment == id);
            EquipmentsBufList.Add(equipmentsbuf);

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int id = Convert.ToInt32(checkBox.Uid);
            equipmentsbuf = equipments.FirstOrDefault(x => x.id_equipment == id);
            EquipmentsBufList.Remove(equipmentsbuf);    
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button checkBox = (Button)sender;
            int id = Convert.ToInt32(checkBox.Uid);
            List<equipments> equipmentdel = ListPickedEquip.Items;
            equipmentsbuf = equipments.FirstOrDefault(x => x.id_equipment == id);
            equipmentdel.Remove(equipmentsbuf);
            ListPickedEquip.ItemsSource = equipmentdel;
            ListPickedEquip.Items.Refresh();

        }
    }
}
