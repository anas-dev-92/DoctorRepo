using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Entities
{
    public class Doctors
    {
        [Key]
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Special { get; set; }
        public string Phone { get; set; }
        public string ShortDescription { get; set; }
        public int Age { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int YearOfExperence { get; set; }
        public string Password { get; set; }
        public string ClinicAddress { get; set; }
        public string  Role { get; set; }
        public ICollection<GeneralAdvice> generalAdvices { get; set; }
        = new List<GeneralAdvice>();
    }
}
