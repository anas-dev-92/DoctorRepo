using Doctor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Services.Admin
{
    public interface IAdminRepository
    {
        IEnumerable<Admins> GetAdmins();
        Admins GetAdmin(int AdminId);
        bool AdminExists(int AdminId);
        void CreateAdmin(Admins admin);
        void UpdateAdmin(int userId, Admins admin);
        void DeleteAdmin(Admins admin);
        void Savechnge();
    }
}
