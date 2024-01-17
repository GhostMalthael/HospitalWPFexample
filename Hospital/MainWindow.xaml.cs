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
using System.IO;
using System.Text.Json;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Repository repository = new Repository();
        public MainWindow()
        {
            InitializeComponent();
            ShowMainDisplay();
            ShowAllSpecializations();
        }

        public void ShowMainDisplay()
        {
            repository.ReadFileJson();
            wrapPanelMainWindow.Children.Clear();
            if (Repository.enterUserAccount == false)
            {
                buttonExit.Visibility = Visibility.Hidden;
                buttonEnter.Visibility = Visibility.Visible;
                buttonMyRecords.Visibility = Visibility.Hidden;
            }
            else
            {
                buttonExit.Visibility = Visibility.Visible;
                buttonEnter.Visibility = Visibility.Hidden;
                buttonMyRecords.Visibility = Visibility.Visible;
            }

            Doctor.AllSpecialization selectedSpecialization = comboBoxAllSpecialization.SelectedItem as Doctor.AllSpecialization? ?? Doctor.AllSpecialization.Unknown;

            if (selectedSpecialization == Doctor.AllSpecialization.Unknown)
            {
                foreach (var doctor in Repository.doctors)
                {
                    UserControlHospital userControlHospital = new UserControlHospital(doctor);
                    wrapPanelMainWindow.Children.Add(userControlHospital);
                    if (Repository.enterUserAccount == false)
                    {
                        userControlHospital.buttonSignUp.Visibility = Visibility.Hidden;
                        userControlHospital.dateTimeSignUpTextBox.Visibility = Visibility.Hidden;
                        userControlHospital.dateTimeSignUp.Visibility = Visibility.Hidden;
                        userControlHospital.serviceComboBox.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        userControlHospital.buttonSignUp.Visibility = Visibility.Visible;
                        userControlHospital.dateTimeSignUpTextBox.Visibility = Visibility.Visible;
                        userControlHospital.dateTimeSignUp.Visibility = Visibility.Visible;
                        userControlHospital.serviceComboBox.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                var filteredDoctors = Repository.doctors.Where(doctor => doctor.Specialization == selectedSpecialization);
                foreach (var doctor in filteredDoctors)
                {
                    UserControlHospital userControlHospital = new UserControlHospital(doctor);
                    wrapPanelMainWindow.Children.Add(userControlHospital);
                    if (Repository.enterUserAccount == false)
                    {
                        userControlHospital.buttonSignUp.Visibility = Visibility.Hidden;
                        userControlHospital.dateTimeSignUpTextBox.Visibility = Visibility.Hidden;
                        userControlHospital.dateTimeSignUp.Visibility = Visibility.Hidden;
                        userControlHospital.serviceComboBox.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        userControlHospital.buttonSignUp.Visibility = Visibility.Visible;
                        userControlHospital.dateTimeSignUpTextBox.Visibility = Visibility.Visible;
                        userControlHospital.dateTimeSignUp.Visibility = Visibility.Visible;
                        userControlHospital.serviceComboBox.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void ShowAllSpecializations()
        {
            comboBoxAllSpecialization.Items.Clear();

            foreach (Doctor.AllSpecialization specialization in Enum.GetValues(typeof(Doctor.AllSpecialization)))
            {
                comboBoxAllSpecialization.Items.Add(specialization);
            }
        }

        private void buttonEnter_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new Authorization().Show();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Repository.enterUserAccount = false;
            Repository.enterAdminAccount = false;
            ShowMainDisplay();
        }

        private void buttonMyRecords_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MyRecords().Show();
        }

        private void comboBoxAllSpecialization_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowMainDisplay();
        }
    }
}
