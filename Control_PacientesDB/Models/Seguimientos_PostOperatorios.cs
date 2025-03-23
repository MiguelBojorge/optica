using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Control_PacientesDB.Models
{
    public class Seguimientos_PostOperatorios
    {
        [Key]
        [Required(ErrorMessage = "El codigo del seguimiento es obligatorio")]
        public int Codigo_seguimiento { get; set; }

        // Clave foránea explícita (FK) para Medico
        [Required(ErrorMessage = "El codigo del medico es obligatorio")]
        public int Codigo_medico { get; set; }

        // Relación con la entidad Medico (sin generar columna extra)
        [ForeignKey("Codigo_medico")]
        public Medicos Medico { get; set; }

        // Clave foránea explícita (FK) para Paciente
        [Required(ErrorMessage = "El codigo del paciente es obligatorio")]
        public int Codigo_paciente { get; set; }

        // Relación con la entidad Paciente (sin generar columna extra)
        [ForeignKey("Codigo_paciente")]
        public Paciente Paciente { get; set; }

        // Clave foránea explícita (FK) para Medicamento
        [Required(ErrorMessage = "El codigo del medicamento es obligatorio")]
        public int Codigo_Medicamento { get; set; }

        // Relación con la entidad Medicamento (sin generar columna extra)
        [ForeignKey("Codigo_Medicamento")]
        public Medicamentos Medicamentos { get; set; }

        // Atributos adicionales
        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public DateTime Fecha_Control { get; set; }

        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public DateTime Programacion_ProximaCita { get; set; }

        [Required(ErrorMessage = "Llenar este campo es obligatorio")]
        public string Observaciones { get; set; }
    }
}
