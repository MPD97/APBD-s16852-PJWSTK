using System;
using System.ComponentModel.DataAnnotations;

namespace zjazd_11.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        public decimal Date { get; set; }
        public decimal DueDate { get; set; }
    }

    public class Patient
    {
        public int PatientId { get; set; }

        [MaxLength(100)]
        public decimal FirstName { get; set; }

        [MaxLength(100)]
        public decimal LastName { get; set; }

        public DateTime BirthDate { get; set; }
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
    }
}