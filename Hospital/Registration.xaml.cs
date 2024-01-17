using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        Repository repository  = new Repository();
        public Registration()
        {
            InitializeComponent();
        }

        private void buttonLoginAccount_Click(object sender, RoutedEventArgs e)
        {
            if(regPasswordTextBox.Text != regRepeatPasswordTextBox.Text)
            {
                errorRepeatPassword.Visibility = Visibility.Visible;
                return;
            }
            else if(string.IsNullOrEmpty(regNameTextBox.Text) ||
                string.IsNullOrEmpty(regSurnameTextBox.Text) ||
                string.IsNullOrEmpty(regLoginTextBox.Text) ||
                string.IsNullOrEmpty(regPasswordTextBox.Text))
            {
                errorNullData.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                repository.RegAccount(regNameTextBox.Text, regSurnameTextBox.Text, regLoginTextBox.Text, regPasswordTextBox.Text);
                errorRepeatPassword.Visibility = Visibility.Hidden;
                errorNullData.Visibility= Visibility.Hidden;
            }
        }

        private void buttonExitAccount_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }

        private void buttonAutSwap_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new Authorization().Show();
        }
    }
}
