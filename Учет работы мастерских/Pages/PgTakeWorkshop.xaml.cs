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
                studentsBufForAdd = students;

                //ListPickedStudent.ItemsSource = studentsBufForResiult;

            }
            catch
            {

            }
        }



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
                foreach (Criterion criteria in (List<Criterion>)ListCriterion.ItemsSource)
                {
                    criteria criterion = new criteria() { title_criterion = criteria.title_criterion };
                    CriterionList.Add(criterion);
                }
                BaseModel.BaseConnect.criteria.AddRange(CriterionList);

                events events = new events() { exercise = TxtExercise.Text, title_event = TxtExercise.Text, id_type_event = (int)ComboBoxCompetisionSelect.SelectedValue, topic_of_the_lesson = TxtTopic.Text };
                BaseModel.BaseConnect.events.Add(events);
                BaseModel.BaseConnect.SaveChanges();
                events NewEvent = BaseModel.BaseConnect.events.SingleOrDefault(x => x.exercise == TxtExercise.Text && x.topic_of_the_lesson == TxtTopic.Text && x.id_type_event == (int)ComboBoxCompetisionSelect.SelectedValue);
                
                 await Task.Delay(15);

                foreach (students students in (List<students>)ListPickedStudent.ItemsSource)
                {
                    foreach (criteria criteria in CriterionList)
                    {
                        criteria_in_event criterion = new criteria_in_event() { id_criterion = criteria.id_criterion, id_event = NewEvent.id_event, };
                        criteria_In_Events.Add(criterion);
                    }
                }
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
                BtnSaveData.IsEnabled = false;
                SaveNotComplete = false;
                PropertyChanged(this, new PropertyChangedEventArgs("SaveNotComplete"));
                IsSaveComplete = true;
                PropertyChanged(this, new PropertyChangedEventArgs("IsSaveComplete"));
                //LoadPages.SwitchPages.Navigate(new PgTakeEquip((workshops)ComboBoxWorkShops.SelectedItem));
            }
            catch (Exception ex)
            {

            }


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
            Criterion criterion = new Criterion() { title_criterion = TxtCriterion.Text, id_Criterion = ListCriterion.Items.Count };
            CriterionTextList.Add(criterion);
            ListCriterion.ItemsSource = CriterionTextList;
            ListCriterion.Items.Refresh();
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

            }
        }

        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);
            studentsBufForResiult.Add(studentsBufForAdd.FirstOrDefault(x => x.id_student == id));
            studentsBufForAdd2.Remove(studentsBufForAdd.FirstOrDefault(x => x.id_student == id));
            ListPickedStudent.ItemsSource = studentsBufForResiult;
            ListAddStudent.ItemsSource = studentsBufForAdd2;
            ListAddStudent.Items.Refresh();
            ListPickedStudent.Items.Refresh();
            FindStudent();
        }

        async void FindStudent()
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
        private void TxtFindStudent_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindStudent();

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);
            studentsBufForAdd2.Add(studentsBufForResiult.FirstOrDefault(x => x.id_student == id));
            studentsBufForResiult.Remove(studentsBufForResiult.FirstOrDefault(x => x.id_student == id));
            ListAddStudent.ItemsSource = studentsBufForAdd2;
            ListPickedStudent.ItemsSource = studentsBufForResiult;
            ListPickedStudent.Items.Refresh();
            ListAddStudent.Items.Refresh();
            FindStudent();
        }
    }
}
