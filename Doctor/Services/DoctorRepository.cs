using Doctor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Services
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DoctorsDbContext _doctorDbContext;

        public DoctorRepository(DoctorsDbContext doctorsDbContext)
        {
            _doctorDbContext = doctorsDbContext ?? throw new ArgumentNullException(nameof(doctorsDbContext));
        }
        public IEnumerable<Doctors> GetDoctors()
        {
            return _doctorDbContext.Doctors.OrderBy(d => d.Name).ToList();
        }

        public Doctors GetDoctors(int doctorId)
        {
            return _doctorDbContext.Doctors.Where(d => d.DoctorId == doctorId).FirstOrDefault();
        }

        public GeneralAdvice GetGeneralAdviceForDoctor(int doctorId, int generalAdviceId)
        {
            return _doctorDbContext.GeneralAdvices.Where(g => g.DoctorId == doctorId && g.AdviceId == generalAdviceId).FirstOrDefault();
        }

        public IEnumerable<GeneralAdvice> GetGeneralAdvicesForDoctor(int doctorId)
        {
            return _doctorDbContext.GeneralAdvices.Where(g => g.DoctorId == doctorId).ToList();
        }
        public bool DoctorExists(int doctorId)
        {
            return _doctorDbContext.Doctors.Any(d => d.DoctorId == doctorId);
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
        public void Savechnge()
        {
            _doctorDbContext.SaveChanges();
        }

        public void CreateDoctor( Doctors doctors)
        {
            try
            {
                _doctorDbContext.Doctors.Add(doctors);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            
        }

        public void UpdateDoctor(int doctor, Doctors doctors)
        {
           
        }

        public void DeleteDoctor(Doctors doctors)
        {
            _doctorDbContext.Remove(doctors);
        }
    }
}
