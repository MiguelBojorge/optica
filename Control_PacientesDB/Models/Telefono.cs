using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Control_PacientesDB.Models
{
    public class Telefono
    {
        [Key]
        public int ID_Telefono { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio")]
        [StringLength(8, ErrorMessage = "El número de teléfono no puede exceder los 8 caracteres")]
        public string Num_Telefono { get; set; }

        [StringLength(5, ErrorMessage = "La compañía no puede exceder los 5 caracteres")]
        public string Company { get; set; }

        // Clave foránea explícita (FK) para Paciente
        [Required]
        public int Codigo_paciente { get; set; }

        // Relación con la entidad Paciente (sin generar columna extra)
        [ForeignKey("Codigo_paciente")]
        public Paciente Paciente { get; set; }
    }
}
