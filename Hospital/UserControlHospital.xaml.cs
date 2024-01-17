using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
    /// Логика взаимодействия для UserControlHospital.xaml
    /// </summary>
    public partial class UserControlHospital : UserControl
    {
        public static ListView ListView { get; set; }
        private Doctor doctor;
        private User user;
        Repository repository = new Repository();
        public UserControlHospital(Doctor doctor)
        {
            InitializeComponent();
            this.doctor = doctor;
            doctorFIO.Text = $"ФИО: {doctor.FIO}";
            doctorSpecialization.Text = $"Специализация: {this.doctor.Specialization}";
            doctorExperience.Text = $"Стаж: {this.doctor.Experience}";
            imageUserControl.Source = new BitmapImage(new Uri(this.doctor.ImagePath));
            foreach (Record.AllService service in Enum.GetValues(typeof(Record.AllService)))
            {
                serviceComboBox.Items.Add(service);
            }
        }

        //public void buttonSignUp_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(dateTimeSignUpTextBox.Text))
        //    {
        //        if (serviceComboBox.SelectedItem != null)
        //        {
        //            Record.AllService selectedService = (Record.AllService)serviceComboBox.SelectedItem;
        //            repository.WriteRecordsFileJson(selectedService, dateTimeSignUpTextBox.Text, doctor.Specialization);
        //            MessageBox.Show("Запись добавлена");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Выберите тип услуги");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Укажите дату и время");
        //    }
        //}
        public void buttonSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(dateTimeSignUpTextBox.Text))
            {
                if (serviceComboBox.SelectedItem != null)
                {
                    Record.AllService selectedService = (Record.AllService)serviceComboBox.SelectedItem;

                    int userId = this.doctor.Id;

                    repository.WriteRecordsFileJson(selectedService, dateTimeSignUpTextBox.Text, doctor.Specialization, userId);
                }
                else
                {
                    MessageBox.Show("Выберите тип услуги");
                }
            }
            else
            {
                MessageBox.Show("Укажите дату и время");
            }
        }


        private void serviceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
