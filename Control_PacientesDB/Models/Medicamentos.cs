using System.ComponentModel.DataAnnotations;

namespace Control_PacientesDB.Models
{
    public class Medicamentos
    {
        [Key]
        public int Codigo_Medicamento { get; set; }

        [Required(ErrorMessage = "El nombre del medicamento es obligatorio")]
        [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres")]
        public string Nombre_Medicamento { get; set; }

        public string Descripcion_medicamento { get; set; }
    }
}
