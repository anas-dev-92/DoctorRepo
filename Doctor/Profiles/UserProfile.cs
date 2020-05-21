using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.Users, Models.User.GetAllUsers>();
            CreateMap<Models.User.UserForCreation, Entities.Users>();
            CreateMap<Entities.Users, Models.User.UserForUpdate>();
        }
       
    }
}
