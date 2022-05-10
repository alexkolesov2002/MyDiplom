using System.Windows;
using System.Windows.Input;

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для WinSettings.xaml
    /// </summary>
    public partial class WinSettings : Window
    {
        public WinSettings()
        {
            InitializeComponent();
        }

        private void BtnGoPost_Click(object sender, RoutedEventArgs e)
        {
            MyString.Email = TxtMail.Text;
            this.Close();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
