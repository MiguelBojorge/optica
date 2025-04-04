using System;
using System.ComponentModel.DataAnnotations;

namespace Control_PacientesDB.Models
{
    public class DiagnosticoViewModel
    {
        public int Codigo_diagnostico { get; set; }

        [Required(ErrorMessage = "El médico es obligatorio.")]
        public int Codigo_medico { get; set; }

        [Required(ErrorMessage = "El paciente es obligatorio.")]
        public int Codigo_paciente { get; set; }

        [Required(ErrorMessage = "Debe ingresar una valoración oftalmológica.")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        public string Valoracion_oftalmologica { get; set; }

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string ResultadosExa_Glucosa { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        public string Valoracion_MedInterna { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        public string Valoracion_Anestesia { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una fecha.")]
        [DataType(DataType.Date)]
        public DateTime Fecha_diagnostico { get; set; }

        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres.")]
        public string Notas_diagnostico { get; set; }

        public ICollection<Cirugias> Cirugias { get; set; } = new List<Cirugias>();

        // Propiedades para mostrar nombres en la vista
        public string MedicoNombre { get; set; }
        public string PacienteNombre { get; set; }
    }
}
