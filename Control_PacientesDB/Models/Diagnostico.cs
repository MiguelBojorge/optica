using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Control_PacientesDB.Models
{
    public class Diagnostico
    {
        [Key]
        [Required(ErrorMessage = "El codigo del diagnostico es obligatorio")]
        public int Codigo_diagnostico { get; set; }

        // Clave foránea explícita (FK) para Medico
        [Required(ErrorMessage = "Agregar un medico es obligatorio")]
        public int Codigo_medico { get; set; }

        // Relación con la entidad Medico (sin generar columna extra)
        [ForeignKey("Codigo_medico")]
        public Medico Medico { get; set; }

        // Clave foránea explícita (FK) para Paciente
        [Required(ErrorMessage = "Seleccionar un paciente por favor")]
        public int Codigo_paciente { get; set; }

        // Relación con la entidad Paciente (sin generar columna extra)
        [ForeignKey("Codigo_paciente")]
        public Paciente Paciente { get; set; }

        // Atributos adicionales
        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public string Valoracion_oftalmologica { get; set; }

        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public string ResultadosExa_Glucosa { get; set; }

        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public string Valoracion_MedInterna { get; set; }

        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public string Valoracion_Anestesia { get; set; }

        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public DateTime Fecha_diagnostico { get; set; }

        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public string Notas_diagnostico { get; set; }

        public ICollection<Cirugias> Cirugias { get; set; } = new List<Cirugias>();
    }
}
