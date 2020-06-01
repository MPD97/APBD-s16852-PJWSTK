using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cw4
{
    public partial class s16852Context : DbContext
    {
        public s16852Context()
        {
        }

        public s16852Context(DbContextOptions<s16852Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Kraj> Kraj { get; set; }
        public virtual DbSet<Rodzina> Rodzina { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }
        public virtual DbSet<Uczelnia> Uczelnia { get; set; }
        public virtual DbSet<Uczen> Uczen { get; set; }
        public virtual DbSet<Wymiana> Wymiana { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s16852;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.IdEnrollment)
                    .HasName("Enrollment_pk");

                entity.Property(e => e.IdEnrollment).ValueGeneratedNever();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdStudyNavigation)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.IdStudy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Enrollment_Studies");
            });

            modelBuilder.Entity<Kraj>(entity =>
            {
                entity.HasKey(e => e.IdKraj)
                    .HasName("Kraj_pk");

                entity.Property(e => e.IdKraj)
                    .HasColumnName("ID_Kraj")
                    .HasColumnType("decimal(19, 0)");

                entity.Property(e => e.CzyUe)
                    .IsRequired()
                    .HasColumnName("Czy_UE")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rodzina>(entity =>
            {
                entity.HasKey(e => e.IdRodzina)
                    .HasName("Rodzina_pk");

                entity.Property(e => e.IdRodzina)
                    .HasColumnName("ID_Rodzina")
                    .HasColumnType("decimal(19, 0)");

                entity.Property(e => e.IdKraj)
                    .HasColumnName("ID_Kraj")
                    .HasColumnType("decimal(19, 0)");

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdKrajNavigation)
                    .WithMany(p => p.Rodzina)
                    .HasForeignKey(d => d.IdKraj)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Rodzina_Kraj");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IndexNumber)
                    .HasName("Student_pk");

                entity.Property(e => e.IndexNumber).HasMaxLength(100);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Salt).HasMaxLength(100);

                entity.HasOne(d => d.IdEnrollmentNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.IdEnrollment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Enrollment");
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(e => e.IdStudy)
                    .HasName("Studies_pk");

                entity.Property(e => e.IdStudy).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Uczelnia>(entity =>
            {
                entity.HasKey(e => e.IdUczelnia)
                    .HasName("Uczelnia_pk");

                entity.Property(e => e.IdUczelnia)
                    .HasColumnName("ID_Uczelnia")
                    .HasColumnType("decimal(19, 0)");

                entity.Property(e => e.IdKraj)
                    .HasColumnName("ID_Kraj")
                    .HasColumnType("decimal(19, 0)");

                entity.Property(e => e.Kierunek)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdKrajNavigation)
                    .WithMany(p => p.Uczelnia)
                    .HasForeignKey(d => d.IdKraj)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Uczelnia_Kraj");
            });

            modelBuilder.Entity<Uczen>(entity =>
            {
                entity.HasKey(e => e.IdUczen)
                    .HasName("Uczen_pk");

                entity.Property(e => e.IdUczen)
                    .HasColumnName("ID_Uczen")
                    .HasColumnType("decimal(19, 0)");

                entity.Property(e => e.IdKraj)
                    .HasColumnName("ID_Kraj")
                    .HasColumnType("decimal(19, 0)");

                entity.Property(e => e.IdUczelnia)
                    .HasColumnName("ID_Uczelnia")
                    .HasColumnType("decimal(19, 0)");

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdKrajNavigation)
                    .WithMany(p => p.Uczen)
                    .HasForeignKey(d => d.IdKraj)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Uczen_Kraj");

                entity.HasOne(d => d.IdUczelniaNavigation)
                    .WithMany(p => p.Uczen)
                    .HasForeignKey(d => d.IdUczelnia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Uczen_Uczelnia");
            });

            modelBuilder.Entity<Wymiana>(entity =>
            {
                entity.HasKey(e => e.IdWymiana)
                    .HasName("Wymiana_pk");

                entity.Property(e => e.IdWymiana)
                    .HasColumnName("ID_Wymiana")
                    .HasColumnType("decimal(19, 0)");

                entity.Property(e => e.DataDo)
                    .HasColumnName("Data_Do")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataOd)
                    .HasColumnName("Data_Od")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdRodzina)
                    .HasColumnName("ID_Rodzina")
                    .HasColumnType("decimal(19, 0)");

                entity.Property(e => e.IdUczelnia)
                    .HasColumnName("ID_Uczelnia")
                    .HasColumnType("decimal(19, 0)");

                entity.Property(e => e.IdUczen)
                    .HasColumnName("ID_Uczen")
                    .HasColumnType("decimal(19, 0)");

                entity.HasOne(d => d.IdRodzinaNavigation)
                    .WithMany(p => p.Wymiana)
                    .HasForeignKey(d => d.IdRodzina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Wymiana_Rodzina");

                entity.HasOne(d => d.IdUczelniaNavigation)
                    .WithMany(p => p.Wymiana)
                    .HasForeignKey(d => d.IdUczelnia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Wymiana_Uczelnia");

                entity.HasOne(d => d.IdUczenNavigation)
                    .WithMany(p => p.Wymiana)
                    .HasForeignKey(d => d.IdUczen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Wymiana_Uczen");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
