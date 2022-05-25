using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для PgTakeWorkShop.xaml
    /// </summary>
    public partial class PgTakeWorkshop : Page, INotifyPropertyChanged
    {
        #region глобальные переменные

        public int Loading { get; set; } = 0;
        public bool IsSaveComplete { get; set; } = false;
        public bool SaveNotComplete { get; set; } = true;
        workshops TakedWorkShop;
        users CurrentUser;
        List<equipments> TakedEquipments;
        public event PropertyChangedEventHandler PropertyChanged;
        List<journal_use_workshop> journal_Use_Workshop = new List<journal_use_workshop>();
        List<Criterion> CriterionTextList = new List<Criterion>();
        List<criteria> CriterionList = new List<criteria>();
        List<criteria> CriterionListBuf = new List<criteria>();
        List<students> students = BaseModel.BaseConnect.students.ToList();
        List<students> studentsBufForAdd = new List<students>();
        List<students> studentsBufForAdd2 = BaseModel.BaseConnect.students.ToList();
        List<students> studentsBufForResiult = new List<students>();
        List<criteria_in_event> criteria_In_Events = new List<criteria_in_event>();



        #endregion
        public PgTakeWorkshop(workshops TakedWorkShop, users CurrentUser, List<equipments> TakedEquipments)
        {
            try
            {

                InitializeComponent();
                this.CurrentUser = CurrentUser;
                this.TakedWorkShop = TakedWorkShop;
                this.TakedEquipments = TakedEquipments;
                ComboBoxCompetisionSelect.ItemsSource = BaseModel.BaseConnect.types_event.ToList();
                ComboBoxCompetisionSelect.DisplayMemberPath = "title_type_event";
                ComboBoxCompetisionSelect.SelectedValuePath = "id_type_event";
                ListAddGroup.ItemsSource = BaseModel.BaseConnect.groups.ToList();
                CalendarDate.DisplayDateStart = DateTime.Now;
                studentsBufForAdd = students;

                //ListPickedStudent.ItemsSource = studentsBufForResiult;

            }
            catch
            {
                System.Windows.MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        async void Zagruzka()
        {
            //try
            //{
            //    while (Loading != 100)
            {
                Loading += 4;
                await Task.Delay(4);
                PropertyChanged(this, new PropertyChangedEventArgs("Loading"));
            }

            if ((List<Criterion>)ListCriterion.ItemsSource == null)
            {
                throw new Exception("Добавьте критерии для мероприятия");
            }
            foreach (Criterion criteria in (List<Criterion>)ListCriterion.ItemsSource)
            {
                criteria criterion = new criteria() { title_criterion = criteria.title_criterion };
                CriterionList.Add(criterion);
            }
            if (ComboBoxCompetisionSelect.SelectedValue == null)
            {
                throw new Exception("Необходимо выбрать тип мероптиятия");
            }
            BaseModel.BaseConnect.criteria.AddRange(CriterionList);

            events events = new events() { exercise = TxtExercise.Text, title_event = TxtName.Text, id_type_event = (int)ComboBoxCompetisionSelect.SelectedValue, topic_of_the_lesson = TxtTopic.Text };
            BaseModel.BaseConnect.events.Add(events);
            BaseModel.BaseConnect.SaveChanges();
            events NewEvent = BaseModel.BaseConnect.events.FirstOrDefault(x => x.id_event == events.id_event);

            await Task.Delay(15);
            var st = ListPickedStudent.ItemsSource;

            foreach (students students in st)
            {
                foreach (criteria criteria in CriterionList)
                {
                    criteria_in_event criterion = new criteria_in_event() { id_criterion = criteria.id_criterion, id_event = NewEvent.id_event, id_student = students.id_student };
                    criteria_In_Events.Add(criterion);
                }
            }
            BaseModel.BaseConnect.criteria_in_event.AddRange(criteria_In_Events);
            BaseModel.BaseConnect.SaveChanges();

            string Date = CalendarDate.SelectedDate.Value.ToShortDateString();
            string Time = ClockTime.Time.ToLongTimeString();


            DateTime dateTime = Convert.ToDateTime(Date + " " + Time);
            if (TakedEquipments != null)
            {
                foreach (equipments equipment in TakedEquipments)
                {
                    journal_use_workshop use = new journal_use_workshop() { id_equipment = equipment.id_equipment, id_user = CurrentUser.id_user, id_workshop = TakedWorkShop.id_workshop, date_use = dateTime, count_equipment = equipment.Count, id_event = NewEvent.id_event };
                    journal_Use_Workshop.Add(use);
                }
                BaseModel.BaseConnect.journal_use_workshop.AddRange(journal_Use_Workshop);
            }
            else
            {
                journal_use_workshop use = new journal_use_workshop() { id_equipment = null, count_equipment = null, id_user = CurrentUser.id_user, id_workshop = TakedWorkShop.id_workshop, date_use = dateTime, id_event = NewEvent.id_event };
                BaseModel.BaseConnect.journal_use_workshop.Add(use);
            }

            BaseModel.BaseConnect.SaveChanges();
            //BtnSaveData.IsEnabled = false;
            SaveNotComplete = false;
            PropertyChanged(this, new PropertyChangedEventArgs("SaveNotComplete"));
            IsSaveComplete = true;
            PropertyChanged(this, new PropertyChangedEventArgs("IsSaveComplete"));
            //LoadPages.SwitchPages.Navigate(new PgTakeEquip((workshops)ComboBoxWorkShops.SelectedItem));
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}




        }

        private void ClockTime_Loaded(object sender, RoutedEventArgs e)
        {
            ClockTime.Time = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
            CalendarDate.SelectedDate = DateTime.Now;
        }


        private void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            Zagruzka();
        }

        private void ComboBoxWorkShops_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnAddCriterion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Criterion criterion = new Criterion() { title_criterion = TxtCriterion.Text, id_Criterion = ListCriterion.Items.Count };
                CriterionTextList.Add(criterion);
                ListCriterion.ItemsSource = CriterionTextList;
                ListCriterion.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDeleteFromList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int id = Convert.ToInt32(button.Uid);
                CriterionTextList.Remove(CriterionTextList.FirstOrDefault(x => x.id_Criterion == id));
                ListCriterion.Items.Refresh();

            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                int id = Convert.ToInt32(button.Uid);
                studentsBufForResiult.Add(studentsBufForAdd.FirstOrDefault(x => x.id_student == id));
                studentsBufForAdd2.Remove(studentsBufForAdd.FirstOrDefault(x => x.id_student == id));
                studentsBufForAdd2 = (List<students>)studentsBufForAdd2.Distinct().ToList();
                studentsBufForResiult = (List<students>)studentsBufForResiult.Distinct().ToList();
                ListPickedStudent.ItemsSource = studentsBufForResiult.Distinct();
                ListAddStudent.ItemsSource = studentsBufForAdd2.Distinct();
                ListAddStudent.Items.Refresh();
                ListPickedStudent.Items.Refresh();
                FindStudent();
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        async void FindStudent()
        {
            try
            {
                studentsBufForAdd = students;

                if (TxtFindStudent.Text != "")
                {
                    studentsBufForAdd = studentsBufForAdd2.FindAll(x => x.FullName.Contains(TxtFindStudent.Text)).ToList();
                    ListAddStudent.ItemsSource = studentsBufForAdd;
                    ListAddStudent.Items.Refresh();
                    ListAddStudent.Visibility = Visibility.Visible;

                }
                else
                {
                    ListAddStudent.Visibility = Visibility.Collapsed;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        private void TxtFindStudent_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindStudent();

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                Button button = (Button)sender;
                int id = Convert.ToInt32(button.Uid);
                studentsBufForAdd2.Add(studentsBufForResiult.FirstOrDefault(x => x.id_student == id));
                studentsBufForResiult.Remove(studentsBufForResiult.FirstOrDefault(x => x.id_student == id));
                studentsBufForAdd2 = (List<students>)studentsBufForAdd2.Distinct().ToList();
                studentsBufForResiult = (List<students>)studentsBufForResiult.Distinct().ToList();
                ListAddStudent.ItemsSource = studentsBufForAdd2.Distinct();
                ListPickedStudent.ItemsSource = studentsBufForResiult.Distinct();
                ListPickedStudent.Items.Refresh();
                ListAddStudent.Items.Refresh();
                FindStudent();
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox checkBox = (CheckBox)sender;
                int id = Convert.ToInt32(checkBox.Uid);
                studentsBufForResiult.AddRange(BaseModel.BaseConnect.students.Where(x => x.id_group == id).ToList());
                List<students> s = new List<students>();
                foreach (students student in studentsBufForAdd2)
                {
                    s.Add(student);
                }

                foreach (students student in s)
                {
                    if (student.id_group == id)
                    {
                        studentsBufForAdd2.Remove(student);
                    }
                }
                ListPickedStudent.ItemsSource = studentsBufForResiult.Distinct().ToList();
                ListPickedStudent.Items.Refresh();
                FindStudent();
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox checkBox = (CheckBox)sender;
                int id = Convert.ToInt32(checkBox.Uid);
                List<students> s = new List<students>();
                studentsBufForResiult.AddRange(BaseModel.BaseConnect.students.Where(x => x.id_group == id).ToList());
                List<students> s1 = new List<students>();
                foreach (students student in studentsBufForResiult)
                {
                    s1.Add(student);
                }

                foreach (students student in s1)
                {
                    if (student.id_group == id)
                    {
                        studentsBufForAdd2.Add(student);
                    }
                }
                studentsBufForAdd2 = (List<students>)studentsBufForAdd2.Distinct().ToList();

                foreach (students student in studentsBufForResiult)
                {
                    if (student.id_group == id)
                    {
                        s.Add(student);
                    }

                }
                foreach (students student in s)
                {

                    studentsBufForResiult.Remove(student);
                }
                ListPickedStudent.ItemsSource = studentsBufForResiult.Distinct();
                ListPickedStudent.Items.Refresh();
                FindStudent();
            }
            catch
            {
                MessageBox.Show("Ошибка, повторите попытку позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}