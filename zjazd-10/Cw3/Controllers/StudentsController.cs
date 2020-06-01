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
            return Ok(Context.Student.ToArrayAsync());
        }
        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            return Ok(Context.Student.FirstOrDefaultAsync(student => student.IndexNumber == indexNumber));
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }
        [HttpPut]
        public IActionResult PutStudent(int studentId)
        {
            if (studentId != 0)
            {
                return Ok("Aktualizacja dokończona");
            }

            return NotFound("Nie udało się zaktualizować studenta");

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