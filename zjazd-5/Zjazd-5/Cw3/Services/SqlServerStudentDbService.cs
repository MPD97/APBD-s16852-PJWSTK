using Cw3.Models;
using Cw4.DTOs.Requests;
using Cw4.DTOs.Responses;
using System;
using System.Data.SqlClient;

namespace Cw4.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {
        private const string ConnectionString = "Data Source=db-mssql;Initial Catalog=s16852;Integrated Security=True";

        public ServiceStudentResult EnrollStudent(EnrollStudentRequest model)
        {
            ServiceStudentResult result = new ServiceStudentResult
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

                    cmd.CommandText = "select TOP(1) IdEnrollment, StartDate from Enrollment where IdStudy=@idStudy and semester=1";
                    cmd.Parameters.AddWithValue("idStudy", idStudies);

                    int idEnrolment;
                    DateTime dateStart;
                    if (!dr.Read())
                    {
                        dateStart = DateTime.Now;
                        cmd.CommandText = "insert into Enrollment (Semester, IdStudy, StartDate) " +
                                           "values 1, @idStudy, @dateNow; SELECT SCOPE_IDENTITY()";
                        cmd.Parameters.AddWithValue("idStudy", idStudies);
                        cmd.Parameters.AddWithValue("dateNow", dateStart);

                        idEnrolment = (int)cmd.ExecuteScalar();
                    }
                    else
                    {
                        idEnrolment = (int)dr["IdEnrollment"];
                        dateStart = (DateTime)dr["StartDate"];
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
                    cmd.CommandText = "INSERT INTO Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) " +
                                        " VALUES @indexNumber, @firstName, @lastName, @birthDate, @idEnrollment ; SELECT SCOPE_IDENTITY()";
                    cmd.Parameters.AddWithValue("indexNumber", model.IndexNumber);
                    cmd.Parameters.AddWithValue("firstName", model.FirstName);
                    cmd.Parameters.AddWithValue("lastName", model.LastName);
                    cmd.Parameters.AddWithValue("birthDate", model.Birthdate);
                    cmd.Parameters.AddWithValue("idEnrollment", idEnrolment);

                    idEnrolment = cmd.ExecuteNonQuery();
                    result.Model = new EnrollStudentResponse
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        IndexNumber = model.IndexNumber,
                        Semester = 1,
                        StartDate = dateStart
                    };
                    tran.Commit();
                }
                catch (SqlException exc)
                {
                    Console.WriteLine(exc.Message);

                    result.Message = "Błąd wewnętrzny.";
                    result.Success = false;
                    tran.Rollback();
                }
            }
            return result;
        }


        public ServicePromoteResult PromoteStudents(EnrollPromoteRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
