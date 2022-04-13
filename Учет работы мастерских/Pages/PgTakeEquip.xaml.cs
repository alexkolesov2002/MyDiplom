using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    public partial class PgTakeEquip : Page, INotifyPropertyChanged
    {
        #region глобальные переменные
        List<equipments> ListEqForDelite;
        List<equipments> ListMainEq;
        public int Loading { get; set; } = 0;
        List<Equipments_In_Workshop> firswork;
        equipments BufClassObjectEq;
        List<equipments> EquipmentsBufList = new List<equipments>();
        public event PropertyChangedEventHandler PropertyChanged;
        users CurrentUser;
        workshops TakedWorkShop;
        public int IndexRow { get; set; } = 0;
        #endregion
        public PgTakeEquip(workshops TakedWorkShop, users CurrentUser)
        {
            try
            {
                
                InitializeComponent();
                this.CurrentUser = CurrentUser;
                this.TakedWorkShop = TakedWorkShop;
                ListMainEq = new List<equipments>();
                firswork = BaseModel.BaseConnect.Equipments_In_Workshop.Where(x => x.id_workshop == TakedWorkShop.id_workshop).ToList();

                foreach (Equipments_In_Workshop q in firswork)
                {
                    ListMainEq.Add(q.equipments);
                }

                ListEquip.ItemsSource = ListMainEq;
               // ListPickedEquip.DisplayMemberPath = "title_equipment";
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
                ListPickedEquip.ItemsSource = EquipmentsBufList;
                foreach (equipments equipments in EquipmentsBufList)
                {
                    ListMainEq.Remove(equipments);
                }
                ListEquip.ItemsSource = ListMainEq;
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
                ListMainEq.Add(equipments);
                ListEquip.Items.Refresh();
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
            try
            { 
                if (ListEqForDelite != null)
                {
                    ListEqForDelite.Clear();
                }
                EquipmentsBufList.Clear();
                ListMainEq.Clear();
                ListPickedEquip.ItemsSource = EquipmentsBufList;
                foreach (Equipments_In_Workshop q in firswork)
                {
                    ListMainEq.Add(q.equipments);
                }

                ListEquip.ItemsSource = ListMainEq;
                ListPickedEquip.Items.Refresh();
                ListEquip.Items.Refresh();

            }
            catch
            {

            }
        }

        private void BtnPlussCount_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);
            List<equipments> ListFinalEq = new List<equipments>();
            ListFinalEq = (List<equipments>)ListPickedEquip.ItemsSource;
            equipments equipments = ListFinalEq.FirstOrDefault(x=>x.id_equipment ==id);
            equipments.Count += 1;
            if(equipments.Count > 6)
            {
                button.IsEnabled = false;
            }
            else button.IsEnabled = true;
            ListPickedEquip.Items.Refresh();
        }

        private void BtnMinuceCount_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);
            List<equipments> ListFinalEq = new List<equipments>();
            ListFinalEq = (List<equipments>)ListPickedEquip.ItemsSource;
            equipments equipments = ListFinalEq.FirstOrDefault(x => x.id_equipment == id);
            equipments.Count -= 1;
            if (equipments.Count < 1)
            {
                button.IsEnabled = false;
            }
            else button.IsEnabled = true;

           ListPickedEquip.Items.Refresh();
        }

        private void BtnMinuceCount_Loaded(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);
            List<equipments> ListFinalEq = new List<equipments>();
            ListFinalEq = (List<equipments>)ListPickedEquip.ItemsSource;
            equipments equipments = ListFinalEq.FirstOrDefault(x => x.id_equipment == id);
           
            if (equipments.Count<= 1)
            {
                button.IsEnabled = false;
            }
            else button.IsEnabled = true;
        }

        private void BtnPlussCount_Loaded(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);
            List<equipments> ListFinalEq = new List<equipments>();
            ListFinalEq = (List<equipments>)ListPickedEquip.ItemsSource;
            equipments equipments = ListFinalEq.FirstOrDefault(x => x.id_equipment == id);
         
            if (equipments.Count >= 6)
            {
                button.IsEnabled = false;
            }
            else button.IsEnabled = true;
        }

        private void GoPageTakeWorkshop_Click(object sender, RoutedEventArgs e)
        {
            Zagruzka();
        }
        async void Zagruzka()
        {
            while (Loading != 100)
            {
                Loading += 4;
                await Task.Delay(4);
                PropertyChanged(this, new PropertyChangedEventArgs("Loading"));
            }
            await Task.Delay(15);

            LoadPages.SwitchPages.Navigate(new PgTakeWorkshop(TakedWorkShop,CurrentUser, (List<equipments>)ListPickedEquip.ItemsSource));


        }

      
    }
}
