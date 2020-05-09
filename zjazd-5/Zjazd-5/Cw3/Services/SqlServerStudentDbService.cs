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

        public ServiceResult EnrollStudent(EnrollStudentRequest model)
        {
            ServiceResult result = new ServiceResult
            {
                Success = true,
                Message = string.Empty
            };

            var st = new Student();
            st.FirstName = model.FirstName;


            using (var con = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    cmd.CommandText = "select TOP(1) IdStudies from Studies where Name=@name";
                    cmd.Parameters.AddWithValue("name", model.Studies);

                    var dr = cmd.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        
                        result.Message = "Studia nie istnieją";
                        result.Success = false;
                        return result;
                    }
                    int idStudies = (int)dr["IdStudies"];

                    cmd.CommandText = "select TOP(1) IdEnrollment from Enrollment where IdStudy=@idStudy and semester=1";
                    cmd.Parameters.AddWithValue("idStudy", idStudies);
                    
                    int idEnrolment;
                    if (!dr.Read())
                    {
                        cmd.CommandText = "insert into Enrollment (Semester, IdStudy, StartDate) " +
                                           "values 1, @idStudy, @dateNow; SELECT SCOPE_IDENTITY()";
                        cmd.Parameters.AddWithValue("idStudy", idStudies);
                        cmd.Parameters.AddWithValue("idStudy", DateTime.Now);

                        idEnrolment = (int)cmd.ExecuteScalar();
                    }
                    else
                    {
                        idEnrolment = (int)dr["IdEnrollment"];
                    }


                    cmd.CommandText = "select TOP(1) IndexNumber from Student where IndexNumber=@indexNumber";
                    cmd.Parameters.AddWithValue("indexNumber", model.IndexNumber);

                    if (dr.Read())
                    {
                        tran.Rollback();

                        result.Message = "Student o takim indexie istnieje w bazie danych.";
                        result.Success = false;
                        return result;
                    }

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
