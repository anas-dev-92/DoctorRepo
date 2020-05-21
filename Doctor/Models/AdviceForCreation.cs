using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Models
{
    public class AdviceForCreation
    {
        [Required]
        public string AdviceTitle { get; set; }
        [MaxLength(200)]
        public string AdviceContent { get; set; }
    }
}
