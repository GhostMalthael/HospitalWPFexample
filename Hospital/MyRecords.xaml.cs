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
using System.Windows.Shapes;

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для MyRecords.xaml
    /// </summary>
    public partial class MyRecords : Window
    {
        Repository repository = new Repository();

        public MyRecords()
        {
            InitializeComponent();
            ShowDisplay();
        }

        private void buttonBackOnCart_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }

        public void ShowDisplay()
        {
            repository.ReadFileJson();
            wrapPanelInCart.Children.Clear();

            foreach (var record in Repository.records)  
            {
                Doctor assignedDoctor = Repository.doctors.FirstOrDefault(d => d.Id == record.IdDoctor);

                RecordUserControl recordUserControl = new RecordUserControl(record, assignedDoctor?.Specialization ?? Doctor.AllSpecialization.Unknown);
                wrapPanelInCart.Children.Add(recordUserControl);
            }
        }
    }
}
