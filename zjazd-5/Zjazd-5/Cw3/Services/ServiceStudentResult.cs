using Cw4.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cw4.Services
{
    public class ServiceStudentResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public EnrollStudentResponse Model { get; set; }
    }
}
