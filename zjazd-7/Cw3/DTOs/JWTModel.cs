using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.DTOs
{
    public class JWTModel
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
