﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Forms;

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
            try
            {
                InitializeComponent();
                ListAddStudent.ItemsSource = students;
                Groups.ItemsSource = BaseModel.BaseConnect.groups.ToList();
                Groups.DisplayMemberPath = "title_group";
                Groups.SelectedValuePath = "id_group";
            }
            catch
            {

                MessageBox.Show("Возникла непредвиденная ошибка, повторите попытку позже", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void ComboBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.ComboBox comboBox = sender as System.Windows.Controls.ComboBox;
                comboBox.ItemsSource = BaseModel.BaseConnect.groups.ToList();
                comboBox.DisplayMemberPath = "title_group";
            }
            catch
            {

                MessageBox.Show("Возникла непредвиденная ошибка, повторите попытку позже", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void BtnAddRow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                students student = new students();
                students.Add(student);
                ListAddStudent.ItemsSource = students;
                ListAddStudent.Items.Refresh();
            }
            catch
            {

                MessageBox.Show("Возникла непредвиденная ошибка, повторите попытку позже", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Вы уверены в правильности введенных данных?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
                {
                    if (Groups.SelectedValue == null)
                    {
                        throw new Exception("Не выбрана группа для внесения студентов");
                    }
                    List<students> students = new List<students>();
                    if (ListAddStudent.Items.Count != 0)
                    {
                        foreach (students student in (List<students>)ListAddStudent.ItemsSource)
                        {
                            if (student.name == " " || student.surname == " " || student.patronymic == " ")
                            {
                                throw new Exception("Не все данные заполнены корректно");
                            }
                            if (Regex.IsMatch(student.name, @"^[а-яА-Я]+$") == false || Regex.IsMatch(student.surname, @"^[а-яА-Я]+$") == false || Regex.IsMatch(student.patronymic, @"^[а-яА-Я]+$") == false)
                            {
                                throw new Exception("Не все данные заполнены корректно");
                            }
                            students newstudents = new students() { id_group = (int)Groups.SelectedValue, name = student.name, surname = student.surname, patronymic = student.patronymic };
                            students.Add(newstudents);

                        }
                        BaseModel.BaseConnect.students.AddRange(students);
                        BaseModel.BaseConnect.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Добавьте минимум одного студента");
                    }
                }
                MessageBox.Show("Данные успешно сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Button button = (Button) sender;
            //int id = Convert.ToInt32(button.Uid);
            //foreach (students student in (List<students>)ListAddStudent.ItemsSource)
            //{
            //    student.id_student = List;

            //}
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                List<students> students = new List<students>();
                students = (List<students>)ListAddStudent.ItemsSource;
                foreach (students student in ListAddStudent.SelectedItems)
                {
                    students.Remove(student);
                }
                ListAddStudent.ItemsSource = students;
                ListAddStudent.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка связи с базой данных, проверьте подключение к интернету", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                ListAddStudent.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Возникла непредвиденная ошибка, повторите попытку позже", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
