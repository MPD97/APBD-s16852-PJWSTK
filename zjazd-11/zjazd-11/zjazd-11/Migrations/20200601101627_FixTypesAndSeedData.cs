using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace zjazd_11.Migrations
{
    public partial class FixTypesAndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Prescriptions",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Prescriptions",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Patients",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Patients",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Doctors",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Doctors",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Doctors",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "m.markowski@gmail.com", "Marek", "Markowski" },
                    { 2, "r.Szkoleniowski@gmail.com", "Ryszard", "Szkoleniowski" }
                });

            migrationBuilder.InsertData(
                table: "Medicaments",
                columns: new[] { "MedicamentId", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Uśmieża ból", "APAP", "Środek przeciwbólowy" },
                    { 2, "Uzupełnia niedobór witaminy C", "Witamina-C", "Suplement diety" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "BirthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 6, 1, 12, 16, 26, 814, DateTimeKind.Local).AddTicks(1551), "Adam", "Przykładowski" },
                    { 2, new DateTime(2020, 6, 1, 12, 16, 26, 817, DateTimeKind.Local).AddTicks(2744), "Kamil", "Nazwiskowski" }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "PrescriptionId", "Date", "DoctorId", "DueDate", "PatientId" },
                values: new object[] { 1, new DateTime(2020, 6, 1, 12, 16, 26, 817, DateTimeKind.Local).AddTicks(5597), 1, new DateTime(2020, 6, 1, 12, 16, 26, 817, DateTimeKind.Local).AddTicks(5984), 1 });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "PrescriptionId", "Date", "DoctorId", "DueDate", "PatientId" },
                values: new object[] { 2, new DateTime(2020, 6, 1, 12, 16, 26, 817, DateTimeKind.Local).AddTicks(7158), 1, new DateTime(2020, 6, 1, 12, 16, 26, 817, DateTimeKind.Local).AddTicks(7182), 2 });

            migrationBuilder.InsertData(
                table: "PrescriptionMedicaments",
                columns: new[] { "PrescriptionId", "MedicamentId", "Details", "Dose" },
                values: new object[] { 1, 1, "2 razy dziennie co 6 godzin", 1 });

            migrationBuilder.InsertData(
                table: "PrescriptionMedicaments",
                columns: new[] { "PrescriptionId", "MedicamentId", "Details", "Dose" },
                values: new object[] { 2, 2, "na noc 2 tabletki", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicaments",
                keyColumns: new[] { "PrescriptionId", "MedicamentId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicaments",
                keyColumns: new[] { "PrescriptionId", "MedicamentId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "MedicamentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PrescriptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PrescriptionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "DueDate",
                table: "Prescriptions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<decimal>(
                name: "Date",
                table: "Prescriptions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<decimal>(
                name: "LastName",
                table: "Patients",
                type: "decimal(18,2)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FirstName",
                table: "Patients",
                type: "decimal(18,2)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LastName",
                table: "Doctors",
                type: "decimal(18,2)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FirstName",
                table: "Doctors",
                type: "decimal(18,2)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Email",
                table: "Doctors",
                type: "datetime2",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
