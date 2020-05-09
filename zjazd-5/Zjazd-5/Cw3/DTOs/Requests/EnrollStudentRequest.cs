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
        [MinLength(2, ErrorMessage = "Minimalna długośc to 2 znaki")]
        [MaxLength(10, ErrorMessage = "Maksymalna długośc to 10 znaków")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwisko")]
        [MinLength(2, ErrorMessage = "Minimalna długośc to 2 znaki")]
        [MaxLength(255, ErrorMessage = "Maksymalna długośc to 255 znaków")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Musisz podać datę urodzenia")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Musisz podać studia")]
        public string Studies { get; set; }
    }
}
