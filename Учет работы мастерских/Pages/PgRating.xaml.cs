using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для PgRating.xaml
    /// </summary>
    public partial class PgRating : Page
    {
        List<students> students = new List<students>();
        List<criteria_in_event> criteria_in_events;
        List<string> criteria_name = new List<string>();
        List<criteria_in_event> criteria_in_eventsNew = BaseModel.BaseConnect.criteria_in_event.ToList();
        int index;
        public PgRating(int index)
        {
            try
            {


                this.index = index;
                InitializeComponent();
                criteria_in_events = BaseModel.BaseConnect.criteria_in_event.Where(x => x.id_event == index).ToList();
                foreach (criteria_in_event student in criteria_in_events)
                {
                    students studentsS = BaseModel.BaseConnect.students.FirstOrDefault(x => x.id_student == student.id_student);
                    students.Add(studentsS);
                    criteria_name.Add(student.criteria.title_criterion);
                }
                ListRating.ItemsSource = students.Distinct();

                ListCriterionName.ItemsSource = criteria_name.Distinct();
                ListCriterionName.IsEnabled = false;
                ScrolView.Height += 1000;
                MainStack.Height += 1000;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        private void Lenin_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.ListBox textBlock = (System.Windows.Controls.ListBox)sender;
                int id = Convert.ToInt32(textBlock.Uid);
                textBlock.ItemsSource = criteria_in_eventsNew.Where(x => x.id_event == index && x.id_student == id).ToList();
            }
            catch
            {
                System.Windows.MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSaveRating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult result = (DialogResult)System.Windows.MessageBox.Show("Вы уверены в правильности введенных данных?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == DialogResult.Yes)
                {
                    BaseModel.BaseConnect.SaveChanges();
                    System.Windows.MessageBox.Show("Данные успешно сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;


            }
            catch
            {
                System.Windows.MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.SwitchPages.GoBack();
        }

        private void MainStack_Loaded(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            stackPanel.Height += 300;
        }

        private void ScrolView_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollViewer scroll = (ScrollViewer)sender;
            scroll.Height += 300;
        }
    }
}
