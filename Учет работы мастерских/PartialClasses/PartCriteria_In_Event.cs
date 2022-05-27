using System;
using System.Windows;

namespace Учет_работы_мастерских
{
    partial class criteria_in_event

    {


        public int Rating
        {
            get
            {
                if (rating == null) return 0;
                else
                {
                    return
                        (int)rating;
                }
            }
            set
            {
                try
                {
                    if (value > 101 || value < 0)
                    {
                        throw new Exception("Некорректо введены данные");
                    }
                    else
                    {
                        rating = value;
                    }
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

    }
}
