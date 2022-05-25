using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для WinAutorise.xaml
    /// </summary>
    public partial class WinAutorise : Window
    {
        public WinAutorise()
        {
            InitializeComponent();
            BaseModel.BaseConnect = new Entities();
        }

        private void BtnAutorise_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                users user = BaseModel.BaseConnect.users.FirstOrDefault(x => x.login == TxtLogin2.Text && x.password == TxtPass2.Password);
                if (user != null)
                {


                  MyString.mw =  new MainWindow(user);
                    MyString.mw.Show(); 
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch
            {
                MessageBox.Show("Отсутсвует соединение с интернетом, повторите попытку позже", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {


                if (e.ChangedButton == MouseButton.Left)
                {
                    this.DragMove();
                }
            }
            catch
            {
                MessageBox.Show("Возникла непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TxtPass2.Visibility = Visibility.Hidden;
            TxtBuf.Visibility = Visibility.Visible;
        }

        private void PackIcon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TxtBuf.Visibility = Visibility.Hidden;
            TxtPass2.Visibility = Visibility.Visible;
           
        }

        private void TxtPass2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TxtBuf.Text = TxtPass2.Password; 
        }

        private void PackIcon_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TxtBuf.Visibility = Visibility.Hidden;
            TxtPass2.Visibility = Visibility.Visible;
        }
    }
}
