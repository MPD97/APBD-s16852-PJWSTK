using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required(ErrorMessage = "Musisz podać index")]
        [RegularExpression("^s[0-9]{5}$", ErrorMessage = "Index musi mieć s na początku i 5 znaków.")]
        public string IndexNumber { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Musisz podać imię")]
        [MinLength(2)]
        [MaxLength(10)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

        [Required]
        public string Studies { get; set; }
    }
}
