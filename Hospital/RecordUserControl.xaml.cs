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

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для RecordUserControl.xaml
    /// </summary>
    public partial class RecordUserControl : UserControl
    {
        private static MyRecords myRecords = new MyRecords();
        private Record Record { get; set; }
        private Doctor.AllSpecialization DoctorSpecialization { get; set; }
        Repository repository = new Repository();
        public RecordUserControl(Record Record, Doctor.AllSpecialization DoctorSpecialization)
        {
            InitializeComponent();
            this.Record = Record;
            this.DoctorSpecialization = DoctorSpecialization;
            recordTimeTextBlock.Text = $"Дата и время приема: {Record.DateAndTime}";
            recordNameDoctor.Text = $"Прием у врача: {this.DoctorSpecialization}";

        }

        private void buttonCanselSignUp_Click(object sender, RoutedEventArgs e)
        {
            int recordId = Record.IdVisit;
            repository.ReadFileJson();
            var recordToRemove = Repository.records.FirstOrDefault(item => item.IdVisit == recordId);
            if (recordToRemove != null)
            {
                Repository.records.Remove(recordToRemove);
                var deleteProduct = myRecords.wrapPanelInCart.Children.OfType<RecordUserControl>().FirstOrDefault(item => item.Record.IdVisit == recordId);
                if (deleteProduct != null)
                {
                    myRecords.wrapPanelInCart.Children.Remove(deleteProduct);
                }
                for (int i = 0; i < Repository.records.Count; i++)
                {
                    Repository.records[i].IdVisit = i + 1;
                }
                repository.ReloadRecords(Repository.records);
            }
        }
    }
}
