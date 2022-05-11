using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для PgSelectWork.xaml
    /// </summary>
    public partial class PgSelectWork : Page, INotifyPropertyChanged
    {
        users CurrentUser;
        public int Loading { get; set; } = 0;
        public PgSelectWork(users CurrentUser)
        {
            try
            {
                this.CurrentUser = CurrentUser;
                InitializeComponent();
                ComboBoxWorkShops.ItemsSource = BaseModel.BaseConnect.workshops.ToList();
                ComboBoxWorkShops.DisplayMemberPath = "title_workshop";
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            }

        public event PropertyChangedEventHandler PropertyChanged;

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
                LoadPages.SwitchPages.Navigate(new PgTakeEquip((workshops)ComboBoxWorkShops.SelectedItem, CurrentUser));
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            }

        private void BtnGoTakeWorkShop_Click(object sender, RoutedEventArgs e)
        {
            Zagruzka();
        }

        private void ComboBoxWorkShops_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnGoTakeWorkShop.IsEnabled = true;
        }
    }
}
