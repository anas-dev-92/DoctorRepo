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
        IEnumerable<GeneralAdvice> GetGeneralAdvicesForDoctor(int doctorId);
        GeneralAdvice GetGeneralAdviceForDoctor(int doctorId, int generalAdviceId);
        bool DoctorExists(int doctorId);
        void AddAdviceForDoctor(int doctorId, GeneralAdvice generalAdvice);
        void UpdateAdvice(int doctor, GeneralAdvice generalAdvice);
        void UpdateDoctor(int doctor, Doctors doctors);
        void DeleteAdvice(GeneralAdvice generalAdvice);
        void DeleteDoctor(Doctors doctors);
        void CreateDoctor( Doctors doctors);
        void Savechnge();
       
    }
}
