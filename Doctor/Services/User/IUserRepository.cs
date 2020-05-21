using Doctor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Services.User
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetUsers();
        Users GetUsers(int UserId);
        bool UserExists(int userId);
        void CreateUser(Users users);
        void UpdateUser(int userId, Users users);
        void DeleteUser(Users users);
        void Savechnge();
    }
}
