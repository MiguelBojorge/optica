using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Control_PacientesDB.Models
{
    public class Especialidades
    {
        [Key]
        public int Codigo_Especialidad { get; set; }

        [Required(ErrorMessage = "El nombre de la especialidad es obligatorio")]
        [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres")]
        public string Nombre { get; set; }

        // Clave foránea explícita (FK) para Medico
        [Required]
        public int Codigo_medico { get; set; }

        // Relación con la entidad Medico (sin generar columna extra)
        [ForeignKey("Codigo_medico")]
        public Medico Medico { get; set; }
    }
}
