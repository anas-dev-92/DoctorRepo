using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Entities
{
    public class GeneralAdvice
    {
        [Key]
        public int AdviceId { get; set; }
        public Doctors Doctors { get; set; }
        public int DoctorId { get; set; }
        public string AdviceTitle { get; set; }
        public string AdviceContent { get; set; }
    }
}
