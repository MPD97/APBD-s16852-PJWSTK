using System.Collections;
using System.Collections.Generic;

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
}