using Doctor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Services.Admin
{
    public class AdminRepository :IAdminRepository
    {
        
            private readonly DoctorsDbContext _context;

            public AdminRepository(DoctorsDbContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public void CreateAdmin(Admins admins)
            {
                try
                {
                    _context.Admins.Add(admins);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
            }

            public void DeleteAdmin(Admins admins)
            {
                _context.Remove(admins);
            }

            public IEnumerable<Admins> GetAdmins()
            {
                return _context.Admins.ToList();
            }

            public Admins GetAdmin(int adminId)
            {
                return _context.Admins.Where(u => u.AdminId == adminId).FirstOrDefault();
            }

            public void Savechnge()
            {
                _context.SaveChanges();
            }

            public void UpdateAdmin(int adminId, Admins admins)
            {

            }

            public bool AdminExists(int adminId)
            {
                return _context.Admins.Any(u => u.AdminId == adminId);
            }
        
    }
}
