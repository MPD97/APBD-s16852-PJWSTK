using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw3.Models;
using Cw4;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cw3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public s16852Context Context { get; set; }

        public StudentsController(s16852Context context)
        {
            Context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetStudent()
        {
            return Ok( await Context.Student.ToArrayAsync());
        }
        [HttpGet("{indexNumber}")]
        public async Task<IActionResult> GetStudent(string indexNumber)
        {
            return Ok( await Context.Student.FirstOrDefaultAsync(student => student.IndexNumber == indexNumber));
        }

        [HttpPost]
        public IActionResult CreateStudent(Models.Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }
        [HttpPut]
        public async Task<IActionResult> PutStudent(Cw4.Student model)
        {
            Cw4.Student student = await Context.Student.FirstOrDefaultAsync(stud => stud.IndexNumber == model.IndexNumber);
            if (student == null)
            {
                return BadRequest("Student nie istnieje");
            }

            Context.Update(model);

            if (await Context.SaveChangesAsync() > 0)
            {
                return Ok(model);
            }
            return StatusCode(500, "Nie można było zapisać studenta do bazy danych");

        }

        [HttpDelete]
        public IActionResult DeleteStudent(int studentId)
        {
            if (studentId != 0)
            {
                return Ok("Usuwanie ukończone");
            }

            return NotFound("Nie udało się usunąć studenta");

        }
    }
}