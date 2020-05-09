using Cw4.DTOs.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Services
{
    public interface IStudentDbService
    {
        ServiceResult EnrollStudent(EnrollStudentRequest request);
        ServiceResult PromoteStudents(int semester, string studies);
    }
}
