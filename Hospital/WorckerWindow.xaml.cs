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
using System.IO;
using Microsoft.Win32;
using static Hospital.Doctor;
using System.Numerics;

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для WorckerWindow.xaml
    /// </summary>
    public partial class WorckerWindow : Window
    {
        private string imagePath;
        Repository repository = new Repository();
        public WorckerWindow()
        {
            InitializeComponent();
            foreach (Doctor.AllSpecialization specialization in Enum.GetValues(typeof(Doctor.AllSpecialization)))
            {
                comboBoxCreateSpecialization.Items.Add(specialization);
            }
            ShowDisplayWorcker();
        }

        private void buttonAddDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxFIODoctor.Text) || !string.IsNullOrEmpty(comboBoxCreateSpecialization.Text) || !string.IsNullOrEmpty(textBoxExperience.Text))
            {
                if (imagePath == null)
                {
                    imagePath = @"C:\\Users\\alexc\\Desktop\\noImage.jpg\";
                }
                else
                {
                    string destinationPath = @"C:\Users\alexc\source\repos\StoreWPF\StoreWPF\ImageStore\";
                    string uniqueFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(imagePath);
                    string destinationFilePath = System.IO.Path.Combine(destinationPath, uniqueFileName);
                    File.Copy(imagePath, destinationFilePath);

                    imagePath = destinationFilePath;
                }
                if (Enum.TryParse(typeof(Doctor.AllSpecialization), comboBoxCreateSpecialization.Text, out object specialization))
                {
                    repository.WriteDoctorFileJson(textBoxFIODoctor.Text, (Doctor.AllSpecialization)specialization, textBoxExperience.Text, imagePath);
                }
                else
                {
                    MessageBox.Show("Ошибка при выборе специализации");
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены");
            }
        }

        private void buttonExitWorker_Click(object sender, RoutedEventArgs e)
        {
            Repository.enterAdminAccount = false;
            Hide();
            new MainWindow().Show();
        }

        private void buttonAddImageDoctor_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri uri = new Uri(openFileDialog.FileName);
                imagePath = openFileDialog.FileName;
                imageDoctor.Source = new BitmapImage(uri);
            }
        }
        public void ShowDisplayWorcker()
        {
            listViewWorker.Items.Clear();
            foreach (var doctor in Repository.doctors)
            {
                UserControlHospital userControl = new UserControlHospital(doctor);
                listViewWorker.Items.Add(userControl);
            }
        }

        private void buttonDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            int idDeleteProduct = listViewWorker.SelectedIndex;
            if (idDeleteProduct == -1)
            {
                MessageBox.Show("Сперва нужно выбрать продукт для удаления");
            }
            else
            {
                Repository.doctors.RemoveAt(idDeleteProduct);
                repository.ReloadDoctors(Repository.doctors);
                ShowDisplayWorcker();
            }
        }

        //    private void buttonEditDoctor_Click(object sender, RoutedEventArgs e)
        //    {
        //        int idEditDoctor = listViewWorker.SelectedIndex;
        //        if (idEditDoctor == -1)
        //        {
        //            MessageBox.Show("Вы не выбрали продукт для изменения.");
        //            return;
        //        }
        //        if (string.IsNullOrEmpty(textBoxFIODoctor.Text) && string.IsNullOrEmpty(textBoxExperience.Text))
        //        {
        //            MessageBox.Show("Все поля должны быть заполнены");
        //            return;
        //        }
        //        if (comboBoxCreateSpecialization.SelectedItem == null)
        //        {
        //            MessageBox.Show("Выберите тип услуги");
        //            return;
        //        }
        //        if (string.IsNullOrEmpty(imagePath))
        //        {
        //            imagePath = @"C:\Users\alexc\Desktop\noImage.jpg";
        //        }
        //        Repository.doctors.RemoveAt(idEditDoctor);
        //        if (Enum.TryParse(typeof(Doctor.AllSpecialization), comboBoxCreateSpecialization.Text, out object specialization))
        //        {
        //            int doctorId = Repository.doctors.FirstOrDefault() != null ? Repository.doctors.Max(x => x.Id) + 1 : 0;

        //            //repository.WriteDoctorFileJson(textBoxFIODoctor.Text, (Doctor.AllSpecialization)specialization, textBoxExperience.Text, imagePath);
        //            Repository.doctors.Add(new Doctor(doctorId, textBoxFIODoctor.Text, textBoxExperience.Text, imagePath)
        //            {
        //                Specialization = specialization
        //            });
        //        }
        //        else
        //        {
        //            MessageBox.Show("Ошибка при выборе специализации");
        //        }
        //        repository.ReloadDoctors(Repository.doctors);
        //        ShowDisplayWorcker();
        //        idEditDoctor = -1;
        //    }
        private void buttonEditDoctor_Click(object sender, RoutedEventArgs e)
        {
            int idEditDoctor = listViewWorker.SelectedIndex;
            if (idEditDoctor == -1)
            {
                MessageBox.Show("Вы не выбрали продукт для изменения.");
                return;
            }
            if (string.IsNullOrEmpty(textBoxFIODoctor.Text) && string.IsNullOrEmpty(textBoxExperience.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }
            if (comboBoxCreateSpecialization.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип услуги");
                return;
            }
            if (string.IsNullOrEmpty(imagePath))
            {
                imagePath = @"C:\Users\alexc\Desktop\noImage.jpg";
            }

            Repository.doctors.RemoveAt(idEditDoctor);
            if (Enum.TryParse(typeof(Doctor.AllSpecialization), comboBoxCreateSpecialization.SelectedItem.ToString(), out object specialization))
            {
                if (specialization is Doctor.AllSpecialization)
                {
                    int doctorId = Repository.doctors.FirstOrDefault() != null ? Repository.doctors.Max(x => x.Id) + 1 : 0;

                    Repository.doctors.Add(new Doctor(doctorId, textBoxFIODoctor.Text, textBoxExperience.Text, imagePath)
                    {
                        Specialization = (Doctor.AllSpecialization)specialization
                    });
                }
                else
                {
                    MessageBox.Show("Ошибка при выборе специализации");
                }
            }
            else
            {
                MessageBox.Show("Ошибка при выборе специализации");
            }
            repository.ReloadDoctors(Repository.doctors);
            ShowDisplayWorcker();
            idEditDoctor = -1;
        }

    }
}
