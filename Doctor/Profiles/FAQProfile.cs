using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Profiles
{
    public class FAQProfile:Profile
    {
        public FAQProfile()
        {
            CreateMap<Entities.FAQs, Models.FAQ.GetFAQ>();
            CreateMap<Models.FAQ.FAQforCreate, Entities.FAQs>();
            CreateMap<Entities.FAQs, Models.FAQ.FAQforUpdate>();
        }
            
    }
}
