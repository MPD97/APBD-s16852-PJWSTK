using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace zjazd_11.Models
{
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