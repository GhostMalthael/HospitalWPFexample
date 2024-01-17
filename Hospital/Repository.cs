using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital
{
    public class Repository
    {
        public static List<User> users = new List<User>();
        public static List<Doctor> doctors = new List<Doctor>();
        public static List<Record> records = new List<Record>();
        public static bool enterUserAccount = false;
        public static bool enterAdminAccount = false;
        public string pathUserDataBase = @"C:\Users\alexc\Desktop\HospitalUsers.json";
        public string pathDoctorDataBase = @"C:\Users\alexc\Desktop\DoctorsDataBase.json";
        public string pathRecordsDataBase = @"C:\Users\alexc\Desktop\ListHealth.json";

        public User GetUserById(int userId)
        {
            return users.FirstOrDefault(x => x.Id == userId);
        }

        public void ReadFileJson()
        {
            string userDataBase = File.ReadAllText(pathUserDataBase);
            if (!string.IsNullOrEmpty(userDataBase))
            {
                users = JsonConvert.DeserializeObject<List<User>>(userDataBase);
            }

            string doctorDataBase = File.ReadAllText(pathDoctorDataBase);
            if(!string.IsNullOrEmpty(doctorDataBase))
            {
                doctors = JsonConvert.DeserializeObject<List<Doctor>>(doctorDataBase);
            }

            string recordDataBase = File.ReadAllText(pathRecordsDataBase);
            if(!string.IsNullOrEmpty(recordDataBase))
            {
                records = JsonConvert.DeserializeObject<List<Record>>(recordDataBase);
            }
        }

        public void RegAccount(string name, string surname, string login, string password)
        {
            ReadFileJson();
            if (users.FirstOrDefault(x => x.Login == login) != null)
            {
                //MessageBox.Show("Логин занят, введите другой");
                //return;
            }
            int userId = users.FirstOrDefault() != null ? users.Max(x => x.Id)+ 1 : 0;
            users.Add(new User(userId, name, surname, login, password));
            string userDataBase = JsonConvert.SerializeObject(users);
            File.WriteAllText(pathUserDataBase, userDataBase);
            //MessageBox.Show("Регистрация прошла успешно");
            Console.WriteLine("Регистрация прошла успешно");
        }

        public void EnterInAccount(string login, string password)
        {
            ReadFileJson();

            var user = users.FirstOrDefault(x => x.Login == login && x.Password == password);
            if(user != null)
            {
                if (login == "admin" && password == "admin")
                {
                    Console.WriteLine($"Добро пожаловать {login}");
                    //MessageBox.Show($"Добро пожаловать {login}");
                    //enterAdminAccount = true;
                    //new WorckerWindow().Show();
                    return;
                }
                //MessageBox.Show($"Добро пожаловать {login}");
                Console.WriteLine($"Добро пожаловать {login}");

                //enterUserAccount = true;
                //new MainWindow().Show();
                return;
            }   
        }
        //public void WriteDoctorFileJson(string fio, string experience, string imagePath)
        //{
        //    WorckerWindow worckerWindow = new WorckerWindow();
        //    ReadFileJson();

        //    if (worckerWindow.comboBoxCreateSpecialization.SelectedItem != null)
        //    {
        //        Doctor.AllSpecialization selectedSpecialization = (Doctor.AllSpecialization)worckerWindow.comboBoxCreateSpecialization.SelectedItem;

        //        int doctorId = doctors.FirstOrDefault() != null ? doctors.Max(x => x.Id) + 1 : 0;
        //        doctors.Add(new Doctor(doctorId, fio, experience, imagePath)
        //        {
        //            Specialization = selectedSpecialization
        //        });

        //        string doctorsDataBase = JsonConvert.SerializeObject(doctors);
        //        File.WriteAllText(pathDoctorDataBase, doctorsDataBase);
        //        MessageBox.Show($"Добавлен новый врач: {fio}");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Выберите специализацию перед добавлением врача.");
        //    }
        //}
        public void WriteDoctorFileJson(string fio, Doctor.AllSpecialization specialization, string experience, string imagePath)
        {
            ReadFileJson();

            int doctorId = doctors.FirstOrDefault() != null ? doctors.Max(x => x.Id) + 1 : 0;
            doctors.Add(new Doctor(doctorId, fio, experience, imagePath)
            {
                Specialization = specialization
            });

            string doctorsDataBase = JsonConvert.SerializeObject(doctors);
            File.WriteAllText(pathDoctorDataBase, doctorsDataBase);
            //MessageBox.Show($"Добавлен новый врач: {fio}");
            Console.WriteLine($"Добавлен новый врач: {fio}");
        }

        //public void WriteRecordsFileJson(Record.AllService service, string dateAndTime, Doctor.AllSpecialization specialization)
        //{
        //    ReadFileJson();
        //    //int doctorId = records.FirstOrDefault() != null ? records.Max(x => x.IdDoctor) + 0 : +1;
        //    int recordIdUser = records.FirstOrDefault() != null ? records.Max(x => x.IdUser) + 1 : +0;
        //    int visitId = records.FirstOrDefault() != null ? records.Max(x => x.IdVisit) + 0 : +1;
        //    int doctorId = Repository.doctors.FirstOrDefault(d => d.Specialization == specialization)?.Id ?? 0;

        //    records.Add(new Record(doctorId, recordIdUser, visitId, service, dateAndTime, specialization));
        //    string recordDataBase = JsonConvert.SerializeObject(records);
        //    File.WriteAllText(pathRecordsDataBase, recordDataBase);
        //    MessageBox.Show($"Запись добавлена");
        //}

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public void WriteRecordsFileJson(Record.AllService service, string dateAndTime, Doctor.AllSpecialization specialization, int userId)
        {
            ReadFileJson();
            int recordIdUser = records.FirstOrDefault() != null ? records.Max(x => x.IdUser) + 1 : 1;
            int visitId = records.FirstOrDefault() != null ? records.Max(x => x.IdVisit) + 1 : 1;
            int doctorId = Repository.doctors.FirstOrDefault(d => d.Specialization == specialization)?.Id ?? 0;

            records.Add(new Record(doctorId, userId, visitId, service, dateAndTime, specialization));
            string recordDataBase = JsonConvert.SerializeObject(records);
            File.WriteAllText(pathRecordsDataBase, recordDataBase);
            //MessageBox.Show($"Запись добавлена");
            Console.WriteLine($"Запись добавлена");
        }

        public void ReloadRecords(List<Record> record)
        {
            File.WriteAllText(pathRecordsDataBase, JsonConvert.SerializeObject(record));
            ReadFileJson();
            //Console.WriteLine("Файл обновлен");
            MessageBox.Show("Файл обновлен");
        }
        public void ReloadDoctors(List<Doctor>doctors)
        {
            File.WriteAllText(pathDoctorDataBase, JsonConvert.SerializeObject(doctors));
            ReadFileJson();
            Console.WriteLine("Файл обновлен");
            //MessageBox.Show("Файл обновлен");
        }
    }
}
