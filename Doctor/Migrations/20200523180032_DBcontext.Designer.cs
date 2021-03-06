﻿// <auto-generated />
using Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doctor.Migrations
{
    [DbContext(typeof(DoctorsDbContext))]
    [Migration("20200523180032_DBcontext")]
    partial class DBcontext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Doctor.Entities.Admins", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Doctor.Entities.Doctors", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("ClinicAddress");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<string>("Role");

                    b.Property<string>("ShortDescription");

                    b.Property<string>("Special");

                    b.Property<int>("YearOfExperence");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctores");
                });

            modelBuilder.Entity("Doctor.Entities.FAQs", b =>
                {
                    b.Property<int>("FaqId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<string>("Question");

                    b.HasKey("FaqId");

                    b.ToTable("fAQss");
                });

            modelBuilder.Entity("Doctor.Entities.GeneralAdvice", b =>
                {
                    b.Property<int>("AdviceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdviceContent");

                    b.Property<string>("AdviceTitle");

                    b.Property<int>("DoctorId");

                    b.HasKey("AdviceId");

                    b.HasIndex("DoctorId");

                    b.ToTable("GeneralAdvices");
                });

            modelBuilder.Entity("Doctor.Entities.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("MedicalHistory");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<string>("Role");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Doctor.Entities.GeneralAdvice", b =>
                {
                    b.HasOne("Doctor.Entities.Doctors", "Doctors")
                        .WithMany("generalAdvices")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
