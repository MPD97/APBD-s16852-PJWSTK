using Cw3.Models;
using Cw4.DTOs.Requests;
using System;
using System.Data.SqlClient;

namespace Cw4.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {
        private const string ConnectionString = "Data Source=db-mssql;Initial Catalog=s16852;Integrated Security=True";

        public SqlServerStudentDbService()
        {

        }

        public ServiceResult EnrollStudent(EnrollStudentRequest request)
        {
            ServiceResult result = new ServiceResult
            {
                Success = true,
                Message = string.Empty
            };

            var st = new Student();
            st.FirstName = request.FirstName;


            using (var con = new SqlConnection(ConnectionString))
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
                        
                        result.Message = "Studia nie istnieja";
                        result.Success = false;
                        return result;
                    }
                    int idstudies = (int)dr["IdStudies"];

                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName) VALUES(@Index, @Fname)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);

                    com.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (SqlException exc)
                {
                    result.Message = "Błąd wewnętrzny.";
                    result.Success = false;
                    tran.Rollback();
                }
            }
            return result;
        }

        public ServiceResult PromoteStudents(int semester, string studies)
        {
            throw new NotImplementedException();
        }
    }
}
