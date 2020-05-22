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
            return _doctorDbContext.Doctores.OrderBy(d => d.Name).ToList();
        }

        public Doctors GetDoctors(int doctorId)
        {
            return _doctorDbContext.Doctores.Where(d => d.DoctorId == doctorId).FirstOrDefault();
        }
        public bool DoctorExists(int doctorId)
        {
            return _doctorDbContext.Doctores.Any(d => d.DoctorId == doctorId);
        }
        public void Savechnge()
        {
            _doctorDbContext.SaveChanges();
        }

        public void CreateDoctor( Doctors doctors)
        {
            try
            {
                _doctorDbContext.Doctores.Add(doctors);
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
