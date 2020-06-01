using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace zjazd_11.Models
{
    public class Medicament
    {
        public int MedicamentId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(100)]
        public string Description { get; set; }
        
        [MaxLength(100)]
        public string Type { get; set; }
    }

    public class PrescriptionMedicament
    {
        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }

        [Key]
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }

        public int? Dose { get; set; }

        [MaxLength(100)]
        public string Details { get; set; }
    }
}
