﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для PgSelectWork.xaml
    /// </summary>
    public partial class PgSelectWork : Page, INotifyPropertyChanged
    {
        public int Loading { get; set; } = 0;
        public PgSelectWork()
        {
            InitializeComponent();
            ComboBoxWorkShops.ItemsSource = BaseModel.BaseConnect.workshops.ToList();
            ComboBoxWorkShops.DisplayMemberPath = "title_workshop";
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        async void Zagruzka()
        {
            while (Loading != 100)
            {
                Loading += 4;
                await Task.Delay(1);
                PropertyChanged(this, new PropertyChangedEventArgs("Loading"));
            }
            LoadPages.SwitchPages.Navigate(new PgTakeWorkShop((workshops)ComboBoxWorkShops.SelectedItem));


        }
        //void Zagruzka()
        //{
        //    while (Loading != 100)
        //    {
        //        Loading += 4;
        //        Thread.Sleep(100);
        //        PropertyChanged(this, new PropertyChangedEventArgs("Loading"));
        //    }


        //}


        private void BtnGoTakeWorkShop_Click(object sender, RoutedEventArgs e)
        {
            Zagruzka(); 
        }
    }
}
