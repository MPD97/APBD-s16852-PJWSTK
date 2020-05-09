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
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    cmd.CommandText = "select IdStudies from studies where name=@name";
                    cmd.Parameters.AddWithValue("name", request.Studies);

                    var dr = cmd.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        
                        result.Message = "Studia nie istnieją";
                        result.Success = false;
                        return result;
                    }
                    int idStudies = (int)dr["IdStudies"];

                    cmd.CommandText = "select top(1) IdEnrollment from Enrollment where idStudy=@idStudy and semester=1";
                    cmd.Parameters.AddWithValue("idStudy", idStudies);
                    int idEnrolment;
                    
                    if (!dr.Read())
                    {
                        cmd.CommandText = "insert into Enrollment (semester,idStudy) " +
                                           "values 1, @idStudy; SELECT SCOPE_IDENTITY()";
                        cmd.Parameters.AddWithValue("idStudy", idStudies);
                       
                        idEnrolment = (int)cmd.ExecuteScalar();

                    }
                    else
                    {
                        idEnrolment = (int)dr["IdEnrollment"];
                    }

                   
                    //com.CommandText = "INSERT INTO Student(IndexNumber, FirstName) VALUES(@Index, @Fname)";
                    //com.Parameters.AddWithValue("index", request.IndexNumber);

                    cmd.ExecuteNonQuery();

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
