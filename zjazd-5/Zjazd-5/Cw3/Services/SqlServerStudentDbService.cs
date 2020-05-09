using Cw3.Models;
using Cw4.DTOs.Requests;
using System;
using System.Data.SqlClient;

namespace Cw4.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {

        public SqlServerStudentDbService()
        {

        }

        public void EnrollStudent(EnrollStudentRequest request)
        {

            var st = new Student();
            st.FirstName = request.FirstName;


            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s16852;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    com.CommandText = "select IdStudies from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        //return BadRequest("Studia nie istnieja");
         
                    }
                    int idstudies = (int)dr["IdStudies"];

                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName) VALUES(@Index, @Fname)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);

                    com.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }
            }

        }

        public void PromoteStudents(int semester, string studies)
        {
            throw new NotImplementedException();
        }
    }
}
