using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Учет_работы_мастерских
{
    public class ItemPositionToIndexConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
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
            catch (Exception ex)
            {
                return null;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
