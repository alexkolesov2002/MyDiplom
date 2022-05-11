using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            try
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
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        async void Zagruzka()
        {
            try
            {
                while (Loading != 100)
                {
                    Loading += 4;
                    await Task.Delay(4);
                    PropertyChanged(this, new PropertyChangedEventArgs("Loading"));
                }
                await Task.Delay(15);
                if (CalendarS.SelectedDate != null && CalendarPO.SelectedDate != null)
                {


                    new WinOtchet((DateTime)CalendarS.SelectedDate, (DateTime)CalendarPO.SelectedDate, ComboSelectWork.Text, ListUseWorkShop.Items.Count).ShowDialog();
                }
                else
                {
                    Loading = 0;
                    PropertyChanged(this, new PropertyChangedEventArgs("Loading"));
                    throw new Exception("Заполните все необходимые параметры");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
        private void BtnAddRating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int id = Convert.ToInt32(button.Uid);

                LoadPages.SwitchPages.Navigate(new PgRating(id));
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalendarS_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
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
                    if (CalendarS.SelectedDate != null && CalendarPO.SelectedDate != null)
                    {
                        ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipments(users.id_user, (DateTime)CalendarS.SelectedDate, (DateTime)CalendarPO.SelectedDate);
                        ListUseWorkShop.Items.Refresh();
                    }

                }
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalendarPO_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
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
                    if (CalendarS.SelectedDate != null && CalendarPO.SelectedDate != null)
                    {
                        ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipments(users.id_user, (DateTime)CalendarS.SelectedDate, (DateTime)CalendarPO.SelectedDate);
                        ListUseWorkShop.Items.Refresh();
                    }

                }
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ComboSelectWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void BtnBuildResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Zagruzka();
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadPages.SwitchPages.Navigate(new PgListUseWorkshop(users));
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
