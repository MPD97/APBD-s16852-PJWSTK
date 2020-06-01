﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zjazd_11.Models
{
    public class s16852Context : DbContext
    {
        DbSet<Doctor> Doctors;
        DbSet<Medicament> Medicaments;
        DbSet<Patient> Patients;
        DbSet<Prescription> Prescriptions;
        DbSet<PrescriptionMedicament> PrescriptionMedicaments;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}