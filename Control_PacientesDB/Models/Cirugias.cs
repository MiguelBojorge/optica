using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Control_PacientesDB.Models
{
    public class Cirugias
    {
        [Key]
        [Required(ErrorMessage = "El codigo de cirugia es obligatorio")]
        public int Codigo_cirugia { get; set; }

        // Clave foránea explícita (FK) para Diagnostico
        [Required(ErrorMessage = "Seleccionar un diagnostico por favor")]
        public int Codigo_diagnostico { get; set; }

        // Relación con la entidad Diagnostico (sin generar columna extra)
        [ForeignKey("Codigo_diagnostico")]
        public Diagnostico Diagnostico { get; set; }

        // Clave foránea explícita (FK) para Medico
        [Required(ErrorMessage = "Selecciona el medico que va a realizar la cirugia")]
        public int Codigo_medico { get; set; }

        // Relación con la entidad Medico (sin generar columna extra)
        [ForeignKey("Codigo_medico")]
        public Medico Medico { get; set; }

        // Atributos adicionales
        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public DateTime Fecha_cirugia { get; set; }

        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public TimeSpan Hora_inicio { get; set; }

        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public TimeSpan Hora_fin { get; set; }
    }
}
