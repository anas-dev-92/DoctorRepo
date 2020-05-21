using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Profiles
{
    public class AdviceProfile :Profile
    {
        public AdviceProfile()
        {
            CreateMap<Entities.GeneralAdvice, Models.GeneralAdviceDto>();
            CreateMap<Models.AdviceForCreation, Entities.GeneralAdvice>();
            CreateMap<Models.AdviceForUpdate, Entities.GeneralAdvice>().ReverseMap(); 
            //another option for above reverseMap
            //CreateMap<Entities.GeneralAdvice, Models.AdviceForUpdate>();

        }
    }
}
