using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для PgSelectWork.xaml
    /// </summary>
    public partial class PgSelectWork : Page, INotifyPropertyChanged
    {
        users CurrentUser;
        public int Loading { get; set; } = 0;
        public PgSelectWork(users CurrentUser)
        {
            this.CurrentUser = CurrentUser;  
            InitializeComponent();
            ComboBoxWorkShops.ItemsSource = BaseModel.BaseConnect.workshops.ToList();
            ComboBoxWorkShops.DisplayMemberPath = "title_workshop";
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        async void Zagruzka()
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
