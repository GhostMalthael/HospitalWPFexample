using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class User
    {
        private int id;
        public int Id 
        { 
            get { return id; }
            set { id = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string surname;
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        private string login;
        public string Login
        {
            get { return login; }
            set {  login = value; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public List<Record> Records { get; set; }

        public User(int id, string name, string surname, string login, string password) 
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Login = login;
            this.Password = password;
            Records = new List<Record>();

        }
    }
}
