﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Переменные
        public PgTakeEquip shop;

        public event PropertyChangedEventHandler PropertyChanged;
        public string NameUs { get; }
        public string SurNameUs { get; }
        public string RoleUs { get; }
        public string MyDate { get; set; }
        public string MyTime { get; set; }
        const double NumberSystem = 60;
        const double baseAngleNumberSystem = 360 / NumberSystem;
        const double baseAngleHour = 30;
        users CurrentUser;
        private bool checkStyle;

        public bool CheckStyle
        {
            get { return checkStyle; }
            set
            {
                if (value == true)
                {
                    checkStyle = value;
                    try
                    {
                        Button4.Style = (Style)this.Resources["menuButtonActive"];
                        Button2.Style = (Style)this.Resources["menuButton"];
                        Button3.Style = (Style)this.Resources["menuButton"];
                        Button1.Style = (Style)this.Resources["menuButton"];
                        LoadPages.SwitchPages.Navigate(new PgListUseWorkshop(CurrentUser));
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("Данный раздел сейчас недоступен", "Предупреждение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1);

                    }
                }
            }
        }

        #endregion



        public MainWindow(users CurrentUser)
        {
            try
            {
                InitializeComponent();
                this.CurrentUser = CurrentUser;
                NameUs = CurrentUser.name + " ";
                PropertyChanged(this, new PropertyChangedEventArgs("NameUs"));
                SurNameUs = CurrentUser.surname;
                PropertyChanged(this, new PropertyChangedEventArgs("SurNameUs"));
                RoleUs = CurrentUser.roles.role_title;
                PropertyChanged(this, new PropertyChangedEventArgs("RoleUs"));
                // shop = new PgTakeWorkShop();
                MainFrame.Navigate(new PgSelectWork(CurrentUser));
                LoadPages.SwitchPages = MainFrame;
                StartClock();
                if (CurrentUser.id_role == 1)
                {
                    Button2.Visibility = Visibility.Visible;
                    Button3.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {

            }
            // ShowMarksClockFace();

        }



        private void MainScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button2.Style = (Style)this.Resources["menuButtonActive"];
                Button1.Style = (Style)this.Resources["menuButton"];
                Button3.Style = (Style)this.Resources["menuButton"];
                Button4.Style = (Style)this.Resources["menuButton"];
                LoadPages.SwitchPages.Navigate(new PgSelectChart());
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Данный раздел сейчас недоступен", "Предупреждение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1);
            }
        }



        public void Button4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button4.Style = (Style)this.Resources["menuButtonActive"];
                Button2.Style = (Style)this.Resources["menuButton"];
                Button3.Style = (Style)this.Resources["menuButton"];
                Button1.Style = (Style)this.Resources["menuButton"];
                LoadPages.SwitchPages.Navigate(new PgListUseWorkshop(CurrentUser));
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Данный раздел сейчас недоступен", "Предупреждение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1);

            }
        }



        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button1.Style = (Style)this.Resources["menuButtonActive"];
                Button2.Style = (Style)this.Resources["menuButton"];
                Button3.Style = (Style)this.Resources["menuButton"];
                Button4.Style = (Style)this.Resources["menuButton"];
                LoadPages.SwitchPages.Navigate(new PgSelectWork(CurrentUser));
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Данный раздел сейчас недоступен", "Предупреждение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1);
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button3.Style = (Style)this.Resources["menuButtonActive"];
            Button2.Style = (Style)this.Resources["menuButton"];
            Button1.Style = (Style)this.Resources["menuButton"];
            Button4.Style = (Style)this.Resources["menuButton"];

            LoadPages.SwitchPages.Navigate(new PgAddStudentsxaml());
        }
        #region Вычисление текущего времени

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                var rotateSecondArrow = new RotateTransform();
                var rotateMinuteArrow = new RotateTransform();
                var rotateHourArrow = new RotateTransform();


                // Данные текущего времени.
                int sec = DateTime.Now.Second;
                int min = DateTime.Now.Minute;
                int hour = DateTime.Now.Hour;
                MyDate = DateTime.Now.ToLongDateString();
                PropertyChanged(this, new PropertyChangedEventArgs("MyDate"));
                MyTime = DateTime.Now.ToShortTimeString();
                PropertyChanged(this, new PropertyChangedEventArgs("MyTime"));



                // Вычисленный угол для секундной стрелки.
                rotateSecondArrow.Angle = baseAngleNumberSystem * sec;

                // Вращение стрелки на вычисленный угол.




                // Угол минутной стрелки от количества полных минут плюс
                // угол секунд приведенный к долям текущей минуты.
                rotateMinuteArrow.Angle = (min * baseAngleNumberSystem) + (rotateSecondArrow.Angle / 60.0);

                MinuteArrow.RenderTransform = rotateMinuteArrow;



                // Данные часа конвертируем в 12-часовой вид,
                // вычисляем угол полных часов плюс
                // угол минут приведенный к долям текущего часа.
                rotateHourArrow.Angle = (hour - 12) * baseAngleHour + rotateMinuteArrow.Angle / 12;

                HourArrow.RenderTransform = rotateHourArrow;



                // После вычисления всех углов и поворотов стрелок,
                // включаем видимость формы.
                this.Show();
            }
            catch
            {

            }
        }


        #endregion



        #region Система перемещения окна

        bool move = false;
        Point constPosition;



        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                // Зафиксируем неизменяемую позицию.
                constPosition = e.GetPosition(this);

                // Разрешаем движение.
                move = true;

                // Чтобы мышь не теряла окно, даже если окно скрывается под тормост окнами.
                this.CaptureMouse();
            }
            catch
            {

            }
        }



        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (move == true)
            {
                // Вычисляем разницу между бывшим и текущим положением курсора от края окна.
                double deltaX = e.GetPosition(this).X - constPosition.X;
                double deltaY = e.GetPosition(this).Y - constPosition.Y;


                // Положение окна тут же корректируем на вычисленную величину(разницу).
                this.Left += deltaX;
                this.Top += deltaY;
            }

        }



        private void Window_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            // Запрещаем движение и отпускаем мышь.
            move = false;
            this.ReleaseMouseCapture();

        }

        #endregion



        #region Дополнительные методы

        // Запуск часов.
        void StartClock()
        {
            try
            {


                // Скрываем форму пока не заработали часы.
                // После первого события Timer_Tick,
                // когда стрелки скорректируют своё положение,
                // сделаем форму видимой.
                this.Hide();

                var timer = new DispatcherTimer();

                timer.Tick += Timer_Tick;
                timer.Start();
            }
            catch
            {

            }
        }


        // Показываем или скрываем метки циферблата
        void ShowMarksClockFace(bool show = true)
        {
            try
            {
                //MinuteMarks();

                HourMarks();
            }
            catch
            {

            }
        }






        // Часовая маркировка циферблата
        void HourMarks()
        {
            try
            {


                for (int i = 0; i < 12; i++)
                {
                    var b = new Border()
                    {
                        // Определяет толщину метки.
                        Height = 2,

                        RenderTransformOrigin = new Point(0.5, 0.5),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Center
                    };


                    var b1 = new Border()
                    {
                        // Определяет длину метки.
                        Width = 10,

                        Background = Brushes.Black,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        BorderBrush = Brushes.Black
                    };


                    b.Child = b1;

                    // Часовые метки через 30 градусов.
                    // Вращаем только контейнер.
                    var rotate = new RotateTransform(i * 30);
                    b.RenderTransform = rotate;


                    ClockFace.Children.Add(b);
                }
            }
            catch
            {

            }
        }










        #endregion

        private void BtnPowerOff_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void LeftBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            new WinAutorise().Show();
            this.Close();

        }
    }
}

