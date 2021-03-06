﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using zjazd_11.Models;

namespace zjazd_11.Migrations
{
    [DbContext(typeof(s16852Context))]
    partial class s16852ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("zjazd_11.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("DoctorId");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            DoctorId = 1,
                            Email = "m.markowski@gmail.com",
                            FirstName = "Marek",
                            LastName = "Markowski"
                        },
                        new
                        {
                            DoctorId = 2,
                            Email = "r.Szkoleniowski@gmail.com",
                            FirstName = "Ryszard",
                            LastName = "Szkoleniowski"
                        });
                });

            modelBuilder.Entity("zjazd_11.Models.Medicament", b =>
                {
                    b.Property<int>("MedicamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("MedicamentId");

                    b.ToTable("Medicaments");

                    b.HasData(
                        new
                        {
                            MedicamentId = 1,
                            Description = "Uśmieża ból",
                            Name = "APAP",
                            Type = "Środek przeciwbólowy"
                        },
                        new
                        {
                            MedicamentId = 2,
                            Description = "Uzupełnia niedobór witaminy C",
                            Name = "Witamina-C",
                            Type = "Suplement diety"
                        });
                });

            modelBuilder.Entity("zjazd_11.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("PatientId");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            PatientId = 1,
                            BirthDate = new DateTime(2020, 6, 1, 12, 19, 43, 80, DateTimeKind.Local).AddTicks(731),
                            FirstName = "Adam",
                            LastName = "Przykładowski"
                        },
                        new
                        {
                            PatientId = 2,
                            BirthDate = new DateTime(2020, 6, 1, 12, 19, 43, 83, DateTimeKind.Local).AddTicks(648),
                            FirstName = "Kamil",
                            LastName = "Nazwiskowski"
                        });
                });

            modelBuilder.Entity("zjazd_11.Models.Prescription", b =>
                {
                    b.Property<int>("PrescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("PrescriptionId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Prescriptions");

                    b.HasData(
                        new
                        {
                            PrescriptionId = 1,
                            Date = new DateTime(2020, 6, 1, 12, 19, 43, 83, DateTimeKind.Local).AddTicks(3879),
                            DoctorId = 1,
                            DueDate = new DateTime(2020, 6, 1, 12, 19, 43, 83, DateTimeKind.Local).AddTicks(4434),
                            PatientId = 1
                        },
                        new
                        {
                            PrescriptionId = 2,
                            Date = new DateTime(2020, 6, 1, 12, 19, 43, 83, DateTimeKind.Local).AddTicks(5764),
                            DoctorId = 1,
                            DueDate = new DateTime(2020, 6, 1, 12, 19, 43, 83, DateTimeKind.Local).AddTicks(5790),
                            PatientId = 2
                        });
                });

            modelBuilder.Entity("zjazd_11.Models.PrescriptionMedicament", b =>
                {
                    b.Property<int>("PrescriptionId")
                        .HasColumnType("int");

                    b.Property<int>("MedicamentId")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("Dose")
                        .HasColumnType("int");

                    b.HasKey("PrescriptionId", "MedicamentId");

                    b.HasIndex("MedicamentId");

                    b.ToTable("PrescriptionMedicaments");

                    b.HasData(
                        new
                        {
                            PrescriptionId = 1,
                            MedicamentId = 1,
                            Details = "2 razy dziennie co 6 godzin",
                            Dose = 1
                        },
                        new
                        {
                            PrescriptionId = 2,
                            MedicamentId = 2,
                            Details = "na noc 2 tabletki",
                            Dose = 2
                        });
                });

            modelBuilder.Entity("zjazd_11.Models.Prescription", b =>
                {
                    b.HasOne("zjazd_11.Models.Doctor", "Doctor")
                        .WithMany("Prescriptions")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("zjazd_11.Models.Patient", "Patient")
                        .WithMany("Prescriptions")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("zjazd_11.Models.PrescriptionMedicament", b =>
                {
                    b.HasOne("zjazd_11.Models.Medicament", "Medicament")
                        .WithMany("PrescriptionMedicaments")
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("zjazd_11.Models.Prescription", "Prescription")
                        .WithMany("PrescriptionMedicaments")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
