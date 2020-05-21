using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Models.User
{
    public class GetAllUsers
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public string MedicalHistory { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
