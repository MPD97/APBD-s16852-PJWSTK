using System;
using System.Collections.Generic;
using System.Text;

namespace Cw1
{
    class Student
    {
        public string Index { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string MothersName { get; set; }
        public string FathersName { get; set; }
        public List<Studies> Studies { get; set; }

    }
}
