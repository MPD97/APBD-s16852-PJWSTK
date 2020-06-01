using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zjazd_11.Models
{
    public class s16852Context : DbContext
    {
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Medicament> Medicaments { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

        public s16852Context(DbContextOptions<s16852Context> options)
           : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PrescriptionMedicament>().HasKey(k => new { k.PrescriptionId, k.MedicamentId });

            //Patient
            modelBuilder.Entity<Patient>().HasData(new Patient
            {
                PatientId = 1,
                FirstName = "Adam",
                LastName = "Przykładowski",
                BirthDate = DateTime.Now
            });
            modelBuilder.Entity<Patient>().HasData(new Patient
            {
                PatientId = 2,
                FirstName = "Kamil",
                LastName = "Nazwiskowski",
                BirthDate = DateTime.Now
            });


            //Doctor
            modelBuilder.Entity<Doctor>().HasData(new Doctor
            {
                DoctorId = 1,
                FirstName = "Marek",
                LastName = "Markowski",
                Email = "m.markowski@gmail.com",
            });
            modelBuilder.Entity<Doctor>().HasData(new Doctor
            {
                DoctorId = 2,
                FirstName = "Ryszard",
                LastName = "Szkoleniowski",
                Email = "r.Szkoleniowski@gmail.com",
            });

            //Prescription
            modelBuilder.Entity<Prescription>().HasData(new Prescription
            {
              PrescriptionId = 1,
              Date = DateTime.Now,
              DueDate = DateTime.Now,
              PatientId = 1,
              DoctorId = 1,
            });
            modelBuilder.Entity<Prescription>().HasData(new Prescription
            {
                PrescriptionId = 2,
                Date = DateTime.Now,
                DueDate = DateTime.Now,
                PatientId = 2,
                DoctorId = 1,
            });

            //Medicament
            modelBuilder.Entity<Medicament>().HasData(new Medicament {
                MedicamentId = 1,
                Name = "APAP",
                Description = "Uśmieża ból",
                Type = "Środek przeciwbólowy",
            });
            modelBuilder.Entity<Medicament>().HasData(new Medicament
            {
                MedicamentId = 2,
                Name = "Witamina-C",
                Description = "Uzupełnia niedobór witaminy C",
                Type = "Suplement diety",
            });

            //PrescriptionMedicament
            modelBuilder.Entity<PrescriptionMedicament>().HasData(new PrescriptionMedicament
            {
                MedicamentId = 1,
                PrescriptionId = 1,
                Details = "2 razy dziennie co 6 godzin",
                Dose = 1,
            }); 
            modelBuilder.Entity<PrescriptionMedicament>().HasData(new PrescriptionMedicament
            {
                MedicamentId = 2,
                PrescriptionId = 2,
                Details = "na noc 2 tabletki",
                Dose = 2,
            });
        }
    }
}
