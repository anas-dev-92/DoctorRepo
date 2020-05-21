using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Profiles
{
    public class AdminProfile:Profile
    {
        public AdminProfile()
        {
            CreateMap<Entities.Admins, Models.Admin.GetAdmin>();
            CreateMap<Models.Admin.AdminForCreate, Entities.Admins>();
            CreateMap<Entities.Admins, Models.Admin.AdminForUpdate>();
        }
    }
}
