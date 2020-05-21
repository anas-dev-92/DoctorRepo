using Doctor.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Services.User
{
    public class UserRepository : IUserRepository
    {
        private readonly DoctorsDbContext _context;

        public UserRepository(DoctorsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void CreateUser(Users users)
        {
            try
            {
                _context.Users.Add(users);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        public void DeleteUser(Users users)
        {
            _context.Remove(users);
        }

        public IEnumerable<Users> GetUsers()
        {
           return _context.Users.ToList();
        }

        public Users GetUsers(int UserId)
        {
            return _context.Users.Where(u=>u.UserId==UserId).FirstOrDefault();
        }

        public void Savechnge()
        {
            _context.SaveChanges();
        }

        public void UpdateUser(int userId, Users users)
        {
            
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.UserId == userId);
        }
    }
}
