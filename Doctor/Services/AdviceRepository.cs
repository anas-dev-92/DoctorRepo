using Doctor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Services
{
    public class AdviceRepository:IAdviceRepository
    {
        private readonly DoctorsDbContext _doctorDbContext;

        public AdviceRepository(DoctorsDbContext doctorsDbContext)
        {
            _doctorDbContext = doctorsDbContext ?? throw new ArgumentNullException(nameof(doctorsDbContext));
        }
        public GeneralAdvice GetGeneralAdviceForDoctor(int doctorId, int generalAdviceId)
        {
            return _doctorDbContext.GeneralAdvices.Where(g => g.DoctorId == doctorId && g.AdviceId == generalAdviceId).FirstOrDefault();
        }
        public IEnumerable<GeneralAdvice> GetGeneralAdvicesForDoctor(int doctorId)
        {
            return _doctorDbContext.GeneralAdvices.Where(g => g.DoctorId == doctorId).ToList();
        }
        public Doctors GetDoctors(int doctorId)
        {
            return _doctorDbContext.Doctores.Where(d => d.DoctorId == doctorId).FirstOrDefault();
        }

        public void AddAdviceForDoctor(int doctorId, GeneralAdvice generalAdvice)
        {
            var doctor = GetDoctors(doctorId);
            doctor.generalAdvices.Add(generalAdvice);
        }
        public void UpdateAdvice(int doctor, GeneralAdvice generalAdvice)
        {

        }
        public void DeleteAdvice(GeneralAdvice generalAdvice)
        {
            _doctorDbContext.Remove(generalAdvice);
        }
        public bool AdviceExists(int doctorId)
        {
            return _doctorDbContext.GeneralAdvices.Any(d => d.DoctorId == doctorId);
        }
        public void Savechnge()
        {
            _doctorDbContext.SaveChanges();
        }
    }
}
