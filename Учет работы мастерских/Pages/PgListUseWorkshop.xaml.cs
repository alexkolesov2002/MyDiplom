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
    /// Логика взаимодействия для PgListUseWorkshop.xaml
    /// </summary>
    public partial class PgListUseWorkshop : Page, INotifyPropertyChanged


    {//VievModelJournalUseWorkshop vievModel = new VievModelJournalUseWorkshop();
        public dynamic MyProperty { get; set; }
        public int Loading { get; set; } = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        public users users = new users();
        public PgListUseWorkshop(users users)
        {
            InitializeComponent();
            this.users = users;
            if (users.id_role == 1)
            {
                ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipments();
                ComboSelectWork.ItemsSource = BaseModel.BaseConnect.workshops.ToList();
                ComboSelectWork.DisplayMemberPath = "title_workshop";
                ComboSelectWork.Visibility = Visibility.Visible;
                BtnBuildResult.Visibility = Visibility.Visible;

            }
            else
            {
                ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipments(users.id_user);
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



        }
        private void BtnAddRating_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);

            LoadPages.SwitchPages.Navigate(new PgRating(id));
        }

        private void CalendarS_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (users.id_role == 1)
            {

                if (CalendarS.SelectedDate != null && CalendarPO.SelectedDate != null)
                {
                    ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipments((DateTime)CalendarS.SelectedDate, (DateTime)CalendarPO.SelectedDate);
                    ListUseWorkShop.Items.Refresh();
                }

            }
            else
            {
                if (CalendarS.SelectedDate != null && CalendarPO.SelectedDate != null)
                {
                    ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipments(users.id_user, (DateTime)CalendarS.SelectedDate, (DateTime)CalendarPO.SelectedDate);
                    ListUseWorkShop.Items.Refresh();
                }
            }
        }

        private void CalendarPO_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (users.id_role == 1)
            {

                if (CalendarS.SelectedDate != null && CalendarPO.SelectedDate != null && ComboSelectWork.SelectedItem != null)
                {
                    ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipmentsAndWork((workshops)ComboSelectWork.SelectedItem, (DateTime)CalendarS.SelectedDate, (DateTime)CalendarPO.SelectedDate);
                    ListUseWorkShop.Items.Refresh();
                }
                else if (ComboSelectWork.SelectedItem != null)
                {
                    ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipmentsAndWork((workshops)ComboSelectWork.SelectedItem);
                    ListUseWorkShop.Items.Refresh();
                }

            }
            else
            {
                if (CalendarS.SelectedDate != null && CalendarPO.SelectedDate != null && ComboSelectWork.SelectedItem != null)
                {
                    ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipmentsAndWork((workshops)ComboSelectWork.SelectedItem, (DateTime)CalendarS.SelectedDate, (DateTime)CalendarPO.SelectedDate);
                    ListUseWorkShop.Items.Refresh();
                }
                else if (ComboSelectWork.SelectedItem != null)
                {
                    ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipmentsAndWork((workshops)ComboSelectWork.SelectedItem);
                    ListUseWorkShop.Items.Refresh();
                }
            }
        }

        private void ComboSelectWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CalendarS.SelectedDate != null && CalendarPO.SelectedDate != null && ComboSelectWork.SelectedItem != null)
            {
                ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipmentsAndWork((workshops)ComboSelectWork.SelectedItem, (DateTime)CalendarS.SelectedDate, (DateTime)CalendarPO.SelectedDate);
                ListUseWorkShop.Items.Refresh();
            }
            else if (ComboSelectWork.SelectedItem != null)
            {
                ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipmentsAndWork((workshops)ComboSelectWork.SelectedItem);
                ListUseWorkShop.Items.Refresh();
            }


        }
    }
}
