using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api_Insi_Web.Models
{
    public partial class TutoresDto
    {
        public int IdTutor { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo Dirección es obligatorio.")]
        public string Direccion { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "El campo Telefono solo debe contener números.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo RelacionConEstudiante es obligatorio.")]
        public string RelacionConEstudiante { get; set; }

        [JsonIgnore]
        public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

        [JsonIgnore]
        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
