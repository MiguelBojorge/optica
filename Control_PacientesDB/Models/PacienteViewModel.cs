using System.ComponentModel.DataAnnotations;
namespace Control_PacientesDB.Models
{
    public class PacienteViewModel
    {
        // Propiedades de Paciente
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

        // Propiedades de Direccion
        [Required(ErrorMessage = "El ID de direccion es obligatorio")]
        public int Id_Direccion { get; set; }

        [Required(ErrorMessage = "La direccion del domicilio es obligatoria")]
        [StringLength(60, ErrorMessage = "La direccion del domicilio no puede exceder los 60 caracteres")]
        public string Direccion_Domicilio { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        [StringLength(25, ErrorMessage = "La ciudad no puede exceder los 25 caracteres")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El departamento es obligatorio")]
        [StringLength(25, ErrorMessage = "El departamento no puede exceder los 25 caracteres")]
        public string Departamentos { get; set; }

        // Propiedades de Telefono
        [Required(ErrorMessage = "El ID del telefono es obligatorio")]
        public int ID_Telefono { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio")]
        [StringLength(8, ErrorMessage = "El número de teléfono no puede exceder los 8 caracteres")]
        public string Num_Telefono { get; set; }

        [StringLength(5, ErrorMessage = "La compañía no puede exceder los 5 caracteres")]
        public string Company { get; set; }
    }
}
