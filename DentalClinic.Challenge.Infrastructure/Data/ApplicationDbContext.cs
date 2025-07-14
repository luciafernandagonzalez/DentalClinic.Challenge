using DentalClinic.Challenge.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Challenge.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Specialty> Specialties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialty>().ToTable("especialidades");
            modelBuilder.Entity<Specialty>().HasKey(s => s.Id);
            modelBuilder.Entity<Specialty>()
                .Property(s => s.Id)
                .HasColumnName("id_especialidad");
            modelBuilder.Entity<Specialty>()
                .Property(s => s.Code)
                .HasColumnName("cod_especialidad")
                .HasMaxLength(15)
                .IsRequired();

            modelBuilder.Entity<Specialty>()
                .HasIndex(s => s.Code)
                .IsUnique();
            modelBuilder.Entity<Specialty>()
                .Property(s => s.Description)
                .HasColumnName("descripcion")
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Specialty>()
                .HasIndex(s => s.Description)
                .IsUnique();
            modelBuilder.Entity<Specialty>()
                .Property(s => s.RowVersion)
                .HasColumnName("rowversion")
                .IsRowVersion();
        }

    }
}
