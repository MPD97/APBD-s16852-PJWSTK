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
                    dr.Close();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "select TOP(1) IdEnrollment, StartDate from Enrollment where IdStudy=@idStudy and semester=1";
                    cmd.Parameters.AddWithValue("idStudy", idStudies);

                    int idEnrolment;
                    DateTime dateStart;
                    if (!dr.Read())
                    {
                        dr.Close();
                        cmd.Parameters.Clear();

                        dateStart = DateTime.Now;
                        cmd.CommandText = "insert into Enrollment (Semester, IdStudy, StartDate) " +
                                           "values 1, @idStudy, @dateNow; SELECT SCOPE_IDENTITY()";
                        cmd.Parameters.AddWithValue("idStudy", idStudies);
                        cmd.Parameters.AddWithValue("dateNow", dateStart);

                        idEnrolment = (int)cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                    }
                    else
                    {
                        idEnrolment = (int)dr["IdEnrollment"];
                        dateStart = (DateTime)dr["StartDate"];
                    }


                    cmd.CommandText = "select TOP(1) IndexNumber from Student where IndexNumber=@indexNumber";
                    cmd.Parameters.AddWithValue("indexNumber", model.IndexNumber);
                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        tran.Rollback();

                        result.Message = "Student o takim indexie istnieje w bazie danych.";
                        result.Success = false;
                        return result;
                    }
                    dr.Close();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "INSERT INTO Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) " +
                                        " VALUES @indexNumber, @firstName, @lastName, @birthDate, @idEnrollment ; SELECT SCOPE_IDENTITY()";
                    cmd.Parameters.AddWithValue("indexNumber", model.IndexNumber);
                    cmd.Parameters.AddWithValue("firstName", model.FirstName);
                    cmd.Parameters.AddWithValue("lastName", model.LastName);
                    cmd.Parameters.AddWithValue("birthDate", model.Birthdate);
                    cmd.Parameters.AddWithValue("idEnrollment", idEnrolment);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                    {
                        tran.Rollback();

                        result.Message = "Nie można utworzyć nowego studenta.";
                        result.Success = false;
                        return result;
                    }

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
            ServicePromoteResult result = new ServicePromoteResult
            {
                Success = true,
                Message = string.Empty
            };

            using (var con = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    cmd.CommandText = "select * from enrollment where IdStudy = (select IdStudy from studies where Name = @studyName) and Semester = @semester";
                    cmd.Parameters.AddWithValue("studyName", model.Studies);
                    cmd.Parameters.AddWithValue("semester", model.Semester);

                    var dr = cmd.ExecuteReader();

                    if (!dr.Read())
                    {
                        dr.Close();
                        result.Message = "Taki enrollment nie istnieje";
                        result.Success = false;
                        return result;
                    }
                    dr.Close();
                    cmd.Parameters.Clear();



                 
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
    }
}
