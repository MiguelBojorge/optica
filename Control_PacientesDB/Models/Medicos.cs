using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Control_PacientesDB.Models
{
    public class Medicos
    {
        [Key]
        public int Codigo_medico { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "El nombre no puede exceder los 30 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(30, ErrorMessage = "El apellido no puede exceder los 30 caracteres")]
        public string Apellidos { get; set; }

        [NotMapped]
        public string NombreCompleto => $"{Nombres} {Apellidos}";
    }
}
