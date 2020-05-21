using Doctor.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor
{
    public class DoctorsDbContext:DbContext
    {
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<GeneralAdvice> GeneralAdvices { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<FAQ> fAQs { get; set; }
        public DbSet<Users> Users { get; set; }
        public DoctorsDbContext(DbContextOptions<DoctorsDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
