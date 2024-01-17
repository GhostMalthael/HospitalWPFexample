using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class Record
    {
        public int IdDoctor {  get; set; }
        public int IdUser {  get; set; }
        public int IdVisit {  get; set; }
        public AllService Service { get; set; }   
        public string DateAndTime {  get; set; }
        public Doctor.AllSpecialization Specialization { get; set; }

        public Record(int idDoctor, int idUser, int idVisit, AllService service, string dateAndTime, Doctor.AllSpecialization specialization)
        {
            this.IdDoctor = idDoctor;
            this.IdUser = idUser;
            this.IdVisit = idVisit;
            Service = service;
            this.DateAndTime = dateAndTime;
            this.Specialization = specialization;
        }
        public enum AllService
        {
            Первичный,
            Вторичный
        }
    }
}
