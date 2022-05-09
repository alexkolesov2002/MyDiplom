using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для PgAddStudentsxaml.xaml
    /// </summary>
    public partial class PgAddStudentsxaml : Page
    {
        List<students> students = new List<students>();
        public PgAddStudentsxaml()
        {
            InitializeComponent();
            ListAddStudent.ItemsSource = students;
            Groups.ItemsSource = BaseModel.BaseConnect.groups.ToList();
            Groups.DisplayMemberPath = "title_group";
            Groups.SelectedValuePath = "id_group";
            
        }

        private void ComboBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.ItemsSource = BaseModel.BaseConnect.groups.ToList();
            comboBox.DisplayMemberPath = "title_group";
            
        }

        private void BtnAddRow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            students student = new students();
            students.Add(student);
            ListAddStudent.ItemsSource = students;
            ListAddStudent.Items.Refresh();

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<students> students = new List<students>(); 
            foreach (students student in (List<students>)ListAddStudent.ItemsSource)
            {
                students newstudents = new students() { id_group = (int)Groups.SelectedValue, name = student.name, surname = student.surname, patronymic = student.patronymic };
                students.Add(newstudents);

            }
            BaseModel.BaseConnect.students.AddRange(students);
            BaseModel.BaseConnect.SaveChanges();
        }

    
    }
}
