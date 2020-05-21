using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Profiles
{
    public class DoctorProfile :Profile
    {
        public DoctorProfile()
        {
            CreateMap<Entities.Doctors, Models.DoctorWithoutAdvice>();
            CreateMap<Models.DoctorForCreate, Entities.Doctors>();
            CreateMap<Entities.Doctors, Models.DoctorForUpdate>();
        }
    }
}
