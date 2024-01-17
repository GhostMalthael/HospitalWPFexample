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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        Repository repository = new Repository();
        public Authorization()
        {
            InitializeComponent();
        }

        private void buttonExitAccount_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MainWindow().Show();
        }

        private void buttonEnterInAccount_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(autLoginTextBox.Text) || string.IsNullOrEmpty(autPasswordTextBox.Text))
            {
                errorNullData.Visibility = Visibility.Visible;
                errorEnterData.Visibility = Visibility.Hidden;
            }
            else if (!Repository.users.Any(x => x.Login == autLoginTextBox.Text || x.Password == autPasswordTextBox.Text))
            {
                errorEnterData.Visibility = Visibility.Visible;
                errorNullData.Visibility = Visibility.Hidden;
            }
            else
            {
                repository.ReadFileJson();
                repository.EnterInAccount(autLoginTextBox.Text, autPasswordTextBox.Text);
                errorEnterData.Visibility = Visibility.Hidden;
                errorNullData.Visibility = Visibility.Hidden;
                Hide();
            }
        }

        private void buttonRegSwap_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new Registration().Show();
        }
    }
}
