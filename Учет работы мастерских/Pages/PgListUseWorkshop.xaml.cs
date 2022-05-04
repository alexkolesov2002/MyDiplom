﻿using System;
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
               
            ListUseWorkShop.ItemsSource = journal_use_workshop.getlistEquipments();
           
        }

        private void BtnAddRating_Click(object sender, RoutedEventArgs e)
        {
            List <criteria_in_event> FullListCriteria= BaseModel.BaseConnect.criteria_in_event.ToList();
            var GroupJournal = from Event in FullListCriteria group Event by Event.id_event;
        }
    }
}
