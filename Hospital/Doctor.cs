using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class Doctor
    {
        public int Id { get; set; }

        public string FIO { get; set; }

        public string Experience { get; set; }

        public string ImagePath { get; set; }
        public AllSpecialization Specialization { get; set; }

        public Doctor(int id, string fIO, string experience, string imagePath)
        {
            Id = id;
            FIO = fIO;
            Experience = experience;
            ImagePath = imagePath;
            
        }
        public enum AllSpecialization
        {
            Кардиолог,
            Невролог,
            Ортопед,
            Гастроэнтеролог,
            Стоматолог,
            Unknown
        }
    }
}
