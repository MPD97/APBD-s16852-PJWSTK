using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cw3.Models;
using Cw4.DTOs.Requests;
using Cw4.DTOs.Responses;
using Cw4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Cw4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        public s16852Context Context { get; set; }

        public EnrollmentsController(s16852Context context)
        {
            Context = context;
        }

        [HttpPost]
        [Authorize(Roles = "employee")]
        public async Task<IActionResult> EnrollStudent(EnrollStudentRequest request)
        {
            if (ModelState.IsValid == false)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

                return BadRequest(allErrors);
            }

            Studies studies = await Context.Studies.FirstOrDefaultAsync(stud => stud.Name == request.Studies);
            if (studies == null)
            {
                return BadRequest("Studia nie istnieją");
            }

            Enrollment enrollment = await Context.Enrollment.FirstOrDefaultAsync(en => en.IdStudy == studies.IdStudy && en.Semester == 1);
            if (enrollment == null)
            {
                enrollment = new Enrollment { Semester = 1, IdStudy = studies.IdStudy, StartDate = DateTime.Now };
                Context.Add(enrollment);
            }

            Student student = await Context.Student.FirstOrDefaultAsync(s => s.IndexNumber == request.IndexNumber);
            if (studies != null)
            {
                return BadRequest("Student o takim indeksie już istnieje w bazie danych.");
            }
            student = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.Birthdate,
                IdEnrollment = enrollment.IdEnrollment
            };
            Context.Add(student);

            if (await Context.SaveChangesAsync() == 0)
            {
                return StatusCode(500, "Błąd podczas zapisu danych do bazy.");
            }
            EnrollStudentResponse result = new EnrollStudentResponse
            {
                IndexNumber = student.IndexNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Semester = enrollment.Semester,
                StartDate = enrollment.StartDate,
            };
            return StatusCode(201, result);
        }

        [HttpPost("promotions")]
        [Authorize(Roles = "employee")]
        public IActionResult EnrollStudent(EnrollPromoteRequest request)
        {
            if (ModelState.IsValid == false)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

                return BadRequest(allErrors);
            }

            var result = _service.PromoteStudents(request);
            if (result.Success == false)
            {
                return BadRequest(result.Message);
            }

            return StatusCode(201, result.Model);
        }
    }
}