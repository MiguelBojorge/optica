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
        public DbSet<Medicos> Medico { get; set; }
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


            base.OnModelCreating(modelBuilder);
        }
    }
}
