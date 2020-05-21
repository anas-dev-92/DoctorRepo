using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Models
{
    public class DoctorForUpdate
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Special { get; set; }
        public string Phone { get; set; }
        [Required]
        [MaxLength(800)]
        public string ShortDescription { get; set; }
        [Required]
        public int Age { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int YearOfExperence { get; set; }
        public string Password { get; set; }
        public string ClinicAddress { get; set; }
    }
}
