﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace zjazd_11.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}