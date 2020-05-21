using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Models.User
{
    public class UserForCreation
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        [MaxLength(800)]
        public string MedicalHistory { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
