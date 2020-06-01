using System.ComponentModel.DataAnnotations;

namespace zjazd_11.Models
{
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
