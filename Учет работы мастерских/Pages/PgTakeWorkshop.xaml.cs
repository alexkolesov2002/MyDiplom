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

        public event PropertyChangedEventHandler PropertyChanged;



        #endregion
        public PgTakeWorkshop()
        {
            try
            {

                InitializeComponent();

                // ListPickedEquip.DisplayMemberPath = "title_equipment";
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
            //LoadPages.SwitchPages.Navigate(new PgTakeEquip((workshops)ComboBoxWorkShops.SelectedItem));


        }

        private void ClockTime_Loaded(object sender, RoutedEventArgs e)
        {
            ClockTime.Time = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
        }


        private void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            Zagruzka();
        }
    }
}
