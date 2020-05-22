﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Entities
{
    public class Admins
    {
        [Key]
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
