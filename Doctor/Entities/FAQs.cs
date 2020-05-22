using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Entities
{
    public class FAQs
    {
        [Key]
        public int FaqId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
