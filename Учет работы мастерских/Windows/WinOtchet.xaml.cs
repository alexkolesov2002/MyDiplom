using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для WinOtchet.xaml
    /// </summary>
    public partial class WinOtchet : Window, INotifyPropertyChanged
    {
        public string StringDate { get; set; } = DateTime.Now.ToShortDateString();
        public string StringTime { get; set; } = DateTime.Now.ToShortTimeString();
        public string StringTimeS { get; set; }
        public string StringTimePo { get; set; }
        public string WorkShop { get; set; }
        public int MyCount { get; set; }
        public WinOtchet(DateTime s, DateTime po, string workshop, int Count)
        {
            InitializeComponent();
            StringTimeS = s.ToShortDateString();
            StringTimePo = po.ToShortDateString();
            WorkShop = "\"" + workshop + "\"";
            MyCount = Count;
            PropertyChanged(this, new PropertyChangedEventArgs("StringTimeS"));
            PropertyChanged(this, new PropertyChangedEventArgs("StringTimePo"));
            PropertyChanged(this, new PropertyChangedEventArgs("WorkShop"));
            PropertyChanged(this, new PropertyChangedEventArgs("MyCount"));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hid.Visibility = Visibility.Collapsed;
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(Print, "Diplom");
            }
            Hid.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string FilePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
            };

            MailAddress mailAddress = new MailAddress("prof-probi-prog@ngknn.ru", "Мася");
            MailAddress toAdress = new MailAddress("boldin_0202@mail.ru", "Try");
            MailMessage message = new MailMessage(mailAddress, toAdress);
            message.Body = "Спокойной ночи";
            message.Subject = "Люблю";
            message.Attachments.Add(new Attachment(FilePath));
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.mail.ru";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(mailAddress.Address, "GBHJU6032w");
            smtpClient.SendMailAsync(message);
            MessageBox.Show("Отчет успешно доставлен", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
