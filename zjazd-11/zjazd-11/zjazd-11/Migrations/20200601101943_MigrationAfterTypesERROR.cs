using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace zjazd_11.Migrations
{
    public partial class MigrationAfterTypesERROR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                });

            migrationBuilder.CreateTable(
                name: "Medicaments",
                columns: table => new
                {
                    MedicamentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Type = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicaments", x => x.MedicamentId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionMedicaments",
                columns: table => new
                {
                    MedicamentId = table.Column<int>(nullable: false),
                    PrescriptionId = table.Column<int>(nullable: false),
                    Dose = table.Column<int>(nullable: true),
                    Details = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMedicaments", x => new { x.PrescriptionId, x.MedicamentId });
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicaments_Medicaments_MedicamentId",
                        column: x => x.MedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "MedicamentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicaments_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    { 1, new DateTime(2020, 6, 1, 12, 19, 43, 80, DateTimeKind.Local).AddTicks(731), "Adam", "Przykładowski" },
                    { 2, new DateTime(2020, 6, 1, 12, 19, 43, 83, DateTimeKind.Local).AddTicks(648), "Kamil", "Nazwiskowski" }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "PrescriptionId", "Date", "DoctorId", "DueDate", "PatientId" },
                values: new object[] { 1, new DateTime(2020, 6, 1, 12, 19, 43, 83, DateTimeKind.Local).AddTicks(3879), 1, new DateTime(2020, 6, 1, 12, 19, 43, 83, DateTimeKind.Local).AddTicks(4434), 1 });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "PrescriptionId", "Date", "DoctorId", "DueDate", "PatientId" },
                values: new object[] { 2, new DateTime(2020, 6, 1, 12, 19, 43, 83, DateTimeKind.Local).AddTicks(5764), 1, new DateTime(2020, 6, 1, 12, 19, 43, 83, DateTimeKind.Local).AddTicks(5790), 2 });

            migrationBuilder.InsertData(
                table: "PrescriptionMedicaments",
                columns: new[] { "PrescriptionId", "MedicamentId", "Details", "Dose" },
                values: new object[] { 1, 1, "2 razy dziennie co 6 godzin", 1 });

            migrationBuilder.InsertData(
                table: "PrescriptionMedicaments",
                columns: new[] { "PrescriptionId", "MedicamentId", "Details", "Dose" },
                values: new object[] { 2, 2, "na noc 2 tabletki", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicaments_MedicamentId",
                table: "PrescriptionMedicaments",
                column: "MedicamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionMedicaments");

            migrationBuilder.DropTable(
                name: "Medicaments");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
