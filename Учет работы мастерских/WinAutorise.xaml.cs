using System.Linq;
using System.Windows;
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
                users user = BaseModel.BaseConnect.users.FirstOrDefault(x => x.login == TxtLogin.Text && x.password == TxtPass.Password);
                if (user != null)
                {


                    new MainWindow(user).Show();
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
    }
}
