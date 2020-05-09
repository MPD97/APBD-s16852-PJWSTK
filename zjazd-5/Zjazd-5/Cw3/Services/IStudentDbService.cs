using Cw4.DTOs.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Services
{
    public interface IStudentDbService
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest model);
        EnrollPromoteResponse PromoteStudents(EnrollPromoteRequest model);
    }
}
