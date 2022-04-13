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
    public partial class PgTakeWorkshop : Page, INotifyPropertyChanged
    {
        #region глобальные переменные

        public int Loading { get; set; } = 0;
        public bool IsSaveComplete { get; set; } = false;
        public bool SaveNotComplete { get; set; } = true;
        workshops TakedWorkShop;
        users CurrentUser;
        List<equipments> TakedEquipments;
        public event PropertyChangedEventHandler PropertyChanged;
        List<journal_use_workshop> journal_Use_Workshop = new List<journal_use_workshop>();



        #endregion
        public PgTakeWorkshop(workshops TakedWorkShop, users CurrentUser, List<equipments> TakedEquipments)
        {
            try
            {

                InitializeComponent();
                this.CurrentUser = CurrentUser;
                this.TakedWorkShop = TakedWorkShop;
                this.TakedEquipments = TakedEquipments;


            }
            catch
            {

            }
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
            SaveNotComplete = false;
            PropertyChanged(this, new PropertyChangedEventArgs("SaveNotComplete"));
            IsSaveComplete = true;
            PropertyChanged(this, new PropertyChangedEventArgs("IsSaveComplete"));
            
            string Date = CalendarDate.SelectedDate.Value.ToShortDateString();
            string Time = ClockTime.Time.ToLongTimeString();
           
         
            DateTime dateTime = Convert.ToDateTime(Date +" "+ Time);
            if (TakedEquipments != null)
            {
                foreach (equipments equipment in TakedEquipments)
                {
                    journal_use_workshop use = new journal_use_workshop() { id_equipment = equipment.id_equipment, id_user = CurrentUser.id_user, id_workshop = TakedWorkShop.id_workshop, date_use = dateTime };
                    journal_Use_Workshop.Add(use);
                }
                BaseModel.BaseConnect.journal_use_workshop.AddRange(journal_Use_Workshop);
            }
            else
            {
                journal_use_workshop use = new journal_use_workshop() { id_equipment = null, id_user = CurrentUser.id_user, id_workshop = TakedWorkShop.id_workshop, date_use = dateTime };
                BaseModel.BaseConnect.journal_use_workshop.Add(use);
            }
 
            BaseModel.BaseConnect.SaveChanges();
            BtnSaveData.IsEnabled = false;
            //LoadPages.SwitchPages.Navigate(new PgTakeEquip((workshops)ComboBoxWorkShops.SelectedItem));


        }

        private void ClockTime_Loaded(object sender, RoutedEventArgs e)
        {
            ClockTime.Time = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
            CalendarDate.SelectedDate  = DateTime.Now;
        }


        private void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            Zagruzka();
        }
    }
}
