using Doctor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Services
{
     public interface IDoctorRepository
    {
        IEnumerable<Doctors> GetDoctors();
        Doctors GetDoctors(int doctorId);
       
        bool DoctorExists(int doctorId);
        
        void UpdateDoctor(int doctor, Doctors doctors);
       
        void DeleteDoctor(Doctors doctors);
        void CreateDoctor( Doctors doctors);
        void Savechnge();
       
    }
}
