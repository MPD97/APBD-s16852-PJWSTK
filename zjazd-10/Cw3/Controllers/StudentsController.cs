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
            Student student = new Student();

            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s16852;Integrated Security=True"))
            {
                using (SqlCommand command = new SqlCommand())
                {

                    command.Connection = connection;
                    command.CommandText = @"SELECT TOP (1) [FirstName]
                                                  ,[LastName]
                                                  ,[BirthDate]
	                                              ,[Name]
	                                              ,[Semester]
                                              FROM Student
                                              INNER JOIN Enrollment
                                              ON Student.IdEnrollment = Enrollment.IdEnrollment
                                              Inner Join Studies
                                              ON Enrollment.IdStudy = Studies.IdStudy
                                              WHERE Student.IndexNumber = @indexNumber;";
                    command.Parameters.AddWithValue("indexNumber", indexNumber);
                    connection.Open();
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        student.FirstName = dataReader["FirstName"].ToString();
                        student.LastName = dataReader["LastName"].ToString();
                        student.BirthDate = DateTime.Parse(dataReader["BirthDate"].ToString());
                        student.StudiesName = dataReader["Name"].ToString();
                        student.SemestrNumber = int.Parse(dataReader["Semester"].ToString());
                    }
                }
            }
            return Ok(student);
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