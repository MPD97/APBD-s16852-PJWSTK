using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.DTOs
{
    public class LoginSaltModel
    {
        public string index { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
