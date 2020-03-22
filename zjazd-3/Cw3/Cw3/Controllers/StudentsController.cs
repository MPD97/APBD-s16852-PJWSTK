using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public string GetStudent(string orderBy)
        {
            return $"Kowalski, Maleski, Andrzejewski sortowanie={orderBy}";
        }
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Malewski");
            }
            return NotFound("Nie znaleziono studenta");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }
        [HttpPut]
        public IActionResult PutStudent(Student student)
        {
            if (student != null)
            {
                return Ok("Zaktualizowano");
            }

            return NotFound("Nie udało się zaktualizować studenta");

        }

        [HttpDelete]
        public IActionResult DeleteStudent(Student student)
        {
            if (student != null)
            {
                return Ok("Usunięto studenta");
            }

            return NotFound("Nie udało się usunąć studenta");

        }
    }
}