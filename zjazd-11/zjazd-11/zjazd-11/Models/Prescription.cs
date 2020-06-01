using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace zjazd_11.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        public decimal Date { get; set; }
        public decimal DueDate { get; set; }

        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }


        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }

    public class Patient
    {
        public int PatientId { get; set; }

        [MaxLength(100)]
        public decimal FirstName { get; set; }

        [MaxLength(100)]
        public decimal LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }

    public class Doctor
    {
        public int DoctorId { get; set; }

        [MaxLength(100)]
        public decimal FirstName { get; set; }

        [MaxLength(100)]
        public decimal LastName { get; set; }

        [MaxLength(100)]
        public DateTime Email { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}