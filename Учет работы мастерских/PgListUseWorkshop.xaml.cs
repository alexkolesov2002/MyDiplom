using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для PgListUseWorkshop.xaml
    /// </summary>
    public partial class PgListUseWorkshop : Page, INotifyPropertyChanged
 

    {//VievModelJournalUseWorkshop vievModel = new VievModelJournalUseWorkshop();
        public dynamic MyProperty { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public PgListUseWorkshop()
        {
            InitializeComponent();
            List<journal_use_workshop> journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.ToList();
            //journal_use_workshop journal_Use_Workshops2 = BaseModel.BaseConnect.journal_use_workshop.FirstOrDefault(x=>x.id_workshop==1);
            //foreach (journal_use_workshop journal_Use_Workshop in journal_Use_Workshops)
            //{
            //    journal_Use_Workshop.workshops.title_workshop
            //}
            var a = journal_use_workshop.getlistEquipments();
            var GroupJournal = from journal in journal_Use_Workshops group journal by journal.id_event;
            
            //var EquipFromGroup = from JournalNew in GroupJournal where JournalNew.
            //MyProperty = GroupJournal;
            //PropertyChanged(this, new PropertyChangedEventArgs("MyProperty"));
            ListUseWorkShop.ItemsSource = a;
           
        }

        
    }
}
