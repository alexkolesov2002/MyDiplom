﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Учет_работы_мастерских
{
   public class ItemPositionToIndexConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ListViewItem lvItem = value as ListViewItem;
            int index = 0;

            if (lvItem != null)
            {
                ListView listView = ItemsControl.ItemsControlFromItemContainer(lvItem) as ListView;
                //нумерацию будем вести с единицы
                index = listView.ItemContainerGenerator.IndexFromContainer(lvItem) + 1;
            }

            return index;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
