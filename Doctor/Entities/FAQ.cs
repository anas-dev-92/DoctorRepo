using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Entities
{
    public class FAQ
    {
        [Key]
        public int FaqId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string CeratedAt { get; set; }
        public string UpdateAt { get; set; }
        public string DeletedAt { get; set; }
    }
}
