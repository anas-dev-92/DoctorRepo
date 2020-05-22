using Doctor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Services
{
    public interface IAdviceRepository
    {
        void AddAdviceForDoctor(int doctorId, GeneralAdvice generalAdvice);
        void UpdateAdvice(int doctor, GeneralAdvice generalAdvice);
        void DeleteAdvice(GeneralAdvice generalAdvice);
        IEnumerable<GeneralAdvice> GetGeneralAdvicesForDoctor(int doctorId);
        GeneralAdvice GetGeneralAdviceForDoctor(int doctorId, int generalAdviceId);
        bool AdviceExists(int doctorId);
        void Savechnge();
    }
}
