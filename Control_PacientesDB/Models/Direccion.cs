using System.ComponentModel.DataAnnotations;
namespace Control_PacientesDB.Models
{
    public class Direccion
    {
        [Key]
        public int Id_Direccion { get; set; }

        [Required(ErrorMessage = "La direccion del domicilio es obligatoria")]
        [StringLength(60, ErrorMessage = "La direccion del domicilio no puede exceder los 60 caracteres")]
        public string Direccion_Domicilio { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        [StringLength(25, ErrorMessage = "La ciudad no puede exceder los 25 caracteres")]
        public string Ciudad { get; set;}

        [Required(ErrorMessage = "El departamento es obligatorio")]
        [StringLength(25, ErrorMessage = "El departamento no puede exceder los 25 caracteres")]
        public string Departamentos { get; set;}
    }
}
