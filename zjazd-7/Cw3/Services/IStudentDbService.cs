using Cw3.Models;
using Cw4.DTOs;
using Cw4.DTOs.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Services
{
    public interface IStudentDbService
    {
        ServiceStudentResult EnrollStudent(EnrollStudentRequest model);
        ServicePromoteResult PromoteStudents(EnrollPromoteRequest model);

        bool LoginStudent(LoginModel model);
        string RefreshToken(LoginModel model, out bool success);




        bool StudentExist(string index);
    }
}
