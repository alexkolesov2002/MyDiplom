using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

using System.Collections.Generic;
using System.IO;
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
        List<students> students = new List<students>();
        List<criteria_in_event> criteria_in_events;
        List<string> criteria_name = new List<string>();
        List<criteria_in_event> criteria_in_eventsNew = BaseModel.BaseConnect.criteria_in_event.ToList();
        int index;
        public PgRating(int index)
        {
            this.index = index;
            InitializeComponent();
            criteria_in_events = BaseModel.BaseConnect.criteria_in_event.Where(x => x.id_event == index).ToList();
            foreach (criteria_in_event student in criteria_in_events)
            {
                students studentsS = BaseModel.BaseConnect.students.FirstOrDefault(x=>x.id_student == student.id_student);
                students.Add(studentsS);
                criteria_name.Add(student.criteria.title_criterion);
            }
            ListRating.ItemsSource = students.Distinct();
           ListCriterionName.ItemsSource = criteria_name.Distinct();

        }

        private void TxtName_Loaded(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void Lenin_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox textBlock = (ListBox)sender;
            int id = Convert.ToInt32(textBlock.Uid);
            textBlock.ItemsSource = criteria_in_eventsNew.Where(x => x.id_event == index && x.id_student == id).ToList();
        }

        private void BtnSaveRating_Click(object sender, RoutedEventArgs e)
        {
            BaseModel.BaseConnect.SaveChanges();
            var doc = new Document();
            RenderTargetBitmap renderTargetBitmap =new RenderTargetBitmap(300, 300, 100, 100, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(Griddd);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            byte[] myBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                pngImage.Save(memoryStream); // Line ARGH

                // mission accomplished if myBytes is populated
                myBytes = memoryStream.ToArray();
            }
            
            //BitmapImage bitmapImage = new BitmapImage();
            //using (var stream = new MemoryStream())
            //{
            //    pngImage.Save(stream);
            //    stream.Seek(0, SeekOrigin.Begin);

            //    bitmapImage.BeginInit();
            //    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            //    bitmapImage.StreamSource = stream;
            //    bitmapImage.EndInit();
            //}
            //System.Windows.Forms.Clipboard.SetImage((Image)bitmapImage);


            PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\allad\Desktop\123" + @"\Document.pdf", System.IO.FileMode.Create));
            doc.Open();
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(myBytes); 
            jpg.Alignment = Element.ALIGN_CENTER;
            doc.Add(jpg);
            doc.Close();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid textBlock = (DataGrid)sender;
            int id = Convert.ToInt32(textBlock.Uid);
            
            textBlock.ItemsSource = criteria_in_eventsNew.Where(x => x.id_event == index && x.id_student == id).ToList();
        }
    }
}
