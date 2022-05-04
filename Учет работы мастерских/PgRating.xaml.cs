using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для PgRating.xaml
    /// </summary>
    public partial class PgRating : Page
    {
        public PgRating(dynamic List)
        {
            
            InitializeComponent();
            ListRating.ItemsSource = List;
        }
    }
}
