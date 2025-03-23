using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Control_PacientesDB.Models
{
    public class Paciente
    {
        [Key]
        [Required(ErrorMessage = "El codigo del paciente es obligatorio")]
        public int Codigo_paciente { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "El nombre no puede exceder los 30 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(30, ErrorMessage = "El apellido no puede exceder los 30 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(16, ErrorMessage = "La cédula no puede exceder los 16 caracteres")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNac { get; set; }

        [Required]
        public int Id_Direccion { get; set; }

        // Relación con la entidad Direccion (sin generar columna extra)
        [ForeignKey("Id_Direccion")]
        public Direccion Direccion { get; set; }

        [NotMapped]
        public string NombreCompleto => $"{Nombres} {Apellidos}";
    }
}
