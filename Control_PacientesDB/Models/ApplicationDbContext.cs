using Microsoft.EntityFrameworkCore;
using Control_PacientesDB.Models;

namespace Control_PacientesDB.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Telefono> Telefono { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Especialidades> Especialidades { get; set; }
        public DbSet<Diagnostico> Diagnostico { get; set; }
        public DbSet<Cirugias> Cirugias { get; set; }
        public DbSet<Medicamentos> Medicamentos { get; set; }
        public DbSet<Seguimientos_PostOperatorios> Seguimientos_PostOperatorios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Direccion)
                .WithMany()
                .HasForeignKey(p => p.Id_Direccion)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Diagnostico -> Medico
            modelBuilder.Entity<Diagnostico>()
                .HasOne(d => d.Medico)
                .WithMany()
                .HasForeignKey(d => d.Codigo_medico)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Diagnostico -> Paciente
            modelBuilder.Entity<Diagnostico>()
                .HasOne(d => d.Paciente)
                .WithMany()
                .HasForeignKey(d => d.Codigo_paciente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Diagnostico>()
                .Property(d => d.Codigo_diagnostico)
                .ValueGeneratedOnAdd();

            // Relación Cirugias -> Diagnostico
            modelBuilder.Entity<Cirugias>()
                .HasOne(c => c.Diagnostico)
                .WithMany(d => d.Cirugias)
                .HasForeignKey(c => c.Codigo_diagnostico)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Cirugias -> Medico
            modelBuilder.Entity<Cirugias>()
                .HasOne(c => c.Medico)
                .WithMany()
                .HasForeignKey(c => c.Codigo_medico)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cirugias>()
                .Property(c => c.Codigo_cirugia)
                .ValueGeneratedOnAdd();

            // Relación Seguimientos -> Medico
            modelBuilder.Entity<Seguimientos_PostOperatorios>()
                .HasOne(s => s.Medico)
                .WithMany()
                .HasForeignKey(s => s.Codigo_medico)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Seguimientos -> Paciente
            modelBuilder.Entity<Seguimientos_PostOperatorios>()
                .HasOne(s => s.Paciente)
                .WithMany()
                .HasForeignKey(s => s.Codigo_paciente)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Seguimientos -> Medicamento
            modelBuilder.Entity<Seguimientos_PostOperatorios>()
                .HasOne(s => s.Medicamentos)
                .WithMany()
                .HasForeignKey(s => s.Codigo_Medicamento)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seguimientos_PostOperatorios>()
                .Property(s => s.Codigo_seguimiento)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Medico>()
                .Property(m => m.Codigo_medico)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Especialidades>()
                .Property(e => e.Codigo_Especialidad)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Medicamentos>()
                .Property(md => md.Codigo_Medicamento)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}
